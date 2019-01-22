using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi
{
    internal class AppContext : ApplicationContext
    {
        private const string RegistryKey =
            "Software\\Microsoft\\Windows\\CurrentVersion\\Run";

        private readonly Settings _settings = Settings.Default;
        private readonly BookmarksFormPresenter _bkmarkPresenter;
        private readonly MainFormPresenter _mainPresenter;
        private readonly NotifyIcon _trayIcon;
        private readonly GlobalHotkey _hotkey = new GlobalHotkey();

        private readonly string _exePath =
            Path.GetFullPath("Logophi.exe");

        public AppContext()
            {
            Application.ApplicationExit += OnApplicationExit;
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            _mainPresenter = new MainFormPresenter(
                _settings.DataDirectory,
                _settings.PersistentCache,
                _settings.SaveHistory);
            _bkmarkPresenter = new BookmarksFormPresenter(
                _mainPresenter.Thesaurus,
                _mainPresenter.Search);
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
            PresentMainForm();
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

        private void PresentMainForm()
            {
            if (_mainPresenter.IsPresenting) return;
            var form = new MainForm();
            //? Possibly move these event handlers somewhere else
            form.ViewBookmarksClicked += OnViewBookmarksClicked;
            form.GithubProjectClicked += OnGithubProjectClicked;
            form.AboutClicked += OnAboutClicked;
            form.ExitClicked +=
                (sender,
                 args) => Application.Exit();
            _mainPresenter.Present(form);
            }

        private void PresentPreferencesForm()
            {
            if (!_mainPresenter.IsPresenting) return;
            var form = new PreferencesForm();
            var result =
                form.ShowDialog((IWin32Window) _mainPresenter.View);
            }

        private void OnSettingsPropertyChanged
            (object sender,
             PropertyChangedEventArgs e)
            {
            switch (e.PropertyName)
                {
                case "PersistentCache":
                    _mainPresenter.Thesaurus.PersistentCache =
                        _settings.PersistentCache;
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

        private void OnSettingsSaving
            (object sender,
             CancelEventArgs args)
            {
            _mainPresenter.View.TopMost = _settings.AlwaysOnTop;
            }

        private static RegistryKey OpenAutoRunKey() =>
            Registry.CurrentUser.OpenSubKey(RegistryKey, true);

        private void OnAboutClicked
            (object sender,
             EventArgs e)
            {
            var form = new AboutForm();
            form.ShowDialog((IWin32Window) _mainPresenter.View);
            }

        private void OnApplicationExit
            (object sender,
             EventArgs e)
            {
            if (_mainPresenter.IsPresenting)
                _mainPresenter.View.Close();
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            _hotkey.Dispose();
            }

        private void OnGithubProjectClicked
            (object sender,
             EventArgs e)
            {
            Process.Start(Resources.GithubUrl);
            }

        private void OnOpen
            (object sender,
             EventArgs e)
            {
            PresentMainForm();
            }

        private void OnViewBookmarksClicked
            (object sender,
             EventArgs e)
            {
            if (!_mainPresenter.IsPresenting
             || _bkmarkPresenter.IsPresenting) return;
            var form = new BookmarksForm();
            _mainPresenter.Thesaurus.BookmarkRemoved +=
                (o,
                 s) => form.Items.Remove(s);
            _mainPresenter.Thesaurus.BookmarkAdded +=
                (o,
                 s) => form.Items.Add(s);
            _bkmarkPresenter.Present(form);
            }
    }
}