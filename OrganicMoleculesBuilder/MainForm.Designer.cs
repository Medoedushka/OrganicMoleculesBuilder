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
            this.txb_Command = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).BeginInit();
            this.SuspendLayout();
            // 
            // pcb_Output
            // 
            this.pcb_Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcb_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcb_Output.Location = new System.Drawing.Point(0, 0);
            this.pcb_Output.Name = "pcb_Output";
            this.pcb_Output.Size = new System.Drawing.Size(604, 465);
            this.pcb_Output.TabIndex = 0;
            this.pcb_Output.TabStop = false;
            // 
            // txb_Command
            // 
            this.txb_Command.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txb_Command.Location = new System.Drawing.Point(0, 352);
            this.txb_Command.Multiline = true;
            this.txb_Command.Name = "txb_Command";
            this.txb_Command.Size = new System.Drawing.Size(604, 113);
            this.txb_Command.TabIndex = 1;
            this.txb_Command.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_Command_KeyDown);
            this.txb_Command.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Command_KeyPress);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 465);
            this.Controls.Add(this.txb_Command);
            this.Controls.Add(this.pcb_Output);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganicMoleculesBuilder";
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcb_Output;
        private System.Windows.Forms.TextBox txb_Command;
    }
}

