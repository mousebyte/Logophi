using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MouseNet.Logophi.Thesaurus
{
    /// <inheritdoc cref="TunaInterface" />
    /// <summary>
    /// Represents a <see cref="TunaInterface" /> that supports browser-like
    /// navigation and features.
    /// </summary>
    internal class Browser : TunaInterface, IBookmarkManager
    {
        private readonly IList _bookmarks = new StringCollection();
        private readonly string _bookmarkPath;

        /// <inheritdoc />
        /// <summary>
        /// Creates a new <see cref="T:MouseNet.Logophi.Thesaurus.Browser" /> object.
        /// </summary>
        /// <param name="dataDirectory">The location to use for persistent storage.</param>
        /// <param name="persistentCache">A value indicating whether or not a persistent cache
        /// should be used.</param>
        /// <param name="persistentHistory">A value indicating whether or not a persistent
        /// history file should be used.</param>
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

            //if a bookmarks file exists, load it
            if (!File.Exists(_bookmarkPath)) return;
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenRead(_bookmarkPath))
                _bookmarks =
                    formatter.Deserialize(strm) as StringCollection;
            }

        /// <summary>
        /// Gets the <see cref="SearchHistory"/> associated with the browser.
        /// </summary>
        public SearchHistory History { get; }
        /// <inheritdoc />
        public event EventHandler<string> BookmarkRemoved;
        /// <inheritdoc />
        public event EventHandler<string> BookmarkAdded;
        /// <inheritdoc />
        public IEnumerable Bookmarks => _bookmarks;

        /// <summary>
        /// Saves the bookmarks to a file.
        /// </summary>
        private void SaveBookmarks()
            {
            var formatter = new BinaryFormatter();
            using (var strm = File.OpenWrite(_bookmarkPath))
                formatter.Serialize(strm, _bookmarks);
            }

        /// <inheritdoc />
        public void AddBookmark
            (object item)
            {
            if (_bookmarks.Contains(item)) return;
            _bookmarks.Add(item);
            SaveBookmarks();
            InvokeBookmarkAdded(this, item as string);
            }

        /// <inheritdoc />
        public void RemoveBookmark
            (object item)
            {
            if (!_bookmarks.Contains(item)) return;
            _bookmarks.Remove(item);
            SaveBookmarks();
            InvokeBookmarkRemoved(this, item as string);
            }

        /// <summary>
        /// Gets or sets a value indicating whether or not the current
        /// search term is bookmarked.
        /// </summary>
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

        protected override void InvokeSearchCompleted
            (object sender,
             SearchEventArgs args)
            {
            if (args.Success && args.Word != History.CurrentItem)
                History.AddItem(args.Word);
            base.InvokeSearchCompleted(sender, args);
            }
    }
}