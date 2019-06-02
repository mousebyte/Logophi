using System;
using System.Collections;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms {
    public partial class MainForm : LogophiForm, IMainFormView {
        public MainForm()
            {
            InitializeComponent();
            }

        public event EventHandler ShowAboutClicked;
        public event EventHandler<string> Search;
        public event EventHandler BackClicked;
        public event EventHandler ForwardClicked;
        public event EventHandler BookmarkClicked;
        public event EventHandler<int> SelectedDefinitionChanged;
        public event EventHandler OpenDictionaryClicked;
        public event EventHandler OpenGithubClicked;
        public IList Definitions => _cDefList.Items;
        public IList DropDownItems => _cSearchText.Items;

        public string SearchText {
            get => _cSearchText.Text;
            set {
                _cSearchText.Text = value;
                TrySearch();
            }
        }

        public bool EnableBackButton {
            get => _cBackBtn.Enabled;
            set => _cBackBtn.Enabled = value;
        }

        public bool EnableForwardButton {
            get => _cForwardBtn.Enabled;
            set => _cForwardBtn.Enabled = value;
        }

        public bool EnableBookmarkButton {
            get => _cBookmarkBtn.Enabled;
            set {
                _cBookmarkBtn.Enabled = value;
                _cBookmarkMenuItem.Enabled = value;
                _cDictionaryMenuItem.Enabled = value;
            }
        }

        public int SelectedDefinitionIndex {
            get => _cDefList.SelectedIndex;
            set => _cDefList.SelectedIndex = value;
        }

        public void BookmarkOn()
            {
            if (!EnableBookmarkButton) return;
            _cBookmarkBtn.Image = Resources.bookmark_enabled;
            _cBookmarkMenuItem.Image = Resources.bookmark_disabled;
            _cBookmarkMenuItem.Text = @"Remove";
            _cToolTip.SetToolTip(_cBookmarkBtn, "Remove Bookmark");
            }

        public void BookmarkOff()
            {
            if (!EnableBookmarkButton) return;
            _cBookmarkBtn.Image = Resources.bookmark_disabled;
            _cBookmarkMenuItem.Image = Resources.bookmark_enabled;
            _cBookmarkMenuItem.Text = @"Add";
            _cToolTip.SetToolTip(_cBookmarkBtn, "Add Bookmark");
            }

        public void ClearSynonyms()
            {
            _cSynonymList.Clear();
            }

        public void ClearAntonyms()
            {
            _cAntonymList.Clear();
            }

        public event EventHandler ExitClicked;
        public event EventHandler ShowBookmarksClicked;
        public event EventHandler ShowPreferencesClicked;

        public void AddSynonym
        (string term,
         int similarity)
            {
            _cSynonymList.AddTerm(term, similarity);
            }

        public void AddAntonym
        (string term,
         int similarity)
            {
            _cAntonymList.AddTerm(term, similarity);
            }

        private void TrySearch()
            {
            InvokeSearch(this, _cSearchText.Text);
            }

        private void OnSearchClicked
        (object sender,
         EventArgs e)
            {
            TrySearch();
            }

        private void OnSearchTextReturnPressed
        (object sender,
         KeyEventArgs e)
            {
            if (e.KeyCode != Keys.Return) return;
            e.Handled = true;
            TrySearch();
            }

        private void OnSearchTextSelectionChangeCommitted
        (object sender,
         EventArgs e)
            {
            InvokeSearch(this, _cSearchText.SelectedItem.ToString());
            }

        private void OnTermEntryDoubleClick
        (object sender,
         string e)
            {
            SearchText = e;
            }

        private void InvokeExitClicked
        (object sender,
         EventArgs args)
            {
            ExitClicked?.Invoke(sender, args);
            }

        private void InvokeShowAboutClicked
        (object sender,
         EventArgs args)
            {
            ShowAboutClicked?.Invoke(sender, args);
            }

        private void InvokeShowBookmarksClicked
        (object sender,
         EventArgs args)
            {
            ShowBookmarksClicked?.Invoke(sender, args);
            }

        private void InvokeShowPreferencesClicked
        (object sender,
         EventArgs args)
            {
            ShowPreferencesClicked?.Invoke(sender, args);
            }

        private void InvokeSearch
        (object sender,
         string args)
            {
            Search?.Invoke(sender, args);
            }

        private void InvokeBackClicked
        (object sender,
         EventArgs args)
            {
            BackClicked?.Invoke(sender, args);
            }

        private void InvokeForwardClicked
        (object sender,
         EventArgs args)
            {
            ForwardClicked?.Invoke(sender, args);
            }

        private void InvokeBookmarkClicked
        (object sender,
         EventArgs args)
            {
            BookmarkClicked?.Invoke(sender, args);
            }

        private void InvokeSelectedDefinitionChanged
        (object sender,
         EventArgs args)
            {
            SelectedDefinitionChanged?.Invoke(
                sender,
                _cDefList.SelectedIndex);
            }

        private void InvokeViewDictionaryClicked
        (object sender,
         EventArgs args)
            {
            OpenDictionaryClicked?.Invoke(sender, args);
            }

        private void InvokeGithubProjectClicked
        (object sender,
         EventArgs args)
            {
            OpenGithubClicked?.Invoke(sender, args);
            }
    }
}