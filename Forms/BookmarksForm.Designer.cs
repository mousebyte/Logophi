﻿namespace MouseNet.Logophi.Forms
{
    partial class BookmarksForm
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
            this._cBookmarksList = new System.Windows.Forms.ListBox();
            this._cRemoveBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _cBookmarksList
            // 
            this._cBookmarksList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._cBookmarksList.FormattingEnabled = true;
            this._cBookmarksList.Location = new System.Drawing.Point(12, 12);
            this._cBookmarksList.Name = "_cBookmarksList";
            this._cBookmarksList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this._cBookmarksList.Size = new System.Drawing.Size(186, 251);
            this._cBookmarksList.TabIndex = 0;
            this._cBookmarksList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.InvokeBookmarkActivated);
            // 
            // _cRemoveBtn
            // 
            this._cRemoveBtn.Location = new System.Drawing.Point(204, 12);
            this._cRemoveBtn.Name = "_cRemoveBtn";
            this._cRemoveBtn.Size = new System.Drawing.Size(31, 30);
            this._cRemoveBtn.TabIndex = 1;
            this._cRemoveBtn.Text = "-";
            this._cRemoveBtn.UseVisualStyleBackColor = true;
            this._cRemoveBtn.Click += new System.EventHandler(this.InvokeBookmarkRemoved);
            // 
            // BookmarksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 276);
            this.Controls.Add(this._cRemoveBtn);
            this.Controls.Add(this._cBookmarksList);
            this.Name = "BookmarksForm";
            this.Text = "Bookmarks";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox _cBookmarksList;
        private System.Windows.Forms.Button _cRemoveBtn;
    }
}