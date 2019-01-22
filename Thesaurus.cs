using System;
using System.Collections;
using System.Collections.Specialized;

namespace MouseNet.Logophi
{
    internal class Thesaurus : TunaInterface, IBookmarkManager
    {
        private readonly IList _bookmarks = new StringCollection();

        public Thesaurus
            (string dataDirectory,
             bool persistentCache,
             bool persistentHistory)
            : base(dataDirectory, persistentCache)
            {
            History =
                new SearchHistory(dataDirectory, persistentHistory);
            }

        public SearchHistory History { get; }
        public event EventHandler<string> BookmarkRemoved;
        public event EventHandler<string> BookmarkAdded;
        public IEnumerable Bookmarks => _bookmarks;

        public void AddBookmark
            (object item)
            {
            if (_bookmarks.Contains(item)) return;
            _bookmarks.Add(item);
            InvokeBookmarkAdded(this, item as string);
            }

        public void RemoveBookmark
            (object item)
            {
            if (!_bookmarks.Contains(item)) return;
            _bookmarks.Remove(item);
            InvokeBookmarkRemoved(this, item as string);
            }

        public bool IsBookmarked {
            get => _bookmarks.Contains(SearchTerm);
            set {
                if (value) AddBookmark(SearchTerm);
                else RemoveBookmark(SearchTerm);
            }
        }

        private void InvokeBookmarkAdded
            (object sender,
             string args)
            {
            BookmarkAdded?.Invoke(sender, args);
            }

        private void InvokeBookmarkRemoved
            (object sender,
             string args)
            {
            BookmarkRemoved?.Invoke(sender, args);
            }

        protected override void InvokeWordSearched
            (object sender,
             string args)
            {
            if (args != History.CurrentItem)
                History.AddItem(args);
            base.InvokeWordSearched(sender, args);
            }
    }
}