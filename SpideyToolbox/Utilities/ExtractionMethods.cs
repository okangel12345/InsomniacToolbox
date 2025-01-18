using DAT1;
using SpideyTextureScaler;
using SpideyToolbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWorks.Windows;

namespace WebWorks.Utilities
{
    public class ExtractionMethods
    {
        // Global method to extract an asset from loaded TOC
        //------------------------------------------------------------------------------------------
        public static void ExtractAsset(ulong assetID, byte span, string path, TOCBase _toc)
        {
            try
            {
                if (_toc == null)
                {
                    _toc = MainWindow._toc;
                }
                
                BinaryReader assetData = _toc.GetAssetReader(span, assetID);

                byte[] assetBytes = assetData.ReadBytes((int)assetData.BaseStream.Length);
                string FileHex = BitConverter.ToString(assetBytes).Replace("-", "");

                string directoryPath = Path.GetDirectoryName(path);

                if (!Directory.Exists(directoryPath))
                {
                    Debug.WriteLine("Couldn't find directory, creating a new one.");
                    Directory.CreateDirectory(directoryPath);
                }

                File.WriteAllBytes(path, assetBytes);
            }
            catch (Exception ex) { MessageBox.Show("Error" + ex); }
        }

        //------------------------------------------------------------------------------------------
        public static void ExtractToStage()
        {
            TOCBase _toc = MainWindow._toc;

            var window = new StageSelectorWindow();
            window.ShowDialog();

            if (window.Stage == null) return;

            var cwd = Directory.GetCurrentDirectory();
            var path = Path.Combine(cwd, "stages");
            var stagePath = Path.Combine(path, window.Stage);
            if (!Directory.Exists(stagePath)) Directory.CreateDirectory(stagePath);

            string[] assetPaths = GetCurrent.AssetsFullPath();
            ulong[] assetIDs = GetCurrent.AssetsIDs();
            byte[] spans = GetCurrent.AssetsSpans();

            for (int i = 0; i < assetPaths.Length; i++)
            {
                string assetPath = Path.Combine(stagePath, $"{spans[i]}", assetPaths[i]);
                ExtractAsset(assetIDs[i], spans[i], assetPath, _toc);
            }
        }

        // Extract as DDS, ASCII
        //------------------------------------------------------------------------------------------
        public static void ExtractAsDDS()
        {
            string tempName = "temp_asset";
            string tempTexture = tempName + ".texture";
            string tempHDTexture = tempName + ".hd.texture";
            string tempDDS = tempName + ".dds";
            string assetName = Path.GetFileNameWithoutExtension(GetCurrent.AssetsNames()[0]) + ".dds";

            using (var saveFileDialog = new SaveFileDialog
            {
                Title = "Save .dds file",
                Filter = "DDS Files (*.dds)|*.txt|All Files (*.*)|*.*",
                FileName = assetName,
                AddExtension = true
            })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string finalName = Path.GetFileName(saveFileDialog.FileName);
                    string workingDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
                    string silkInput = Path.Combine(workingDirectory, tempTexture);
                    string silkHD = Path.Combine(workingDirectory, tempHDTexture);

                    try
                    {
                        ExtractAsset(GetCurrent.AssetsIDs()[0], 0, silkInput, MainWindow._toc);
                        ExtractAsset(GetCurrent.AssetsIDs()[0], 1, silkHD, MainWindow._toc);

                        var p = new SpideyTextureScaler.Program
                        {
                            texturestats = new List<TextureBase>
                            {
                                new Source(),
                                new DDS(),
                                new Output()
                            }
                        };

                        p.Extract(new FileInfo(silkInput), new DirectoryInfo(workingDirectory), true);

                        File.Delete(silkInput);
                        File.Delete(silkHD);

                        string tempNamePath = Path.Combine(workingDirectory, tempDDS);
                        string finalNamePath = Path.Combine(workingDirectory, finalName);

                        if (File.Exists(tempNamePath)) File.Move(tempNamePath, finalNamePath);
                    }
                    catch { }
                }
            }
        }
        public static void ExtractAsASCII()
        {
            bool toci29;

            if (MainWindow._toc is TOC_I29)
            { toci29 = true; }
            else
            { toci29 = false; }

            string assetPath = GetCurrent.AssetsNames()[0];
            ulong assetID = GetCurrent.AssetsIDs()[0];
            byte assetSpan = GetCurrent.AssetsSpans()[0];

            ExtractAsciiWindow extractAsciiWindow = new ExtractAsciiWindow(assetPath, toci29, assetID, assetSpan, MainWindow._toc);

            extractAsciiWindow.ShowDialog();
        }
    }
}
