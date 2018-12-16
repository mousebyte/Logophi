using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseNet.Logophi.Views
{
    public interface IPreferencesFormView : IView<IWin32Window>
    {
        bool PersistentCache { get; set; }
        bool AutoRun { get; set; }
        bool AlwaysOnTop { get; set; }
        bool PersistentHistory { get; set; }
        event EventHandler DeleteCacheClicked;
        event EventHandler DeleteHistoryClicked;
    }
}
