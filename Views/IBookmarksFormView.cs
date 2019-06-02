using System;
using System.Collections;

namespace MouseNet.Logophi.Views {
    /// <inheritdoc />
    /// <summary>
    ///     Exposes a bookmarks form view.
    /// </summary>
    public interface IBookmarksFormView : IView {
        /// <summary>
        ///     Occurs when a bookmark is activated.
        /// </summary>
        event EventHandler<string> BookmarkActivated;

        /// <summary>
        ///     Occurs when a bookmark is removed.
        /// </summary>
        event EventHandler<string> BookmarkRemoved;

        /// <summary>
        ///     Gets the list of bookmarks displayed in the view.
        /// </summary>
        IList Items { get; }
    }
}