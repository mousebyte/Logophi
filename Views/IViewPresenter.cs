namespace MouseNet.Logophi.Views
{
    public interface IViewPresenter<TView>
    {
        void Present
            (TView view);
        TView View { get; }
        bool IsPresenting { get; }
    }
}