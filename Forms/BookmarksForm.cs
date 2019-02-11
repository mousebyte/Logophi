using System;
using System.Collections;
using System.Windows.Forms;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms
{
    public partial class BookmarksForm : Form, IBookmarksFormView
    {
        public BookmarksForm()
            {
            InitializeComponent();
            }

        public IList Items => _cBookmarksList.Items;
        public event EventHandler<string> BookmarkRemoved;

        public void Show
            (object parent)
            {
            if (!(parent is IWin32Window window)) return;
            base.Show(window);
            }

        public event EventHandler<ViewEventArgs> ViewEventActivated;

        private void InvokeBookmarkActivated
            (object sender,
             MouseEventArgs args)
            {
            var i = _cBookmarksList.IndexFromPoint(args.Location);
            if (i != ListBox.NoMatches)
                ViewEventActivated?.Invoke(
                    this,
                    new ViewEventArgs(Items[i]));
            }

        private void InvokeBookmarkRemoved
            (object sender,
             EventArgs args)
            {
            BookmarkRemoved?.Invoke(sender,
                                    _cBookmarksList
                                       .SelectedItem.ToString());
            }
    }
}