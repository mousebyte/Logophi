using System;
using System.Collections;

namespace MouseNet.Logophi.Views
{
    /// <inheritdoc />
    /// <summary>
    ///     Exposes a bookmarks form view.
    /// </summary>
    public interface IBookmarksFormView : IView
    {
        /// <summary>
        ///     Gets the list of bookmarks displayed in the view.
        /// </summary>
        IList Items { get; }
        /// <summary>
        ///     Occurs when the view is closed.
        /// </summary>
        event EventHandler Closed;
        /// <summary>
        ///     Occurs when a bookmark is removed.
        /// </summary>
        event EventHandler<string> BookmarkRemoved;
    }
}