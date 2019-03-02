namespace XViewerV3
{
    partial class frm_Main_Cl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Main_Cl));
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_settings = new System.Windows.Forms.Button();
            this.lstv = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_play = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btn_deleteall = new System.Windows.Forms.Button();
            this.aboutme = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_add.BackgroundImage")));
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_add.Location = new System.Drawing.Point(312, 475);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(41, 38);
            this.btn_add.TabIndex = 0;
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_delete.BackgroundImage")));
            this.btn_delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_delete.Location = new System.Drawing.Point(266, 475);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(41, 38);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_settings
            // 
            this.btn_settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_settings.BackgroundImage")));
            this.btn_settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_settings.Location = new System.Drawing.Point(57, 475);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(44, 38);
            this.btn_settings.TabIndex = 2;
            this.btn_settings.UseVisualStyleBackColor = true;
            this.btn_settings.Click += new System.EventHandler(this.button3_Click);
            // 
            // lstv
            // 
            this.lstv.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name});
            this.lstv.FullRowSelect = true;
            this.lstv.GridLines = true;
            this.lstv.HideSelection = false;
            this.lstv.Location = new System.Drawing.Point(12, 55);
            this.lstv.MultiSelect = false;
            this.lstv.Name = "lstv";
            this.lstv.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lstv.RightToLeftLayout = true;
            this.lstv.Size = new System.Drawing.Size(342, 414);
            this.lstv.TabIndex = 3;
            this.lstv.UseCompatibleStateImageBehavior = false;
            this.lstv.View = System.Windows.Forms.View.Details;
            this.lstv.SelectedIndexChanged += new System.EventHandler(this.lstv_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "نام بیمار";
            this.name.Width = 250;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "لیست بیماران";
            // 
            // btn_play
            // 
            this.btn_play.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_play.BackgroundImage")));
            this.btn_play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_play.Location = new System.Drawing.Point(12, 9);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(44, 40);
            this.btn_play.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btn_play, "نمایش");
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_deleteall
            // 
            this.btn_deleteall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_deleteall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_deleteall.BackgroundImage")));
            this.btn_deleteall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_deleteall.Location = new System.Drawing.Point(107, 475);
            this.btn_deleteall.Name = "btn_deleteall";
            this.btn_deleteall.Size = new System.Drawing.Size(44, 38);
            this.btn_deleteall.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_deleteall, "حذف تمام اطلاعات");
            this.btn_deleteall.UseVisualStyleBackColor = true;
            this.btn_deleteall.Click += new System.EventHandler(this.button1_Click);
            // 
            // aboutme
            // 
            this.aboutme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutme.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aboutme.BackgroundImage")));
            this.aboutme.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aboutme.Location = new System.Drawing.Point(13, 475);
            this.aboutme.Name = "aboutme";
            this.aboutme.Size = new System.Drawing.Size(38, 38);
            this.aboutme.TabIndex = 2;
            this.aboutme.UseVisualStyleBackColor = true;
            this.aboutme.Click += new System.EventHandler(this.aboutme_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(365, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbl_Status
            // 
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(0, 17);
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 552);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstv);
            this.Controls.Add(this.aboutme);
            this.Controls.Add(this.btn_deleteall);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_add);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "نرم افزار منشی";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Main_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.ListView lstv;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button aboutme;
        private System.Windows.Forms.Button btn_deleteall;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Status;
    }
}

