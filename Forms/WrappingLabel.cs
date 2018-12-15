using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseNet.Forms.Controls
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a <see cref="T:System.Windows.Forms.Label" /> with text wrapping capability.
    /// </summary>
    /// <seealso cref="T:System.Windows.Forms.Label" />
    public class WrappingLabel : Label
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:MouseNet.Forms.Controls.WrappingLabel" /> class.
        /// </summary>
        public WrappingLabel()
            {
            InitializeComponent();
            }

        /// <summary>
        ///     Initializes the <see cref="WrappingLabel" />.
        /// </summary>
        private void InitializeComponent()
            {
            AutoSize = false;
            }

        /// <summary>
        ///     Wraps the text to fit the label's width.
        /// </summary>
        private void WrapText()
            {
            var size = TextRenderer.MeasureText(
                Text,
                Font,
                new Size(Width, int.MaxValue),
                TextFormatFlags.WordBreak);
            Height = size.Height + Padding.Vertical;
            }

        /// <inheritdoc />
        /// <remarks>
        ///     This override calls <c>WrapText</c> after the base method.
        /// </remarks>
        protected override void OnFontChanged
            (EventArgs e)
            {
            base.OnFontChanged(e);
            WrapText();
            }

        /// <inheritdoc />
        /// <remarks>
        ///     This override calls <c>WrapText</c> after the base method.
        /// </remarks>
        protected override void OnSizeChanged
            (EventArgs e)
            {
            base.OnSizeChanged(e);
            WrapText();
            }

        /// <inheritdoc />
        /// <remarks>
        ///     This override calls <c>WrapText</c> after the base method.
        /// </remarks>
        protected override void OnTextChanged
            (EventArgs e)
            {
            base.OnTextChanged(e);
            WrapText();
            }
    }
}