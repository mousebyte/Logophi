using System;
using System.Windows.Forms;

namespace MouseNet.Logophi.Forms
{
    public class LogophiForm : Form
    {
        public void Present
            (object owner = null)
            {
            switch (owner)
                {
                case null:
                    Show();
                    break;
                case IWin32Window wnd:
                    Show(wnd);
                    break;
                default:
                    throw new ArgumentException(
                        "Invalid owner specified.");
                }
            }

        public bool PresentModal
            (object owner)
            {
            if (owner == null || !(owner is IWin32Window wnd))
                throw new ArgumentException(
                    "Invalid owner specified.");
            switch (ShowDialog(wnd))
                {
                case DialogResult.OK:
                case DialogResult.Yes:
                    return true;
                default:
                    return false;
                }
            }

        public void ToFront()
            {
            Activate();
            WindowState = FormWindowState.Normal;
            BringToFront();
            }
    }
}