using System;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class PreferencesDialogPresenter
        : IViewPresenter<IPreferencesDialogView>
    {
        private IPreferencesDialogView _view;
        public IView View => _view;
        public bool IsPresenting { get; private set; }

        public void Present
            (IPreferencesDialogView view)
            {
            Present(view, null);
            }

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

        public event EventHandler DeleteCacheClicked;
        public event EventHandler DeleteHistoryClicked;
    }
}