using System;

namespace MouseNet.Logophi.Views
{
    
    
    public interface IViewPresenter<TView> : IDisposable
    {
        TView View { get; }
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