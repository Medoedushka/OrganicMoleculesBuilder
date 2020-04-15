namespace OrganicMoleculesBuilder
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pcb_Text = new System.Windows.Forms.PictureBox();
            this.pcb_Arrow = new System.Windows.Forms.PictureBox();
            this.pcb_WavyBond = new System.Windows.Forms.PictureBox();
            this.pcb_DashedBond = new System.Windows.Forms.PictureBox();
            this.pcb_HashedWedgetBond = new System.Windows.Forms.PictureBox();
            this.pcb_WedgetBond = new System.Windows.Forms.PictureBox();
            this.pcb_None = new System.Windows.Forms.PictureBox();
            this.pcb_SolidBond = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.экспортироватьКакPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Text)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Arrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_WavyBond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_DashedBond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_HashedWedgetBond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_WedgetBond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_None)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_SolidBond)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1030, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортироватьКакPNGToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 19);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pcb_Text);
            this.groupBox1.Controls.Add(this.pcb_Arrow);
            this.groupBox1.Controls.Add(this.pcb_WavyBond);
            this.groupBox1.Controls.Add(this.pcb_DashedBond);
            this.groupBox1.Controls.Add(this.pcb_HashedWedgetBond);
            this.groupBox1.Controls.Add(this.pcb_WedgetBond);
            this.groupBox1.Controls.Add(this.pcb_None);
            this.groupBox1.Controls.Add(this.pcb_SolidBond);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(81, 199);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // pcb_Text
            // 
            this.pcb_Text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_Text.Image = global::OrganicMoleculesBuilder.Properties.Resources.icons8_text_color_25px;
            this.pcb_Text.Location = new System.Drawing.Point(7, 157);
            this.pcb_Text.Name = "pcb_Text";
            this.pcb_Text.Size = new System.Drawing.Size(30, 30);
            this.pcb_Text.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_Text.TabIndex = 0;
            this.pcb_Text.TabStop = false;
            this.pcb_Text.Click += new System.EventHandler(this.pcb_Text_Click);
            // 
            // pcb_Arrow
            // 
            this.pcb_Arrow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_Arrow.Image = global::OrganicMoleculesBuilder.Properties.Resources.icons8_right_25px;
            this.pcb_Arrow.Location = new System.Drawing.Point(43, 157);
            this.pcb_Arrow.Name = "pcb_Arrow";
            this.pcb_Arrow.Size = new System.Drawing.Size(30, 30);
            this.pcb_Arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_Arrow.TabIndex = 0;
            this.pcb_Arrow.TabStop = false;
            this.pcb_Arrow.Click += new System.EventHandler(this.pcb_Arrow_Click);
            // 
            // pcb_WavyBond
            // 
            this.pcb_WavyBond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_WavyBond.Image = global::OrganicMoleculesBuilder.Properties.Resources.WavyBond1;
            this.pcb_WavyBond.Location = new System.Drawing.Point(24, 121);
            this.pcb_WavyBond.Name = "pcb_WavyBond";
            this.pcb_WavyBond.Size = new System.Drawing.Size(30, 30);
            this.pcb_WavyBond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_WavyBond.TabIndex = 0;
            this.pcb_WavyBond.TabStop = false;
            this.pcb_WavyBond.Click += new System.EventHandler(this.pcb_WavyBond_Click);
            // 
            // pcb_DashedBond
            // 
            this.pcb_DashedBond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_DashedBond.Image = global::OrganicMoleculesBuilder.Properties.Resources.DashedBond1;
            this.pcb_DashedBond.Location = new System.Drawing.Point(43, 85);
            this.pcb_DashedBond.Name = "pcb_DashedBond";
            this.pcb_DashedBond.Size = new System.Drawing.Size(30, 30);
            this.pcb_DashedBond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_DashedBond.TabIndex = 0;
            this.pcb_DashedBond.TabStop = false;
            this.pcb_DashedBond.Click += new System.EventHandler(this.pcb_DashedBond_Click);
            // 
            // pcb_HashedWedgetBond
            // 
            this.pcb_HashedWedgetBond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_HashedWedgetBond.Image = global::OrganicMoleculesBuilder.Properties.Resources.Связьб4;
            this.pcb_HashedWedgetBond.Location = new System.Drawing.Point(7, 85);
            this.pcb_HashedWedgetBond.Name = "pcb_HashedWedgetBond";
            this.pcb_HashedWedgetBond.Size = new System.Drawing.Size(30, 30);
            this.pcb_HashedWedgetBond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_HashedWedgetBond.TabIndex = 0;
            this.pcb_HashedWedgetBond.TabStop = false;
            this.pcb_HashedWedgetBond.Click += new System.EventHandler(this.pcb_HashedWedgetBond_Click);
            // 
            // pcb_WedgetBond
            // 
            this.pcb_WedgetBond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_WedgetBond.Image = global::OrganicMoleculesBuilder.Properties.Resources.WedgetBond;
            this.pcb_WedgetBond.Location = new System.Drawing.Point(43, 49);
            this.pcb_WedgetBond.Name = "pcb_WedgetBond";
            this.pcb_WedgetBond.Size = new System.Drawing.Size(30, 30);
            this.pcb_WedgetBond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_WedgetBond.TabIndex = 0;
            this.pcb_WedgetBond.TabStop = false;
            this.pcb_WedgetBond.Click += new System.EventHandler(this.pcb_WedgetBond_Click);
            // 
            // pcb_None
            // 
            this.pcb_None.BackColor = System.Drawing.Color.LightCoral;
            this.pcb_None.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_None.Image = global::OrganicMoleculesBuilder.Properties.Resources.icons8_cursor_25px;
            this.pcb_None.Location = new System.Drawing.Point(24, 16);
            this.pcb_None.Name = "pcb_None";
            this.pcb_None.Size = new System.Drawing.Size(30, 30);
            this.pcb_None.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_None.TabIndex = 0;
            this.pcb_None.TabStop = false;
            this.pcb_None.Click += new System.EventHandler(this.pcb_None_Click);
            // 
            // pcb_SolidBond
            // 
            this.pcb_SolidBond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_SolidBond.Image = global::OrganicMoleculesBuilder.Properties.Resources.SolidBond1;
            this.pcb_SolidBond.Location = new System.Drawing.Point(7, 49);
            this.pcb_SolidBond.Name = "pcb_SolidBond";
            this.pcb_SolidBond.Size = new System.Drawing.Size(30, 30);
            this.pcb_SolidBond.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcb_SolidBond.TabIndex = 0;
            this.pcb_SolidBond.TabStop = false;
            this.pcb_SolidBond.Click += new System.EventHandler(this.pcb_SolidBond_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCoral;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 537);
            this.panel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(108, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 537);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.White;
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(5, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(720, 522);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(168)))), ((int)(((byte)(168)))));
            this.panel3.Location = new System.Drawing.Point(7, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(720, 522);
            this.panel3.TabIndex = 1;
            // 
            // экспортироватьКакPNGToolStripMenuItem
            // 
            this.экспортироватьКакPNGToolStripMenuItem.Name = "экспортироватьКакPNGToolStripMenuItem";
            this.экспортироватьКакPNGToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.экспортироватьКакPNGToolStripMenuItem.Text = "Экспортировать как PNG...";
            this.экспортироватьКакPNGToolStripMenuItem.Click += new System.EventHandler(this.экспортироватьКакPNGToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.ClientSize = new System.Drawing.Size(1030, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganicMoleculesBuilder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Text)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Arrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_WavyBond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_DashedBond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_HashedWedgetBond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_WedgetBond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_None)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_SolidBond)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pcb_WedgetBond;
        private System.Windows.Forms.PictureBox pcb_SolidBond;
        private System.Windows.Forms.PictureBox pcb_WavyBond;
        private System.Windows.Forms.PictureBox pcb_DashedBond;
        private System.Windows.Forms.PictureBox pcb_HashedWedgetBond;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pcb_Arrow;
        private System.Windows.Forms.PictureBox pcb_Text;
        private System.Windows.Forms.PictureBox pcb_None;
        private System.Windows.Forms.ToolStripMenuItem экспортироватьКакPNGToolStripMenuItem;
    }
}

