using System;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    ///     Presents an <see cref="T:MouseNet.Logophi.Views.IPreferencesDialogView" />.
    /// </summary>
    internal class PreferencesDialogPresenter
        : ViewPresenter<IPreferencesDialogView>
    {
        private readonly EventHandler _onDeleteCache;
        private readonly EventHandler _onDeleteHistory;

        /// <summary>
        ///     Creates a new instance of the <see cref="PreferencesDialogPresenter" /> class.
        /// </summary>
        /// <param name="deleteCacheAction">The action to take to delete the cache.</param>
        /// <param name="deleteHistoryAction">The action to take to delete the history.</param>
        public PreferencesDialogPresenter
            (Action deleteCacheAction,
             Action deleteHistoryAction)
            {
            _onDeleteCache = deleteCacheAction.ToHandler();
            _onDeleteHistory = deleteHistoryAction.ToHandler();
            }

        protected override void InitializeView()
            {
            View.DeleteCacheClicked += _onDeleteCache;
            View.DeleteHistoryClicked += _onDeleteHistory;
            }
    }
}