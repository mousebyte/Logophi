using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms
{
    public partial class MainForm : LogophiForm, IMainFormView
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
        public event EventHandler OpenDictionaryClicked;
        public event EventHandler OpenGithubClicked;
        public event EventHandler<ViewEventArgs> ViewEventActivated;
        public IList Definitions => _cDefList.Items;
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
        
        public void AddSynonym
            (string term,
             int similarity)
            {
            _cSynonymList.Items.Add(MakeItem(term, similarity));
            }

        public void AddAntonym
            (string term,
             int similarity)
            {
            _cAntonymList.Items.Add(MakeItem(term, similarity));
            }

        private static ListViewItem MakeItem
            (string term,
             int similarity)
            {
            var item = new ListViewItem(term);
            switch (Math.Abs(similarity))
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

        private void TrySearch()
            {
            InvokeSearch(this, _cSearchText.Text);
            }

        private void OnExitClicked
            (object sender,
             EventArgs args)
            {
            InvokeViewEventActivated(this,
                                     new ViewEventArgs(
                                         "ExitClicked"));
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

        private void OnShowAboutClicked
            (object sender,
             EventArgs args)
            {
            InvokeViewEventActivated(this,
                                     new ViewEventArgs(
                                         "ShowAboutClicked"));
            }

        private void OnShowBookmarksClicked
            (object sender,
             EventArgs args)
            {
            InvokeViewEventActivated(sender,
                                     new ViewEventArgs(
                                         "ShowBookmarksClicked"));
            }

        private void OnShowPreferencesClicked
            (object sender,
             EventArgs args)
            {
            InvokeViewEventActivated(this,
                                     new ViewEventArgs(
                                         "ShowPreferencesClicked"));
            }

        private void OnTermEntryDoubleClick
            (object sender,
             EventArgs e)
            {
            if (!(sender is ListView view)) return;
            InvokeSearch(this, view.SelectedItems[0].Text);
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

        private void InvokeViewEventActivated
            (object sender,
             ViewEventArgs args)
            {
            ViewEventActivated?.Invoke(sender, args);
            }
    }
}