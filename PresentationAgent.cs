using System;
using System.Windows.Forms;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Views.Presentation;
using MouseNet.Logophi.Thesaurus;

namespace MouseNet.Logophi
{
    /// <inheritdoc />
    /// <summary>
    /// Manages presentation objects.
    /// </summary>
    internal class PresentationAgent : IDisposable
    {
        private readonly BookmarksFormPresenter
            _bookmarksFormPresenter;

        private readonly MainFormPresenter _mainFormPresenter;

        private readonly PreferencesDialogPresenter
            _preferencesDialogPresenter;

        private readonly Browser _thesaurus;

        public PresentationAgent
            (Browser thesaurus)
            {
            _thesaurus = thesaurus;
            //create presenters and hook up event handlers
            _mainFormPresenter = new MainFormPresenter(
                thesaurus,
                Application.Exit,
                PresentBookmarksForm,
                PresentPreferencesForm,
                PresentAboutDialog);
            _bookmarksFormPresenter =
                new BookmarksFormPresenter(thesaurus, _mainFormPresenter.Search);
            _preferencesDialogPresenter =
                new PreferencesDialogPresenter(DeleteCache, DeleteHistory);
            }

        public event EventHandler PreferencesSaved;
        
        private void InvokePreferencesSaved
            (object sender,
             EventArgs args)
            {
            PreferencesSaved?.Invoke(sender, args);
            }

        /// <inheritdoc />
        public void Dispose()
            {
            _mainFormPresenter?.Dispose();
            _bookmarksFormPresenter?.Dispose();
            _preferencesDialogPresenter?.Dispose();
            }

        /// <summary>
        /// Closes the Logophi main window.
        /// </summary>
        public void CloseMainForm()
            {
            if (_mainFormPresenter.IsPresenting)
                _mainFormPresenter.View.Close();
            }

        /// <summary>
        /// Presents the about window to the user.
        /// </summary>
        private void PresentAboutDialog()
            {
            var dialog = new AboutForm();
            dialog.ShowDialog((IWin32Window) _mainFormPresenter.View);
            dialog.Dispose();
            }

        /// <summary>
        /// Presents the bookmarks window to the user.
        /// </summary>
        private void PresentBookmarksForm()
            {
            _bookmarksFormPresenter.Present(
                new BookmarksForm(),
                _mainFormPresenter.View);
            }

        /// <summary>
        /// Presents the Logophi main window to the user, or
        /// brings it to the front if it is already open.
        /// </summary>
        public void PresentMainForm()
            {
            if (_mainFormPresenter.IsPresenting)
                _mainFormPresenter.View.ToFront();
            else _mainFormPresenter.Present(new MainForm());
            }

        /// <summary>
        /// Presents the preferences window to the user.
        /// </summary>
        private void PresentPreferencesForm()
            {
            var dialog = new PreferencesForm();

            _preferencesDialogPresenter.Present(
                dialog,
                _mainFormPresenter.View);
            dialog.Dispose();
            //notify listeners that preferences were saved
            InvokePreferencesSaved(this, EventArgs.Empty);
            }

        private void DeleteCache()
            {
            _thesaurus.ClearCache();
            }

        private void DeleteHistory()
            {
            _thesaurus.History.Clear();
            }
    }
}