using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Logophi
{
    public partial class MainForm : Form
    {
        private readonly SearchHistory _history = new SearchHistory();
        private readonly Thesaurus _thesaurus = new Thesaurus();

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
            //if Definitions was null or something weird happened,
            //so put an error message in the list box. Otherwise
            //go ahead and add the word to the search history,
            //unless it's identical to the last page.
            if (_cDefList.Items.Count != 0
             && word != _history.CurrentItem)
                {
                _history.AddItem(word);
                if (!_cSearchText.Items.Contains(_cSearchText.Text))
                    _cSearchText.Items.Add(_cSearchText.Text);
                } else _cDefList.Items.Add("No results found.");

            _cDefList.SelectedIndex = 0;
            _cBackBtn.Enabled = _history.CanGoBackward;
            _cForwardBtn.Enabled = _history.CanGoForward;
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
             EventArgs e) { }

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