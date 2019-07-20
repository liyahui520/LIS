namespace Devices.Fujifilm
{
    partial class UCDRI_CHEM_NX500iVCConfig
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.radioButtonRS232 = new System.Windows.Forms.RadioButton();
            this.ucSerialPortConfig1 = new Devices.UCSerialPortConfig();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioButtonRS232);
            this.panel3.Controls.Add(this.ucSerialPortConfig1);
            this.panel3.Controls.Add(this.radioButtonTCP);
            // 
            // radioButtonRS232
            // 
            this.radioButtonRS232.AutoSize = true;
            this.radioButtonRS232.Checked = true;
            this.radioButtonRS232.Location = new System.Drawing.Point(74, 21);
            this.radioButtonRS232.Name = "radioButtonRS232";
            this.radioButtonRS232.Size = new System.Drawing.Size(58, 19);
            this.radioButtonRS232.TabIndex = 2;
            this.radioButtonRS232.TabStop = true;
            this.radioButtonRS232.Text = "串口";
            this.radioButtonRS232.UseVisualStyleBackColor = true;
            // 
            // ucSerialPortConfig1
            // 
            this.ucSerialPortConfig1.Location = new System.Drawing.Point(16, 59);
            this.ucSerialPortConfig1.Name = "ucSerialPortConfig1";
            this.ucSerialPortConfig1.Size = new System.Drawing.Size(374, 188);
            this.ucSerialPortConfig1.TabIndex = 3;
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.AutoSize = true;
            this.radioButtonTCP.Location = new System.Drawing.Point(151, 21);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(58, 19);
            this.radioButtonTCP.TabIndex = 4;
            this.radioButtonTCP.TabStop = true;
            this.radioButtonTCP.Text = "网口";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            this.radioButtonTCP.Visible = false;
            // 
            // NewUCNX500IVCConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "NewUCNX500IVCConfig";
            this.Load += new System.EventHandler(this.NewUCNX500IVCConfig_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonRS232;
        private UCSerialPortConfig ucSerialPortConfig1;
        private System.Windows.Forms.RadioButton radioButtonTCP;

    }
}
