using DAT1;
using Microsoft.VisualBasic;
using SpideyToolbox.Utilities;
using SpideyToolbox.Windows;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing.Text;
using System.Globalization;
using System.Runtime;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

// Spidey Toolbox is an alternative Modding Tool for Insomniac Games videogames.

// Source code for the Modding Tool developed by Tkachov can be found here:
// https://github.com/Tkachov/Overstrike/tree/main/ModdingTool

namespace SpideyToolbox
{
    //----------------------------------------------------------------------------------------------
    public partial class MainWindow : Form
    {
        ToolUtils toolUtils = new ToolUtils();
        public MainWindow()
        {
            InitializeComponent();

            LoadPreferences();

            ToolUtils toolUtils = new ToolUtils();
            SpideyHome spideyHome = new SpideyHome();
            toolUtils.LoadFormIntoPanel(spideyHome, panel_Main);

            // Apply Style
            ToolboxStyle.ApplyToolBoxStyle(this, Handle, menuStrip1);
            panel_Main.Dock = DockStyle.Fill;
        }

        // Initialize
        //------------------------------------------------------------------------------------------

        // Tick
        private Thread _tickThread;
        private List<Thread> _taskThreads = new();

        // loaded data
        private TOCBase? _toc = null;
        private List<Asset> _assets = new();
        private Dictionary<string, List<int>> _assetsByPath = new();
        //private ObservableCollection<Asset> _displayedAssetList = new();

        // replaced data
        private Dictionary<Asset, string> _replacedAssets = new();
        private Dictionary<Asset, string> _addedAssets = new();


        // TODO: Add the UI windows
        // ui
        // private SearchWindow _searchWindow = null;
        // private HashToolWindow _hashToolWindow = null;

        // Load user settings
        //------------------------------------------------------------------------------------------
        #region User Settings
        private void LoadPreferences()
        {
            // Clear overlay text
            OverlayHeaderLabel.Text = "";
            OverlayOperationLabel.Text = "";

            LoadRecentTOC();
        }

        // Load most recent TOC as long as _recentTOC1 isn't null and the setting is enabled.
        private void LoadRecentTOC(object sender = null, EventArgs e = null, string TOC = "")
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            if (settings._autoloadRecent == true && settings._recentTOC1 != null && TOC == "")
            {
                string recentTOC = settings._recentTOC1;
                if (File.Exists(recentTOC)) StartLoadTOCThread(recentTOC);
            }
            else if (TOC != "")
            {
                StartLoadTOCThread(TOC);
            }

            LoadRecentMenus();
        }

        private void LoadRecentMenus()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            ToolStripMenuItem[] toolStripMenuItems = { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6 };
            string[] recentTOCs = { settings._recentTOC1, settings._recentTOC2, settings._recentTOC3, settings._recentTOC4, settings._recentTOC5 };

