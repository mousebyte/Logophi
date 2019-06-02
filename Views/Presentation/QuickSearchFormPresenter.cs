using System.Drawing;
using System.Windows.Forms;
using MouseNet.Logophi.Thesaurus;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi.Views.Presentation {
    internal class
        QuickSearchFormPresenter : ViewPresenter<IQuickSearchFormView> {
        private readonly Browser _browser;

        public QuickSearchFormPresenter(Browser browser)
            {
            _browser = browser;
            _browser.SearchCompleted += OnSearchCompleted;
            }

        protected override void InitializeView()
            {
            View.Search += OnSearch;
            var workArea = Screen
                           .FromHandle(NativeMethods.GetForegroundWindow())
                           .WorkingArea;
            View.Location = new Point(workArea.Right - 210, workArea.Bottom);
            }

        private void OnSearchCompleted(object sender, SearchEventArgs args)
            {
            if (!IsPresenting) return;
            if (args.Success)
                foreach (var termEntry in _browser.Definitions[0].Synonyms)
                    View.AddSynonym(termEntry.Value, termEntry.Similarity);
            else View.AddSynonym("No results found", 100);
            View.ShowTerms();
            }

        private void OnSearch(object sender, string word)
            {
            _browser.SearchWord(word);
            }
    }
}