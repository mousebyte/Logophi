using System;

namespace MouseNet.Logophi.Views
{
    
    
    public interface IViewPresenter<TView> : IDisposable where TView : IView
    {
        TView View { get; }
        void Present
            (TView view);

        void Present
            (TView view,
             object parent);

        bool PresentDialog
            (TView view,
             object parent);
        
        bool IsPresenting { get; }
    }

    public class ViewEventArgs : EventArgs
    {
        internal ViewEventArgs
            (object tag)
            {
            Tag = tag;
            }
        
        public object Tag { get; }
    }
}