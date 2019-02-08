using System;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Views.Presentation
{
    /// <inheritdoc />
    /// <summary>
    /// Presents an <see cref="T:MouseNet.Logophi.Views.IPreferencesDialogView" />.
    /// </summary>
    internal class PreferencesDialogPresenter
        : IViewPresenter<IPreferencesDialogView>
    {
        private IPreferencesDialogView _view;
        public IPreferencesDialogView View => _view;
        /// <summary>
        /// Gets a value indicating whether or not the view is being presented to the user.
        /// </summary>
        public bool IsPresenting { get; private set; }

        /// <inheritdoc />
        public void Present
            (IPreferencesDialogView view)
            {
            Present(view, null);
            }

        /// <inheritdoc />
        public void Present
            (IPreferencesDialogView view,
             object parent)
            {
            _view = view;
            view.ViewEventActivated += OnViewEventActivated;
            IsPresenting = true;
            var result = view.ShowDialog((IWin32Window) parent);
            if (result != DialogResult.OK) Settings.Default.Reload();
            else Settings.Default.Save();
            }

        public void Dispose()
            {
            _view?.Dispose();
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