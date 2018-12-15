using System.Diagnostics;
using System.Windows.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Forms
{
    public partial class About : Form
    {
        public About()
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