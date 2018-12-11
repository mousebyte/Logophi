namespace MouseNet.Logophi
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._cDefList = new System.Windows.Forms.ListBox();
            this._cTabs = new System.Windows.Forms.TabControl();
            this._cTabSynonyms = new System.Windows.Forms.TabPage();
            this._cSynonymList = new System.Windows.Forms.ListView();
            this._cTabAntonyms = new System.Windows.Forms.TabPage();
            this._cAntonymList = new System.Windows.Forms.ListView();
            this._cSearchText = new System.Windows.Forms.ComboBox();
            this._cMenuStrip = new System.Windows.Forms.MenuStrip();
            this._cBookmarksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cOptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cBookmarksView = new System.Windows.Forms.ToolStripMenuItem();
            this._cToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cBookmarkBtn = new System.Windows.Forms.Button();
            this._cSearchBtn = new System.Windows.Forms.Button();
            this._cForwardBtn = new System.Windows.Forms.Button();
            this._cBackBtn = new System.Windows.Forms.Button();
            this._cTabs.SuspendLayout();
            this._cTabSynonyms.SuspendLayout();
            this._cTabAntonyms.SuspendLayout();
            this._cMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cDefList
            // 
            this._cDefList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cDefList.FormattingEnabled = true;
            this._cDefList.Location = new System.Drawing.Point(7, 69);
            this._cDefList.Name = "_cDefList";
            this._cDefList.Size = new System.Drawing.Size(388, 186);
            this._cDefList.TabIndex = 2;
            this._cDefList.SelectedIndexChanged += new System.EventHandler(this.OnDefListSelectedIndexChanged);
            // 
            // _cTabs
            // 
            this._cTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cTabs.Controls.Add(this._cTabSynonyms);
            this._cTabs.Controls.Add(this._cTabAntonyms);
            this._cTabs.Location = new System.Drawing.Point(0, 265);
            this._cTabs.Name = "_cTabs";
            this._cTabs.SelectedIndex = 0;
            this._cTabs.Size = new System.Drawing.Size(407, 236);
            this._cTabs.TabIndex = 3;
            // 
            // _cTabSynonyms
            // 
            this._cTabSynonyms.Controls.Add(this._cSynonymList);
            this._cTabSynonyms.Location = new System.Drawing.Point(4, 22);
            this._cTabSynonyms.Name = "_cTabSynonyms";
            this._cTabSynonyms.Padding = new System.Windows.Forms.Padding(3);
            this._cTabSynonyms.Size = new System.Drawing.Size(399, 210);
            this._cTabSynonyms.TabIndex = 0;
            this._cTabSynonyms.Text = "Synonyms";
            this._cTabSynonyms.UseVisualStyleBackColor = true;
            // 
            // _cSynonymList
            // 
            this._cSynonymList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cSynonymList.Location = new System.Drawing.Point(3, 3);
            this._cSynonymList.MultiSelect = false;
            this._cSynonymList.Name = "_cSynonymList";
            this._cSynonymList.Size = new System.Drawing.Size(393, 204);
            this._cSynonymList.TabIndex = 0;
            this._cSynonymList.UseCompatibleStateImageBehavior = false;
            this._cSynonymList.View = System.Windows.Forms.View.List;
            this._cSynonymList.ItemActivate += new System.EventHandler(this.OnTermEntryDoubleClick);
            // 
            // _cTabAntonyms
            // 
            this._cTabAntonyms.Controls.Add(this._cAntonymList);
            this._cTabAntonyms.Location = new System.Drawing.Point(4, 22);
            this._cTabAntonyms.Name = "_cTabAntonyms";
            this._cTabAntonyms.Padding = new System.Windows.Forms.Padding(3);
            this._cTabAntonyms.Size = new System.Drawing.Size(399, 188);
            this._cTabAntonyms.TabIndex = 1;
            this._cTabAntonyms.Text = "Antonyms";
            this._cTabAntonyms.UseVisualStyleBackColor = true;
            // 
            // _cAntonymList
            // 
            this._cAntonymList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this._cAntonymList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cAntonymList.Location = new System.Drawing.Point(3, 3);
            this._cAntonymList.Name = "_cAntonymList";
            this._cAntonymList.Size = new System.Drawing.Size(393, 182);
            this._cAntonymList.TabIndex = 0;
            this._cAntonymList.UseCompatibleStateImageBehavior = false;
            this._cAntonymList.View = System.Windows.Forms.View.List;
            this._cAntonymList.ItemActivate += new System.EventHandler(this.OnTermEntryDoubleClick);
            // 
            // _cSearchText
            // 
            this._cSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cSearchText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._cSearchText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._cSearchText.Location = new System.Drawing.Point(7, 33);
            this._cSearchText.Name = "_cSearchText";
            this._cSearchText.Size = new System.Drawing.Size(253, 21);
            this._cSearchText.Sorted = true;
            this._cSearchText.TabIndex = 7;
            this._cSearchText.SelectionChangeCommitted += new System.EventHandler(this.OnSearchTextSelectionChangeCommitted);
            this._cSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchTextReturnPressed);
            // 
            // _cMenuStrip
            // 
            this._cMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cBookmarksMenuItem,
            this._cOptionsMenuItem});
            this._cMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._cMenuStrip.Name = "_cMenuStrip";
            this._cMenuStrip.Size = new System.Drawing.Size(407, 24);
            this._cMenuStrip.TabIndex = 8;
            this._cMenuStrip.Text = "_cMenuStrip";
            // 
            // _cBookmarksMenuItem
            // 
            this._cBookmarksMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cBookmarksView});
            this._cBookmarksMenuItem.Name = "_cBookmarksMenuItem";
            this._cBookmarksMenuItem.Size = new System.Drawing.Size(78, 20);
            this._cBookmarksMenuItem.Text = "&Bookmarks";
            // 
            // _cOptionsMenuItem
            // 
            this._cOptionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this._cOptionsMenuItem.Name = "_cOptionsMenuItem";
            this._cOptionsMenuItem.Size = new System.Drawing.Size(61, 20);
            this._cOptionsMenuItem.Text = "&Options";
            // 
            // _cBookmarksView
            // 
            this._cBookmarksView.Name = "_cBookmarksView";
            this._cBookmarksView.Size = new System.Drawing.Size(180, 22);
            this._cBookmarksView.Text = "&View Bookmarks";
            this._cBookmarksView.Click += new System.EventHandler(this.OnViewBookmarksClicked);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            // 
            // _cBookmarkBtn
            // 
            this._cBookmarkBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cBookmarkBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._cBookmarkBtn.Enabled = false;
            this._cBookmarkBtn.FlatAppearance.BorderSize = 0;
            this._cBookmarkBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cBookmarkBtn.Image = global::MouseNet.Logophi.Properties.Resources.bookmark_disabled;
            this._cBookmarkBtn.Location = new System.Drawing.Point(374, 27);
            this._cBookmarkBtn.Name = "_cBookmarkBtn";
            this._cBookmarkBtn.Size = new System.Drawing.Size(21, 30);
            this._cBookmarkBtn.TabIndex = 10;
            this._cToolTip.SetToolTip(this._cBookmarkBtn, "Forward");
            this._cBookmarkBtn.UseVisualStyleBackColor = true;
            this._cBookmarkBtn.Click += new System.EventHandler(this.OnBookmarkClicked);
            // 
            // _cSearchBtn
            // 
            this._cSearchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cSearchBtn.AutoSize = true;
            this._cSearchBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._cSearchBtn.FlatAppearance.BorderSize = 0;
            this._cSearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cSearchBtn.Image = ((System.Drawing.Image)(resources.GetObject("_cSearchBtn.Image")));
            this._cSearchBtn.Location = new System.Drawing.Point(266, 27);
            this._cSearchBtn.Name = "_cSearchBtn";
            this._cSearchBtn.Size = new System.Drawing.Size(30, 30);
            this._cSearchBtn.TabIndex = 6;
            this._cToolTip.SetToolTip(this._cSearchBtn, "Search");
            this._cSearchBtn.UseVisualStyleBackColor = true;
            this._cSearchBtn.Click += new System.EventHandler(this.OnSearchClicked);
            // 
            // _cForwardBtn
            // 
            this._cForwardBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cForwardBtn.AutoSize = true;
            this._cForwardBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._cForwardBtn.Enabled = false;
            this._cForwardBtn.FlatAppearance.BorderSize = 0;
            this._cForwardBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cForwardBtn.Image = ((System.Drawing.Image)(resources.GetObject("_cForwardBtn.Image")));
            this._cForwardBtn.Location = new System.Drawing.Point(338, 27);
            this._cForwardBtn.Name = "_cForwardBtn";
            this._cForwardBtn.Size = new System.Drawing.Size(30, 30);
            this._cForwardBtn.TabIndex = 5;
            this._cToolTip.SetToolTip(this._cForwardBtn, "Forward");
            this._cForwardBtn.UseVisualStyleBackColor = true;
            this._cForwardBtn.Click += new System.EventHandler(this.OnForwardClicked);
            // 
            // _cBackBtn
            // 
            this._cBackBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cBackBtn.AutoSize = true;
            this._cBackBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._cBackBtn.Enabled = false;
            this._cBackBtn.FlatAppearance.BorderSize = 0;
            this._cBackBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cBackBtn.Image = ((System.Drawing.Image)(resources.GetObject("_cBackBtn.Image")));
            this._cBackBtn.Location = new System.Drawing.Point(302, 27);
            this._cBackBtn.Name = "_cBackBtn";
            this._cBackBtn.Size = new System.Drawing.Size(30, 30);
            this._cBackBtn.TabIndex = 4;
            this._cToolTip.SetToolTip(this._cBackBtn, "Back");
            this._cBackBtn.UseVisualStyleBackColor = true;
            this._cBackBtn.Click += new System.EventHandler(this.OnBackClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 504);
            this.Controls.Add(this._cBookmarkBtn);
            this.Controls.Add(this._cSearchText);
            this.Controls.Add(this._cSearchBtn);
            this.Controls.Add(this._cForwardBtn);
            this.Controls.Add(this._cBackBtn);
            this.Controls.Add(this._cTabs);
            this.Controls.Add(this._cDefList);
            this.Controls.Add(this._cMenuStrip);
            this.MainMenuStrip = this._cMenuStrip;
            this.Name = "MainForm";
            this.Text = "Logophi";
            this._cTabs.ResumeLayout(false);
            this._cTabSynonyms.ResumeLayout(false);
            this._cTabAntonyms.ResumeLayout(false);
            this._cMenuStrip.ResumeLayout(false);
            this._cMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox _cDefList;
        private System.Windows.Forms.TabControl _cTabs;
        private System.Windows.Forms.TabPage _cTabSynonyms;
        private System.Windows.Forms.TabPage _cTabAntonyms;
        private System.Windows.Forms.ListView _cSynonymList;
        private System.Windows.Forms.ListView _cAntonymList;
        private System.Windows.Forms.Button _cBackBtn;
        private System.Windows.Forms.Button _cForwardBtn;
        private System.Windows.Forms.Button _cSearchBtn;
        private System.Windows.Forms.ComboBox _cSearchText;
        private System.Windows.Forms.MenuStrip _cMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _cBookmarksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _cOptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _cBookmarksView;
        private System.Windows.Forms.ToolTip _cToolTip;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.Button _cBookmarkBtn;
    }
}

