using System;
using System.Collections;
using System.Windows.Forms;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms
{
    public partial class BookmarksForm
        : LogophiForm, IBookmarksFormView
    {
        public BookmarksForm()
            {
            InitializeComponent();
            }

        public IList Items => _cBookmarksList.Items;
        public event EventHandler<string> BookmarkActivated;
        public event EventHandler<string> BookmarkRemoved;

        private void InvokeBookmarkActivated
            (object sender,
             MouseEventArgs args)
            {
            var i = _cBookmarksList.IndexFromPoint(args.Location);
            if (i != ListBox.NoMatches)
                BookmarkActivated?.Invoke(
                    sender,
                    Items[i].ToString());
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