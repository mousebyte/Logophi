using System;

namespace MouseNet.Logophi.Views
{
    public interface IView : IDisposable
    {
        event EventHandler Closed; 
        void Close();
        
        void Present(object parent = null);

        bool PresentDialog
            (object parent);

        event EventHandler<ViewEventArgs> ViewEventActivated;
    }
}