using System.Windows.Forms;

namespace MouseNet.Logophi.Views
{
    public interface IPreferencesDialogView : IView
    {
        bool PersistentCache { get; set; }
        bool AutoRun { get; set; }
        bool AlwaysOnTop { get; set; }
        bool PersistentHistory { get; set; }
        decimal MaxHistory { get; set; }

        DialogResult ShowDialog
            (IWin32Window parent);
    }
}
