using System.Windows.Forms;
using System.Windows.Input;

namespace MouseNet.Logophi.Utilities
{
    /// <summary>
    /// Contains extension methods used by Logophi.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Removes modifiers from a <see cref="Keys"/> value.
        /// </summary>
        /// <param name="keys">The <see cref="Keys"/> to extract a keycode from.</param>
        /// <returns>The keycode value of the specified <see cref="Keys"/>.</returns>
        public static Keys GetKeyCode
            (this Keys keys)
            {
            return keys & Keys.KeyCode;
            }

        /// <summary>
        /// Removes the keycodes from a <see cref="Keys"/> value.
        /// </summary>
        /// <param name="keys">The <see cref="Keys"/> to extract modifiers from.</param>
        /// <returns>The modifiers of the specified <see cref="Keys"/>.</returns>
        public static Keys GetModifiers
            (this Keys keys)
            {
            return keys & Keys.Modifiers;
            }

        /// <summary>
        /// Converts a <see cref="ModifierKeys"/> value to <see cref="Keys"/>.
        /// </summary>
        /// <param name="modifierKeys">The <see cref="ModifierKeys"/> to convert.</param>
        /// <returns>The specified <see cref="ModifierKeys"/> as a <see cref="Keys"/> value.</returns>
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
        
        /// <summary>
        /// Converts a <see cref="Keys"/> value to <see cref="ModifierKeys"/>.
        /// </summary>
        /// <param name="keys">The <see cref="Keys"/> to convert.</param>
        /// <returns>The specified <see cref="Keys"/> as a <see cref="ModifierKeys"/> value.</returns>
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