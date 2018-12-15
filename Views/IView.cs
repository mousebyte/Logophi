using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseNet.Logophi.Views
{
    public interface IView<in TParent>
    {
        void Show();
        void Show
            (TParent parent);

        void Close();
    }
}