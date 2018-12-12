using System;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class BookmarksFormPresenter
        : IViewPresenter<IBookmarksFormView>
    {
        private readonly Action<string> _callback;
        private readonly Thesaurus _thesaurus;
        private IBookmarksFormView _view;

        public BookmarksFormPresenter
            (Thesaurus thesaurus,
             Action<string> callback)
            {
            _thesaurus = thesaurus;
            _callback = callback;
            }

        public void Present
            (IBookmarksFormView view)
            {
            _view = view;
            foreach (var bookmark in _thesaurus.Bookmarks)
                _view.Items.Add(bookmark);
            _view.BookmarkRemoved += OnBookmarkRemoved;
            _view.BookmarkActivated += OnBookmarkActivated;
            _view.Show();
            }

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
            _view.Items.Remove(e);
            _thesaurus.RemoveBookmark(e);
            }
    }
}