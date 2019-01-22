using System;

namespace MouseNet.Logophi.Views
{
    public interface IView : IDisposable
    {
        void Close();
        void Show();
        
        void Show(object parent);

        event EventHandler<ViewEventArgs> ViewEventActivated;
    }
}