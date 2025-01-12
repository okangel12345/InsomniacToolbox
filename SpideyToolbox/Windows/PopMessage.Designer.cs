namespace SpideyToolbox.Windows
{
    partial class PopMessage
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
            pictureBox1 = new PictureBox();
            richTextBox1 = new RichTextBox();
            okButton = new Button();
            cancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(115, 115);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(12, 12, 12);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Font = new Font("Segoe UI", 11F);
            richTextBox1.ForeColor = SystemColors.Control;
            richTextBox1.Location = new Point(133, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(249, 115);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "Sample Text";
            // 
            // okButton
            // 
            okButton.Font = new Font("Segoe UI", 9F);
            okButton.Location = new Point(198, 101);
            okButton.Name = "okButton";
            okButton.Size = new Size(89, 26);
            okButton.TabIndex = 4;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            cancelButton.Font = new Font("Segoe UI", 9F);
            cancelButton.Location = new Point(293, 101);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(89, 26);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // PopMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(394, 139);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(richTextBox1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PopMessage";
            Text = "Assert";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;
        private Button okButton;
        private Button cancelButton;
    }
}