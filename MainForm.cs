using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi
{
    public partial class MainForm : Form
    {
        private readonly SearchHistory _history = new SearchHistory();

        private readonly Thesaurus _thesaurus =
            new Thesaurus(Settings.Default.PersistentCache);

        public MainForm()
            {
            InitializeComponent();
            }

        private void FetchDefinitions
            (string word)
            {
            _cDefList.Items.Clear();
            _thesaurus.SearchWord(word);
            if (_thesaurus.Definitions != null)
                foreach (var def in _thesaurus.Definitions)
                    _cDefList.Items.Add(
                        $"{def.PartOfSpeech}: {def.Definition}");
            else return;
            
            
            if (_cDefList.Items.Count != 0)
                {
                _cBookmarkBtn.Enabled = true;
                if (word != _history.CurrentItem)
                    {
                    _history.AddItem(word);
                    if (!_cSearchText.Items.Contains(
                            _cSearchText.Text))
                        _cSearchText.Items.Add(_cSearchText.Text);
                    }
                } else
                {
                _cBookmarkBtn.Enabled = false;
                _cDefList.Items.Add("No results found.");
                }

            _cDefList.SelectedIndex = 0;
            _cBackBtn.Enabled = _history.CanGoBackward;
            _cForwardBtn.Enabled = _history.CanGoForward;
            UpdateBookmarkButton(_thesaurus.IsBookmarked);
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

        private void OnSearchTextSelectionChangeCommitted
            (object sender,
             EventArgs e)
            {
            FetchDefinitions(_cSearchText.SelectedItem.ToString());
            }

        private void Search()
            {
            FetchDefinitions(_cSearchText.Text);
            }

        private void SearchFromHistory()
            {
            FetchDefinitions(_history.CurrentItem);
            _cSearchText.Text = _history.CurrentItem;
            }

        private void OnBackClicked
            (object sender,
             EventArgs e)
            {
            //If Definitions is null, the search failed and a history
            //entry wasn't added, so skip going back.
            if (_thesaurus.Definitions != null)
                _history.GoBack();
            SearchFromHistory();
            }

        private void OnViewBookmarksClicked
            (object sender,
             EventArgs e)
            {
            var bookmarksForm =
                new BookmarksForm(_thesaurus.Bookmarks);
            bookmarksForm.BookmarkDoubleClick +=
                (o,
                 s) =>
                    {
                    _cSearchText.Text = s;
                    FetchDefinitions(s);
                    };
            bookmarksForm.BookmarkRemoved +=
                (o,
                 s) =>
                    {
                    _thesaurus.RemoveBookmark(s);
                    UpdateBookmarkButton(_thesaurus.IsBookmarked);
                    };
            bookmarksForm.Show(this);
            }

        private void OnDefListSelectedIndexChanged
            (object sender,
             EventArgs e)
            {
            _cSynonymList.Items.Clear();
            _cAntonymList.Items.Clear();
            if (_thesaurus.Definitions == null) return;
            var def = _thesaurus.Definitions[_cDefList.SelectedIndex];
            foreach (var syn in def.Synonyms)
                _cSynonymList.Items.Add(MakeListViewItem(syn));
            foreach (var ant in def.Antonyms)
                _cAntonymList.Items.Add(MakeListViewItem(ant));
            }

        private void UpdateBookmarkButton
            (bool val)
            {
            if (val)
                {
                _cBookmarkBtn.Image = Resources.bookmark_enabled;
                _cToolTip.SetToolTip(_cBookmarkBtn,
                                     "Remove Bookmark");
                } else
                {
                _cBookmarkBtn.Image = Resources.bookmark_disabled;
                _cToolTip.SetToolTip(_cBookmarkBtn, "Add Bookmark");
                }
            }

        private void OnBookmarkClicked
            (object sender,
             EventArgs e)
            {
            _thesaurus.IsBookmarked = !_thesaurus.IsBookmarked;
            UpdateBookmarkButton(_thesaurus.IsBookmarked);
            }

        private void OnForwardClicked
            (object sender,
             EventArgs e)
            {
            _history.GoForward();
            SearchFromHistory();
            }

        private void OnSearchClicked
            (object sender,
             EventArgs e)
            {
            Search();
            }

        private void OnSearchTextReturnPressed
            (object sender,
             KeyEventArgs e)
            {
            if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Enter)
                return;
            e.Handled = true;
            Search();
            }

        private void OnTermEntryDoubleClick
            (object sender,
             EventArgs e)
            {
            if (!(sender is ListView view)) return;
            _cSearchText.Text = view.SelectedItems[0].Text;
            FetchDefinitions(view.SelectedItems[0].Text);
            }
    }
}