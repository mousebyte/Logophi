using System;
using System.Collections;

namespace MouseNet.Logophi.Views
{
    public interface IMainFormView : IView
    {
        IList Definitions { get; }
        IList DropDownItems { get; }
        string SearchText { get; set; }
        bool EnableBackButton { get; set; }
        bool EnableForwardButton { get; set; }
        bool EnableBookmarkButton { get; set; }
        int SelectedDefinitionIndex { get; set; }

        void AddSynonym
            (string term,
             int similarity);

        void AddAntonym
            (string term,
             int similarity);

        void BookmarkOn();
        void BookmarkOff();
        void ClearSynonyms();
        void ClearAntonyms();
        void ToFront();

        event EventHandler Closed;
        event EventHandler<string> Search;
        event EventHandler BackClicked;
        event EventHandler ForwardClicked;
        event EventHandler BookmarkClicked;
        event EventHandler OpenDictionaryClicked;
        event EventHandler OpenGithubClicked;
        event EventHandler<int> SelectedDefinitionChanged;
    }
}