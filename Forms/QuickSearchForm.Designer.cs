namespace MouseNet.Logophi.Forms
{
    partial class QuickSearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickSearchForm));
            this._cSearchText = new System.Windows.Forms.TextBox();
            this._btnSearch = new System.Windows.Forms.Button();
            this._cTermList = new MouseNet.Logophi.Forms.TermList();
            this._cTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _cSearchText
            // 
            this._cSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cSearchText.Location = new System.Drawing.Point(12, 12);
            this._cSearchText.Name = "_cSearchText";
            this._cSearchText.Size = new System.Drawing.Size(152, 20);
            this._cSearchText.TabIndex = 0;
            this._cSearchText.WordWrap = false;
            // 
            // _btnSearch
            // 
            this._btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSearch.BackgroundImage = global::MouseNet.Logophi.Properties.Resources.search;
            this._btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this._btnSearch.Location = new System.Drawing.Point(170, 9);
            this._btnSearch.Name = "_btnSearch";
            this._btnSearch.Size = new System.Drawing.Size(26, 26);
            this._btnSearch.TabIndex = 1;
            this._btnSearch.UseVisualStyleBackColor = true;
            this._btnSearch.Click += new System.EventHandler(this.OnSearchClick);
            // 
            // _cTermList
            // 
            this._cTermList.BackColor = System.Drawing.SystemColors.Window;
            this._cTermList.BoldColor = System.Drawing.Color.Black;
            this._cTermList.LightColor = System.Drawing.Color.DarkGray;
            this._cTermList.Location = new System.Drawing.Point(0, 0);
            this._cTermList.Name = "_cTermList";
            this._cTermList.NormalColor = System.Drawing.Color.DimGray;
            this._cTermList.Padding = new System.Windows.Forms.Padding(4);
            this._cTermList.Size = new System.Drawing.Size(356, 200);
            this._cTermList.TabIndex = 2;
            this._cTermList.Visible = false;
            this._cTermList.Click += new System.EventHandler(this.InvokeTermListClick);
            this._cTermList.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.OnTermListItemAdded);
            // 
            // _cTimer
            // 
            this._cTimer.Interval = 10;
            // 
            // QuickSearchForm
            // 
            this.AcceptButton = this._btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 44);
            this.Controls.Add(this._btnSearch);
            this.Controls.Add(this._cSearchText);
            this.Controls.Add(this._cTermList);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuickSearchForm";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "QuickSearchForm";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.OnShown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _cSearchText;
        private System.Windows.Forms.Button _btnSearch;
        private TermList _cTermList;
        private System.Windows.Forms.Timer _cTimer;
    }
}