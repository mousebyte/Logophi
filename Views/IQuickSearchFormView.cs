using System;

namespace MouseNet.Logophi.Views
{
    interface IQuickSearchFormView : IView
    {
        string SearchText { get; }
        event EventHandler<string> Search;
        event EventHandler Click;
        void AddSynonym(string term, int similarity);
        void ShowTerms();
    }
}