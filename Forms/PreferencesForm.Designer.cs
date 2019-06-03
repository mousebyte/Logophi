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
            this._cAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this._cAutoRun = new System.Windows.Forms.CheckBox();
            this._cDeleteCache = new System.Windows.Forms.Button();
            this._cPersistCache = new System.Windows.Forms.CheckBox();
            this._cTabSearchHistory = new System.Windows.Forms.TabPage();
            this._cClearHistory = new System.Windows.Forms.Button();
            this._lblMaxHistory = new System.Windows.Forms.Label();
            this._cMaxHistory = new System.Windows.Forms.NumericUpDown();
            this._cSaveHistory = new System.Windows.Forms.CheckBox();
            this._cTabHotkey = new System.Windows.Forms.TabPage();
            this._cHotkeyEdit = new MouseNet.Forms.Controls.HotkeyEdit();
            this._cEnableHotkey = new System.Windows.Forms.CheckBox();
            this._cCancelBtn = new System.Windows.Forms.Button();
            this._cAcceptBtn = new System.Windows.Forms.Button();
            this._cTabs.SuspendLayout();
            this._cTabGeneral.SuspendLayout();
            this._cTabSearchHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cMaxHistory)).BeginInit();
            this._cTabHotkey.SuspendLayout();
            this.SuspendLayout();
            // 
            // _cTabs
            // 
            this._cTabs.Controls.Add(this._cTabGeneral);
            this._cTabs.Controls.Add(this._cTabSearchHistory);
            this._cTabs.Controls.Add(this._cTabHotkey);
            this._cTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this._cTabs.Location = new System.Drawing.Point(0, 0);
            this._cTabs.Name = "_cTabs";
            this._cTabs.SelectedIndex = 0;
            this._cTabs.Size = new System.Drawing.Size(298, 139);
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
            this._cTabGeneral.Size = new System.Drawing.Size(290, 113);
            this._cTabGeneral.TabIndex = 0;
            this._cTabGeneral.Text = "General";
            this._cTabGeneral.UseVisualStyleBackColor = true;
            // 
            // _cAlwaysOnTop
            // 
            this._cAlwaysOnTop.AutoSize = true;
            this._cAlwaysOnTop.Checked = global::MouseNet.Logophi.Properties.Settings.Default.AlwaysOnTop;
            this._cAlwaysOnTop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MouseNet.Logophi.Properties.Settings.Default, "AlwaysOnTop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cAlwaysOnTop.Location = new System.Drawing.Point(20, 82);
            this._cAlwaysOnTop.Name = "_cAlwaysOnTop";
            this._cAlwaysOnTop.Size = new System.Drawing.Size(92, 17);
            this._cAlwaysOnTop.TabIndex = 2;
            this._cAlwaysOnTop.Text = "Always on top";
            this._cAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // _cAutoRun
            // 
            this._cAutoRun.AutoSize = true;
            this._cAutoRun.Checked = global::MouseNet.Logophi.Properties.Settings.Default.AutoRun;
            this._cAutoRun.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MouseNet.Logophi.Properties.Settings.Default, "AutoRun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cAutoRun.Location = new System.Drawing.Point(20, 49);
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
            this._cPersistCache.Checked = global::MouseNet.Logophi.Properties.Settings.Default.PersistentCache;
            this._cPersistCache.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MouseNet.Logophi.Properties.Settings.Default, "PersistentCache", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
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
            this._cTabSearchHistory.Controls.Add(this._lblMaxHistory);
            this._cTabSearchHistory.Controls.Add(this._cMaxHistory);
            this._cTabSearchHistory.Controls.Add(this._cSaveHistory);
            this._cTabSearchHistory.Location = new System.Drawing.Point(4, 22);
            this._cTabSearchHistory.Name = "_cTabSearchHistory";
            this._cTabSearchHistory.Padding = new System.Windows.Forms.Padding(3);
            this._cTabSearchHistory.Size = new System.Drawing.Size(290, 113);
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
            // _lblMaxHistory
            // 
            this._lblMaxHistory.AutoSize = true;
            this._lblMaxHistory.Location = new System.Drawing.Point(17, 43);
            this._lblMaxHistory.Name = "_lblMaxHistory";
            this._lblMaxHistory.Size = new System.Drawing.Size(203, 13);
            this._lblMaxHistory.TabIndex = 1;
            this._lblMaxHistory.Text = "Maximum number of history items to keep:";
            // 
            // _cMaxHistory
            // 
            this._cMaxHistory.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MouseNet.Logophi.Properties.Settings.Default, "MaxHistory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cMaxHistory.Location = new System.Drawing.Point(226, 41);
            this._cMaxHistory.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._cMaxHistory.Name = "_cMaxHistory";
            this._cMaxHistory.Size = new System.Drawing.Size(45, 20);
            this._cMaxHistory.TabIndex = 2;
            this._cMaxHistory.Value = global::MouseNet.Logophi.Properties.Settings.Default.MaxHistory;
            // 
            // _cSaveHistory
            // 
            this._cSaveHistory.AutoSize = true;
            this._cSaveHistory.Checked = global::MouseNet.Logophi.Properties.Settings.Default.SaveHistory;
            this._cSaveHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this._cSaveHistory.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MouseNet.Logophi.Properties.Settings.Default, "SaveHistory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cSaveHistory.Location = new System.Drawing.Point(20, 16);
            this._cSaveHistory.Name = "_cSaveHistory";
            this._cSaveHistory.Size = new System.Drawing.Size(171, 17);
            this._cSaveHistory.TabIndex = 0;
            this._cSaveHistory.Text = "Save history between sessions";
            this._cSaveHistory.UseVisualStyleBackColor = true;
            // 
            // _cTabHotkey
            // 
            this._cTabHotkey.Controls.Add(this._cHotkeyEdit);
            this._cTabHotkey.Controls.Add(this._cEnableHotkey);
            this._cTabHotkey.Location = new System.Drawing.Point(4, 22);
            this._cTabHotkey.Name = "_cTabHotkey";
            this._cTabHotkey.Padding = new System.Windows.Forms.Padding(3);
            this._cTabHotkey.Size = new System.Drawing.Size(290, 113);
            this._cTabHotkey.TabIndex = 2;
            this._cTabHotkey.Text = "Hotkey";
            this._cTabHotkey.UseVisualStyleBackColor = true;
            // 
            // _cHotkeyEdit
            // 
            this._cHotkeyEdit.AllowCtrlOnlyHotkeys = false;
            this._cHotkeyEdit.AllowShiftOnlyHotkeys = false;
            this._cHotkeyEdit.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::MouseNet.Logophi.Properties.Settings.Default, "Hotkey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cHotkeyEdit.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", global::MouseNet.Logophi.Properties.Settings.Default, "EnableHotkey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cHotkeyEdit.Enabled = global::MouseNet.Logophi.Properties.Settings.Default.EnableHotkey;
            this._cHotkeyEdit.Location = new System.Drawing.Point(151, 41);
            this._cHotkeyEdit.Name = "_cHotkeyEdit";
            this._cHotkeyEdit.Size = new System.Drawing.Size(133, 29);
            this._cHotkeyEdit.TabIndex = 1;
            this._cHotkeyEdit.Value = global::MouseNet.Logophi.Properties.Settings.Default.Hotkey;
            // 
            // _cEnableHotkey
            // 
            this._cEnableHotkey.AutoSize = true;
            this._cEnableHotkey.Checked = global::MouseNet.Logophi.Properties.Settings.Default.EnableHotkey;
            this._cEnableHotkey.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::MouseNet.Logophi.Properties.Settings.Default, "EnableHotkey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this._cEnableHotkey.Location = new System.Drawing.Point(20, 48);
            this._cEnableHotkey.Name = "_cEnableHotkey";
            this._cEnableHotkey.Size = new System.Drawing.Size(125, 17);
            this._cEnableHotkey.TabIndex = 0;
            this._cEnableHotkey.Text = "Enable global hotkey";
            this._cEnableHotkey.UseVisualStyleBackColor = true;
            // 
            // _cCancelBtn
            // 
            this._cCancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cCancelBtn.Location = new System.Drawing.Point(130, 145);
            this._cCancelBtn.Name = "_cCancelBtn";
            this._cCancelBtn.Size = new System.Drawing.Size(75, 23);
            this._cCancelBtn.TabIndex = 1;
            this._cCancelBtn.Text = "&Cancel";
            this._cCancelBtn.UseVisualStyleBackColor = true;
            // 
            // _cAcceptBtn
            // 
            this._cAcceptBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._cAcceptBtn.Location = new System.Drawing.Point(211, 145);
            this._cAcceptBtn.Name = "_cAcceptBtn";
            this._cAcceptBtn.Size = new System.Drawing.Size(75, 23);
            this._cAcceptBtn.TabIndex = 2;
            this._cAcceptBtn.Text = "&Ok";
            this._cAcceptBtn.UseVisualStyleBackColor = true;
            // 
            // PreferencesForm
            // 
            this.AcceptButton = this._cAcceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cCancelBtn;
            this.ClientSize = new System.Drawing.Size(298, 177);
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
            ((System.ComponentModel.ISupportInitialize)(this._cMaxHistory)).EndInit();
            this._cTabHotkey.ResumeLayout(false);
            this._cTabHotkey.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _cTabs;
        private System.Windows.Forms.TabPage _cTabGeneral;
        private System.Windows.Forms.TabPage _cTabSearchHistory;
        private System.Windows.Forms.Button _cClearHistory;
        private System.Windows.Forms.NumericUpDown _cMaxHistory;
        private System.Windows.Forms.Label _lblMaxHistory;
        private System.Windows.Forms.CheckBox _cSaveHistory;
        private System.Windows.Forms.CheckBox _cPersistCache;
        private System.Windows.Forms.Button _cDeleteCache;
        private System.Windows.Forms.CheckBox _cAutoRun;
        private System.Windows.Forms.Button _cCancelBtn;
        private System.Windows.Forms.Button _cAcceptBtn;
        private System.Windows.Forms.CheckBox _cAlwaysOnTop;
        private System.Windows.Forms.TabPage _cTabHotkey;
        private System.Windows.Forms.CheckBox _cEnableHotkey;
        private MouseNet.Forms.Controls.HotkeyEdit _cHotkeyEdit;
    }
}