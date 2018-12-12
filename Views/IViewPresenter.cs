namespace MouseNet.Logophi.Views
{
    public interface IViewPresenter<in TView>
    {
        void Present
            (TView view);
    }
}