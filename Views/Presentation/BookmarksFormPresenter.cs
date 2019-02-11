using System;
using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    ///     Presents an <see cref="IBookmarksFormView" />.
    /// </summary>
    internal class BookmarksFormPresenter
        : IViewPresenter<IBookmarksFormView>
    {
        private readonly IBookmarkManager _bookmarkManager;

        public BookmarksFormPresenter
            (IBookmarkManager bookmarkManager)
            {
            _bookmarkManager = bookmarkManager;
            _bookmarkManager.BookmarkAdded += OnBookmarkAdded;
            _bookmarkManager.BookmarkRemoved += OnBookmarkRemoved;
            }

        /// <summary>
        ///     Gets a value indicating whether or not the view is being presented to the user.
        /// </summary>
        public bool IsPresenting { get; private set; }

        /// <inheritdoc />
        public void Present
            (IBookmarksFormView view)
            {
            Present(view, null);
            }

        /// <inheritdoc />
        public void Present
            (IBookmarksFormView view,
             object parent)
            {
            View = view;
            foreach (var bookmark in _bookmarkManager.Bookmarks)
                View.Items.Add(bookmark);
            View.ViewEventActivated += OnViewEventActivated;
            View.BookmarkRemoved += OnBookmarkRemoved;
            View.Closed += OnClosed;
            if (parent == null) View.Show();
            else View.Show(parent);
            IsPresenting = true;
            }

        /// <inheritdoc />
        public IBookmarksFormView View { get; private set; }

        public void Dispose()
            {
            View?.Dispose();
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

        private void OnClosed
            (object sender,
             EventArgs e)
            {
            IsPresenting = false;
            View.Dispose();
            }

        private void OnViewEventActivated
            (object sender,
             ViewEventArgs e)
            {
            BookmarkActivated?.Invoke(sender, e.Tag as string);
            }

        /// <summary>
        ///     Occurs when an item in the bookmarks list is activated.
        /// </summary>
        public event EventHandler<string> BookmarkActivated;
    }
}