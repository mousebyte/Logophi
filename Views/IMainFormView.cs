using System;
using System.Collections;
using System.Windows.Forms;

namespace MouseNet.Logophi.Views
{
    public interface IMainFormView : IView<IWin32Window>
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
        bool TopMost { get; set; }

        void SetBookmarkState
            (bool bookmarked);

        event EventHandler Closed;
        event EventHandler<string> Search;
        event EventHandler BackClicked;
        event EventHandler ForwardClicked;
        event EventHandler BookmarkClicked;
        event EventHandler ViewDictionaryClicked;
        event EventHandler<int> SelectedDefinitionChanged;
        /*TODO:
         remove the following event after it's no longer
         subscribed to in the presenter class
        */
        event EventHandler PreferencesClicked;
    }
}