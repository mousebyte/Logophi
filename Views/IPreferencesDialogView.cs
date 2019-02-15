using System;

namespace MouseNet.Logophi.Views
{
    /// <inheritdoc />
    /// <summary>
    /// Exposes a preferences dialog view.
    /// </summary>
    public interface IPreferencesDialogView : 
        IView
    {
        /// <summary>
        /// Occurs when the delete cache button is clicked.
        /// </summary>
        event EventHandler DeleteCacheClicked;
        /// <summary>
        /// Occurs when the delete history button is clicked.
        /// </summary>
        event EventHandler DeleteHistoryClicked;
    }
}
