using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi
{
    internal class SettingsHelper : IDisposable
    {
        private readonly Browser _browser;
        private readonly Settings _settings;
        private Keys _registeredHotkey = Keys.None;

        public SettingsHelper
            (Settings settings,
             Browser browser)
            {
            _settings = settings;
            _browser = browser;
            _settings.PropertyChanged += OnSettingsPropertyChanged;
            var key = OpenAutoRunKey();
            if (key == null || !_settings.AutoRun || key.ValueCount == 0)
                return;
            _settings.AutoRun = true;
            key.Dispose();
            UpdatePreferences();
            }

        public GlobalHotkey Hotkey { get; } = new GlobalHotkey();

        public void Dispose()
            {
            Hotkey.Dispose();
            }

        public void UpdatePreferences()
            {
            _settings.Save();
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

        /// <summary>
        ///     Registers the hotkey stored in settings, or updates
        ///     the currently registered hotkey if it is different than
        ///     the one stored in settings.
        /// </summary>
        private void RegisterHotkey()
            {
            //return if no hotkey is set, or if the value in settings
            //is the same as the hotkey already registered
            if (_settings.Hotkey == Keys.None
             || _settings.Hotkey == _registeredHotkey)
                return;
            if (_registeredHotkey != Keys.None)
                Hotkey.UnregisterHotkey(0);
            Hotkey.RegisterHotkey(_settings.Hotkey);
            _registeredHotkey = _settings.Hotkey;
            }

        /// <summary>
        ///     Unregisters the global hotkey if one is set.
        /// </summary>
        private void UnregisterHotkey()
            {
            if (_registeredHotkey == Keys.None) return;
            Hotkey.UnregisterHotkey(0);
            _registeredHotkey = Keys.None;
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
                    _browser.PersistentCache =
                        _settings.PersistentCache;
                    break;
                case "SaveHistory":
                    _browser.History.PersistentHistory =
                        _settings.SaveHistory;
                    break;
                case "MaxHistory":
                    _browser.History.MaxItems =
                        (int) _settings.MaxHistory;
                    break;
                }
            }

        /// <summary>
        ///     Opens the Logophi autorun registry key.
        /// </summary>
        /// <returns>
        ///     The <see cref="RegistryKey" /> that allows
        ///     logophi to be run at logon.
        /// </returns>
        private static RegistryKey OpenAutoRunKey()
            {
            return Registry.CurrentUser.OpenSubKey(
                Resources.AutoRunKey,
                true);
            }
    }
}