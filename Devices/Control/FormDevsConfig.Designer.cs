﻿namespace Devices.Control
{
    partial class FormDevsConfig
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
            this.ucDevicesCollectionConfig1 = new Devices.UCDevicesCollectionConfig();
            this.SuspendLayout();
            // 
            // ucDevicesCollectionConfig1
            // 
            this.ucDevicesCollectionConfig1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDevicesCollectionConfig1.Location = new System.Drawing.Point(0, 0);
            this.ucDevicesCollectionConfig1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucDevicesCollectionConfig1.Name = "ucDevicesCollectionConfig1";
            this.ucDevicesCollectionConfig1.Size = new System.Drawing.Size(620, 472);
            this.ucDevicesCollectionConfig1.TabIndex = 0;
            // 
            // FormDevsConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 472);
            this.Controls.Add(this.ucDevicesCollectionConfig1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormDevsConfig";
            this.Text = "设备配置";
            this.ResumeLayout(false);

        }

        #endregion

        private UCDevicesCollectionConfig ucDevicesCollectionConfig1;
    }
}