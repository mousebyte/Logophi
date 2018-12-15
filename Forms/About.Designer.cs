namespace MouseNet.Logophi.Forms
{
    partial class About
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._lblProduct = new System.Windows.Forms.Label();
            this._lblVersion = new System.Windows.Forms.Label();
            this._lblAbout = new MouseNet.Forms.Controls.WrappingLabel();
            this._lblGithubLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MouseNet.Logophi.Properties.Resources.logophi1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // _lblProduct
            // 
            this._lblProduct.AutoSize = true;
            this._lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblProduct.Location = new System.Drawing.Point(274, 12);
            this._lblProduct.Name = "_lblProduct";
            this._lblProduct.Size = new System.Drawing.Size(191, 39);
            this._lblProduct.TabIndex = 1;
            this._lblProduct.Text = "_lblProduct";
            // 
            // _lblVersion
            // 
            this._lblVersion.AutoSize = true;
            this._lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblVersion.ForeColor = System.Drawing.SystemColors.GrayText;
            this._lblVersion.Location = new System.Drawing.Point(278, 67);
            this._lblVersion.Name = "_lblVersion";
            this._lblVersion.Size = new System.Drawing.Size(58, 13);
            this._lblVersion.TabIndex = 2;
            this._lblVersion.Text = "_lblVersion";
            // 
            // _lblAbout
            // 
            this._lblAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblAbout.Location = new System.Drawing.Point(278, 98);
            this._lblAbout.Name = "_lblAbout";
            this._lblAbout.Size = new System.Drawing.Size(202, 15);
            this._lblAbout.TabIndex = 3;
            this._lblAbout.Text = "_lblAbout";
            // 
            // _lblGithubLink
            // 
            this._lblGithubLink.AutoSize = true;
            this._lblGithubLink.LinkArea = new System.Windows.Forms.LinkArea(21, 6);
            this._lblGithubLink.Location = new System.Drawing.Point(281, 251);
            this._lblGithubLink.Name = "_lblGithubLink";
            this._lblGithubLink.Size = new System.Drawing.Size(134, 17);
            this._lblGithubLink.TabIndex = 4;
            this._lblGithubLink.TabStop = true;
            this._lblGithubLink.Text = "Visit the project on Github";
            this._lblGithubLink.UseCompatibleTextRendering = true;
            this._lblGithubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 283);
            this.Controls.Add(this._lblGithubLink);
            this.Controls.Add(this._lblAbout);
            this.Controls.Add(this._lblVersion);
            this.Controls.Add(this._lblProduct);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "About";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label _lblProduct;
        private System.Windows.Forms.Label _lblVersion;
        private MouseNet.Forms.Controls.WrappingLabel _lblAbout;
        private System.Windows.Forms.LinkLabel _lblGithubLink;
    }
}