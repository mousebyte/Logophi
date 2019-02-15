using System;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IBookmarksFormView" />.
    /// </summary>
    internal class BookmarksFormPresenter
        : ViewPresenter<IBookmarksFormView>
    {
        private readonly IBookmarkManager _bookmarkManager;
        private readonly EventHandler<string> _onBookmarkActivated;

        public BookmarksFormPresenter
            (IBookmarkManager bookmarkManager,
             Action<string> bookmarkActivatedAction)
            {
            _bookmarkManager = bookmarkManager;
            _onBookmarkActivated =
                bookmarkActivatedAction.ToHandler();
            _bookmarkManager.BookmarkAdded += OnBookmarkAdded;
            _bookmarkManager.BookmarkRemoved += OnBookmarkRemoved;
            }

        protected override void InitializeView()
            {
            foreach (var bookmark in _bookmarkManager.Bookmarks)
                View.Items.Add(bookmark);
            View.BookmarkActivated += _onBookmarkActivated;
            View.BookmarkRemoved += OnBookmarkRemoved;
            }

        private void OnBookmarkAdded
            (object sender,
             string e)
            {
            if (!IsPresenting) return;
            View.Items.Add(e);
            }

        private void OnBookmarkRemoved
            (object sender,
             string e)
            {
            if (!IsPresenting) return;
            View.Items.Remove(e);
            _bookmarkManager.RemoveBookmark(e);
            }
    }
}