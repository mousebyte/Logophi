using System;
using System.Drawing;
using System.Windows.Forms;
using MouseNet.Logophi.Views;

namespace MouseNet.Logophi.Forms {
    public partial class QuickSearchForm : LogophiForm, IQuickSearchFormView {
        public QuickSearchForm()
            {
            InitializeComponent();
            }

        private int _endY;

        private void OnShown(object sender, EventArgs args)
            {
            _endY = Location.Y - 54;
            AnimateSlide();
            }

        private void OnTermListItemAdded(object sender, ControlEventArgs args)
            {
            args.Control.Click += InvokeTermListClick;
            }

        public event EventHandler TermListClick;

        private void InvokeTermListClick(object sender, EventArgs args)
            {
            TermListClick?.Invoke(sender, args);
            }

        private void InvokeSearch(object sender, string args)
            {
            Search?.Invoke(sender, args);
            }

        private void GrowOnTick(object sender, EventArgs args)
            {
            if (Height < 200)
                {
                SuspendLayout();
                var sz = new Size(26, 26);
                Location -= sz;
                Size += sz;
                ResumeLayout(true);
                }
            else _cTimer.Stop();
            }

        private void SlideOnTick(object sender, EventArgs args)
            {
            if (Location.Y > _endY)
                Location -= new Size(0, 5);
            else _cTimer.Stop();
            }

        private void AnimateSlide()
            {
            _cTimer.Tick += SlideOnTick;
            _cTimer.Start();
            while(_cTimer.Enabled) Application.DoEvents();
            _cTimer.Tick -= SlideOnTick;
            }

        private void AnimateGrow()
            {
            _cTimer.Tick += GrowOnTick;
            _cTimer.Start();
            while(_cTimer.Enabled) Application.DoEvents();
            _cTimer.Tick -= GrowOnTick;
            }

        private void OnSearchClick(object sender, EventArgs args)
            {
            InvokeSearch(this, SearchText);
            }

        public void AddSynonym(string term, int similarity)
            {
            _cTermList.AddTerm(term, similarity);
            }

        public void ShowTerms()
            {
            _cSearchText.Visible = false;
            _btnSearch.Visible = false;
            _cTermList.Visible = true;
            Opacity = 1;
            AnimateGrow();
            }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
            if (keyData != Keys.Escape)
                return base.ProcessCmdKey(ref msg, keyData);
            Close();
            return true;
            }

        public string SearchText => _cSearchText.Text;
        public event EventHandler<string> Search;
    }
}