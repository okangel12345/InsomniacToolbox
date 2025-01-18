using DAT1;
using SpideyToolbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
