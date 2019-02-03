using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MouseNet.Logophi.Thesaurus
{
    internal class Browser : TunaInterface, IBookmarkManager
    {
        private readonly IList _bookmarks = new StringCollection();
        private readonly string _bookmarkPath;

        public Browser
            (string dataDirectory,
             bool persistentCache,
             bool persistentHistory)
            : base(dataDirectory, persistentCache)
            {
            History =
                new SearchHistory(dataDirectory, persistentHistory);
            _bookmarkPath =
                Path.Combine(dataDirectory, "bookmarks.lphi");
            if(!File.Exists(_bookmarkPath)) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenRead(_bookmarkPath))
                _bookmarks =
                    formatter.Deserialize(strm) as StringCollection;
            }

        public SearchHistory History { get; }
        public event EventHandler<string> BookmarkRemoved;
        public event EventHandler<string> BookmarkAdded;
        public IEnumerable Bookmarks => _bookmarks;

        private void SaveBookmarks()
            {
            var formatter = new BinaryFormatter();
            using(var strm = File.OpenWrite(_bookmarkPath))
                formatter.Serialize(strm, _bookmarks);
            }

        public void AddBookmark
            (object item)
            {
            if (_bookmarks.Contains(item)) return;
            _bookmarks.Add(item);
            SaveBookmarks();
            InvokeBookmarkAdded(this, item as string);
            }

        public void RemoveBookmark
            (object item)
            {
            if (!_bookmarks.Contains(item)) return;
            _bookmarks.Remove(item);
            SaveBookmarks();
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