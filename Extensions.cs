using System.Windows.Forms;
using System.Windows.Input;

namespace MouseNet.Logophi
{
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