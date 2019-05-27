using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Views;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi {
    internal class Logophi {
        private readonly Settings _settings = Settings.Default;
        private readonly Browser _browser;
        private readonly SettingsHelper _settingsHelper;
        private readonly MainFormPresenter _mainFormPresenter;

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
            _settingsHelper = new SettingsHelper(_settings, _browser);
            _mainFormPresenter = new MainFormPresenter(_browser);
            Bookmarks = new BookmarksFormPresenter(_browser);
            Bookmarks.ViewPresented += OnBookmarksViewPresented;
            }

        private void OnBookmarksViewPresented(object sender, EventArgs args)
            {
            Bookmarks.View.BookmarkActivated +=
                (o, s) => _mainFormPresenter.Search(s);
            }
    }
}