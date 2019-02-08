using System;
using System.Collections;

namespace MouseNet.Logophi.Thesaurus
{
    /// <summary>
    /// Exposes an object which manages a collection of bookmarks.
    /// </summary>
    internal interface IBookmarkManager
    {
        /// <summary>
        /// Gets the bookmarks stored in the bookmark manager.
        /// </summary>
        IEnumerable Bookmarks { get; }

        /// <summary>
        /// Bookmarks an item.
        /// </summary>
        /// <param name="item">The item to add to the bookmark manager.</param>
        void AddBookmark
            (object item);

        /// <summary>
        /// Removes a bookmarked item.
        /// </summary>
        /// <param name="item">The item to remove from the bookmark manager.</param>
        void RemoveBookmark
            (object item);
        
        /// <summary>
        /// Occurs when a bookmark is removed.
        /// </summary>
        event EventHandler<string> BookmarkRemoved;
        
        /// <summary>
        /// Occurs when a bookmark is added.
        /// </summary>
        event EventHandler<string> BookmarkAdded;
    }
}
