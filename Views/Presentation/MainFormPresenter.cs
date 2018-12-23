using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using MouseNet.Logophi.Forms;
using MouseNet.Logophi.Properties;

namespace MouseNet.Logophi.Views.Presentation
{
    internal class MainFormPresenter : IViewPresenter<IMainFormView>
    {
        private readonly SearchHistory _history;

        public MainFormPresenter
            (string dataDirectory,
             bool persistentCache,
             bool persistentHistory)
            {
            Thesaurus = new Thesaurus(dataDirectory, persistentCache);
            _history =
                new SearchHistory(dataDirectory, persistentHistory);
            }

        private bool SearchValid => Thesaurus.Definitions != null;
        public Thesaurus Thesaurus { get; }

        public void Present
            (IMainFormView view)
            {
            View = view;
            _history.MaxItems = (int) Settings.Default.MaxHistory;
            if (_history.Count > 0)
                foreach (var i in _history)
                    if (!view.DropDownItems.Contains(i))
                        view.DropDownItems.Add(i);
            View.Search += OnSearch;
            View.SelectedDefinitionChanged +=
                OnSelectedDefinitionChanged;
            View.BackClicked += OnBackClicked;
            View.ForwardClicked += OnForwardClicked;
            View.BookmarkClicked += OnBookmarkClicked;
            View.ViewDictionaryClicked += OnViewDictionaryClicked;
            View.PreferencesClicked += OnPreferencesClicked;
            View.Closed += OnClosed;
            View.Show();
            IsPresenting = true;
            }

        public IMainFormView View { get; private set; }
        public bool IsPresenting { get; private set; }

        public void Search
            (string word)
            {
            OnSearch(this, word);
            }

        private void HandleInvalidSearch()
            {
            View.Definitions.Add(Resources.InvalidSearch);
            View.EnableBookmarkButton = false;
            }

        private static ListViewItem MakeListViewItem
            (TermEntry term)
            {
            var item = new ListViewItem(term.Value);
            switch (Math.Abs(term.Similarity))
                {
                case 100:
                    item.Font = new Font(item.Font, FontStyle.Bold);
                    break;
                case 50:
                    item.ForeColor = Color.DimGray;
                    break;
                case 10:
                    item.ForeColor = Color.DarkGray;
                    break;
                default:
                    item.ForeColor = Color.LightGray;
                    break;
                }

            return item;
            }

        private void PopulateDefinitions
            (string word)
            {
            foreach (var def in Thesaurus.Definitions)
                View.Definitions.Add(
                    $"{def.PartOfSpeech}: {def.Definition}");
            View.EnableBookmarkButton = true;
            View.SelectedDefinitionIndex = 0;
            if (word == _history.CurrentItem) return;
            _history.AddItem(word);
            if (!View.DropDownItems.Contains(word))
                View.DropDownItems.Insert(0, word);
            }

        private void SearchFromHistory()
            {
            OnSearch(this, _history.CurrentItem);
            View.SearchText = _history.CurrentItem;
            }

        private static void UpdateAutoRunSetting()
            {
            var key = Registry.CurrentUser.OpenSubKey(
                "Software\\Microsoft\\Windows\\CurrentVersion\\Run",
                true);
            if (key == null) return;
            if (Settings.Default.AutoRun)
                key.SetValue("Logophi",
                             Path.Combine(
                                 Environment.CurrentDirectory,
                                 "Logophi.exe"));
            else key.DeleteValue("Logophi");
            }

        private void OnBackClicked
            (object sender,
             EventArgs e)
            {
            if (!_history.CanGoBackward) return;
            if (SearchValid)
                _history.GoBack();
            SearchFromHistory();
            }

        private void OnBookmarkClicked
            (object sender,
             EventArgs e)
            {
            if (!SearchValid) return;
            Thesaurus.IsBookmarked = !Thesaurus.IsBookmarked;
            View.SetBookmarkState(Thesaurus.IsBookmarked);
            }

        private void OnClosed
            (object sender,
             EventArgs e)
            {
            IsPresenting = false;
            }

        private void OnForwardClicked
            (object sender,
             EventArgs e)
            {
            if (!_history.CanGoForward) return;
            _history.GoForward();
            SearchFromHistory();
            }

        private void OnPreferencesClicked
            (object sender,
             EventArgs e)
            {
            //TODO: move this the heck outa here
            var autoRun = Settings.Default.AutoRun;
            var form = new PreferencesForm();
            form.DeleteHistoryClicked +=
                (o,
                 args) => _history.Clear();
            form.DeleteCacheClicked +=
                (o,
                 args) => Thesaurus.ClearCache();
            var result = form.ShowDialog((IWin32Window) View);
            if (result == DialogResult.OK)
                {
                Thesaurus.PersistentCache =
                    Settings.Default.PersistentCache;
                View.TopMost = Settings.Default.AlwaysOnTop;
                _history.PersistentHistory =
                    Settings.Default.SaveHistory;
                _history.MaxItems = (int) Settings.Default.MaxHistory;
                Settings.Default.Save();
                if (autoRun != Settings.Default.AutoRun)
                    UpdateAutoRunSetting();
                }

            form.Dispose();
            }

        private void OnSearch
            (object sender,
             string word)
            {
            if (View.SearchText != word) View.SearchText = word;
            View.Definitions.Clear();
            Thesaurus.SearchWord(word);

            if (!SearchValid || Thesaurus.Definitions.Count == 0)
                HandleInvalidSearch();
            else PopulateDefinitions(word);

            View.EnableBackButton = _history.CanGoBackward;
            View.EnableForwardButton = _history.CanGoForward;
            View.SetBookmarkState(Thesaurus.IsBookmarked);
            }

        private void OnSelectedDefinitionChanged
            (object sender,
             int e)
            {
            View.Synonyms.Clear();
            View.Antonyms.Clear();
            if (!SearchValid) return;
            var def = Thesaurus.Definitions[e];
            foreach (var syn in def.Synonyms)
                View.Synonyms.Add(MakeListViewItem(syn));
            foreach (var ant in def.Antonyms)
                View.Antonyms.Add(MakeListViewItem(ant));
            }

        private void OnViewDictionaryClicked
            (object sender,
             EventArgs e)
            {
            if (!SearchValid) return;
            Process.Start(Resources.DictionaryUrl + View.SearchText);
            }
    }
}