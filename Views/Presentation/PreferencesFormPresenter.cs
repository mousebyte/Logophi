using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class PreferencesFormPresenter : IViewPresenter<IPreferencesFormView>
    {
        public IPreferencesFormView View { get; private set; }
        public bool IsPresenting { get; private set; }

        public void Present
            (IPreferencesFormView view)
            {
            View = view;
            }
    }
}