            for (int i = 0; i < toolStripMenuItems.Length; i++)
            {
                if (!string.IsNullOrEmpty(recentTOCs[i]))
                {
                    string numTOC = recentTOCs[i];
                    toolStripMenuItems[i].Text = recentTOCs[i];
                    toolStripMenuItems[i].Visible = true;
                    toolStripMenuItems[i].Click += (sender, e) => LoadRecentTOC(sender, e, numTOC);
                }
                else
                {
                    toolStripMenuItems[i].Visible = false;
                }
            }
        }



        #endregion
        #region Save Recent TXT
        private void SaveRecentTXT(string path)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            // Remove the path if it already exists and shift other entries up
            if (settings._recentTOC1 == path)
            {
                // Do nothing; already in the correct spot
            }
            else if (settings._recentTOC2 == path)
            {
                settings._recentTOC2 = settings._recentTOC3;
                settings._recentTOC3 = settings._recentTOC4;
                settings._recentTOC4 = settings._recentTOC5;
                settings._recentTOC5 = null; // Clear the last slot
            }
            else if (settings._recentTOC3 == path)
            {
                settings._recentTOC3 = settings._recentTOC4;
                settings._recentTOC4 = settings._recentTOC5;
                settings._recentTOC5 = null;
            }
            else if (settings._recentTOC4 == path)
            {
                settings._recentTOC4 = settings._recentTOC5;
                settings._recentTOC5 = null;
            }
            else if (settings._recentTOC5 == path)
            {
                settings._recentTOC5 = null;
            }

            // Shift everything down and add the new path to _recentTOC1
            settings._recentTOC5 = settings._recentTOC4;
            settings._recentTOC4 = settings._recentTOC3;
            settings._recentTOC3 = settings._recentTOC2;
            settings._recentTOC2 = settings._recentTOC1;
            settings._recentTOC1 = path;

            // Save the updated settings
            settingsWindow.SaveSettings(settings);
        }
        #endregion

        // Load Ticks
        //------------------------------------------------------------------------------------------
        #region Tick
        private void StartTickThread()
        {
            _tickThread = new Thread(TickThread)
            {
                IsBackground = true
            };
            _tickThread.Start();
        }
        private void TickThread()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(16);
                    Tick();
                }
            }
            catch (ThreadInterruptedException)
            {} // Do nothing
            catch
            {} // Do nothing
        }
        private void Tick()
        {
            List<Thread> threadsToRemove = new();
            foreach (var thread in _taskThreads)
            {
                if (!thread.IsAlive)
                {
                    threadsToRemove.Add(thread);
                }
            }
        }

        #endregion

        #region LoadTOC

        // Start loading the TOC
        //------------------------------------------------------------------------------------------
        private void StartLoadTOCThread(string path)
        {
            // Ensure the path to the toc exists
            var tocPath = path;

            if (!File.Exists(path))
            {
                toolUtils.ToolMessage($"TOC file \"{path}\" not found.", "Error", 0, 0);
                return;
            }

            // Recent path handling
            SaveRecentTXT(path);
            LoadRecentMenus();

            // Start a new thread for TOC loading
            Thread thread = new(() =>
            {
                try
                {
                    LoadTOC(tocPath);
                    this.Invoke(() =>
                    {
                        panel_Main.Dock = DockStyle.None;
                        panel_Main.Visible = false;
                    });
                }
                catch (Exception ex)
                {
                    try
                    {
                        this.Invoke(() =>
                        {
                            toolUtils.ToolMessage($"An error occurred while loading the TOC: {ex.Message}", "Error", 0, 1);
                        });
                    }
                    catch { }

                }
            });

            _taskThreads.Add(thread);
            thread.Start();
        }

        //------------------------------------------------------------------------------------------
        private void LoadTOC(string path)
        {
            // Update overlay
            this.Invoke(() =>
            {
                OverlayHeaderLabel.Text = "Loading 'toc'...";
                OverlayOperationLabel.Text = "-";
            });

            // Load TOC
            _toc = LoadTOCFile(path);
            if (_toc == null) return;

            var archiveNames = new List<string>();
            for (uint i = 0; i < _toc.GetArchivesCount(); ++i)
            {
                var fn = _toc.GetArchiveFilename(i);

                if (_toc is TOC_I29 && fn.StartsWith("d\\"))
                {
                    fn = fn.Substring(2); // Clean up for readability
                }

                archiveNames.Add(fn);
            }

            _assets.Clear();
            _replacedAssets.Clear();

            int progress = 0;
            int progressTotal = _toc.AssetIdsSection.Values.Count;
            byte spanIndex = 0;

            foreach (var span in _toc.SpansSection.Values)
            {
                for (int i = (int)span.AssetIndex; i < span.AssetIndex + span.Count; ++i)
                {
                    bool hasHeader = (spanIndex % 8 == 0);
                    if (hasHeader && _toc is TOC_I29)
                    {
                        hasHeader = (((TOC_I29)_toc).SizesSection.Values[i].HeaderOffset != -1);
                    }

                    _assets.Add(new Asset
                    {
                        Span = spanIndex,
                        Id = _toc.AssetIdsSection.Values[i],
                        Size = (uint)_toc.GetSizeInArchiveByAssetIndex(i),
                        HasHeader = hasHeader,
                        Name = "",
                        Archive = archiveNames[(int)_toc.GetArchiveIndexByAssetIndex(i)]
                    });

                    ++progress;
                    if (progress % 1000 == 0)
                    {
                        this.Invoke(() =>
                        {
                            OverlayHeaderLabel.Text = "Loading 'toc'...";
                            OverlayOperationLabel.Text = $"{progress}/{progressTotal} assets";
                        });
                    }
                }
                ++spanIndex;
            }

            this.Invoke(() => OverlayOperationLabel.Text = "-");

            // Load known hashes
            var appdir = AppDomain.CurrentDomain.BaseDirectory;
            var hashes_fn = Path.Combine(appdir, "hashes.txt");
            var knownHashes = new Dictionary<ulong, string>();

            if (File.Exists(hashes_fn))
            {
                var lines = File.ReadLines(hashes_fn);
                progress = 0;
                progressTotal = lines.Count();

                foreach (var line in lines)
                {
                    try
                    {
                        var firstComma = line.IndexOf(',');
                        if (firstComma == -1) continue;

                        var lastComma = line.LastIndexOf(',');
                        var assetPath = (lastComma == -1 ? line.Substring(firstComma + 1) : line.Substring(firstComma + 1, lastComma - firstComma - 1));
                        var assetId = ulong.Parse(line.Substring(0, firstComma), NumberStyles.HexNumber);

                        if (assetPath.Trim().Length > 0)
                        {
                            knownHashes.Add(assetId, assetPath);
                        }
                    }
                    catch { }

                    ++progress;
                    if (progress % 1000 == 0)
                    {
                        this.Invoke(() =>
                        {
                            OverlayHeaderLabel.Text = "Loading 'hashes.txt'...";
                            OverlayOperationLabel.Text = $"{progress}/{progressTotal} hashes";
                        });
                    }
                }
            }

            this.Invoke(() => OverlayOperationLabel.Text = "-");

            // Build tree structure
            var root = new TreeNode("Root");
            var unknownNode = new TreeNode("[Unnamed Assets]");
            var wemNode = new TreeNode("[WEM]");

            //root.Nodes.Add(unknownNode);
            //root.Nodes.Add(wemNode);

            void AddPath(string dir, int assetIndex, bool makeFullPath = false)
            {
                if (dir == null) dir = "";
                if (makeFullPath)
                    _assets[assetIndex].FullPath = Path.Combine(dir, _assets[assetIndex].Name);

                if (dir == "") dir = "/";
                var parts = dir.Split('\\');
                var currentNode = root;

                foreach (var part in parts)
                {
                    var childNode = currentNode.Nodes[part] ?? currentNode.Nodes.Add(part, part);
                    currentNode = childNode;
                }

                if (!_assetsByPath.ContainsKey(dir))
                {
                    _assetsByPath[dir] = new();
                }

                _assetsByPath[dir].Add(assetIndex);
            }

            void AddUnknown(string dir, int assetIndex)
            {
                var parts = dir.Split('\\');
                var currentNode = unknownNode;

                //Debug.WriteLine($"Processing directory: {dir}");
                foreach (var part in parts)
                {
                    //Debug.WriteLine($"Processing part: {part}");
                    var childNode = currentNode.Nodes[part] ?? currentNode.Nodes.Add(part, part);
                    currentNode = childNode;
                }

                if (!_assetsByPath.ContainsKey(dir))
                {
                    //Debug.WriteLine($"Adding new path entry for: {dir}");
                    _assetsByPath[dir] = new();
                }

                _assetsByPath[dir].Add(assetIndex);
                //Debug.WriteLine($"Asset index {assetIndex} added to path: {dir}");
            }

            // Add assets to tree
            var usedHashes = new Dictionary<ulong, string>();
            for (var i = 0; i < _assets.Count; ++i)
            {
                var asset = _assets[i];
                var assetId = asset.Id;
                if (knownHashes.ContainsKey(assetId))
                {
                    var assetPath = DAT1.Utils.Normalize(knownHashes[assetId]);
                    usedHashes[assetId] = assetPath;
                    asset.Name = Path.GetFileName(assetPath);
                    AddPath(Path.GetDirectoryName(assetPath), i, true);
                }

                ++progress;
                if (progress % 1000 == 0)
                {
                    this.Invoke(() =>
                    {
                        OverlayHeaderLabel.Text = "Building tree...";
                        OverlayOperationLabel.Text = "";
                    });
                }
            }

            var unknown = root.Nodes[0];
            var wems = root.Nodes[1];

            for (var i = 0; i < _assets.Count; ++i)
            {
                var asset = _assets[i];
                if (asset.Name != "") continue;

                var assetId = asset.Id;
                var isWem = ((assetId & 0xFFFFFFFF00000000) == 0xE000000000000000);

                if (isWem)
                {
                    var wemNumber = assetId & 0xFFFFFFFF;
                    asset.Name = $"{wemNumber}.wem";
                    AddUnknown($"[WEM]\\{asset.Archive}", i);
                }
                else
                {
                    asset.Name = $"{assetId:X016}";
                    AddUnknown($"[UNKNOWN]\\{asset.Archive}", i);
                }
            }

            // Populate TreeView
            this.Invoke(() =>
            {

                TreeView_Assets.Nodes.Clear();
                TreeView_Assets.Nodes.Add(root);
                TreeView_Assets.Nodes.Add(unknownNode);
                TreeView_Assets.CollapseAll();

                foreach (TreeNode root in TreeView_Assets.Nodes)
                {
                    if (root.Text == "Root")
                    {
                        root.Expand();
                    }
                }

                OverlayHeaderLabel.Text = "";
                OverlayOperationLabel.Text = "";
            });
        }

        //------------------------------------------------------------------------------------------
        private static TOCBase? LoadTOCFile(string tocPath)
        {
            // RCRA, MSM2
            TOC_I29 toc_i29 = new();
            if (toc_i29.Load(tocPath))
            {
                return toc_i29;
            }

            // MSMR, MSMM
            TOC_I20 toc_i20 = new();
            if (toc_i20.Load(tocPath))
            {
                return toc_i20;
            }

            return null;
        }

        #endregion

        #region Handle tree view

        // Handle clicks
        //------------------------------------------------------------------------------------------
        private void TreeView_Assets_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var selectedNode = e.Node;

            string path = TreeView_Assets.SelectedNode.FullPath;
            string fullPath1 = path.Replace("Root\\", "");
            string fullPath = fullPath1.Replace("[Unnamed Assets]\\", "");

            if (selectedNode != null)
            {
                dataGridView_Files.Rows.Clear();
                PopulateDataGridForNode(fullPath);
            }

            // Update status bar
            if (_assetsByPath.ContainsKey(fullPath))
            {
                string numAssets = "- " + _assetsByPath[fullPath].Count.ToString() + " assets";
                OverlayOperationLabel.Text = numAssets;
            }
            else
            {
                OverlayOperationLabel.Text = "";
            }

            OverlayHeaderLabel.Text = "Current directory: " + fullPath;
        }

        // Populate the grid view
        //------------------------------------------------------------------------------------------
        private void PopulateDataGridForNode(string folderPath)
        {
            dataGridView_Files.SuspendLayout();

            dataGridView_Files.Rows.Clear();

            if (_assetsByPath.ContainsKey(folderPath))
            {
                // Loop through the assets under the selected folder
                foreach (var assetName in _assetsByPath[folderPath])
                {
                    var asset = _assets[assetName];
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

                    dataGridView_Files.Rows.Add(false, asset.Name, displaySize, archive, span);
                }
            }

            dataGridView_Files.ResumeLayout();
            dataGridView_Files.ClearSelection();
        }
        #endregion

        #region User Input
        private void loadTOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.Title = "Open TOC File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    StartLoadTOCThread(filePath);
                }
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new SettingsWindow();
            settings.ShowDialog();
        }

        #endregion
    }
}
