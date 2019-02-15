using System;

namespace MouseNet.Logophi.Views
{
    
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IView" />.
    /// </summary>
    /// <typeparam name="TView">The type of view.</typeparam>
    public interface IViewPresenter<TView> : IDisposable
    {
        /// <summary>
        /// The view currently being presented.
        /// </summary>
        TView View { get; }
        
        /// <summary>
        /// Presents a <see cref="TView"/>.
        /// </summary>
        /// <param name="view">The view to present.</param>
        void Present
            (TView view);

        /// <summary>
        /// Presents a <see cref="TView"/> using the given object as
        /// its parent.
        /// </summary>
        /// <param name="view">The view to present.</param>
        /// <param name="parent">The parent to use for the view.</param>
        void Present
            (TView view,
             object parent);

        bool PresentDialog
            (TView view,
             object parent);
        
        bool IsPresenting { get; }
    }
}