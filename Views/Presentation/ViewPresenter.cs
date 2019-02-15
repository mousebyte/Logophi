using System;

namespace MouseNet.Logophi.Views.Presentation
{
    internal abstract class ViewPresenter<TView>
        : IViewPresenter<TView>
        where TView : IView
    {
        public void Dispose()
            {
            View?.Dispose();
            }

        private void OnClosed
            (object sender,
             EventArgs e)
            {
            IsPresenting = false;
            View.Dispose();
            }

        public TView View { get; private set; }

        private void InitializeViewInternal
            (TView view)
            {
            View = view;
            View.Closed += OnClosed;
            IsPresenting = true;
            InitializeView();
            }

        protected abstract void InitializeView();

        public void Present
            (TView view)
            {
            Present(view, null);
            }

        public void Present
            (TView view,
             object parent)
            {
            InitializeViewInternal(view);
            if (parent == null) view.Present();
            else view.Present(parent);
            }

        public bool PresentDialog
            (TView view,
             object parent)
            {
            if (parent == null)
                throw new ArgumentNullException(
                    nameof(parent),
                    @"Parent cannot be null.");
            InitializeViewInternal(view);
            return view.PresentDialog(parent);
            }

        public bool IsPresenting { get; private set; }
    }
}