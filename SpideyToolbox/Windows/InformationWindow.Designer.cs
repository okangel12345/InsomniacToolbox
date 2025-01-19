namespace WebWorks.Windows
{
    partial class InformationWindow
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
            panel1 = new Panel();
            label1 = new Label();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.BackgroundImage = FormIcons.WebWorksIconPNG;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Location = new Point(12, 609);
            panel1.Name = "panel1";
            panel1.Size = new Size(1047, 60);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Xeroda", 32F);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(211, 43);
            label1.TabIndex = 1;
            label1.Text = "WebWorks";
            // 
            // InformationWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1071, 681);
            Controls.Add(label1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InformationWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WebWorks - Information";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
    }
}