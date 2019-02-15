using System;

namespace MouseNet.Logophi.Views.Presentation
{
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

        public event EventHandler DeleteCacheClicked;
        public event EventHandler DeleteHistoryClicked;
    }
}