using System;
using System.Collections;

namespace MouseNet.Logophi.Views
{
    public interface IBookmarksFormView
    {
        IList Items { get; }
        void Show();
        event EventHandler<string> BookmarkRemoved;
        event EventHandler<string> BookmarkActivated;
    }
}