using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace MouseNet.Logophi.Utilities
{
    /// <inheritdoc />
    /// <summary>
    /// Allows for the registration and removal of global hotkeys,
    /// and notifies listeners when they are activated.
    /// </summary>
    public class GlobalHotkey : IDisposable
    {
        //the win32 window message that indicates a hotkey was pressed.
        private const int HotkeyMsg = 0x0312;
        private readonly Form _form;
        private readonly IList<Keys> _hotkeys = new List<Keys>();

        public GlobalHotkey()
            {
            _form = new DummyForm(this);
            }

        /// <summary>
        /// Returns the hotkeys registered in the <see cref="GlobalHotkey"/> object.
        /// </summary>
        public IEnumerable<Keys> Hotkeys => _hotkeys;

        /// <inheritdoc />
        public void Dispose()
            {
            for (var i = 0; i < _hotkeys.Count; i++)
                UnregisterHotkey(i);
            _form?.Dispose();
            }

        /// <summary>
        /// Registers a global hotkey.
        /// </summary>
        /// <param name="hotkey">The hotkey to register.</param>
        /// <returns>An integer identifier that must be used when
        /// unregistering the hotkey.</returns>
        public int RegisterHotkey
            (Keys hotkey)
            {
            //extract the modifiers and keycode from the hotkey
            var modifiers =
                (int) hotkey.GetModifiers().ToModifierKeys();
            var keycode = (int) hotkey.GetKeyCode();
            
            //register the hotkey to the dummy form and add it to the list
            if (!NativeMethods.RegisterHotKey(_form.Handle,
                                              _hotkeys.Count + 1,
                                              modifiers,
                                              keycode))
                throw new InvalidOperationException(
                    "Failed to register hotkey.");
            _hotkeys.Add(hotkey);
            return _hotkeys.Count;
            }

        /// <summary>
        /// Unregisters the hotkey indicated by the given identifier.
        /// </summary>
        /// <param name="id">The identifier of the hotkey to unregister.
        /// This value is returned by <see cref="RegisterHotkey"/>.</param>
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

        /// <inheritdoc />
        /// <summary>
        /// A dummy form to which hotkeys are registered.
        /// </summary>
        private class DummyForm : Form
        {
            private readonly GlobalHotkey _parent;

            public DummyForm
                (GlobalHotkey parent)
                {
                _parent = parent;
                }

            /// <inheritdoc />
            protected override void WndProc
                (ref Message m)
                {
                base.WndProc(ref m);

                if (m.Msg != HotkeyMsg) return;

                //if a hotkey was pressed, extract the hotkey and modifiers from
                //the message parameters and invoke the owner's HotkeyPressed event
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