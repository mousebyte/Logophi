using System;
using System.Drawing;

namespace MouseNet.Logophi.Views {
    /// <inheritdoc />
    /// <summary>
    ///     Exposes a view, which can be presented by an <see cref="IViewPresenter{TView}" />.
    /// </summary>
    public interface IView : IDisposable {
        /// <summary>
        ///     Occurs when the view is closed.
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        ///     Closes the view.
        /// </summary>
        void Close();

        /// <summary>
        ///     Presents the view to the user.
        /// </summary>
        /// <param name="parent">An optional parent of the view.</param>
        void Present(object parent = null);

        /// <summary>
        ///     Presents the view to the user as a dialog.
        /// </summary>
        /// <param name="parent">The parent of the view.</param>
        /// <returns>A value indicating the result of the dialog.</returns>
        bool PresentModal(object parent);

        Point Location { get; set; }
        Size Size { get; set; }
    }
}