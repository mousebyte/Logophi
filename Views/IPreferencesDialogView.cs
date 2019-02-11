using System.Windows.Forms;

namespace MouseNet.Logophi.Views
{
    /// <inheritdoc />
    /// <summary>
    ///     Exposes a preferences dialog view.
    /// </summary>
    public interface IPreferencesDialogView : IView
    {
        /// <summary>
        ///     Gets or sets a value indicating whether or not the persistent
        ///     cache option is enabled.
        /// </summary>
        bool PersistentCache { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether or not the autorun
        ///     option is enabled.
        /// </summary>
        bool AutoRun { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether or not the always on
        ///     top option is enabled.
        /// </summary>
        bool AlwaysOnTop { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether or not the persistent
        ///     history option is enabled.
        /// </summary>
        bool PersistentHistory { get; set; }
        /// <summary>
        ///     Gets or sets a value representing the maximum number of items
        ///     to keep in history.
        /// </summary>
        decimal MaxHistory { get; set; }

        /// <summary>
        ///     Shows the dialog to the user.
        /// </summary>
        /// <param name="parent">The parent of the dialog.</param>
        /// <returns>The result of the dialog.</returns>
        DialogResult ShowDialog
            (IWin32Window parent);
    }
}