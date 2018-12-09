namespace Thesaurus.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._cDefList = new System.Windows.Forms.ListBox();
            this._cTabs = new System.Windows.Forms.TabControl();
            this._cTabSynonyms = new System.Windows.Forms.TabPage();
            this._cSynonymList = new System.Windows.Forms.ListView();
            this._cTabAntonyms = new System.Windows.Forms.TabPage();
            this._cAntonymList = new System.Windows.Forms.ListView();
            this._cSearchBtn = new System.Windows.Forms.Button();
            this._cForwardBtn = new System.Windows.Forms.Button();
            this._cBackBtn = new System.Windows.Forms.Button();
            this._cSearchText = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bookmarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._cTabs.SuspendLayout();
            this._cTabSynonyms.SuspendLayout();
            this._cTabAntonyms.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this._cTabs.Size = new System.Drawing.Size(407, 239);
            this._cTabs.TabIndex = 3;
            // 
            // _cTabSynonyms
            // 
            this._cTabSynonyms.Controls.Add(this._cSynonymList);
            this._cTabSynonyms.Location = new System.Drawing.Point(4, 22);
            this._cTabSynonyms.Name = "_cTabSynonyms";
            this._cTabSynonyms.Padding = new System.Windows.Forms.Padding(3);
            this._cTabSynonyms.Size = new System.Drawing.Size(399, 213);
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
            this._cSynonymList.Size = new System.Drawing.Size(393, 207);
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
            this._cTabAntonyms.Size = new System.Drawing.Size(430, 215);
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
            this._cAntonymList.Size = new System.Drawing.Size(424, 209);
            this._cAntonymList.TabIndex = 0;
            this._cAntonymList.UseCompatibleStateImageBehavior = false;
            this._cAntonymList.View = System.Windows.Forms.View.List;
            // 
            // _cSearchBtn
            // 
            this._cSearchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cSearchBtn.AutoSize = true;
            this._cSearchBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._cSearchBtn.FlatAppearance.BorderSize = 0;
            this._cSearchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._cSearchBtn.Image = global::Thesaurus.Forms.Properties.Resources.search;
            this._cSearchBtn.Location = new System.Drawing.Point(293, 27);
            this._cSearchBtn.Name = "_cSearchBtn";
            this._cSearchBtn.Size = new System.Drawing.Size(30, 30);
            this._cSearchBtn.TabIndex = 6;
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
            this._cForwardBtn.Location = new System.Drawing.Point(365, 27);
            this._cForwardBtn.Name = "_cForwardBtn";
            this._cForwardBtn.Size = new System.Drawing.Size(30, 30);
            this._cForwardBtn.TabIndex = 5;
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
            this._cBackBtn.Location = new System.Drawing.Point(329, 27);
            this._cBackBtn.Name = "_cBackBtn";
            this._cBackBtn.Size = new System.Drawing.Size(30, 30);
            this._cBackBtn.TabIndex = 4;
            this._cBackBtn.UseVisualStyleBackColor = true;
            this._cBackBtn.Click += new System.EventHandler(this.OnBackClicked);
            // 
            // _cSearchText
            // 
            this._cSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cSearchText.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._cSearchText.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this._cSearchText.Location = new System.Drawing.Point(7, 33);
            this._cSearchText.Name = "_cSearchText";
            this._cSearchText.Size = new System.Drawing.Size(280, 21);
            this._cSearchText.Sorted = true;
            this._cSearchText.TabIndex = 7;
            this._cSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnSearchTextReturnPressed);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bookmarksToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(407, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bookmarksToolStripMenuItem
            // 
            this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
            this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.bookmarksToolStripMenuItem.Text = "Bookmarks";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 504);
            this.Controls.Add(this._cSearchText);
            this.Controls.Add(this._cSearchBtn);
            this.Controls.Add(this._cForwardBtn);
            this.Controls.Add(this._cBackBtn);
            this.Controls.Add(this._cTabs);
            this.Controls.Add(this._cDefList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Logophi";
            this._cTabs.ResumeLayout(false);
            this._cTabSynonyms.ResumeLayout(false);
            this._cTabAntonyms.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bookmarksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}

