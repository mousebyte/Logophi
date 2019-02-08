using System;
using System.Diagnostics;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi.Views.Presentation
{
    //TODO: Refactor this class, needs functions redesigned and inline comments
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IMainFormView" />.
    /// </summary>
    internal class MainFormPresenter : IViewPresenter<IMainFormView>
    {
        private readonly Browser _thesaurus;
        private IMainFormView _view;
        private bool SearchValid => _thesaurus.Definitions != null;

        public MainFormPresenter
            (Browser thesaurus)
            {
            _thesaurus = thesaurus;
            thesaurus.BookmarkRemoved += OnBookmarkRemoved;
            thesaurus.BookmarkAdded += OnBookmarkAdded;
        }

        /// <inheritdoc />
        public void Present
            (IMainFormView view)
            {
            Present(view, null);
            }

        /// <inheritdoc />
        public void Present
            (IMainFormView view,
             object parent)
            {
            _view = view;
            PopulateDropDownItems();
            _view.ViewEventActivated += OnViewEventActivated;
            _view.Search += OnSearch;
            _view.SelectedDefinitionChanged +=
                OnSelectedDefinitionChanged;
            _view.BackClicked += OnBackClicked;
            _view.ForwardClicked += OnForwardClicked;
            _view.BookmarkClicked += OnBookmarkClicked;
            _view.OpenDictionaryClicked += OnOpenDictionaryClicked;
            _view.OpenGithubClicked += OnOpenGithubClicked;
            _view.Closed += OnClosed;
            if (parent == null) _view.Show();
            else _view.Show(parent);
            IsPresenting = true;
            }
        /// <summary>
        /// Occurs when the show bookmarks button is clicked.
        /// </summary>
        public event EventHandler ShowBookmarksClicked;
        /// <summary>
        /// Occurs when the show preferences button is clicked.
        /// </summary>
        public event EventHandler ShowPreferencesClicked;
        /// <summary>
        /// Occurs when the show about button is clicked.
        /// </summary>
        public event EventHandler ShowAboutClicked;

        private void OnBookmarkAdded
            (object sender,
             string e)
            {
            if (IsPresenting && e == _thesaurus.SearchTerm)
                _view.BookmarkOn();
            }

        private void OnBookmarkRemoved
            (object sender,
             string e)
            {
            if (IsPresenting && e == _thesaurus.SearchTerm)
                _view.BookmarkOff();
            }

        /// <summary>
        /// Populates the drop down items of the view's search box
        /// with values from the browser's history.
        /// </summary>
        private void PopulateDropDownItems()
            {
            if (_thesaurus.History.Count <= 0) return;
            foreach (var i in _thesaurus.History)
                if (!_view.DropDownItems.Contains(i))
                    _view.DropDownItems.Add(i);
            }

        private void OnViewEventActivated
            (object sender,
             ViewEventArgs e)
            {
            switch (e.Tag)
                {
                case "ShowBookmarksClicked":
                    ShowBookmarksClicked?.Invoke(
                        this,
                        EventArgs.Empty);
                    break;
                case "ShowPreferencesClicked":
                    ShowPreferencesClicked?.Invoke(
                        this,
                        EventArgs.Empty);
                    break;
                case "ShowAboutClicked":
                    ShowAboutClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "ExitClicked":
                    Application.Exit();
                    break;
                }
            }

        /// <inheritdoc />
        public IMainFormView View => _view;
        /// <summary>
        /// Gets a value indicating whether or not the view is being shown to the user.
        /// </summary>
        public bool IsPresenting { get; private set; }

        /// <summary>
        /// Searches for a word.
        /// </summary>
        /// <param name="word">The word to search for.</param>
        public void Search
            (string word)
            {
            OnSearch(this, word);
            }

        /// <summary>
        /// Performs actions necessary to handle an invalid search.
        /// </summary>
        private void HandleInvalidSearch()
            {
            _view.Definitions.Add(Resources.InvalidSearch);
            _view.EnableBookmarkButton = false;
            }

        /// <summary>
        /// Populates the definitions box with values from the browser.
        /// </summary>
        /// <param name="word">The word that the definitions belong to.</param>
        private void PopulateDefinitions
            (string word)
            {
            foreach (var def in _thesaurus.Definitions)
                _view.Definitions.Add(
                    $"{def.PartOfSpeech}: {def.Definition}");
            _view.EnableBookmarkButton = true;
            _view.SelectedDefinitionIndex = 0;
            if (!_view.DropDownItems.Contains(word))
                _view.DropDownItems.Insert(0, word);
            }

        /// <summary>
        /// Searches for the current history item.
        /// </summary>
        private void SearchFromHistory()
            {
            OnSearch(this, _thesaurus.History.CurrentItem);
            _view.SearchText = _thesaurus.History.CurrentItem;
            }

        private void OnOpenGithubClicked
            (object sender,
             EventArgs e)
            {
            Process.Start(Resources.GithubUrl);
            }

        private void OnBackClicked
            (object sender,
             EventArgs e)
            {
            if (!_thesaurus.History.CanGoBackward) return;
            if (SearchValid)
                _thesaurus.History.GoBack();
            SearchFromHistory();
            }

        //TODO: Depricate this
        /// <summary>
        /// Updates the states of the bookmark buttons.
        /// </summary>
        private void UpdateBookmarkButtonState()
            {
            if (_thesaurus.IsBookmarked) _view.BookmarkOn();
            else _view.BookmarkOff();
            }

        private void OnBookmarkClicked
            (object sender,
             EventArgs e)
            {
            if (!SearchValid) return;
            _thesaurus.IsBookmarked = !_thesaurus.IsBookmarked;
            UpdateBookmarkButtonState();
            }

        private void OnClosed
            (object sender,
             EventArgs e)
            {
            IsPresenting = false;
            }

        private void OnForwardClicked
            (object sender,
             EventArgs e)
            {
            if (!_thesaurus.History.CanGoForward) return;
            _thesaurus.History.GoForward();
            SearchFromHistory();
            }

        private void OnSearch
            (object sender,
             string word)
            {
            if (_view.SearchText != word) _view.SearchText = word;
            _view.Definitions.Clear();
            _thesaurus.SearchWord(word);

            if (!SearchValid || _thesaurus.Definitions.Count == 0)
                HandleInvalidSearch();
            else PopulateDefinitions(word);

            _view.EnableBackButton = _thesaurus.History.CanGoBackward;
            _view.EnableForwardButton =
                _thesaurus.History.CanGoForward;
            UpdateBookmarkButtonState();
            }

        private void OnSelectedDefinitionChanged
            (object sender,
             int e)
            {
            _view.ClearSynonyms();
            _view.ClearAntonyms();
            if (!SearchValid) return;
            var def = _thesaurus.Definitions[e];
            foreach (var syn in def.Synonyms)
                _view.AddSynonym(syn.Value, syn.Similarity);
            foreach (var ant in def.Antonyms)
                _view.AddAntonym(ant.Value, ant.Similarity);
            }

        private void OnOpenDictionaryClicked
            (object sender,
             EventArgs e)
            {
            if (!SearchValid) return;
            Process.Start(Resources.DictionaryUrl + _view.SearchText);
            }

        public void Dispose()
            {
            _view?.Dispose();
            }
    }
}