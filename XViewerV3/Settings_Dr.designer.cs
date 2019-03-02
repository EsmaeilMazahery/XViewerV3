namespace XViewerV3
{
    partial class Settings_Dr
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_filespath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txt_new = new System.Windows.Forms.TextBox();
            this.btn_filespath = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_virtualapp = new System.Windows.Forms.TextBox();
            this.btn_virtualapp = new System.Windows.Forms.Button();
            this.txt_drivelater = new System.Windows.Forms.TextBox();
            this.txt_ipserver = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_testnet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "مسیر ذخیره فایل بیماران";
            this.toolTip1.SetToolTip(this.label2, "مکانی که اطلاعات بیمارانی ذخیره میگردد");
            // 
            // txt_filespath
            // 
            this.txt_filespath.Location = new System.Drawing.Point(88, 97);
            this.txt_filespath.Name = "txt_filespath";
            this.txt_filespath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_filespath.Size = new System.Drawing.Size(355, 22);
            this.txt_filespath.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "مسیر پیشفرض  پوشه بیمار جدید";
            this.toolTip1.SetToolTip(this.label3, "برای افزودن اطلاعات بیمار جدید از این مکان استفاده می شود");
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(234, 300);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 33);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "ذخیره";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(153, 300);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 33);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "انصراف";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "درایو";
            this.toolTip1.SetToolTip(this.label1, "به صورت زیر مشخص شود\r\nمثال:\r\nc:");
            // 
            // txt_new
            // 
            this.txt_new.Location = new System.Drawing.Point(88, 163);
            this.txt_new.Name = "txt_new";
            this.txt_new.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_new.Size = new System.Drawing.Size(355, 22);
            this.txt_new.TabIndex = 1;
            // 
            // btn_filespath
            // 
            this.btn_filespath.Location = new System.Drawing.Point(22, 93);
            this.btn_filespath.Name = "btn_filespath";
            this.btn_filespath.Size = new System.Drawing.Size(60, 29);
            this.btn_filespath.TabIndex = 3;
            this.btn_filespath.Text = "انتخاب";
            this.btn_filespath.UseVisualStyleBackColor = true;
            this.btn_filespath.Click += new System.EventHandler(this.btn_filespath_Click);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(22, 159);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(60, 29);
            this.btn_new.TabIndex = 3;
            this.btn_new.Text = "انتخاب";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(195, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "مسیر برنامه مجازی سازی";
            // 
            // txt_virtualapp
            // 
            this.txt_virtualapp.Location = new System.Drawing.Point(88, 37);
            this.txt_virtualapp.Name = "txt_virtualapp";
            this.txt_virtualapp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_virtualapp.Size = new System.Drawing.Size(278, 22);
            this.txt_virtualapp.TabIndex = 1;
            // 
            // btn_virtualapp
            // 
            this.btn_virtualapp.Location = new System.Drawing.Point(22, 33);
            this.btn_virtualapp.Name = "btn_virtualapp";
            this.btn_virtualapp.Size = new System.Drawing.Size(60, 29);
            this.btn_virtualapp.TabIndex = 3;
            this.btn_virtualapp.Text = "انتخاب";
            this.btn_virtualapp.UseVisualStyleBackColor = true;
            this.btn_virtualapp.Click += new System.EventHandler(this.btn_virtualapp_Click);
            // 
            // txt_drivelater
            // 
            this.txt_drivelater.Location = new System.Drawing.Point(372, 37);
            this.txt_drivelater.Name = "txt_drivelater";
            this.txt_drivelater.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_drivelater.Size = new System.Drawing.Size(71, 22);
            this.txt_drivelater.TabIndex = 1;
            // 
            // txt_ipserver
            // 
            this.txt_ipserver.Location = new System.Drawing.Point(89, 25);
            this.txt_ipserver.Name = "txt_ipserver";
            this.txt_ipserver.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_ipserver.Size = new System.Drawing.Size(271, 22);
            this.txt_ipserver.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(363, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "سرور :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btn_testnet
            // 
            this.btn_testnet.Location = new System.Drawing.Point(11, 20);
            this.btn_testnet.Name = "btn_testnet";
            this.btn_testnet.Size = new System.Drawing.Size(60, 33);
            this.btn_testnet.TabIndex = 3;
            this.btn_testnet.Text = "تست";
            this.btn_testnet.UseVisualStyleBackColor = true;
            this.btn_testnet.Click += new System.EventHandler(this.btn_testnet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_testnet);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_ipserver);
            this.groupBox1.Location = new System.Drawing.Point(22, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 67);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تنظیمات کلاینت";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 343);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.btn_filespath);
            this.Controls.Add(this.btn_virtualapp);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_new);
            this.Controls.Add(this.txt_filespath);
            this.Controls.Add(this.txt_drivelater);
            this.Controls.Add(this.txt_virtualapp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Name = "Settings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_filespath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox txt_new;
        private System.Windows.Forms.Button btn_filespath;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_virtualapp;
        private System.Windows.Forms.Button btn_virtualapp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_drivelater;
        private System.Windows.Forms.TextBox txt_ipserver;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_testnet;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}