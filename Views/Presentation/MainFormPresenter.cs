using System;
using System.Diagnostics;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class MainFormPresenter : IViewPresenter<IMainFormView>
    {
        private readonly Thesaurus _thesaurus;
        private IMainFormView _view;
        private bool SearchValid => _thesaurus.Definitions != null;

        public MainFormPresenter
            (Thesaurus thesaurus)
            {
            _thesaurus = thesaurus;
            }

        public void Present
            (IMainFormView view)
            {
            Present(view, null);
            }

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

        public event EventHandler ShowBookmarksClicked;
        public event EventHandler ShowPreferencesClicked;
        public event EventHandler ShowAboutClicked;

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

        public IView View => _view;
        public bool IsPresenting { get; private set; }

        public void Search
            (string word)
            {
            OnSearch(this, word);
            }

        private void HandleInvalidSearch()
            {
            _view.Definitions.Add(Resources.InvalidSearch);
            _view.EnableBookmarkButton = false;
            }

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