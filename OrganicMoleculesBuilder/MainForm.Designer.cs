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
            this.pcb_Output = new System.Windows.Forms.PictureBox();
            this.txb_Command = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcb_Output
            // 
            this.pcb_Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_Output.Dock = System.Windows.Forms.DockStyle.Left;
            this.pcb_Output.Location = new System.Drawing.Point(0, 0);
            this.pcb_Output.Name = "pcb_Output";
            this.pcb_Output.Size = new System.Drawing.Size(556, 465);
            this.pcb_Output.TabIndex = 0;
            this.pcb_Output.TabStop = false;
            // 
            // txb_Command
            // 
            this.txb_Command.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txb_Command.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.txb_Command.Location = new System.Drawing.Point(4, 3);
            this.txb_Command.Name = "txb_Command";
            this.txb_Command.Size = new System.Drawing.Size(298, 217);
            this.txb_Command.TabIndex = 2;
            this.txb_Command.Text = "";
            this.txb_Command.TextChanged += new System.EventHandler(this.rtb_Out_TextChanged);
            this.txb_Command.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_Command_KeyDown);
            this.txb_Command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Command_KeyPress);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.panel1.Controls.Add(this.txb_Command);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(556, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 465);
            this.panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 465);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pcb_Output);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganicMoleculesBuilder";
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb_Output;
        private System.Windows.Forms.RichTextBox txb_Command;
        private System.Windows.Forms.Panel panel1;
    }
}

