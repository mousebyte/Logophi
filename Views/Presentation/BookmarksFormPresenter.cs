using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi.Views.Presentation {
    /// <inheritdoc />
    /// <summary>
    ///     Presents an <see cref="IBookmarksFormView" />.
    /// </summary>
    internal class BookmarksFormPresenter
        : ViewPresenter<IBookmarksFormView> {
        private readonly IBookmarkManager _bookmarkManager;

        public BookmarksFormPresenter
            (IBookmarkManager bookmarkManager)
            {
            _bookmarkManager = bookmarkManager;
            _bookmarkManager.BookmarkAdded += OnBookmarkAdded;
            _bookmarkManager.BookmarkRemoved += OnBookmarkRemoved;
            }

        protected override void InitializeView()
            {
            foreach (var bookmark in _bookmarkManager.Bookmarks)
                View.Items.Add(bookmark);
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