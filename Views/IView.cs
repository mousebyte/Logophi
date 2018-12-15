namespace MouseNet.Logophi.Views
{
    public interface IView<in TParent>
    {
        void Close();
        void Show();

        void Show
            (TParent parent);
    }
}