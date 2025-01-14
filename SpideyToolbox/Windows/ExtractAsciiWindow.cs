using DAT1;
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

namespace WebWorks.Windows
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

            string hash = textBox_FileHash.Text.Trim();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Ascii File (*.ascii)|*.ascii";
            saveFileDialog.DefaultExt = "*.ascii";
            saveFileDialog.InitialDirectory = Path.Combine(WebWorksMisc);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = saveFileDialog.FileName;

                if (!selectedFilePath.EndsWith(".ascii", StringComparison.OrdinalIgnoreCase))
                {
                    selectedFilePath += ".ascii";
                }

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(selectedFilePath);
                string arguments = $"{hash} {fileNameWithoutExtension}";

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

                    // Move the file to the user's selected location
                    string destinationFilePath = selectedFilePath;

                    // Assuming the file was created in the Utilities\ModelTool\PS5 directory (you may need to adjust based on the actual behavior)
                    string sourceFilePath = Path.Combine(WebWorksMisc, Path.GetFileName(selectedFilePath));

                    // Move the model file if it exists, replace it if already exists
                    if (File.Exists(sourceFilePath))
                    {
                        // Delete the destination file if it already exists
                        if (sourceFilePath != destinationFilePath)
                        {
                            if (File.Exists(destinationFilePath))
                            {
                                File.Delete(destinationFilePath);
                            }
                        }

                        File.Move(sourceFilePath, destinationFilePath);
                    }
                    else
                    {
                        richTextBox_Log.Text = $"The model file was not created at the expected location: {sourceFilePath}";
                        return;
                    }

                    // Move the _materials.txt file
                    string materialsFileName = fileNameWithoutExtension + "_materials.txt";
                    string materialsFilePath = Path.Combine(WebWorksMisc, materialsFileName);

                    if (File.Exists(materialsFilePath))
                    {
                        string destinationMaterialsFilePath = Path.Combine(Path.GetDirectoryName(destinationFilePath), materialsFileName);

                        // Delete the materials file if it already exists
                        if (destinationMaterialsFilePath != materialsFilePath)
                        {
                            if (File.Exists(destinationMaterialsFilePath))
                            {
                                File.Delete(destinationMaterialsFilePath);
                            }
                        }
                        File.Move(materialsFilePath, destinationMaterialsFilePath);
                    }
                    else
                    {
                        richTextBox_Log.AppendText($"The materials file was not created at the expected location: {materialsFilePath}");
                        return;
                    }

                    // Inform the user of the successful operation
                    richTextBox_Log.Text = ($"Files successfully saved to: {destinationFilePath}\nMaterials file: {Path.Combine(Path.GetDirectoryName(destinationFilePath), materialsFileName)}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error executing command: {ex.Message}", "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            Debug.WriteLine("Extracting with ALERT");

            Debug.WriteLine($"Extracting {_assetID}, with span {_assetSpan}, to {outputPath}");

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
                    Debug.WriteLine("Running process!");

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

