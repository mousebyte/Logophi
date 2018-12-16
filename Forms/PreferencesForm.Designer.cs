namespace MouseNet.Logophi.Forms
{
    partial class PreferencesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this._cTabs = new System.Windows.Forms.TabControl();
            this._cTabGeneral = new System.Windows.Forms.TabPage();
            this._cAutoRun = new System.Windows.Forms.CheckBox();
            this._cDeleteCache = new System.Windows.Forms.Button();
            this._cPersistCache = new System.Windows.Forms.CheckBox();
            this._cTabSearchHistory = new System.Windows.Forms.TabPage();
            this._cClearHistory = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this._lblMaxHistory = new System.Windows.Forms.Label();
            this._cSaveHistory = new System.Windows.Forms.CheckBox();
            this._cCancelBtn = new System.Windows.Forms.Button();
            this._cAcceptBtn = new System.Windows.Forms.Button();
            this._cAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this._cTabs.SuspendLayout();
            this._cTabGeneral.SuspendLayout();
            this._cTabSearchHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // _cTabs
            // 
            this._cTabs.Controls.Add(this._cTabGeneral);
            this._cTabs.Controls.Add(this._cTabSearchHistory);
            this._cTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this._cTabs.Location = new System.Drawing.Point(0, 0);
            this._cTabs.Name = "_cTabs";
            this._cTabs.SelectedIndex = 0;
            this._cTabs.Size = new System.Drawing.Size(298, 156);
            this._cTabs.TabIndex = 0;
            // 
            // _cTabGeneral
            // 
            this._cTabGeneral.Controls.Add(this._cAlwaysOnTop);
            this._cTabGeneral.Controls.Add(this._cAutoRun);
            this._cTabGeneral.Controls.Add(this._cDeleteCache);
            this._cTabGeneral.Controls.Add(this._cPersistCache);
            this._cTabGeneral.Location = new System.Drawing.Point(4, 22);
            this._cTabGeneral.Name = "_cTabGeneral";
            this._cTabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this._cTabGeneral.Size = new System.Drawing.Size(290, 130);
            this._cTabGeneral.TabIndex = 0;
            this._cTabGeneral.Text = "General";
            this._cTabGeneral.UseVisualStyleBackColor = true;
            // 
            // _cAutoRun
            // 
            this._cAutoRun.AutoSize = true;
            this._cAutoRun.Location = new System.Drawing.Point(20, 46);
            this._cAutoRun.Name = "_cAutoRun";
            this._cAutoRun.Size = new System.Drawing.Size(134, 17);
            this._cAutoRun.TabIndex = 1;
            this._cAutoRun.Text = "Run Logophi at startup";
            this._cAutoRun.UseVisualStyleBackColor = true;
            // 
            // _cDeleteCache
            // 
            this._cDeleteCache.Location = new System.Drawing.Point(166, 12);
            this._cDeleteCache.Name = "_cDeleteCache";
            this._cDeleteCache.Size = new System.Drawing.Size(82, 23);
            this._cDeleteCache.TabIndex = 1;
            this._cDeleteCache.Text = "&Delete Cache";
            this._cDeleteCache.UseVisualStyleBackColor = true;
            this._cDeleteCache.Click += new System.EventHandler(this.InvokeDeleteCacheClicked);
            // 
            // _cPersistCache
            // 
            this._cPersistCache.AutoSize = true;
            this._cPersistCache.Location = new System.Drawing.Point(20, 16);
            this._cPersistCache.Name = "_cPersistCache";
            this._cPersistCache.Size = new System.Drawing.Size(140, 17);
            this._cPersistCache.TabIndex = 0;
            this._cPersistCache.Text = "Enable persistent cache";
            this._cPersistCache.UseVisualStyleBackColor = true;
            // 
            // _cTabSearchHistory
            // 
            this._cTabSearchHistory.Controls.Add(this._cClearHistory);
            this._cTabSearchHistory.Controls.Add(this.numericUpDown1);
            this._cTabSearchHistory.Controls.Add(this._lblMaxHistory);
            this._cTabSearchHistory.Controls.Add(this._cSaveHistory);
            this._cTabSearchHistory.Location = new System.Drawing.Point(4, 22);
            this._cTabSearchHistory.Name = "_cTabSearchHistory";
            this._cTabSearchHistory.Padding = new System.Windows.Forms.Padding(3);
            this._cTabSearchHistory.Size = new System.Drawing.Size(290, 130);
            this._cTabSearchHistory.TabIndex = 1;
            this._cTabSearchHistory.Text = "Search History";
            this._cTabSearchHistory.UseVisualStyleBackColor = true;
            // 
            // _cClearHistory
            // 
            this._cClearHistory.Location = new System.Drawing.Point(20, 66);
            this._cClearHistory.Name = "_cClearHistory";
            this._cClearHistory.Size = new System.Drawing.Size(75, 23);
            this._cClearHistory.TabIndex = 3;
            this._cClearHistory.Text = "&Clear History";
            this._cClearHistory.UseVisualStyleBackColor = true;
            this._cClearHistory.Click += new System.EventHandler(this.InvokeDeleteHistoryClicked);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(226, 41);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(45, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // _lblMaxHistory
            // 
            this._lblMaxHistory.AutoSize = true;
            this._lblMaxHistory.Location = new System.Drawing.Point(17, 43);
            this._lblMaxHistory.Name = "_lblMaxHistory";
            this._lblMaxHistory.Size = new System.Drawing.Size(203, 13);
            this._lblMaxHistory.TabIndex = 1;
            this._lblMaxHistory.Text = "Maximum number of history items to keep:";
            // 
            // _cSaveHistory
            // 
            this._cSaveHistory.AutoSize = true;
            this._cSaveHistory.Checked = true;
            this._cSaveHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cSaveHistory.Location = new System.Drawing.Point(20, 16);
            this._cSaveHistory.Name = "_cSaveHistory";
            this._cSaveHistory.Size = new System.Drawing.Size(171, 17);
            this._cSaveHistory.TabIndex = 0;
            this._cSaveHistory.Text = "Save history between sessions";
            this._cSaveHistory.UseVisualStyleBackColor = true;
            // 
            // _cCancelBtn
            // 
            this._cCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cCancelBtn.Location = new System.Drawing.Point(130, 162);
            this._cCancelBtn.Name = "_cCancelBtn";
            this._cCancelBtn.Size = new System.Drawing.Size(75, 23);
            this._cCancelBtn.TabIndex = 1;
            this._cCancelBtn.Text = "&Cancel";
            this._cCancelBtn.UseVisualStyleBackColor = true;
            // 
            // _cAcceptBtn
            // 
            this._cAcceptBtn.Location = new System.Drawing.Point(211, 162);
            this._cAcceptBtn.Name = "_cAcceptBtn";
            this._cAcceptBtn.Size = new System.Drawing.Size(75, 23);
            this._cAcceptBtn.TabIndex = 2;
            this._cAcceptBtn.Text = "&Ok";
            this._cAcceptBtn.UseVisualStyleBackColor = true;
            // 
            // _cAlwaysOnTop
            // 
            this._cAlwaysOnTop.AutoSize = true;
            this._cAlwaysOnTop.Location = new System.Drawing.Point(20, 76);
            this._cAlwaysOnTop.Name = "_cAlwaysOnTop";
            this._cAlwaysOnTop.Size = new System.Drawing.Size(92, 17);
            this._cAlwaysOnTop.TabIndex = 2;
            this._cAlwaysOnTop.Text = "Always on top";
            this._cAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // PreferencesForm
            // 
            this.AcceptButton = this._cAcceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cCancelBtn;
            this.ClientSize = new System.Drawing.Size(298, 197);
            this.Controls.Add(this._cAcceptBtn);
            this.Controls.Add(this._cCancelBtn);
            this.Controls.Add(this._cTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PreferencesForm";
            this.ShowInTaskbar = false;
            this.Text = "Preferences";
            this._cTabs.ResumeLayout(false);
            this._cTabGeneral.ResumeLayout(false);
            this._cTabGeneral.PerformLayout();
            this._cTabSearchHistory.ResumeLayout(false);
            this._cTabSearchHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _cTabs;
        private System.Windows.Forms.TabPage _cTabGeneral;
        private System.Windows.Forms.TabPage _cTabSearchHistory;
        private System.Windows.Forms.Button _cClearHistory;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label _lblMaxHistory;
        private System.Windows.Forms.CheckBox _cSaveHistory;
        private System.Windows.Forms.CheckBox _cPersistCache;
        private System.Windows.Forms.Button _cDeleteCache;
        private System.Windows.Forms.CheckBox _cAutoRun;
        private System.Windows.Forms.Button _cCancelBtn;
        private System.Windows.Forms.Button _cAcceptBtn;
        private System.Windows.Forms.CheckBox _cAlwaysOnTop;
    }
}