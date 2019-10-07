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
            this.rtb_Command = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).BeginInit();
            this.SuspendLayout();
            // 
            // pcb_Output
            // 
            this.pcb_Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_Output.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcb_Output.Location = new System.Drawing.Point(0, 0);
            this.pcb_Output.Name = "pcb_Output";
            this.pcb_Output.Size = new System.Drawing.Size(617, 311);
            this.pcb_Output.TabIndex = 0;
            this.pcb_Output.TabStop = false;
            // 
            // rtb_Command
            // 
            this.rtb_Command.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Command.Location = new System.Drawing.Point(0, 311);
            this.rtb_Command.Name = "rtb_Command";
            this.rtb_Command.Size = new System.Drawing.Size(617, 119);
            this.rtb_Command.TabIndex = 1;
            this.rtb_Command.Text = "";
            this.rtb_Command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Command_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 430);
            this.Controls.Add(this.rtb_Command);
            this.Controls.Add(this.pcb_Output);
            this.Name = "MainForm";
            this.Text = "OrganicMoleculesBuilder";
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb_Output;
        private System.Windows.Forms.RichTextBox rtb_Command;
    }
}

