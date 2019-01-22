using System;
using System.Collections;

namespace MouseNet.Logophi.Views
{
    public interface IBookmarksFormView : IView
    {
        IList Items { get; }
        event EventHandler Closed;
        event EventHandler<string> BookmarkRemoved;
    }
}