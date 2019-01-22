using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace MouseNet.Logophi
{
    public class GlobalHotkey : IDisposable
    {
        private const int HotkeyMsg = 0x0312;
        private readonly Form _form;
        private int _currentId;
        private readonly IList<Keys> _hotkeys = new List<Keys>();

        public GlobalHotkey()
            {
            _form = new DummyForm(this);
            }

        public IEnumerable<Keys> Hotkeys => _hotkeys;

        public void Dispose()
            {
            for (var i = 0; i < _currentId; i++)
                UnregisterHotkey(i);
            _form?.Dispose();
            }

        public int RegisterHotkey
            (Keys hotkey)
            {
            var modifiers =
                (int) hotkey.GetModifiers().ToModifierKeys();
            var keycode = (int) hotkey.GetKeyCode();
            if (!NativeMethods.RegisterHotKey(_form.Handle,
                                              _currentId++,
                                              modifiers,
                                              keycode))
                throw new InvalidOperationException(
                    "Failed to register hotkey.");
            _hotkeys.Add(hotkey);
            return _currentId;
            }

        public void UnregisterHotkey
            (int id)
            {
            NativeMethods.UnregisterHotKey(_form.Handle, id);
            _hotkeys.RemoveAt(id);
            }

        private void InvokeHotkeyPressed
            (object sender,
             HotkeyEventArgs args)
            {
            HotkeyPressed?.Invoke(sender, args);
            }

        public event EventHandler<HotkeyEventArgs> HotkeyPressed;

        private class DummyForm : Form
        {
            private readonly GlobalHotkey _parent;

            public DummyForm
                (GlobalHotkey parent)
                {
                _parent = parent;
                }

            protected override void WndProc
                (ref Message m)
                {
                base.WndProc(ref m);

                if (m.Msg != HotkeyMsg) return;


                var hotkey = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
                var modifiers =
                    (ModifierKeys) ((int) m.LParam & 0xFFFF);
                _parent.InvokeHotkeyPressed(
                    _parent,
                    new HotkeyEventArgs(modifiers.ToKeys(), hotkey));
                }
        }
    }

    public class HotkeyEventArgs : EventArgs
    {
        internal HotkeyEventArgs
            (Keys modifiers,
             Keys key)
            {
            Modifiers = modifiers;
            KeyCode = key;
            }

        public Keys KeyCode { get; }
        public Keys Modifiers { get; }
        public Keys Hotkey => KeyCode | Modifiers;
    }

    public static class Extensions
    {
        public static Keys GetKeyCode
            (this Keys keys)
            {
            return keys & Keys.KeyCode;
            }

        public static Keys GetModifiers
            (this Keys keys)
            {
            return keys & Keys.Modifiers;
            }

        public static Keys ToKeys
            (this ModifierKeys modifierKeys)
            {
            var key = Keys.None;
            if (modifierKeys.HasFlag(ModifierKeys.Alt))
                key |= Keys.Alt;
            if (modifierKeys.HasFlag(ModifierKeys.Control))
                key |= Keys.Control;
            if (modifierKeys.HasFlag(ModifierKeys.Shift))
                key |= Keys.Shift;
            if (modifierKeys.HasFlag(ModifierKeys.Windows))
                key |= Keys.LWin;
            return key;
            }

        public static ModifierKeys ToModifierKeys
            (this Keys keys)
            {
            var modifiers = ModifierKeys.None;
            if (keys.HasFlag(Keys.Alt))
                modifiers |= ModifierKeys.Alt;
            if (keys.HasFlag(Keys.Control))
                modifiers |= ModifierKeys.Control;
            if (keys.HasFlag(Keys.Shift))
                modifiers |= ModifierKeys.Shift;
            if (keys.HasFlag(Keys.LWin))
                modifiers |= ModifierKeys.Windows;
            return modifiers;
            }
    }
}