namespace BDHero.Plugin.FFmpegMuxer
{
    partial class PluginInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginInfoForm));
            this.lblFFMpegVer = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblMkvPropEditVer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFFMpegVer
            // 
            this.lblFFMpegVer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFFMpegVer.Location = new System.Drawing.Point(13, 29);
            this.lblFFMpegVer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFFMpegVer.Name = "lblFFMpegVer";
            this.lblFFMpegVer.Size = new System.Drawing.Size(651, 133);
            this.lblFFMpegVer.TabIndex = 5;
            this.lblFFMpegVer.Text = "version -?-";
            this.lblFFMpegVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(552, 313);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 35);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblMkvPropEditVer
            // 
            this.lblMkvPropEditVer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMkvPropEditVer.Location = new System.Drawing.Point(13, 200);
            this.lblMkvPropEditVer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMkvPropEditVer.Name = "lblMkvPropEditVer";
            this.lblMkvPropEditVer.Size = new System.Drawing.Size(651, 95);
            this.lblMkvPropEditVer.TabIndex = 7;
            this.lblMkvPropEditVer.Text = "version -?-";
            this.lblMkvPropEditVer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "ffmpeg.exe version:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "mkvpropedit.exe version:";
            // 
            // PluginInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 362);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMkvPropEditVer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblFFMpegVer);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginInfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Plugin information";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PluginInfoForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFFMpegVer;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblMkvPropEditVer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}