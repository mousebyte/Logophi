using System;
using System.Collections;
using System.Windows.Forms;

namespace MouseNet.Logophi.Views
{
    public interface IBookmarksFormView : IView<IWin32Window>
    {
        IList Items { get; }
        event EventHandler Closed;
        event EventHandler<string> BookmarkRemoved;
        event EventHandler<string> BookmarkActivated;
    }
}