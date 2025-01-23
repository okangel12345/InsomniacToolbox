using DAT1;
using SpideyToolbox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using WebWorks.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WebWorks.Windows.Tools
{
    public partial class ExtractAsciiWindow : Form
    {
        private string _assetPath;
        private ulong _assetHash;
        private ulong _assetID;
        private byte _assetSpan;
        private TOCBase _toc;
        public ExtractAsciiWindow(string assetPath, bool toci29, ulong assetID, byte assetSpan, TOCBase toc)
        {
            InitializeComponent();
            ModdingLab.ToolboxStyle.ApplyToolBoxStyle(this, Handle);

            _assetPath = assetPath;
            _assetID = assetID;
            _assetSpan = assetSpan;
            _toc = toc;

            textBox_FileHash.ReadOnly = true;
            textBox_FilePath.ReadOnly = true;

            string assetName = Path.GetFileName(_assetPath);

            this.Text = Text + assetName;

            if (toci29)
            { comboBox_OutputGame.SelectedIndex = 2; }
            else { comboBox_OutputGame.SelectedIndex = 0; }

            FillFields();
        }
        private void FillFields()
        {
            _assetHash = CRC64.Hash(_assetPath, true);

            textBox_FilePath.Text = _assetPath;
            textBox_FileHash.Text = $"{_assetHash:X016}";
        }

        private string WebWorksMisc = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "WebWorksMisc");
        private string sm2Extract = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "WebWorksMisc", "sm2_extract.exe");
        private string sm1ModelTool = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "WebWorksMisc", "sm1_model.exe");

        #region Run Commands for model extraction
        private void RunAsciiCommandSM2()
        {
            // Check if the executable exists
            if (!File.Exists(sm2Extract))
            {
                MessageBox.Show($"The executable was not found at:\n{sm2Extract}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Fill in spider.ini
            string archivePath = MainWindow._toc.AssetArchivePath;
            string filePath = Path.Combine(WebWorksMisc, "spider.ini");

            File.WriteAllText(filePath, archivePath);

            // Dialog and hash
            string hash = textBox_FileHash.Text.Trim();
            
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Ascii File (*.ascii)|*.ascii";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(textBox_FilePath.Text) + ".ascii";
            saveFileDialog.DefaultExt = "*.ascii";
            saveFileDialog.InitialDirectory = Path.Combine(WebWorksMisc);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = saveFileDialog.FileName;
                string fileName = Path.GetFileNameWithoutExtension(selectedFilePath);
                string directoryName = Path.GetDirectoryName(selectedFilePath); 

                string finalSelectedPath = Path.Combine(directoryName, fileName);

                string arguments = $"{hash} \"{finalSelectedPath}\"";

                // Execute the command
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = sm2Extract;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(sm2Extract); // Set the working directory
                    process.StartInfo.CreateNoWindow = true; // Hide the console window

                    process.Start();
                    process.WaitForExit(); // Wait for the process to complete before proceeding

                    richTextBox_Log.AppendText($"Running {Path.GetFileName(sm2Extract)}");
                    richTextBox_Log.AppendText($"\nAsset hash: {hash}");
                    richTextBox_Log.AppendText($"\nOutput file: {selectedFilePath}");

                    richTextBox_Log.AppendText($"\n\nFile successfully created!");
                }
                catch (Exception ex)
                {
                    richTextBox_Log.AppendText($"Error executing command: {ex.Message}");
                }
            }
        }
        private void RunAsciiCommandALERT()
        {
            string outputPath = Path.Combine(WebWorksMisc, "tempfile_model.model");

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            richTextBox_Log.AppendText("Extracting with ALERT");

            richTextBox_Log.AppendText($"\nExtracting {_assetID}, with span {_assetSpan}, to {outputPath}");

            ExtractionMethods.ExtractAsset(_assetID, _assetSpan, outputPath, _toc);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(_assetPath);
            saveFileDialog.Filter = "Ascii File (*.ascii)|*.ascii";
            saveFileDialog.DefaultExt = "*.ascii";
            saveFileDialog.InitialDirectory = Path.Combine(WebWorksMisc);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = saveFileDialog.FileName;

                string outputAscii = selectedFilePath;
                string outputMaterials = Path.GetFileNameWithoutExtension(selectedFilePath) + "_materials.txt";

                string arguments = $"tempfile_model.model {outputAscii} {outputMaterials}";

                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = sm1ModelTool;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.WorkingDirectory = Path.GetDirectoryName(sm1ModelTool);
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();
                    process.WaitForExit();

                    string destinationFilePath = selectedFilePath;
                    string sourceFilePath = Path.Combine(WebWorksMisc, Path.GetFileName(selectedFilePath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error executing command: {ex.Message}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
        }

        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox_OutputGame.SelectedIndex == 2)
            {
                RunAsciiCommandSM2();
            }
            else
            {
                RunAsciiCommandALERT();
            }
        }
    }
}

