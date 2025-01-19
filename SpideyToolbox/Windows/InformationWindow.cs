using SpideyToolbox.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebWorks.Windows
{
    public partial class InformationWindow : Form
    {
        public InformationWindow()
        {
            InitializeComponent();

            ToolUtils.ApplyStyle(this, Handle);

            MaximizeBox = false;
            MinimizeBox = false;

            SetInformationText(richTextBox1);
        }
        private void SetInformationText(RichTextBox richTextBox)
        {
            // Set RichTextBox properties for a black background and read-only mode
            richTextBox.BackColor = Color.Black;
            richTextBox.ForeColor = Color.White; // Default text color
            richTextBox.ReadOnly = true;
            richTextBox.Font = new Font("Segoe UI", 10);

            // Clear any existing text
            richTextBox.Clear();

            // Add formatted text
            AppendFormattedText(richTextBox, "WebWorks Modding Suite\n", 14, FontStyle.Bold);
            AppendFormattedText(richTextBox, "WebWorks is a comprehensive modding suite for Insomniac Games’ titles. It is built on Tkachov's Overstrike and Modding Tool code, as well as hypermorphicmods' Spandex and Spidey Texture Scaler projects.\n\n", 10, FontStyle.Regular);

            AppendFormattedText(richTextBox, "Supported Games:\n", 12, FontStyle.Bold);
            AppendFormattedText(richTextBox, "- Marvel's Spider-Man: Remastered\n", 10, FontStyle.Regular);
            AppendFormattedText(richTextBox, "- Marvel's Spider-Man: Miles Morales\n", 10, FontStyle.Regular);
            AppendFormattedText(richTextBox, "- Marvel's Spider-Man 2\n", 10, FontStyle.Regular);
            AppendFormattedText(richTextBox, "- Ratchet & Clank: Rift Apart\n\n", 10, FontStyle.Regular);

            AppendFormattedText(richTextBox, "Credits:\n", 12, FontStyle.Bold);
            AppendFormattedText(richTextBox, "Tkachov/Overstrike: https://github.com/Tkachov/Overstrike\n", 10, FontStyle.Regular);
            AppendFormattedText(richTextBox, "Spandex: https://github.com/hypermorphicmods/Spandex\n", 10, FontStyle.Regular);
            AppendFormattedText(richTextBox, "Spidey Texture Scaler: https://github.com/hypermorphicmods/SpideyTextureScaler\n\n", 10, FontStyle.Regular);
        }

        private void AppendFormattedText(RichTextBox richTextBox, string text, int fontSize, FontStyle style)
        {
            // Preserve current text formatting and append new styled text
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionFont = new Font("Segoe UI", fontSize, style);
            richTextBox.SelectionColor = Color.White; // Set text color to white
            richTextBox.AppendText(text);
        }

    }
}
