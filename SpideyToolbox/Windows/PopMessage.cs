using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WebWorks.Windows;

namespace SpideyToolbox.Windows
{
    public partial class PopMessage : Form
    {
        public PopMessage(string message, string caption, int buttons, int icons)
        {
            InitializeComponent();
            Utilities.ToolboxStyle.ApplyToolBoxStyle(this, Handle);

            CustomMessage(message, caption, buttons, icons);
        }


        // Method to customize the message
        //--------------------------------------------------------------------------------------
        private void CustomMessage(string message, string caption, int buttons, int icons)
        {
            richTextBox1.Text = message;
            this.Text = caption;

            // Buttons
            switch (buttons)
            {
                // OK button
                case 0:
                    okButton.Text = "OK";
                    okButton.Click += ResultOK;
                    okButton.Location = new System.Drawing.Point(293, 101);
                    cancelButton.Visible = false;
                    break;
                // Yes, no buttons
                case 1:
                    okButton.Text = "Yes";
                    cancelButton.Text = "No";
                    cancelButton.Click += ResultNo;
                    okButton.Click += ResultYes;
                    break;
                // OK, cancel buttons
                case 2:
                    okButton.Click += ResultOK;
                    cancelButton.Click += ResultCancel;
                    break;
            }

            // Icons
            switch (icons)
            {
                case 0:
                    pictureBox1.Image = FormIcons.Warning;
                    System.Media.SystemSounds.Asterisk.Play();
                    break;
                case 1:
                    pictureBox1.Image = FormIcons.Error;
                    System.Media.SystemSounds.Exclamation.Play();
                    break;
            }
        }

        // Handle form results
        //------------------------------------------------------------------------------------------
        private void ResultOK(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void ResultYes(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }
        private void ResultNo(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }
        private void ResultCancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
