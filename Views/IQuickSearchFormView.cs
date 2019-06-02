using System;

namespace MouseNet.Logophi.Views {
    internal interface IQuickSearchFormView : IView {
        event EventHandler Click;
        event EventHandler<string> Search;
        void AddSynonym(string term, int similarity);
        string SearchText { get; }
        void ShowTerms();
    }
}