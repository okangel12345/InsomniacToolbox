using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWorks.Utilities
{
    public class GetCurrent
    {
        // Get currently selected / active DataGridView and Asset parameters
        //------------------------------------------------------------------------------------------
        public static DataGridView DataGridView()
        {
            var activeForm = Form.ActiveForm;
            var dataGridView = FindControlRecursive<DataGridView>(activeForm, "dataGridView_Files");

            return dataGridView;
        }
        public static T FindControlRecursive<T>(Control parent, string name) where T : Control
        {
            foreach (Control control in parent.Controls)
            {
                if (control is T targetControl && control.Name == name)
                {
                    return targetControl;
                }
                var childControl = FindControlRecursive<T>(control, name);
                if (childControl != null)
                {
                    return childControl;
                }
            }

            return null;
        }

        // Get asset paremeters from DataGridView
        //------------------------------------------------------------------------------------------
        // 0    - Name
        // 1    - Size
        // 2    - Archive
        // 3    - Span
        // 4    - ID
        // 5    - Full path
        // 6    - Ref

        public static ulong[] AssetsIDs()
        {
            var dataGridView = DataGridView();
            ulong[] assetIDs = new ulong[dataGridView.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                object cellValue = selectedRow.Cells[4].Value;

                if (cellValue != null && ulong.TryParse(cellValue.ToString(), out ulong assetID))
                {
                    assetIDs[i] = assetID;
                    i++;
                }
            }

            return assetIDs;
        }
        public static string[] AssetsNames()
        {
            var dataGridView = DataGridView();
            string[] assetNames = new string[dataGridView.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                assetNames[i] = selectedRow.Cells[0].Value?.ToString();
                i++;
            }
            return assetNames;
        }
        public static byte[] AssetsSpans()
        {
            var dataGridView = DataGridView();
            byte[] spans = new byte[dataGridView.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                spans[i] = Convert.ToByte(selectedRow.Cells[3].Value);
                i++;
            }
            return spans;
        }
        public static string[] AssetsArchives()
        {
            var dataGridView = DataGridView();
            string[] assetArchives = new string[dataGridView.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                assetArchives[i] = selectedRow.Cells[2].Value?.ToString();
                i++;
            }
            return assetArchives;
        }
        public static string[] AssetsFullPath()
        {
            var dataGridView = DataGridView();
            string[] assetPaths = new string[dataGridView.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                if (!string.IsNullOrEmpty(selectedRow.Cells[5].Value?.ToString()))
                {
                    assetPaths[i] = selectedRow.Cells[5].Value?.ToString();
                }
                else
                {
                    assetPaths[i] = AssetsNames()[i];
                }
                

                i++;
            }
            return assetPaths;
        }
        public static string[] AssetsHashes()
        {
            var dataGridView = DataGridView();
            string[] assetHashes = new string[dataGridView.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView.SelectedRows)
            {
                assetHashes[i] = selectedRow.Cells[6].Value?.ToString();
                i++;
            }
            return assetHashes;
        }
    }
}
