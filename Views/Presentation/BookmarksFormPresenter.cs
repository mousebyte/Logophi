using System;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class BookmarksFormPresenter
        : IViewPresenter<IBookmarksFormView>
    {
        private readonly Action<string> _callback;
        private readonly TunaInterface _tunaInterface;

        public BookmarksFormPresenter
            (TunaInterface tunaInterface,
             Action<string> callback)
            {
            _tunaInterface = tunaInterface;
            _callback = callback;
            }

        public void Present
            (IBookmarksFormView view)
            {
            View = view;
            foreach (var bookmark in _tunaInterface.Bookmarks)
                View.Items.Add(bookmark);
            View.BookmarkRemoved += OnBookmarkRemoved;
            View.BookmarkActivated += OnBookmarkActivated;
            View.Closed += OnClosed;
            View.Show();
            IsPresenting = true;
            }

        public IBookmarksFormView View { get; private set; }
        public bool IsPresenting { get; private set; }

        private void OnBookmarkActivated
            (object sender,
             string e)
            {
            _callback(e);
            }

        private void OnBookmarkRemoved
            (object sender,
             string e)
            {
            View.Items.Remove(e);
            _tunaInterface.RemoveBookmark(e);
            }

        private void OnClosed
            (object sender,
             EventArgs e)
            {
            IsPresenting = false;
            }
    }
}