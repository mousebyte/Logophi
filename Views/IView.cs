using System;

namespace MouseNet.Logophi.Views
{
    /// <inheritdoc />
    /// <summary>
    ///     Exposes a view, which can be presented by an <see cref="IViewPresenter{TView}" />.
    /// </summary>
    public interface IView : IDisposable
    {
        /// <summary>
        ///     Closes the view.
        /// </summary>
        void Close();

        /// <summary>
        ///     Shows the view.
        /// </summary>
        void Show();

        /// <summary>
        ///     Shows the view using the given object as its parent.
        /// </summary>
        /// <param name="parent">The parent of the view.</param>
        void Show
            (object parent);

        //TODO: Depricate ViewEventActivated, because it's silly.
        /// <summary>
        ///     Occurs when a view sends a message to its presenter.
        /// </summary>
        event EventHandler<ViewEventArgs> ViewEventActivated;
    }
}