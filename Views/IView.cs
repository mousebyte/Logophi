using System;

namespace MouseNet.Logophi.Views
{
    /// <inheritdoc />
    /// <summary>
    /// Exposes a view, which can be presented by an <see cref="T:MouseNet.Logophi.Views.IViewPresenter`1" />.
    /// </summary>
    public interface IView : IDisposable
    {
        event EventHandler Closed; 
        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();
        
        /// <summary>
        /// Presents the view to the user.
        /// </summary>
        /// <param name="parent">An optional parent of the view.</param>
        void Present(object parent = null);

        /// <summary>
        /// Presents the view to the user as a dialog.
        /// </summary>
        /// <param name="parent">The parent of the view.</param>
        /// <returns>A value indicating the result of the dialog.</returns>
        bool PresentDialog
            (object parent);
    }
}