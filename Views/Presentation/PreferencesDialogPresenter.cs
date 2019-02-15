using System;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IPreferencesDialogView" />.
    /// </summary>
    internal class PreferencesDialogPresenter
        : ViewPresenter<IPreferencesDialogView>
    {
        protected override void InitializeView()
        {
            View.ViewEventActivated += OnViewEventActivated;
        }

        private void OnViewEventActivated
            (object sender,
             ViewEventArgs e)
            {
            switch (e.Tag)
                {
                case "DeleteCacheClicked":
                    DeleteCacheClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "DeleteHistoryClicked":
                    DeleteHistoryClicked?.Invoke(
                        this,
                        EventArgs.Empty);
                    break;
                }
            }

        /// <summary>
        /// Occurs when the delete cache button is clicked.
        /// </summary>
        public event EventHandler DeleteCacheClicked;
        /// <summary>
        /// Occurs when the delete history button is clicked.
        /// </summary>
        public event EventHandler DeleteHistoryClicked;
    }
}