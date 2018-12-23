using System;
using System.Diagnostics;
using System.IO;
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
            SetupDirectories();
            _mainFormPresenter = new MainFormPresenter(
                Settings.Default.DataDirectory,
                Settings.Default.PersistentCache,
                Settings.Default.SaveHistory);
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
            PresentMainForm();
            }

        private void SetupDirectories()
            {
            if (Settings.Default.DataDirectory == string.Empty)
                {
                Settings.Default.DataDirectory = Path.Combine(
                    Environment.GetFolderPath(
                        Environment
                           .SpecialFolder.LocalApplicationData),
                    Resources.AppName);
                Settings.Default.Save();
                }

            if (!Directory.Exists(Settings.Default.DataDirectory))
                Directory.CreateDirectory(
                    Settings.Default.DataDirectory);
            }

        private void PresentMainForm()
            {
            if (_mainFormPresenter.IsPresenting) return;
            var form = new MainForm();
            //? Possibly move these event handlers somewhere else
            form.ViewBookmarksClicked += OnViewBookmarksClicked;
            form.GithubProjectClicked += OnGithubProjectClicked;
            form.AboutClicked += OnAboutClicked;
            form.ExitClicked +=
                (sender,
                 args) => Application.Exit();
            _mainFormPresenter.Present(form);
            }

        private void OnAboutClicked
            (object sender,
             EventArgs e)
            {
            var form = new AboutForm();
            form.ShowDialog((IWin32Window) _mainFormPresenter.View);
            }

        private void OnApplicationExit
            (object sender,
             EventArgs e)
            {
            if (_mainFormPresenter.IsPresenting)
                _mainFormPresenter.View.Close();
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
            }

        private void OnGithubProjectClicked
            (object sender,
             EventArgs e)
            {
            Process.Start(Resources.GithubUrl);
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
            if (!_mainFormPresenter.IsPresenting
             || _bookmarksFormPresenter.IsPresenting) return;
            var form = new BookmarksForm();
            _mainFormPresenter.Thesaurus.BookmarkRemoved +=
                (o,
                 s) => form.Items.Remove(s);
            _mainFormPresenter.Thesaurus.BookmarkAdded +=
                (o,
                 s) => form.Items.Add(s);
            _bookmarksFormPresenter.Present(form);
            }
    }
}