using System;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Views;
using MouseNet.Logophi.Views.Presentation;

namespace MouseNet.Logophi
{
    /// <inheritdoc />
    /// <summary>
    ///     Manages presentation objects.
    /// </summary>
    internal class PresentationAgent : IDisposable
    {
        public PresentationAgent
            (Browser browser,
             Action exitAction,
             Action showBookmarksAction,
             Action showPreferencesAction,
             Action showAboutAction,
             Action deleteCacheAction,
             Action deleteHistoryAction)
            {
            var main = new MainFormPresenter(
                browser);
            Main = main;
            Bookmarks =
                new BookmarksFormPresenter(browser);
            Preferences =
                new PreferencesDialogPresenter(
                    deleteCacheAction,
                    deleteHistoryAction);
            }

        public IViewPresenter<IMainFormView> Main { get; }
        public IViewPresenter<IBookmarksFormView> Bookmarks { get; }
        public IViewPresenter<IPreferencesDialogView> Preferences {
            get;
        }

        /// <inheritdoc />
        public void Dispose()
            {
            Main.Dispose();
            Bookmarks.Dispose();
            Preferences.Dispose();
            }

        private void InvokePreferencesSaved
            (object sender,
             EventArgs args)
            {
            PreferencesSaved?.Invoke(sender, args);
            }

        public event EventHandler PreferencesSaved;
    }
}