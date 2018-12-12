using System;
using System.Windows.Forms;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Properties;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi
{
    internal class AppContext : ApplicationContext
    {
        private readonly BookmarksFormPresenter
            _bookmarksFormPresenter;

        private readonly MainFormPresenter _mainFormPresenter;
        private readonly NotifyIcon _trayIcon;

        public AppContext()
            {
            Application.ApplicationExit += OnApplicationExit;
            _mainFormPresenter =
                new MainFormPresenter(
                    Settings.Default.PersistentCache);
            _bookmarksFormPresenter = new BookmarksFormPresenter(
                _mainFormPresenter.Thesaurus,
                _mainFormPresenter.Search);
            var openMenuItem = new ToolStripMenuItem {Text = @"Open"};
            openMenuItem.Click += OnOpen;
            var exitMenuItem = new ToolStripMenuItem {Text = @"Exit"};
            exitMenuItem.Click +=
                (sender,
                 args) => Application.Exit();
            _trayIcon = new NotifyIcon
                {
                Icon = Resources.logophi,
                Text = Resources.AppName,
                Visible = true,
                ContextMenuStrip = new ContextMenuStrip
                    {
                    Items = {openMenuItem, exitMenuItem}
                    }
                };
            _trayIcon.DoubleClick += OnOpen;
            }

        private void PresentMainForm()
            {
            var form = new MainForm();
            form.ViewBookmarksClicked += OnViewBookmarksClicked;
            _mainFormPresenter.Present(form);
            }

        private void OnApplicationExit
            (object sender,
             EventArgs e)
            {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            }

        private void OnOpen
            (object sender,
             EventArgs e)
            {
            PresentMainForm();
            }

        private void OnViewBookmarksClicked
            (object sender,
             EventArgs e)
            {
            var form = new BookmarksForm();
            _bookmarksFormPresenter.Present(form);
            }
    }
}