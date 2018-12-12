using System;
using System.Drawing;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class MainFormPresenter : IViewPresenter<IMainFormView>
    {
        private readonly SearchHistory _history = new SearchHistory();
        private IMainFormView _view;

        public MainFormPresenter
            (bool persistentCache)
            {
            Thesaurus = new Thesaurus(persistentCache);
            }

        private bool SearchValid => Thesaurus.Definitions != null;
        public Thesaurus Thesaurus { get; }

        public void Present
            (IMainFormView view)
            {
            _view = view;
            _view.Search += OnSearch;
            _view.SelectedDefinitionChanged +=
                OnSelectedDefinitionChanged;
            _view.BackClicked += OnBackClicked;
            _view.ForwardClicked += OnForwardClicked;
            _view.BookmarkClicked += OnBookmarkClicked;
            _view.Show();
            }

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

        private static ListViewItem MakeListViewItem
            (TermEntry term)
            {
            var item = new ListViewItem(term.Value);
            switch (Math.Abs(term.Similarity))
                {
                case 100:
                    item.Font = new Font(item.Font, FontStyle.Bold);
                    break;
                case 50:
                    item.ForeColor = Color.DimGray;
                    break;
                case 10:
                    item.ForeColor = Color.DarkGray;
                    break;
                default:
                    item.ForeColor = Color.LightGray;
                    break;
                }

            return item;
            }

        private void PopulateDefinitions
            (string word)
            {
            foreach (var def in Thesaurus.Definitions)
                _view.Definitions.Add(
                    $"{def.PartOfSpeech}: {def.Definition}");
            _view.EnableBookmarkButton = true;
            _view.SelectedDefinitionIndex = 0;
            if (word == _history.CurrentItem) return;
            _history.AddItem(word);
            if (!_view.DropDownItems.Contains(word))
                _view.DropDownItems.Add(word);
            }

        private void SearchFromHistory()
            {
            OnSearch(this, _history.CurrentItem);
            _view.SearchText = _history.CurrentItem;
            }

        private void OnBackClicked
            (object sender,
             EventArgs e)
            {
            if (!_history.CanGoBackward) return;
            if (SearchValid)
                _history.GoBack();
            SearchFromHistory();
            }

        private void OnBookmarkClicked
            (object sender,
             EventArgs e)
            {
            if (!SearchValid) return;
            Thesaurus.IsBookmarked = !Thesaurus.IsBookmarked;
            _view.SetBookmarkState(Thesaurus.IsBookmarked);
            }

        private void OnForwardClicked
            (object sender,
             EventArgs e)
            {
            if (!_history.CanGoForward) return;
            _history.GoForward();
            SearchFromHistory();
            }

        private void OnSearch
            (object sender,
             string word)
            {
            if (_view.SearchText != word) _view.SearchText = word;
            _view.Definitions.Clear();
            Thesaurus.SearchWord(word);

            if (!SearchValid || Thesaurus.Definitions.Count == 0)
                HandleInvalidSearch();
            else PopulateDefinitions(word);

            _view.EnableBackButton = _history.CanGoBackward;
            _view.EnableForwardButton = _history.CanGoForward;
            _view.SetBookmarkState(Thesaurus.IsBookmarked);
            }

        private void OnSelectedDefinitionChanged
            (object sender,
             int e)
            {
            _view.Synonyms.Clear();
            _view.Antonyms.Clear();
            if (!SearchValid) return;
            var def = Thesaurus.Definitions[e];
            foreach (var syn in def.Synonyms)
                _view.Synonyms.Add(MakeListViewItem(syn));
            foreach (var ant in def.Antonyms)
                _view.Antonyms.Add(MakeListViewItem(ant));
            }
    }
}