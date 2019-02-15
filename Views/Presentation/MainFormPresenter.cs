using System;
using System.Diagnostics;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class MainFormPresenter : ViewPresenter<IMainFormView>
    {
        private readonly Browser _browser;
        private bool SearchValid => _browser.Definitions != null;

        public MainFormPresenter
            (Browser browser)
            {
            _browser = browser;
            browser.BookmarkRemoved += OnBookmarkRemoved;
            browser.BookmarkAdded += OnBookmarkAdded;
            browser.SearchCompleted += OnSearchCompleted;
            }

        protected override void InitializeView()
            {
            PopulateDropDownItems();
            View.ViewEventActivated += OnViewEventActivated;
            View.Search += OnSearch;
            View.SelectedDefinitionChanged +=
                OnSelectedDefinitionChanged;
            View.BackClicked += OnBackClicked;
            View.ForwardClicked += OnForwardClicked;
            View.BookmarkClicked += OnBookmarkClicked;
            View.OpenDictionaryClicked += OnOpenDictionaryClicked;
            View.OpenGithubClicked += OnOpenGithubClicked;
            }

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
            if (IsPresenting && e == _browser.SearchTerm)
                View.BookmarkOn();
            }

        private void OnBookmarkRemoved
            (object sender,
             string e)
            {
            if (IsPresenting && e == _browser.SearchTerm)
                View.BookmarkOff();
            }

        /// <summary>
        /// Populates the drop down items of the view's search box
        /// with values from the browser's history.
        /// </summary>
        private void PopulateDropDownItems()
            {
            if (_browser.History.Count <= 0) return;
            foreach (var i in _browser.History)
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
            foreach (var def in _browser.Definitions)
                View.Definitions.Add(
                    $"{def.PartOfSpeech}: {def.Definition}");

            View.EnableBookmarkButton = true;
            View.SelectedDefinitionIndex = 0;
            //add to the dropdown items if it's not already there
            if (!View.DropDownItems.Contains(_browser.SearchTerm))
                View.DropDownItems.Insert(0, _browser.SearchTerm);
            }

        /// <summary>
        /// Searches for the current history item.
        /// </summary>
        private void SearchFromHistory()
            {
            OnSearch(this, _browser.History.CurrentItem);
            View.SearchText = _browser.History.CurrentItem;
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
            if (!_browser.History.CanGoBackward) return;
            //check if the search is valid because invalid search
            //terms are not added to history
            if (SearchValid)
                _browser.History.GoBack();
            SearchFromHistory();
            }

        private void OnBookmarkClicked
            (object sender,
             EventArgs e)
            {
            if (SearchValid)
                _browser.IsBookmarked = !_browser.IsBookmarked;
            }

        private void OnForwardClicked
            (object sender,
             EventArgs e)
            {
            if (!_browser.History.CanGoForward) return;
            _browser.History.GoForward();
            SearchFromHistory();
            }

        private void OnSearch
            (object sender,
             string word)
            {
            if (View.SearchText != word) View.SearchText = word;
            View.Definitions.Clear();
            _browser.SearchWord(word);
            }

        private void OnSearchCompleted
            (object sender,
             SearchEventArgs e)
            {
            //handle valid or invalid search
            if (e.Success) HandleSuccessfulSearch();
            else HandleInvalidSearch();

            //update button states
            View.EnableBackButton = _browser.History.CanGoBackward;
            View.EnableForwardButton = _browser.History.CanGoForward;

            if (_browser.IsBookmarked) View.BookmarkOn();
            else View.BookmarkOff();
            }

        private void OnSelectedDefinitionChanged
            (object sender,
             int e)
            {
            //clear lists
            View.ClearSynonyms();
            View.ClearAntonyms();
            if (!SearchValid) return;
            //populate synonyms and antonyms from currently selected def
            var def = _browser.Definitions[e];
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
                Process.Start(Resources.DictionaryUrl
                            + View.SearchText);
            }
    }
}