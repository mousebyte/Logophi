using System;
using System.Windows.Forms;

namespace MouseNet.Logophi
{
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
}