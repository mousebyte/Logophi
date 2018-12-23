using System;
using System.Collections;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms
{
    public partial class MainForm : Form, IMainFormView
    {
        public MainForm()
            {
            InitializeComponent();
            }

        public event EventHandler<string> Search;
        public event EventHandler BackClicked;
        public event EventHandler ForwardClicked;
        public event EventHandler BookmarkClicked;
        public event EventHandler<int> SelectedDefinitionChanged;
        public IList Definitions => _cDefList.Items;
        public IList Synonyms => _cSynonymList.Items;
        public IList Antonyms => _cAntonymList.Items;
        public IList DropDownItems => _cSearchText.Items;

        public string SearchText {
            get => _cSearchText.Text;
            set => _cSearchText.Text = value;
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

        public void SetBookmarkState
            (bool bookmarked)
            {
            if (!EnableBookmarkButton) return;
            if (bookmarked)
                {
                _cBookmarkBtn.Image = Resources.bookmark_enabled;
                _cBookmarkMenuItem.Image =
                    Resources.bookmark_disabled;
                _cBookmarkMenuItem.Text = @"Remove";
                _cToolTip.SetToolTip(_cBookmarkBtn,
                                     "Remove Bookmark");
                } else
                {
                _cBookmarkBtn.Image = Resources.bookmark_disabled;
                _cBookmarkMenuItem.Image = Resources.bookmark_enabled;
                _cBookmarkMenuItem.Text = @"Add";
                _cToolTip.SetToolTip(_cBookmarkBtn, "Add Bookmark");
                }
            }

        public event EventHandler ViewDictionaryClicked;
        public event EventHandler PreferencesClicked;

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
             EventArgs e)
            {
            if (!(sender is ListView view)) return;
            InvokeSearch(this, view.SelectedItems[0].Text);
            }

        private void OnViewBookmarksClicked
            (object sender,
             EventArgs args)
            {
            ViewBookmarksClicked?.Invoke(sender, args);
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
            ViewDictionaryClicked?.Invoke(sender, args);
            }

        private void InvokePreferencesClicked
            (object sender,
             EventArgs args)
            {
            PreferencesClicked?.Invoke(sender, args);
            }

        private void InvokeExitClicked
            (object sender,
             EventArgs args)
            {
            ExitClicked?.Invoke(sender, args);
            }

        private void InvokeAboutClicked
            (object sender,
             EventArgs args)
            {
            AboutClicked?.Invoke(sender, args);
            }

        private void InvokeGithubProjectClicked
            (object sender,
             EventArgs args)
            {
            GithubProjectClicked?.Invoke(sender, args);
            }

        public event EventHandler ViewBookmarksClicked;
        public event EventHandler GithubProjectClicked;
        public event EventHandler AboutClicked;
        public event EventHandler ExitClicked;
    }
}