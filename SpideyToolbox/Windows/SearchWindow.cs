using SpideyToolbox.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WebWorks.Windows
{
    public partial class SearchWindow : Form
    {
        private List<Asset> _assets;
        private Dictionary<string, List<int>> _assetsByPath;
        private System.Action<string> _callback;
        private System.Action<string, System.Collections.IList> _contextMenuCallback;
        private ObservableCollection<SearchResult> _displayedResults = new();

        public SearchWindow(List<Asset> assets, Dictionary<string, List<int>> assetsByPath)
        {
            InitializeComponent();
            _assets = assets;
            _assetsByPath = assetsByPath;

            SearchTextBox.Text = "";
            Search();
        }



        class SearchResult
        {
            public int AssetIndex { get; set; }
            public byte Span { get; set; }
            public ulong Id;
            public uint Size { get; set; }
            public string SizeFormatted { get => SizeFormat.FormatSize(Size); }

            public string Path { get; set; }
            public string Archive { get; set; }
            public string RefPath { get => $"{Span}/{Id:X016}"; }
        }

        private void SearchTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e. == Key.Enter)
            //{
            //    Search();
            //}
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            dataGridView_Files.Rows.Clear();
            _displayedResults.Clear();

            var search = Normalize(SearchTextBox.Text.Trim());
            var words = search.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);

            if (words.Length > 0)
            {
                // search in fullpath
                var i = 0;
                foreach (var asset in _assets)
                {
                    if (asset.FullPath != null && MatchesWords(Normalize(asset.FullPath), words))
                    {
                        _displayedResults.Add(new SearchResult
                        {
                            AssetIndex = i,
                            Span = asset.Span,
                            Id = asset.Id,
                            Size = asset.Size,
                            Path = asset.FullPath,
                            Archive = asset.Archive
                        });
                    }
                    ++i;
                }

                // search in fake paths (dirname + name)
                foreach (var path in _assetsByPath.Keys)
                {
                    foreach (var assetIndex in _assetsByPath[path])
                    {
                        var asset = _assets[assetIndex];
                        if (asset.FullPath != null) continue;

                        var fakepath = Path.Combine(path, asset.Name);
                        if (MatchesWords(Normalize(fakepath), words))
                        {
                            var archive = asset.Archive;
                            var span = asset.Span;
                            var size = asset.Size;
                            string displaySize;

                            // Dynamically update size
                            if (size >= 1024 * 1024) // 1 MB or more
                            {
                                displaySize = $"{size / (1024.0 * 1024.0):F2} MB";
                            }
                            else if (size >= 1024) // 1 KB or more
                            {
                                displaySize = $"{size / 1024.0:F2} KB";
                            }
                            else // Less than 1 KB
                            {
                                displaySize = $"{size} bytes";
                            }

                            dataGridView_Files.Rows.Add(asset.Name, displaySize, archive, span);
                        }
                    }
                }
            }

            label_ResultCount.Text = $"{_displayedResults.Count} results";
        }

        private static string Normalize(string text)
        {
            return text.Replace('\\', '/').ToLower();
        }

        private static bool MatchesWords(string path, IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (!path.Contains(word)) return false;
            }
            return true;
        }
    }
}
