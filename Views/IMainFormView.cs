using System;
using System.Collections;

namespace MouseNet.Logophi.Views
{
    public interface IMainFormView
    {
        IList Definitions { get; }
        IList Synonyms { get; }
        IList Antonyms { get; }
        IList DropDownItems { get; }
        string SearchText { get; set; }
        bool EnableBackButton { get; set; }
        bool EnableForwardButton { get; set; }
        bool EnableBookmarkButton { get; set; }
        int SelectedDefinitionIndex { get; set; }

        void SetBookmarkState
            (bool bookmarked);

        void Show();
        event EventHandler<string> Search;
        event EventHandler BackClicked;
        event EventHandler ForwardClicked;
        event EventHandler BookmarkClicked;
        event EventHandler<int> SelectedDefinitionChanged;
    }
}