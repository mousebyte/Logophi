﻿using System;
using System.Windows.Forms;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi
{
    internal class PresentationAgent : IDisposable
    {
        private readonly BookmarksFormPresenter
            _bookmarksFormPresenter;

        private readonly MainFormPresenter _mainFormPresenter;

        private readonly PreferencesDialogPresenter
            _preferencesDialogPresenter;

        public PresentationAgent
            (Thesaurus thesaurus)
            {
            _mainFormPresenter = new MainFormPresenter(thesaurus);
            _mainFormPresenter.ShowAboutClicked += OnShowAboutClicked;
            _mainFormPresenter.ShowBookmarksClicked +=
                OnShowBookmarksClicked;
            _mainFormPresenter.ShowPreferencesClicked +=
                OnShowPreferencesClicked;
            _bookmarksFormPresenter =
                new BookmarksFormPresenter(thesaurus);
            _bookmarksFormPresenter.BookmarkActivated +=
                OnBookmarkActivated;
            _preferencesDialogPresenter =
                new PreferencesDialogPresenter();
            }

        public void Dispose()
            {
            _mainFormPresenter?.Dispose();
            _bookmarksFormPresenter?.Dispose();
            _preferencesDialogPresenter?.Dispose();
            }

        public void CloseMainForm()
            {
            if (_mainFormPresenter.IsPresenting)
                _mainFormPresenter.View.Close();
            }

        public void PresentAboutDialog()
            {
            var dialog = new AboutForm();
            dialog.ShowDialog((IWin32Window) _mainFormPresenter.View);
            dialog.Dispose();
            }

        public void PresentBookmarksForm()
            {
            var form = new BookmarksForm();
            _bookmarksFormPresenter.Present(
                form,
                _mainFormPresenter.View);
            }
        
        public void PresentMainForm()
            {
            var form = new MainForm();
            _mainFormPresenter.Present(form);
            }

        public void PresentPreferencesForm()
            {
            var dialog = new PreferencesForm();

            _preferencesDialogPresenter.Present(
                dialog,
                _mainFormPresenter.View);
            dialog.Dispose();
            }

        private void OnBookmarkActivated
            (object sender,
             string e)
            {
            _mainFormPresenter.Search(e);
            }

        private void OnShowAboutClicked
            (object sender,
             EventArgs e)
            {
            PresentAboutDialog();
            }

        private void OnShowBookmarksClicked
            (object sender,
             EventArgs e)
            {
            PresentBookmarksForm();
            }

        private void OnShowPreferencesClicked
            (object sender,
             EventArgs e)
            {
            PresentPreferencesForm();
            }
    }
}