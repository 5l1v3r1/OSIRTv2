﻿namespace OSIRT.UI.Splash
{
    partial class SplashScreen
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
            this.uiSplashLoadingProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.uiProgressLabel = new System.Windows.Forms.Label();
            this.uiVersionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uiSplashLoadingProgressBar
            // 
            this.uiSplashLoadingProgressBar.Location = new System.Drawing.Point(8, 235);
            this.uiSplashLoadingProgressBar.MarqueeAnimationSpeed = 10;
            this.uiSplashLoadingProgressBar.Maximum = 1000;
            this.uiSplashLoadingProgressBar.Name = "uiSplashLoadingProgressBar";
            this.uiSplashLoadingProgressBar.Size = new System.Drawing.Size(790, 23);
            this.uiSplashLoadingProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.uiSplashLoadingProgressBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(641, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Joseph Williams";
            // 
            // uiProgressLabel
            // 
            this.uiProgressLabel.AutoSize = true;
            this.uiProgressLabel.BackColor = System.Drawing.Color.Transparent;
            this.uiProgressLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.uiProgressLabel.Location = new System.Drawing.Point(538, 219);
            this.uiProgressLabel.Name = "uiProgressLabel";
            this.uiProgressLabel.Size = new System.Drawing.Size(224, 13);
            this.uiProgressLabel.TabIndex = 2;
            this.uiProgressLabel.Text = "Checking previous case closed successfully...";
            // 
            // uiVersionLabel
            // 
            this.uiVersionLabel.AutoSize = true;
            this.uiVersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.uiVersionLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.uiVersionLabel.Location = new System.Drawing.Point(1, 3);
            this.uiVersionLabel.Name = "uiVersionLabel";
            this.uiVersionLabel.Size = new System.Drawing.Size(126, 13);
            this.uiVersionLabel.TabIndex = 3;
            this.uiVersionLabel.Text = "v 3.3.933 (Cef55) - BETA";
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OSIRT.Properties.Resources.splash_screen_small;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(803, 261);
            this.Controls.Add(this.uiVersionLabel);
            this.Controls.Add(this.uiProgressLabel);
            this.Controls.Add(this.uiSplashLoadingProgressBar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar uiSplashLoadingProgressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label uiProgressLabel;
        private System.Windows.Forms.Label uiVersionLabel;
    }
}