using System;
using System.Diagnostics;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IMainFormView" />.
    /// </summary>
    internal class MainFormPresenter : IViewPresenter<IMainFormView>
    {
        private readonly Browser _thesaurus;
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
            View = view;
            //populate dropdown items from search history
            PopulateDropDownItems();
            
            //connect event handlers
            _thesaurus.SearchCompleted += OnSearchCompleted;
            View.ViewEventActivated += OnViewEventActivated;
            View.Search += OnSearch;
            View.SelectedDefinitionChanged +=
                OnSelectedDefinitionChanged;
            View.BackClicked += OnBackClicked;
            View.ForwardClicked += OnForwardClicked;
            View.BookmarkClicked += OnBookmarkClicked;
            View.OpenDictionaryClicked += OnOpenDictionaryClicked;
            View.OpenGithubClicked += OnOpenGithubClicked;
            View.Closed += OnClosed;
            
            //show the view
            if (parent == null) View.Show();
            else View.Show(parent);
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
                View.BookmarkOn();
            }

        private void OnBookmarkRemoved
            (object sender,
             string e)
            {
            if (IsPresenting && e == _thesaurus.SearchTerm)
                View.BookmarkOff();
            }

        /// <summary>
        /// Populates the drop down items of the view's search box
        /// with values from the browser's history.
        /// </summary>
        private void PopulateDropDownItems()
            {
            if (_thesaurus.History.Count <= 0) return;
            foreach (var i in _thesaurus.History)
                if (!View.DropDownItems.Contains(i))
                    View.DropDownItems.Add(i);
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
        public IMainFormView View { get; private set; }
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
            View.Definitions.Add(Resources.InvalidSearch);
            View.EnableBookmarkButton = false;
            }

        /// <summary>
        /// Performs actions necessary to handle a successful search.
        /// </summary>
        private void HandleSuccessfulSearch()
            {
            //populate the list of definitions
            foreach (var def in _thesaurus.Definitions)
                View.Definitions.Add(
                    $"{def.PartOfSpeech}: {def.Definition}");
            
            View.EnableBookmarkButton = true;
            View.SelectedDefinitionIndex = 0;
            //add to the dropdown items if it's not already there
            if (!View.DropDownItems.Contains(_thesaurus.SearchTerm))
                View.DropDownItems.Insert(0, _thesaurus.SearchTerm);
            }

        private void OnSearchCompleted
            (object sender,
             SearchEventArgs e)
            {
            //handle valid or invalid search
            if(e.Success) HandleSuccessfulSearch();
            else HandleInvalidSearch();

            //update button states
            View.EnableBackButton = _thesaurus.History.CanGoBackward;
            View.EnableForwardButton =
                _thesaurus.History.CanGoForward;
            
            if (_thesaurus.IsBookmarked) View.BookmarkOn();
            else View.BookmarkOff();
        }

        /// <summary>
        /// Searches for the current history item.
        /// </summary>
        private void SearchFromHistory()
            {
            OnSearch(this, _thesaurus.History.CurrentItem);
            View.SearchText = _thesaurus.History.CurrentItem;
            }

        private static void OnOpenGithubClicked
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
            //check if the search is valid because invalid search
            //terms are not added to history
            if (SearchValid)
                _thesaurus.History.GoBack();
            SearchFromHistory();
            }
        
        private void OnBookmarkClicked
            (object sender,
             EventArgs e)
            {
            if (SearchValid)
                _thesaurus.IsBookmarked = !_thesaurus.IsBookmarked;
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
            if (View.SearchText != word) View.SearchText = word;
            View.Definitions.Clear();
            _thesaurus.SearchWord(word);
            }

        private void OnSelectedDefinitionChanged
            (object sender,
             int e)
            {
            //clear synonyms and antonyms
            View.ClearSynonyms();
            View.ClearAntonyms();
            //if the search is invalid, return and leave them blank
            if (!SearchValid) return;
            //get the definition at the currently selected index and
            //populate synonyms and antonyms from it
            var def = _thesaurus.Definitions[e];
            foreach (var syn in def.Synonyms)
                View.AddSynonym(syn.Value, syn.Similarity);
            foreach (var ant in def.Antonyms)
                View.AddAntonym(ant.Value, ant.Similarity);
            }

        private void OnOpenDictionaryClicked
            (object sender,
             EventArgs e)
            {
            if (SearchValid)
                Process.Start(Resources.DictionaryUrl + View.SearchText);
            }

        public void Dispose()
            {
            View?.Dispose();
            }
    }
}