using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Logophi.Forms {
    public partial class TermList : Panel {
        private static readonly Font BaseFont = new Font(
            new FontFamily("Arial"),
            10,
            GraphicsUnit.Point);

        public TermList()
            {
            InitializeComponent();
            }


        protected override void OnResize(EventArgs eventargs)
            {
            base.OnResize(eventargs);
            LayoutItems();
            }

        public void LayoutItems(int startAt = 1)
            {
            if (Controls.Count == 0) return;
            var height = Controls[0].Height;
            SuspendLayout();
            for (var i = startAt; i < Controls.Count; i++)
                {
                var current = Controls[i];
                var previous = Controls[i - 1];
                var loc = previous.Location + new Size(previous.Width + 4, 0);
                if (loc.X + current.Width > Width - 22)
                    loc = new Point(4, loc.Y + height);
                current.Location = loc;
                }
            ResumeLayout(false);
            }


        public void AddTerm(string term, int similarity)
            {
            var item = new TermListItem {BackColor = BackColor, Text = term, Font = BaseFont};
            switch (Math.Abs(similarity))
                {
                case 100:
                    item.Font = new Font(BaseFont, FontStyle.Bold);
                    item.ForeColor = BoldColor;
                    break;
                case 50:
                    item.ForeColor = NormalColor;
                    break;
                case 10:
                    item.ForeColor = LightColor;
                    break;
                default:
                    item.ForeColor = Color.LightGray;
                    break;
                }

            item.DoubleClick += OnItemDoubleClick;
            Controls.Add(item);
            if (Controls.Count == 1) item.Location = new Point(4, 4);
            else LayoutItems(Controls.Count - 1);
            item.DrawToBuffer();
            }


        public void Clear()
            {
            Controls.Clear();
            }

        public event EventHandler<string> ItemActivated;

        private void InvokeItemActivated(object sender, string args)
            {
            ItemActivated?.Invoke(sender, args);
            }

        private void OnItemDoubleClick(object sender, EventArgs args)
            {
            InvokeItemActivated(this, (sender as Control)?.Text);
            }

        [Browsable(true)] public Color BoldColor { get; set; } = Color.Black;

        [Browsable(true)]
        public Color NormalColor { get; set; } = Color.DimGray;

        [Browsable(true)]
        public Color LightColor { get; set; } = Color.DarkGray;

        private class TermListItem : Control {
            private Color _foreColor;

            private static readonly BufferedGraphicsContext Ctx =
                BufferedGraphicsManager.Current;

            private BufferedGraphics _gBuff;

            public TermListItem()
                {
                TextChanged += OnTextOrFontChanged;
                FontChanged += OnTextOrFontChanged;
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                }

            protected override void OnClick(EventArgs e)
                {
                base.OnClick(e);
                if (Focused) return;
                Focus();
                _foreColor = ForeColor;
                ForeColor = Color.White;
                DrawToBuffer();
                }

            protected override void OnLostFocus(EventArgs e)
                {
                base.OnLostFocus(e);
                ForeColor = _foreColor;
                DrawToBuffer();
                }

            private void OnTextOrFontChanged(object sender, EventArgs args)
                {
                Size = Size.Add(
                    TextRenderer.MeasureText(Text, Font),
                    new Size(4, 4));
                _gBuff?.Dispose();
                _gBuff = Ctx.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
                DrawToBuffer();
                }

            public void DrawToBuffer()
                {
                var g = _gBuff.Graphics;
                g.FillRectangle(
                    Focused
                        ? SystemBrushes.Highlight
                        : new SolidBrush(BackColor),
                    ClientRectangle);
                var format = new StringFormat(StringFormatFlags.NoWrap)
                    {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                    };
                g.DrawString(
                    Text,
                    Font,
                    new SolidBrush(ForeColor),
                    ClientRectangle,
                    format);
                Invalidate();
                }

            protected override void OnPaint(PaintEventArgs e)
                {
                _gBuff.Render(e.Graphics);
                }
        }
    }
}