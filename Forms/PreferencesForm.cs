using System;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms
{
    public partial class PreferencesForm
        : LogophiForm, IPreferencesDialogView
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

        public decimal MaxHistory {
            get => _cMaxHistory.Value;
            set => _cMaxHistory.Value = value;
        }

        public event EventHandler<ViewEventArgs> ViewEventActivated;

        private void InvokeDeleteCacheClicked
            (object sender,
             EventArgs args)
            {
            InvokeViewEventActivated(this,
                                     new ViewEventArgs(
                                         "DeleteCacheClicked"));
            }

        private void InvokeDeleteHistoryClicked
            (object sender,
             EventArgs args)
            {
            InvokeViewEventActivated(this,
                                     new ViewEventArgs(
                                         "DeleteHistoryClicked"));
            }

        private void InvokeViewEventActivated
            (object sender,
             ViewEventArgs args)
            {
            ViewEventActivated?.Invoke(sender, args);
            }
    }
}