using System;

namespace MouseNet.Logophi.Views {
    /// <inheritdoc />
    /// <summary>
    ///     Presents an <see cref="IView" />.
    /// </summary>
    /// <typeparam name="TView">The type of view.</typeparam>
    public interface IViewPresenter<TView> : IDisposable where TView : IView {
        event EventHandler ViewPresented;

        bool IsPresenting { get; }

        /// <summary>
        ///     Presents a <see cref="TView" />.
        /// </summary>
        /// <param name="view">The view to present.</param>
        void Present(TView view);

        /// <summary>
        ///     Presents a <see cref="TView" /> using the given object as
        ///     its parent.
        /// </summary>
        /// <param name="view">The view to present.</param>
        /// <param name="parent">The parent to use for the view.</param>
        void Present(TView view, object parent);

        /// <summary>
        ///     Presents a <see cref="TView" /> as a modal dialog using the
        ///     given object as its parent.
        /// </summary>
        /// <param name="view">The view to present.</param>
        /// <param name="parent">The parent to use for the view.</param>
        /// <returns>The dialog result.</returns>
        bool PresentDialog(TView view, object parent);

        /// <summary>
        ///     The view currently being presented.
        /// </summary>
        TView View { get; }
    }
}