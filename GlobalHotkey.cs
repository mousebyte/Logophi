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
        private readonly IList<Keys> _hotkeys = new List<Keys>();

        public GlobalHotkey()
            {
            _form = new DummyForm(this);
            }

        public IEnumerable<Keys> Hotkeys => _hotkeys;

        public void Dispose()
            {
            for (var i = 0; i < _hotkeys.Count; i++)
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
                                              _hotkeys.Count + 1,
                                              modifiers,
                                              keycode))
                throw new InvalidOperationException(
                    "Failed to register hotkey.");
            _hotkeys.Add(hotkey);
            return _hotkeys.Count;
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
}