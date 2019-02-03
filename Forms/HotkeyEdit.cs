using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseNet.Logophi.Forms
{
    public class HotkeyEdit : TextBox
    {
        private Keys _modifiers;
        private const string CtrlKey = "Ctrl +";
        private const string ShiftKey = "Shift +";
        private const string AltKey = "Alt +";
        public Keys Hotkey { get; set; }

        public Keys Modifiers {
            get => _modifiers;
            set => _modifiers = value;
        }

        public HotkeyEdit()
            {
            
            }

        private void OnPreviewKeyDown
            (object sender,
             PreviewKeyDownEventArgs e)
            {
            Text = string.Empty;
            if (e.Modifiers == Keys.None || e.Modifiers == Keys.Shift)
                {
                Text = @"None";
                return;
                }

            _modifiers = e.Modifiers;
            if ((e.Modifiers & Keys.Control) == Keys.Control)
                Text += CtrlKey;
            if ((e.Modifiers & Keys.Shift) == Keys.Shift)
                Text += ShiftKey;
            if ((e.Modifiers & Keys.Alt) == Keys.Alt)
                Text += AltKey;
            }

        private void OnKeyDown
            (object sender,
             KeyEventArgs e)
            {
            e.Handled = true;
            }
    }
}
