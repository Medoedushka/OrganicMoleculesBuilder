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
            this.txb_Command = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pcb_Output = new System.Windows.Forms.PictureBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_Command
            // 
            this.txb_Command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_Command.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txb_Command.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.txb_Command.Location = new System.Drawing.Point(3, 407);
            this.txb_Command.Multiline = false;
            this.txb_Command.Name = "txb_Command";
            this.txb_Command.Size = new System.Drawing.Size(455, 25);
            this.txb_Command.TabIndex = 1;
            this.txb_Command.Text = "";
            this.txb_Command.TextChanged += new System.EventHandler(this.rtb_Out_TextChanged);
            this.txb_Command.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_Command_KeyDown);
            this.txb_Command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Command_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.pcb_Output);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.txb_Command);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 506);
            this.panel1.TabIndex = 3;
            // 
            // pcb_Output
            // 
            this.pcb_Output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcb_Output.BackColor = System.Drawing.SystemColors.Window;
            this.pcb_Output.Location = new System.Drawing.Point(3, 3);
            this.pcb_Output.Name = "pcb_Output";
            this.pcb_Output.Size = new System.Drawing.Size(455, 398);
            this.pcb_Output.TabIndex = 3;
            this.pcb_Output.TabStop = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.richTextBox1.Location = new System.Drawing.Point(3, 438);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(455, 65);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.rtb_Out_TextChanged);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_Command_KeyDown);
            this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Command_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 506);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganicMoleculesBuilder";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox txb_Command;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pcb_Output;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

