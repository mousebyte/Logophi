﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi
{
    internal class AppContext : ApplicationContext
    {
        private readonly PresentationAgent _agent;

        private readonly string _exePath =
            Path.GetFullPath("Logophi.exe");

        private readonly GlobalHotkey _hotkey = new GlobalHotkey();
        private readonly Settings _settings = Settings.Default;
        private readonly Thesaurus _thesaurus;
        private readonly NotifyIcon _trayIcon;

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
            _thesaurus = new Thesaurus(_settings.DataDirectory,
                                       _settings.PersistentCache,
                                       _settings.SaveHistory);
            _agent = new PresentationAgent(_thesaurus);
            _agent.PresentMainForm();
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
            _agent.PresentMainForm();
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
                    if (_hotkey.HotkeyCount > 0)
                        _hotkey.UnregisterHotkey(0);
                    if (_settings.EnableHotkey
                     && _settings.Hotkey != Keys.None)
                        _hotkey.RegisterHotkey(_settings.Hotkey);
                    return;
                case "AutoRun":
                    var key = OpenAutoRunKey();
                    if (key == null) return;
                    if (_settings.AutoRun)
                        key.SetValue(Resources.AppName, _exePath);
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