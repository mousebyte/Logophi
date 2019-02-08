using System;
using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IBookmarksFormView" />.
    /// </summary>
    internal class BookmarksFormPresenter
        : IViewPresenter<IBookmarksFormView>
    {
        private readonly IBookmarkManager _bookmarkManager;
        private IBookmarksFormView _view;

        public BookmarksFormPresenter
            (IBookmarkManager bookmarkManager)
            {
            _bookmarkManager = bookmarkManager;
            _bookmarkManager.BookmarkAdded += OnBookmarkAdded;
            _bookmarkManager.BookmarkRemoved += OnBookmarkRemoved;
            }

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
            _view = view;
            foreach (var bookmark in _bookmarkManager.Bookmarks)
                _view.Items.Add(bookmark);
            _view.ViewEventActivated += OnViewEventActivated;
            _view.BookmarkRemoved += OnBookmarkRemoved;
            _view.Closed += OnClosed;
            if (parent == null) _view.Show();
            else _view.Show(parent);
            IsPresenting = true;
            }

        /// <inheritdoc />
        public IBookmarksFormView View => _view;
        /// <summary>
        /// Gets a value indicating whether or not the view is being presented to the user.
        /// </summary>
        public bool IsPresenting { get; private set; }

        public void Dispose()
            {
            _view?.Dispose();
            }

        private void OnBookmarkAdded
            (object sender,
             string e)
            {
            if (!IsPresenting) return;
            _view.Items.Add(e);
            }

        private void OnBookmarkRemoved
            (object sender,
             string e)
            {
            if (!IsPresenting) return;
            _view.Items.Remove(e);
            _bookmarkManager.RemoveBookmark(e);
            }

        private void OnClosed
            (object sender,
             EventArgs e)
            {
            IsPresenting = false;
            _view.Dispose();
            }

        private void OnViewEventActivated
            (object sender,
             ViewEventArgs e)
            {
            BookmarkActivated?.Invoke(sender, e.Tag as string);
            }

        /// <summary>
        /// Occurs when an item in the bookmarks list is activated.
        /// </summary>
        public event EventHandler<string> BookmarkActivated;
    }
}