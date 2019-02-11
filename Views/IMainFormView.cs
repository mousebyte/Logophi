using System;
using System.Collections;

namespace MouseNet.Logophi.Views
{
    /// <inheritdoc />
    /// <summary>
    ///     Exposes a main form view.
    /// </summary>
    public interface IMainFormView : IView
    {
        /// <summary>
        ///     Gets the list of definitions displayed in the main window.
        /// </summary>
        IList Definitions { get; }
        /// <summary>
        ///     Gets the items in the search box's drop down list.
        /// </summary>
        IList DropDownItems { get; }
        /// <summary>
        ///     Gets or sets the text in the search box.
        /// </summary>
        string SearchText { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether or not the back
        ///     button is enabled.
        /// </summary>
        bool EnableBackButton { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether or not the forward
        ///     button is enabled.
        /// </summary>
        bool EnableForwardButton { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether or not the bookmark
        ///     button is enabled.
        /// </summary>
        bool EnableBookmarkButton { get; set; }
        /// <summary>
        ///     Gets or sets the index of the currently selected definition.
        /// </summary>
        int SelectedDefinitionIndex { get; set; }

        /// <summary>
        ///     Adds a term to the list of antonyms.
        /// </summary>
        /// <param name="term">The word to add to the list.</param>
        /// <param name="similarity">The similarity value of the antonym.</param>
        void AddAntonym
            (string term,
             int similarity);

        /// <summary>
        ///     Adds a term to the list of synonyms.
        /// </summary>
        /// <param name="term">The word to add to the list.</param>
        /// <param name="similarity">The similarity value of the synonym.</param>
        void AddSynonym
            (string term,
             int similarity);

        /// <summary>
        ///     Clears the list of antonyms.
        /// </summary>
        void ClearAntonyms();

        /// <summary>
        ///     Clears the list of synonyms.
        /// </summary>
        void ClearSynonyms();

        /// <summary>
        ///     Brings the view to the front.
        /// </summary>
        void ToFront();

        /// <summary>
        ///     Toggles the bookmark buttons to the off state, indicating
        ///     that the current search term is not bookmarked.
        /// </summary>
        void BookmarkOff();

        /// <summary>
        ///     Toggles the bookmark buttons to the on state, indicating
        ///     that the current search term is bookmarked.
        /// </summary>
        void BookmarkOn();

        /// <summary>
        ///     Occurs when the view is closed.
        /// </summary>
        event EventHandler Closed;
        /// <summary>
        ///     Occurs when a search is initiated.
        /// </summary>
        event EventHandler<string> Search;
        /// <summary>
        ///     Occurs when the back button is clicked.
        /// </summary>
        event EventHandler BackClicked;
        /// <summary>
        ///     Occurs when the forward button is clicked.
        /// </summary>
        event EventHandler ForwardClicked;
        /// <summary>
        ///     Occurs when the bookmark button is clicked.
        /// </summary>
        event EventHandler BookmarkClicked;
        /// <summary>
        ///     Occurs when the open dictionary button is clicked.
        /// </summary>
        event EventHandler OpenDictionaryClicked;
        /// <summary>
        ///     Occurs when the open github button is clicked.
        /// </summary>
        event EventHandler OpenGithubClicked;
        /// <summary>
        ///     Occurs when the selected definition is changed.
        /// </summary>
        event EventHandler<int> SelectedDefinitionChanged;
    }
}