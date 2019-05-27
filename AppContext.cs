using System;
using System.IO;
using System.Windows.Forms;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi {
    /// <inheritdoc />
    /// <summary>
    ///     The Logophi application context. Sets up the application environment
    ///     and handles various application-wide tasks.
    /// </summary>
    internal class AppContext : ApplicationContext {
        private readonly PresentationAgent _agent;
        private readonly Settings _settings = Settings.Default;
        private readonly Browser _browser;
        private readonly NotifyIcon _trayIcon;
        private readonly SettingsHelper _settingsHelper;
        private readonly Logophi _logophi;

        public AppContext()
            {
            Application.ApplicationExit += OnApplicationExit;
            SetupDirectories();
            //set up the tray icon
            var openMenuItem = new ToolStripMenuItem {Text = @"Open"};
            openMenuItem.Click += OnOpen;
            var exitMenuItem = new ToolStripMenuItem {Text = @"Exit"};
            exitMenuItem.Click +=
            (sender,
             args) => Application.Exit();
            _logophi = new Logophi();
            _logophi.TrayIcon.ContextMenuStrip = new ContextMenuStrip
                {
                Items = {openMenuItem, exitMenuItem}
                };
            _trayIcon = new NotifyIcon
                {
                Icon = Resources.logophi,
                Text = Resources.AppName,
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip
                    {
                    Items = {openMenuItem, exitMenuItem}
                    }
                };
            _trayIcon.DoubleClick += OnOpen;

            _browser = new Browser(
                _settings.DataDirectory,
                _settings.PersistentCache,
                _settings.SaveHistory);
            _browser.History.MaxItems = (int) _settings.MaxHistory;

            _settingsHelper = new SettingsHelper(_settings, _browser);
            _settingsHelper.Hotkey.HotkeyPressed += OnHotkeyPressed;

            _agent = new PresentationAgent(
                _browser,
                Application.Exit,
                PresentBookmarksForm,
                PresentPreferencesForm,
                PresentAboutDialog,
                DeleteCache,
                DeleteHistory);
            PresentMainForm();
            }

        private void DeleteCache()
            {
            _browser.ClearCache();
            }

        private void DeleteHistory()
            {
            _browser.History.Clear();
            }

        /// <summary>
        /// Closes the Logophi main window.
        /// </summary>
        private void CloseMainForm()
            {
            if (_agent.Main.IsPresenting)
                _agent.Main.View.Close();
            }

        /// <summary>
        /// Presents the about window to the user.
        /// </summary>
        private void PresentAboutDialog()
            {
            var dialog = new AboutForm();
            dialog.ShowDialog((IWin32Window) _agent.Main.View);
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
                form.ExitClicked += (sender, args) => Application.Exit();
                form.ShowAboutClicked += (sender, args) => PresentAboutDialog();
                form.ShowBookmarksClicked +=
                    (sender, args) => PresentBookmarksForm();
                form.ShowPreferencesClicked +=
                    (sender, args) => PresentPreferencesForm();
                _logophi.Main.Present(form);
                }
            else _logophi.Main.View.ToFront();
            }

        /// <summary>
        /// Presents the preferences window to the user.
        /// </summary>
        private void PresentPreferencesForm()
            {
            var dialog = new PreferencesForm();

            var accept =
                _agent.Preferences.PresentDialog(
                    dialog,
                    _agent.Main.View);
            if (accept) _settingsHelper.UpdatePreferences();
            else _settings.Reload();
            }

        /// <inheritdoc />
        protected override void Dispose
            (bool disposing)
            {
            base.Dispose(disposing);
            if (!disposing) return;
            _agent.Dispose();
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            _settingsHelper.Dispose();
            _browser.Dispose();
            }

        /// <summary>
        ///     If necessary, creates the Logophi directory in local appdata
        ///     and stores the path in settings.
        /// </summary>
        private void SetupDirectories()
            {
            //make sure the DataDirectory setting is set
            if (_settings.DataDirectory == string.Empty)
                {
                _settings.DataDirectory = Path.Combine(
                    Environment.GetFolderPath(
                        Environment
                            .SpecialFolder.LocalApplicationData),
                    Resources.AppName);
                _settings.Save();
                }

            //create the data directory if necessary
            if (!Directory.Exists(_settings.DataDirectory))
                Directory.CreateDirectory(_settings.DataDirectory);
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
            PresentMainForm();
            }

        private void OnOpen
        (object sender,
         EventArgs e)
            {
            PresentMainForm();
            }
    }
}