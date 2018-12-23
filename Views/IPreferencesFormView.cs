using System;
using System.Windows.Forms;

namespace MouseNet.Logophi.Views
{
    public interface IPreferencesFormView : IView<IWin32Window>
    {
        bool PersistentCache { get; set; }
        bool AutoRun { get; set; }
        bool AlwaysOnTop { get; set; }
        bool PersistentHistory { get; set; }
        decimal MaxHistory { get; set; }
        event EventHandler DeleteCacheClicked;
        event EventHandler DeleteHistoryClicked;

        DialogResult ShowDialog
            (IWin32Window parent);
    }
}
