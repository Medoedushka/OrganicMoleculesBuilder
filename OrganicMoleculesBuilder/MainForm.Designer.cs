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
            this.chb_CommandsDebug = new System.Windows.Forms.CheckBox();
            this.pcb_Output = new System.Windows.Forms.PictureBox();
            this.rtb_Debug = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_saveCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atomicCompositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txb_Command
            // 
            this.txb_Command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_Command.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txb_Command.Font = new System.Drawing.Font("Arial", 9.75F);
            this.txb_Command.Location = new System.Drawing.Point(3, 415);
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
            this.panel1.Controls.Add(this.chb_CommandsDebug);
            this.panel1.Controls.Add(this.pcb_Output);
            this.panel1.Controls.Add(this.rtb_Debug);
            this.panel1.Controls.Add(this.txb_Command);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(461, 524);
            this.panel1.TabIndex = 3;
            // 
            // chb_CommandsDebug
            // 
            this.chb_CommandsDebug.AutoSize = true;
            this.chb_CommandsDebug.Location = new System.Drawing.Point(4, 444);
            this.chb_CommandsDebug.Name = "chb_CommandsDebug";
            this.chb_CommandsDebug.Size = new System.Drawing.Size(152, 17);
            this.chb_CommandsDebug.TabIndex = 4;
            this.chb_CommandsDebug.Text = "Редактировать команды";
            this.chb_CommandsDebug.UseVisualStyleBackColor = true;
            this.chb_CommandsDebug.CheckedChanged += new System.EventHandler(this.chb_CommandsDebug_CheckedChanged);
            // 
            // pcb_Output
            // 
            this.pcb_Output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcb_Output.BackColor = System.Drawing.SystemColors.Window;
            this.pcb_Output.Location = new System.Drawing.Point(3, 28);
            this.pcb_Output.Name = "pcb_Output";
            this.pcb_Output.Size = new System.Drawing.Size(455, 381);
            this.pcb_Output.TabIndex = 3;
            this.pcb_Output.TabStop = false;
            // 
            // rtb_Debug
            // 
            this.rtb_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Debug.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Debug.Font = new System.Drawing.Font("Arial", 10F);
            this.rtb_Debug.Location = new System.Drawing.Point(3, 467);
            this.rtb_Debug.Name = "rtb_Debug";
            this.rtb_Debug.ReadOnly = true;
            this.rtb_Debug.Size = new System.Drawing.Size(455, 52);
            this.rtb_Debug.TabIndex = 2;
            this.rtb_Debug.Text = "";
            this.rtb_Debug.TextChanged += new System.EventHandler(this.rtb_Debug_TextChanged);
            this.rtb_Debug.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txb_Command_KeyDown);
            this.rtb_Debug.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtb_Command_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(461, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_saveCode});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // btn_saveCode
            // 
            this.btn_saveCode.Name = "btn_saveCode";
            this.btn_saveCode.Size = new System.Drawing.Size(127, 22);
            this.btn_saveCode.Text = "Save code";
            this.btn_saveCode.Click += new System.EventHandler(this.btn_saveCode_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analysisToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // analysisToolStripMenuItem
            // 
            this.analysisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atomicCompositionToolStripMenuItem});
            this.analysisToolStripMenuItem.Name = "analysisToolStripMenuItem";
            this.analysisToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.analysisToolStripMenuItem.Text = "Analysis";
            // 
            // atomicCompositionToolStripMenuItem
            // 
            this.atomicCompositionToolStripMenuItem.Name = "atomicCompositionToolStripMenuItem";
            this.atomicCompositionToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.atomicCompositionToolStripMenuItem.Text = "Atomic composition";
            this.atomicCompositionToolStripMenuItem.Click += new System.EventHandler(this.atomicCompositionToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 524);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrganicMoleculesBuilder";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcb_Output)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox txb_Command;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pcb_Output;
        private System.Windows.Forms.RichTextBox rtb_Debug;
        private System.Windows.Forms.CheckBox chb_CommandsDebug;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btn_saveCode;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atomicCompositionToolStripMenuItem;
    }
}

