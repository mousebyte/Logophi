using System;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Views;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi {
    internal class Logophi : IDisposable {
        private readonly Settings _settings = Settings.Default;
        private readonly Browser _browser;
        private readonly MainFormPresenter _mainFormPresenter;
        public SettingsHelper SettingsHelper { get; }

        public NotifyIcon TrayIcon { get; } = new NotifyIcon
            {
            Icon = Resources.logophi,
            Text = Resources.AppName,
            Visible = true,
            };


        public IViewPresenter<IMainFormView> Main => _mainFormPresenter;
        public IViewPresenter<IBookmarksFormView> Bookmarks { get; }
        public IViewPresenter<IPreferencesDialogView> Preferences { get; }

        public Logophi()
            {
            _browser = new Browser(
                _settings.DataDirectory,
                _settings.PersistentCache,
                _settings.SaveHistory);
            _browser.History.MaxItems = (int) _settings.MaxHistory;
            SettingsHelper = new SettingsHelper(_settings, _browser);
            _mainFormPresenter = new MainFormPresenter(_browser);
            Bookmarks = new BookmarksFormPresenter(_browser);
            Bookmarks.ViewPresented += OnBookmarksViewPresented;
            Preferences = new PreferencesDialogPresenter();
            Preferences.ViewPresented += OnPreferencesDialogPresented;
            }

        private void OnBookmarksViewPresented(object sender, EventArgs args)
            {
            Bookmarks.View.BookmarkActivated += OnBookmarkActivated;
            }

        private void OnDeleteCache(object sender, EventArgs args)
            {
            _browser.ClearCache();
            }

        private void OnDeleteHistory(object sender, EventArgs args)
            {
            _browser.History.Clear();
            }

        private void OnBookmarkActivated(object sender, string args)
            {
            _mainFormPresenter.Search(args);
            }

        private void OnPreferencesDialogPresented(object sender, EventArgs args)
            {
            Preferences.View.DeleteHistoryClicked += OnDeleteHistory;
            Preferences.View.DeleteCacheClicked += OnDeleteCache;
            }

        public void Dispose()
            {
            _browser.Dispose();
            SettingsHelper.Dispose();
            _mainFormPresenter.Dispose();
            TrayIcon.Visible = false;
            TrayIcon.Dispose();
            Bookmarks.Dispose();
            Preferences.Dispose();
            }
    }
}