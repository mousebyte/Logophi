using System;
using System.Windows.Forms;

namespace MouseNet.Logophi.Utilities
{
    /// <inheritdoc />
    /// <summary>
    ///     Provides data for the <see cref="E:MouseNet.Logophi.Utilities.GlobalHotkey.HotkeyPressed" /> event.
    /// </summary>
    public class HotkeyEventArgs : EventArgs
    {
        internal HotkeyEventArgs
            (Keys modifiers,
             Keys key)
            {
            Modifiers = modifiers;
            KeyCode = key;
            }

        /// <summary>
        ///     The keycode of the hotkey that was pressed.
        /// </summary>
        public Keys KeyCode { get; }
        /// <summary>
        ///     The modifiers of the hotkey that was pressed.
        /// </summary>
        public Keys Modifiers { get; }
        /// <summary>
        ///     The value of the hotkey that was pressed.
        /// </summary>
        public Keys Hotkey => KeyCode | Modifiers;
    }
}