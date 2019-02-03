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
            _hotkey.HotkeyPressed += OnHotkeyPressed;
            RegisterHotkey();
            _thesaurus = new Browser(_settings.DataDirectory,
                                       _settings.PersistentCache,
                                       _settings.SaveHistory);
            _thesaurus.History.MaxItems = (int) _settings.MaxHistory;
            _agent = new PresentationAgent(_thesaurus);
            Activate();
            }

        public void Activate()
            {
            _agent.PresentMainForm();
            }

        private void OnHotkeyPressed
            (object sender,
             HotkeyEventArgs e)
            {
            Activate();
            }

        private void SetupDirectories()
            {
            if (_settings.DataDirectory == string.Empty)
                {
                _settings.DataDirectory = Path.Combine(
                    Environment.GetFolderPath(
                        Environment
                           .SpecialFolder.LocalApplicationData),
                    Resources.AppName);
                _settings.Save();
                }

            if (!Directory.Exists(_settings.DataDirectory))
                Directory.CreateDirectory(_settings.DataDirectory);
            }

        private void OnApplicationExit
            (object sender,
             EventArgs e)
            {
            _agent.CloseMainForm();
            _agent.Dispose();
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            _hotkey.Dispose();
            }

        private void OnOpen
            (object sender,
             EventArgs e)
            {
            Activate();
            }

        private void RegisterHotkey()
            {
            if (_settings.Hotkey == Keys.None
             || _settings.Hotkey == _registeredHotkey)
                return;
            if (_registeredHotkey != Keys.None)
                _hotkey.UnregisterHotkey(0);
            _hotkey.RegisterHotkey(_settings.Hotkey);
            _registeredHotkey = _settings.Hotkey;
            }

        private void UnregisterHotkey()
            {
            if (_registeredHotkey == Keys.None) return;
            _hotkey.UnregisterHotkey(0);
            _registeredHotkey = Keys.None;
            }

        private void OnSettingsPropertyChanged
            (object sender,
             PropertyChangedEventArgs e)
            {
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
                case "EnableHotkey":
                    if (_settings.EnableHotkey)
                        RegisterHotkey();
                    else UnregisterHotkey();
                    break;
                case "AutoRun":
                    var key = OpenAutoRunKey();
                    if (key == null) return;
                    if (_settings.AutoRun)
                        key.SetValue(Resources.AppName, Application.ExecutablePath);
                    else if (key.GetValue(Resources.AppName) != null)
                        key.DeleteValue(Resources.AppName);
                    break;
                }
            }

        private static RegistryKey OpenAutoRunKey() =>
            Registry.CurrentUser.OpenSubKey(
                Resources.AutoRunKey,
                true);
    }
}