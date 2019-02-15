using System;

namespace MouseNet.Logophi.Views
{
    /// <summary>
    /// Exposes a view, which can be presented by an <see cref="IViewPresenter{TView}"/>.
    /// </summary>
    public interface IView : IDisposable
    {
        event EventHandler Closed; 
        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();
        
        void Present(object parent = null);

        bool PresentDialog
            (object parent);

        //TODO: Depricate ViewEventActivated, because it's silly.
        /// <summary>
        /// Occurs when a view sends a message to its presenter.
        /// </summary>
        event EventHandler<ViewEventArgs> ViewEventActivated;
    }
}