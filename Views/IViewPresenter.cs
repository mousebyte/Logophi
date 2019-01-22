using System;

namespace MouseNet.Logophi.Views
{
    public interface IViewPresenter : IDisposable
    {
        IView View { get; }
        bool IsPresenting { get; }
    }
    
    public interface IViewPresenter<in TView> : IViewPresenter
    {
        void Present
            (TView view);

        void Present
            (TView view,
             object parent);
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