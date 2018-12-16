using System;
using System.Windows.Forms;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms
{
    public partial class PreferencesForm : Form, IPreferencesFormView
    {
        public PreferencesForm()
            {
            InitializeComponent();
            }

        public bool PersistentCache {
            get => _cPersistCache.Checked;
            set => _cPersistCache.Checked = value;
        }

        public bool AutoRun {
            get => _cAutoRun.Checked;
            set => _cAutoRun.Checked = value;
        }

        public bool AlwaysOnTop {
            get => _cAlwaysOnTop.Checked;
            set => _cAlwaysOnTop.Checked = value;
        }

        public bool PersistentHistory {
            get => _cSaveHistory.Checked;
            set => _cSaveHistory.Checked = value;
        }

        public event EventHandler DeleteCacheClicked;
        public event EventHandler DeleteHistoryClicked;

        private void InvokeDeleteCacheClicked
            (object sender,
             EventArgs args)
            {
            DeleteCacheClicked?.Invoke(sender, args);
            }

        private void InvokeDeleteHistoryClicked
            (object sender,
             EventArgs args)
            {
            DeleteHistoryClicked?.Invoke(sender, args);
            }
    }
}