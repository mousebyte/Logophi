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

        public event EventHandler DeleteCacheClicked;
        public event EventHandler DeleteHistoryClicked;
    }
}