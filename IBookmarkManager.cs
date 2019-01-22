using System;
using System.Collections;

namespace MouseNet.Logophi
{
    internal interface IBookmarkManager
    {
        IEnumerable Bookmarks { get; }

        void AddBookmark
            (object item);

        void RemoveBookmark
            (object item);
        
        bool IsBookmarked { get; set; }
        event EventHandler<string> BookmarkRemoved;
        event EventHandler<string> BookmarkAdded;
    }
}
