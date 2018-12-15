using System.Diagnostics;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
            {
            InitializeComponent();
            _lblProduct.Text = Resources.AppName;
            _lblVersion.Text = Application.ProductVersion;
            _lblAbout.Text = Resources.about;
            }

        private void OnLinkClicked
            (object sender,
             LinkLabelLinkClickedEventArgs e)
            {
            _lblGithubLink.LinkVisited = true;
            Process.Start(Resources.GithubUrl);
            }
    }
}