namespace MouseNet.Logophi.Views
{
    public interface IViewPresenter<TView>
    {
        TView View { get; }
        bool IsPresenting { get; }

        void Present
            (TView view);
    }
}