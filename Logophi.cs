using System;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Views;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi {
    internal class Logophi : IDisposable {
        private readonly Browser _browser;
        private readonly MainFormPresenter _mainFormPresenter;
        public SettingsHelper SettingsHelper { get; }

        public IViewPresenter<IMainFormView> Main => _mainFormPresenter;
        public IViewPresenter<IBookmarksFormView> Bookmarks { get; }
        public IViewPresenter<IPreferencesDialogView> Preferences { get; }
        public IViewPresenter<IQuickSearchFormView> QuickSearch { get; }

        public Logophi(Settings settings)
            {
            _browser = new Browser(
                settings.DataDirectory,
                settings.PersistentCache,
                settings.SaveHistory);
            _browser.History.MaxItems = (int) settings.MaxHistory;
            SettingsHelper = new SettingsHelper(settings, _browser);
            _mainFormPresenter = new MainFormPresenter(_browser);
            Bookmarks = new BookmarksFormPresenter(_browser);
            Bookmarks.ViewPresented += OnBookmarksViewPresented;
            Preferences = new PreferencesDialogPresenter();
            Preferences.ViewPresented += OnPreferencesDialogPresented;
            QuickSearch = new QuickSearchFormPresenter(_browser);
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
            Bookmarks.Dispose();
            Preferences.Dispose();
            }
    }
}