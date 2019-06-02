using System;
using System.Windows.Forms;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi {
    /// <inheritdoc />
    /// <summary>
    ///     The Logophi application context. Sets up the application environment
    ///     and handles various application-wide tasks.
    /// </summary>
    internal class AppContext : ApplicationContext {
        private readonly Logophi _logophi;
        private readonly Settings _settings = Settings.Default;

        private readonly NotifyIcon _trayIcon = new NotifyIcon
            {
            Icon = Resources.logophi,
            Text = Resources.AppName,
            Visible = true,
            };

        public AppContext()
            {
            Application.ApplicationExit += OnApplicationExit;
            _logophi = new Logophi(_settings);
            //set up the tray icon
            var openMenuItem = new ToolStripMenuItem {Text = @"Open"};
            openMenuItem.Click += OnOpen;
            var exitMenuItem = new ToolStripMenuItem {Text = @"Exit"};
            exitMenuItem.Click +=
            (sender,
             args) => Application.Exit();
            _trayIcon.ContextMenuStrip = new ContextMenuStrip
                {
                Items = {openMenuItem, exitMenuItem}
                };
            _trayIcon.DoubleClick += OnOpen;
            _logophi.SettingsHelper.Hotkey.HotkeyPressed += OnHotkeyPressed;
            _logophi.SettingsHelper.UpdatePreferences();
            PresentMainForm();
            }


        /// <summary>
        /// Closes the Logophi main window.
        /// </summary>
        private void CloseMainForm()
            {
            if (_logophi.Main.IsPresenting)
                _logophi.Main.View.Close();
            }

        /// <summary>
        /// Presents the about window to the user.
        /// </summary>
        private void PresentAboutDialog()
            {
            var dialog = new AboutForm();
            dialog.ShowDialog((IWin32Window) _logophi.Main.View);
            dialog.Dispose();
            }

        /// <summary>
        /// Presents the bookmarks window to the user.
        /// </summary>
        private void PresentBookmarksForm()
            {
            _logophi.Bookmarks.Present(new BookmarksForm(), _logophi.Main.View);
            }

        /// <summary>
        /// Presents the Logophi main window to the user, or
        /// brings it to the front if it is already open.
        /// </summary>
        public void PresentMainForm()
            {
            if (!_logophi.Main.IsPresenting)
                {
                var form = new MainForm();
                ConnectMainFormEvents(form);
                _logophi.Main.Present(form);
                }
            else _logophi.Main.View.ToFront();
            }

        private void ConnectMainFormEvents(MainForm form)
            {
            form.ExitClicked += (sender, args) => Application.Exit();
            form.ShowAboutClicked += (sender, args) => PresentAboutDialog();
            form.ShowBookmarksClicked +=
                (sender, args) => PresentBookmarksForm();
            form.ShowPreferencesClicked +=
                (sender, args) => PresentPreferencesForm();
            }

        /// <summary>
        /// Presents the preferences window to the user.
        /// </summary>
        private void PresentPreferencesForm()
            {
            var dialog = new PreferencesForm();
            var accept =
                _logophi.Preferences.PresentDialog(
                    dialog,
                    _logophi.Main.View);
            if (accept) _logophi.SettingsHelper.UpdatePreferences();
            else _logophi.SettingsHelper.ReloadPreferences();
            dialog.Dispose();
            }

        /// <inheritdoc />
        protected override void Dispose
            (bool disposing)
            {
            base.Dispose(disposing);
            if (!disposing) return;
            _logophi.Dispose();
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            }

        private void OnApplicationExit
        (object sender,
         EventArgs e)
            {
            CloseMainForm();
            }

        private void OnHotkeyPressed
        (object sender,
         HotkeyEventArgs e)
            {
            if (_settings.UseQuckSearch) PresentQuickSearchForm();
            else PresentMainForm();
            }

        private void PresentQuickSearchForm()
            {
            if (_logophi.QuickSearch.IsPresenting) return;

            var form = new QuickSearchForm();

            form.TermListClick += (sender, args) =>
                                      {
                                      var text = form.SearchText;
                                      form.Close();
                                      if (!_logophi.Main.IsPresenting)
                                          PresentMainForm();
                                      _logophi.Main.View.SearchText = text;
                                      };
            _logophi.QuickSearch.Present(form);
            }

        private void OnOpen
        (object sender,
         EventArgs e)
            {
            PresentMainForm();
            }
    }
}