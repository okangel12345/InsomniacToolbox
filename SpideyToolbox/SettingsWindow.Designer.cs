namespace SpideyToolbox
{
    partial class SettingsWindow
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
            btn_Save = new Button();
            textBox_AuthorName = new TextBox();
            check_AutoLoadToc = new CheckBox();
            label1 = new Label();
            check_LoadModToc = new CheckBox();
            SuspendLayout();
            // 
            // btn_Save
            // 
            btn_Save.Location = new Point(317, 119);
            btn_Save.Name = "btn_Save";
            btn_Save.Size = new Size(75, 23);
            btn_Save.TabIndex = 1;
            btn_Save.Text = "Save";
            btn_Save.UseVisualStyleBackColor = true;
            btn_Save.Click += btn_Save_Click_1;
            // 
            // textBox_AuthorName
            // 
            textBox_AuthorName.BackColor = Color.FromArgb(22, 22, 22);
            textBox_AuthorName.BorderStyle = BorderStyle.FixedSingle;
            textBox_AuthorName.ForeColor = SystemColors.Control;
            textBox_AuthorName.Location = new Point(119, 12);
            textBox_AuthorName.Name = "textBox_AuthorName";
            textBox_AuthorName.Size = new Size(273, 23);
            textBox_AuthorName.TabIndex = 2;
            textBox_AuthorName.Text = "name";
            // 
            // check_AutoLoadToc
            // 
            check_AutoLoadToc.AutoSize = true;
            check_AutoLoadToc.Checked = true;
            check_AutoLoadToc.CheckState = CheckState.Checked;
            check_AutoLoadToc.ForeColor = SystemColors.Control;
            check_AutoLoadToc.Location = new Point(12, 119);
            check_AutoLoadToc.Name = "check_AutoLoadToc";
            check_AutoLoadToc.Size = new Size(171, 19);
            check_AutoLoadToc.TabIndex = 3;
            check_AutoLoadToc.Text = "Auto-load most recent TOC";
            check_AutoLoadToc.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 4;
            label1.Text = "Author name:";
            // 
            // check_LoadModToc
            // 
            check_LoadModToc.AutoSize = true;
            check_LoadModToc.Checked = true;
            check_LoadModToc.CheckState = CheckState.Checked;
            check_LoadModToc.ForeColor = SystemColors.Control;
            check_LoadModToc.Location = new Point(12, 94);
            check_LoadModToc.Name = "check_LoadModToc";
            check_LoadModToc.Size = new Size(130, 19);
            check_LoadModToc.TabIndex = 5;
            check_LoadModToc.Text = "Load modded TOCs";
            check_LoadModToc.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(404, 154);
            Controls.Add(check_LoadModToc);
            Controls.Add(label1);
            Controls.Add(textBox_AuthorName);
            Controls.Add(check_AutoLoadToc);
            Controls.Add(btn_Save);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "SettingsWindow";
            Text = "Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_Save;
        private CheckBox check_AutoLoadToc;
        private TextBox textBox_AuthorName;
        private Label label1;
        private CheckBox check_LoadModToc;
    }
}