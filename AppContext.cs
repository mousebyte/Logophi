using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi
{
    /// <inheritdoc />
    /// <summary>
    /// The Logophi application context. Sets up the application environment
    /// and handles various application-wide tasks.
    /// </summary>
    internal class AppContext : ApplicationContext
    {
        private readonly PresentationAgent _agent;
        private readonly GlobalHotkey _hotkey = new GlobalHotkey();
        private readonly Settings _settings = Settings.Default;
        private readonly Browser _thesaurus;
        private readonly NotifyIcon _trayIcon;
        private Keys _registeredHotkey = Keys.None;

        public AppContext()
            {
            Application.ApplicationExit += OnApplicationExit;
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            SetupDirectories();
            
            //set up the tray icon
            var openMenuItem = new ToolStripMenuItem {Text = @"Open"};
            openMenuItem.Click += OnOpen;
            var exitMenuItem = new ToolStripMenuItem {Text = @"Exit"};
            exitMenuItem.Click +=
                (sender,
                 args) => Application.Exit();
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
            
            //initialize the hotkey feature
            _hotkey.HotkeyPressed += OnHotkeyPressed;
            RegisterHotkey();
            
            _thesaurus = new Browser(_settings.DataDirectory,
                                     _settings.PersistentCache,
                                     _settings.SaveHistory);
            _thesaurus.History.MaxItems = (int) _settings.MaxHistory;
            _agent = new PresentationAgent(_thesaurus);
            _agent.PreferencesSaved += OnPreferencesSaved;
            Activate();
            }
        
        /// <summary>
        /// Shows the main form.
        /// </summary>
        public void Activate()
            {
            _agent.PresentMainForm();
            }

        /// <inheritdoc />
        protected override void Dispose
            (bool disposing)
            {
            base.Dispose(disposing);
            if (!disposing) return;
            _agent?.Dispose();
            _trayIcon.Visible = false;
            _trayIcon?.Dispose();
            _hotkey?.Dispose();
            }

        /// <summary>
        /// Registers the hotkey stored in settings, or updates
        /// the currently registered hotkey if it is different than
        /// the one stored in settings.
        /// </summary>
        private void RegisterHotkey()
            {
            //return if no hotkey is set, or if the value in settings
            //is the same as the hotkey already registered
            if (_settings.Hotkey == Keys.None
             || _settings.Hotkey == _registeredHotkey)
                return;
            if (_registeredHotkey != Keys.None)
                _hotkey.UnregisterHotkey(0);
            _hotkey.RegisterHotkey(_settings.Hotkey);
            _registeredHotkey = _settings.Hotkey;
            }

        /// <summary>
        /// If necessary, creates the Logophi directory in local appdata
        /// and stores the path in settings.
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

        /// <summary>
        /// Unregisters the global hotkey if one is set.
        /// </summary>
        private void UnregisterHotkey()
            {
            if (_registeredHotkey == Keys.None) return;
            _hotkey.UnregisterHotkey(0);
            _registeredHotkey = Keys.None;
            }

        private void OnApplicationExit
            (object sender,
             EventArgs e)
            {
            _agent.CloseMainForm();
            }

        private void OnHotkeyPressed
            (object sender,
             HotkeyEventArgs e)
            {
            Activate();
            }

        private void OnOpen
            (object sender,
             EventArgs e)
            {
            Activate();
            }

        private void OnPreferencesSaved
            (object sender,
             EventArgs e)
            {
            //update hotkey registration
            if (_settings.EnableHotkey)
                RegisterHotkey();
            else UnregisterHotkey();
            
            //update autorun registry entry
            var key = OpenAutoRunKey();
            if (key == null) return;
            if (_settings.AutoRun)
                key.SetValue(Resources.AppName,
                             Application.ExecutablePath);
            else if (key.GetValue(Resources.AppName) != null)
                key.DeleteValue(Resources.AppName);
            }

        private void OnSettingsPropertyChanged
            (object sender,
             PropertyChangedEventArgs e)
            {
            //update browser property values when corresponding
            //settings values are changed.
            switch (e.PropertyName)
                {
                case "PersistentCache":
                    _thesaurus.PersistentCache =
                        _settings.PersistentCache;
                    break;
                case "SaveHistory":
                    _thesaurus.History.PersistentHistory =
                        _settings.SaveHistory;
                    break;
                case "MaxHistory":
                    _thesaurus.History.MaxItems =
                        (int) _settings.MaxHistory;
                    break;
                }
            }

        /// <summary>
        /// Opens the Logophi autorun registry key.
        /// </summary>
        /// <returns>The <see cref="RegistryKey"/> that allows
        /// logophi to be run at logon.</returns>
        private static RegistryKey OpenAutoRunKey()
            {
            return Registry.CurrentUser.OpenSubKey(
                Resources.AutoRunKey,
                true);
            }
    }
}