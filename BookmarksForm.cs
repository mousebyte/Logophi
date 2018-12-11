using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseNet.Logophi
{
    public partial class BookmarksForm : Form
    {
        public BookmarksForm
            (IEnumerable<string> bookmarks)
            {
            InitializeComponent();
            foreach (var bookmark in bookmarks)
                _cBookmarksList.Items.Add(bookmark);
            }

        public event EventHandler<string> BookmarkRemoved;
        public event EventHandler<string> BookmarkDoubleClick;

        private void InvokeBookmarkDoubleClick
            (object sender,
             string args)
            {
            BookmarkDoubleClick?.Invoke(sender, args);
            }

        private void InvokeBookmarkRemoved
            (object sender,
             string args)
            {
            BookmarkRemoved?.Invoke(sender, args);
            }

        private void OnRemoveClicked
            (object sender,
             EventArgs e)
            {
            InvokeBookmarkRemoved(this,
                                  _cBookmarksList.SelectedItem as
                                      string);
            _cBookmarksList.Items.RemoveAt(
                _cBookmarksList.SelectedIndex);
            }

        private void OnBookmarkListItemDoubleClick
            (object sender,
             MouseEventArgs e)
            {
            var i = _cBookmarksList.IndexFromPoint(e.Location);
            if (i != ListBox.NoMatches)
                InvokeBookmarkDoubleClick(this,
                                          _cBookmarksList.Items[i] as
                                              string);
            }
    }
}