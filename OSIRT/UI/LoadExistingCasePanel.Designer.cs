﻿namespace OSIRT.UI
{
    partial class LoadExistingCasePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.uiWhySha512Label = new System.Windows.Forms.Label();
            this.uiHashProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.uiFileNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.uiFileHashTextBox = new System.Windows.Forms.TextBox();
            this.uiHashLabel = new System.Windows.Forms.Label();
            this.uiLastModifiedTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.uiInvalidPasswordLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uiOpenCaseButton = new System.Windows.Forms.Button();
            this.uiPasswordTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(997, 630);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(997, 77);
            this.panel3.TabIndex = 15;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 558);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(997, 72);
            this.panel2.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.uiWhySha512Label);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.uiFileNameTextBox);
            this.groupBox2.Controls.Add(this.uiHashProgressBar);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.uiLastModifiedTextBox);
            this.groupBox2.Controls.Add(this.uiHashLabel);
            this.groupBox2.Controls.Add(this.uiFileHashTextBox);
            this.groupBox2.Location = new System.Drawing.Point(211, 298);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(600, 112);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Case Container Details";
            // 
            // uiWhySha512Label
            // 
            this.uiWhySha512Label.AutoSize = true;
            this.uiWhySha512Label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiWhySha512Label.Location = new System.Drawing.Point(575, 110);
            this.uiWhySha512Label.Name = "uiWhySha512Label";
            this.uiWhySha512Label.Size = new System.Drawing.Size(19, 13);
            this.uiWhySha512Label.TabIndex = 13;
            this.uiWhySha512Label.Text = "[?]";
            this.uiWhySha512Label.Visible = false;
            this.uiWhySha512Label.Click += new System.EventHandler(this.uiWhySha512Label_Click);
            // 
            // uiHashProgressBar
            // 
            this.uiHashProgressBar.Location = new System.Drawing.Point(6, 126);
            this.uiHashProgressBar.MarqueeAnimationSpeed = 10;
            this.uiHashProgressBar.Name = "uiHashProgressBar";
            this.uiHashProgressBar.Size = new System.Drawing.Size(588, 19);
            this.uiHashProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.uiHashProgressBar.TabIndex = 12;
            this.uiHashProgressBar.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Case Container Name";
            // 
            // uiFileNameTextBox
            // 
            this.uiFileNameTextBox.Location = new System.Drawing.Point(6, 48);
            this.uiFileNameTextBox.Name = "uiFileNameTextBox";
            this.uiFileNameTextBox.ReadOnly = true;
            this.uiFileNameTextBox.Size = new System.Drawing.Size(588, 20);
            this.uiFileNameTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Last Modified";
            // 
            // uiFileHashTextBox
            // 
            this.uiFileHashTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiFileHashTextBox.Location = new System.Drawing.Point(6, 126);
            this.uiFileHashTextBox.Name = "uiFileHashTextBox";
            this.uiFileHashTextBox.ReadOnly = true;
            this.uiFileHashTextBox.Size = new System.Drawing.Size(588, 20);
            this.uiFileHashTextBox.TabIndex = 7;
            this.uiFileHashTextBox.Visible = false;
            // 
            // uiHashLabel
            // 
            this.uiHashLabel.AutoSize = true;
            this.uiHashLabel.Location = new System.Drawing.Point(6, 110);
            this.uiHashLabel.Name = "uiHashLabel";
            this.uiHashLabel.Size = new System.Drawing.Size(32, 13);
            this.uiHashLabel.TabIndex = 10;
            this.uiHashLabel.Text = "Hash";
            this.uiHashLabel.Visible = false;
            // 
            // uiLastModifiedTextBox
            // 
            this.uiLastModifiedTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiLastModifiedTextBox.Location = new System.Drawing.Point(6, 87);
            this.uiLastModifiedTextBox.Name = "uiLastModifiedTextBox";
            this.uiLastModifiedTextBox.ReadOnly = true;
            this.uiLastModifiedTextBox.Size = new System.Drawing.Size(588, 20);
            this.uiLastModifiedTextBox.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.uiInvalidPasswordLabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.uiOpenCaseButton);
            this.groupBox1.Controls.Add(this.uiPasswordTextBox);
            this.groupBox1.Location = new System.Drawing.Point(354, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 133);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // uiInvalidPasswordLabel
            // 
            this.uiInvalidPasswordLabel.AutoSize = true;
            this.uiInvalidPasswordLabel.ForeColor = System.Drawing.Color.Red;
            this.uiInvalidPasswordLabel.Location = new System.Drawing.Point(104, 117);
            this.uiInvalidPasswordLabel.Name = "uiInvalidPasswordLabel";
            this.uiInvalidPasswordLabel.Size = new System.Drawing.Size(87, 13);
            this.uiInvalidPasswordLabel.TabIndex = 6;
            this.uiInvalidPasswordLabel.Text = "Invalid Password";
            this.uiInvalidPasswordLabel.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // uiOpenCaseButton
            // 
            this.uiOpenCaseButton.Location = new System.Drawing.Point(107, 72);
            this.uiOpenCaseButton.Name = "uiOpenCaseButton";
            this.uiOpenCaseButton.Size = new System.Drawing.Size(75, 23);
            this.uiOpenCaseButton.TabIndex = 5;
            this.uiOpenCaseButton.Text = "Open Case";
            this.uiOpenCaseButton.UseVisualStyleBackColor = true;
            this.uiOpenCaseButton.Click += new System.EventHandler(this.uiOpenCaseButton_Click);
            // 
            // uiPasswordTextBox
            // 
            this.uiPasswordTextBox.Location = new System.Drawing.Point(31, 46);
            this.uiPasswordTextBox.Name = "uiPasswordTextBox";
            this.uiPasswordTextBox.Size = new System.Drawing.Size(244, 20);
            this.uiPasswordTextBox.TabIndex = 3;
            // 
            // LoadExistingCasePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "LoadExistingCasePanel";
            this.Size = new System.Drawing.Size(997, 630);
            this.Load += new System.EventHandler(this.LoadExistingCasePanel_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button uiOpenCaseButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uiPasswordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox uiLastModifiedTextBox;
        private System.Windows.Forms.TextBox uiFileHashTextBox;
        private System.Windows.Forms.TextBox uiFileNameTextBox;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label uiHashLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar uiHashProgressBar;
        private System.Windows.Forms.Label uiInvalidPasswordLabel;
        private System.Windows.Forms.Label uiWhySha512Label;
    }
}
