using DAT1;
using SpideyTextureScaler;
using SpideyToolbox.Utilities;
using SpideyToolbox.Windows;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using WebWorks.Windows;
using WebWorks.Utilities;
using Newtonsoft.Json.Linq;
using Spiderman;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Newtonsoft.Json;
// Spidey Toolbox is an alternative Modding Tool for Insomniac Games videogames.
//
// Source code for the Modding Tool developed by Tkachov can be found here:
// https://github.com/Tkachov/Overstrike/tree/main/ModdingTool

namespace SpideyToolbox
{
    // Spidey Toolbox Main Window
    //----------------------------------------------------------------------------------------------
    public partial class MainWindow : Form
    {
        ToolUtils toolUtils = new ToolUtils();
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();

            Instance = this;

            ToolUtils.ApplyStyle(this, Handle, menuStrip1, contextMenuStrip1);

            panel_Main.Dock = DockStyle.Fill;
        }

        // Initialize
        //------------------------------------------------------------------------------------------

        // Tick
        private Thread _tickThread;
        private List<Thread> _taskThreads = new();

        // loaded data
        public static TOCBase? _toc = null;
        public List<Asset> _assets = new();
        public Dictionary<string, List<int>> _assetsByPath = new();
        //private ObservableCollection<Asset> _displayedAssetList = new();

        // replaced data
        private Dictionary<Asset, string> _replacedAssets = new();
        private Dictionary<Asset, string> _addedAssets = new();

        public string _selectedHashes;

        // Load user settings
        //------------------------------------------------------------------------------------------
        #region User Settings
        private void LoadPreferences()
        {
            // Clear overlay text
            OverlayHeaderLabel.Text = "";
            OverlayOperationLabel.Text = "";

            LoadRecentTOC();
            LoadRecentMenus();
        }

        // Load most recent TOC as long as _recentTOC1 isn't null and the setting is enabled
        private void LoadRecentTOC(object sender = null, EventArgs e = null, string TOC = "")
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            if (settings._autoloadRecent == true && settings._recentTOC1 != null && TOC == "")
            {
                StartLoadTOCThread(settings._recentTOC1);
            }
            else if (TOC != "")
            {
                StartLoadTOCThread(TOC);
            }
        }
        private void LoadRecentMenus()
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            ToolStripMenuItem[] toolStripMenuItems = { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6 };
            string[] recentTOCs = { settings._recentTOC1, settings._recentTOC2, settings._recentTOC3, settings._recentTOC4, settings._recentTOC5 };


            int nonEmptyCount = recentTOCs.Count(t => !string.IsNullOrEmpty(t));
            if (nonEmptyCount >= 1)
            {
                loadRecentToolStripMenuItem.Visible = true;
            }
            else
            {
                loadRecentToolStripMenuItem.Visible = false;
            }

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

            // Load all hashes and pick hashes always

            string appDir = Path.GetDirectoryName(Application.ExecutablePath);

            foreach (string file in Directory.GetFiles(appDir))
            {
                string fileName = Path.GetFileName(file);

                if (fileName.StartsWith("hashes_", StringComparison.OrdinalIgnoreCase) ||
                    fileName.Equals("hashes.txt", StringComparison.OrdinalIgnoreCase))
                {
                    var menuItem = new ToolStripMenuItem(fileName);

                    hashesToolStripMenuItem.DropDownItems.Add(menuItem);
                    menuItem.CheckOnClick = true;
                    menuItem.Click += AssetsMenuClick;

                    if (hashesToolStripMenuItem.DropDownItems
                        .OfType<ToolStripMenuItem>()
                        .All(item => !item.Checked))
                    {
                        menuItem.Checked = true;
                    }
                }
            }
        }
        private void AssetsMenuClick(object? sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in hashesToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == sender;
            }
        }

        #endregion

        #region Save Recent TXT
        private void SaveRecentTXT(string path)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            // Available setting slots
            string[] recentTOC = new string[]
            {
                settings._recentTOC1,
                settings._recentTOC2,
                settings._recentTOC3,
                settings._recentTOC4,
                settings._recentTOC5
            };

            // Check if the path already exists
            int existingIndex = Array.IndexOf(recentTOC, path);

            if (existingIndex != -1)
            {
                // If path exists, remove it and shift others down
                for (int i = existingIndex; i > 0; i--)
                {
                    recentTOC[i] = recentTOC[i - 1];
                }
            }
            else
            {
                // If path doesn't exist, shift all paths down
                for (int i = recentTOC.Length - 1; i > 0; i--)
                {
                    recentTOC[i] = recentTOC[i - 1];
                }
            }

            // Assign the path to the first slot
            recentTOC[0] = path;

            // Update settings and save
            UpdateSettings(settings, recentTOC);
            settingsWindow.SaveSettings(settings);
        }
        private void UpdateSettings(AppSettings settings, string[] recentTOC)
        {
            settings._recentTOC1 = recentTOC[0];
            settings._recentTOC2 = recentTOC[1];
            settings._recentTOC3 = recentTOC[2];
            settings._recentTOC4 = recentTOC[3];
            settings._recentTOC5 = recentTOC[4];
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

        private bool isRunning = true;
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
            { } // Do nothing
            catch
            { } // Do nothing
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
            SettingsWindow settingsWindow = new SettingsWindow();
            AppSettings settings = settingsWindow.LoadSettings();

            var tocPath = path;

            _assets.Clear();
            _replacedAssets.Clear();
            _addedAssets.Clear();
            _assetsByPath.Clear();

            TreeView_Assets.Nodes.Clear();
            dataGridView_Files.Rows.Clear();

            SetEnvironment.Home();

            this.Invoke(() =>
            {
                SaveRecentTXT(path);
                LoadRecentMenus();
            });

            if (!settings._loadtocModded)
            {
                string gameFolder = Path.GetDirectoryName(path);
                string backupTOC = Path.Combine(gameFolder, "toc.BAK");

                if (File.Exists(backupTOC))
                {
                    tocPath = backupTOC;
                }
                else
                {
                    tocPath = path;
                }
            }

            if (!File.Exists(tocPath))
            {
                toolUtils.ToolMessage($"TOC file \"{tocPath}\" not found.", "Error", 0, 0);
                return;
            }


            // Start a new thread for TOC loading
            //--------------------------------------------------------------------------------------
            Thread thread = new(() =>
            {
                try
                {
                    LoadTOC(tocPath);

                    Invoke(() =>
                    {
                        menuStrip1.Visible = true;
                        SetEnvironment.ModdingTool();
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

        // Start loading the TOC file
        //------------------------------------------------------------------------------------------
        private void LoadTOC(string path)
        {

            this.Invoke(() =>
            {
                OverlayHeaderLabel.Text = $"Loading '{Path.GetFileName(path)}'...";
                OverlayOperationLabel.Text = "-";

                SetEnvironment.Home();

                menuStrip1.Visible = false;
            });

            // LoadTOCFile to idenfity which type of TOC it is

            _toc = LoadTOCFile(path);
            if (_toc == null) return;

            var archiveNames = new List<string>();
            for (uint i = 0; i < _toc.GetArchivesCount(); ++i)
            {
                var fn = _toc.GetArchiveFilename(i);

                if (_toc is TOC_I29 && fn.StartsWith("d\\"))
                {
                    fn = fn.Substring(2); // Clean up for readability, RCRA and MSM2 need this.
                }

                archiveNames.Add(fn);
            }

            Debug.WriteLine("2");

            // Clear existing lists
            _assets.Clear();
            _replacedAssets.Clear();

            // Start getting assets
            //--------------------------------------------------------------------------------------

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
                            OverlayHeaderLabel.Text = $"Loading '{Path.GetFileName(path)}'...";
                            OverlayOperationLabel.Text = $"{progress}/{progressTotal} assets";
                        });
                    }
                }
                ++spanIndex;
            }

            this.Invoke(() => OverlayOperationLabel.Text = "-");

            // Load known hashes
            //--------------------------------------------------------------------------------------

            var appdir = AppDomain.CurrentDomain.BaseDirectory;

            var selectedHashes = hashesToolStripMenuItem.DropDownItems
                                    .OfType<ToolStripMenuItem>()
                                    .FirstOrDefault(item => item.Checked)?.Text;

            _selectedHashes = selectedHashes;

            if (selectedHashes == null)
            {
                selectedHashes = "hashes.txt";
            }

            var hashes_fn = Path.Combine(appdir, selectedHashes);

            var knownHashes = new Dictionary<ulong, string>();
            knownHashes.Clear();

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
                            OverlayHeaderLabel.Text = $"Loading '{selectedHashes}'...";
                            OverlayOperationLabel.Text = $"{progress}/{progressTotal} hashes";
                        });
                    }
                }
            }

            this.Invoke(() => OverlayOperationLabel.Text = "-");

            // Add paths to build the tree
            //--------------------------------------------------------------------------------------

            var root = new TreeNode("Root");
            root.Nodes.Clear();

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

            var usedHashes = new Dictionary<ulong, string>();
            usedHashes.Clear();

            for (var i = 0; i < _assets.Count; ++i)
            {
                var asset = _assets[i];
                var assetId = asset.Id;

                if (knownHashes.ContainsKey(assetId))
                {
                    var assetPath = Utils.Normalize(knownHashes[assetId]);
                    usedHashes[assetId] = assetPath;
                    asset.Name = Path.GetFileName(assetPath);
                    AddPath(Path.GetDirectoryName(assetPath), i, true);
                }

                ++progress;
                if (progress % 1000 == 0)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        OverlayHeaderLabel.Text = "Building tree...";
                        OverlayOperationLabel.Text = "-";

                        TreeView_Assets.Nodes.Clear();
                    });
                }
            }

            //--------------------------------------------------------------------------------------
            this.Invoke((MethodInvoker)delegate
            {
                if (root != null)
                {
                    SortNodesRecursively(root);
                }
            });

            void SortNodesRecursively(TreeNode node)
            {
                var sortedChildNodes = node.Nodes.Cast<TreeNode>()
                                               .OrderBy(childNode => childNode.Text)
                                               .ToList();

                node.Nodes.Clear();
                node.Nodes.AddRange(sortedChildNodes.ToArray());

                foreach (TreeNode child in node.Nodes)
                {
                    SortNodesRecursively(child);
                }
            }

            //--------------------------------------------------------------------------------------

            var unknown = root.Nodes["[UNKNOWN]"];
            var wems = root.Nodes["[WEM]"];

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
                    AddPath($"[WEM]\\{asset.Archive}", i);
                }
                else
                {
                    asset.Name = $"{assetId:X016}";
                    AddPath($"[UNKNOWN]\\{asset.Archive}", i);
                }
            }

            // Populate TreeView
            //--------------------------------------------------------------------------------------
            this.Invoke(() =>
            {
                TreeView_Assets.Nodes.Clear();

                TreeView_Assets.Nodes.Add(root);

                TreeView_Assets.CollapseAll();

                foreach (TreeNode rootNode in TreeView_Assets.Nodes)
                {
                    if (rootNode.Text == "Root")
                    {
                        rootNode.Expand();
                    }
                }

                OverlayHeaderLabel.Text = "";
                OverlayOperationLabel.Text = "";
            });
        }

        // Identify TOC file
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

        // Show assets from folder in Data Grid
        //------------------------------------------------------------------------------------------
        private void TreeView_Assets_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = e.Node;

            // Check if the clicked node is the selected node
            if (TreeView_Assets.SelectedNode == null || TreeView_Assets.SelectedNode != selectedNode)
            {
                return;
            }

            string path = selectedNode.FullPath;
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
                    var span = asset.Span.ToString();
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

                    dataGridView_Files.Rows.Add(asset.Name, displaySize, archive, span, asset.Id, asset.FullPath, asset.RefPath);
                }
            }

            dataGridView_Files.ResumeLayout();
            dataGridView_Files.ClearSelection();
        }

        #endregion

        #region User Input
        // Load MOD menu
        //------------------------------------------------------------------------------------------
        private void ToolStrip_Mod_Click(object sender, EventArgs e)
        {
            int replacedAssetsCount = _replacedAssets.Count;
            int addedAssetsCount = _addedAssets.Count;

            menuItem_ReplacedAssets.Text = $"{replacedAssetsCount} replaced, {addedAssetsCount} new";

            if (replacedAssetsCount > 0 || addedAssetsCount > 0)
            {
                menuItem_ClearAll.Enabled = true;
                menuItem_WWPROJ_Handle.Text = "Save as .wwproj...";
            }
            else
            {
                menuItem_ClearAll.Enabled = false;
                menuItem_WWPROJ_Handle.Text = "Add from .wwproj...";
            }

            if (Directory.Exists(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "stages")))
            {
                menuItem_AddFromStage.Enabled = true;
            }
        }

        // Load TOC menu item, Ctrl + O
        //------------------------------------------------------------------------------------------
        private void ToolStrip_LoadToc_Click(object sender, EventArgs e)
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

        // Additional menu item buttons
        //------------------------------------------------------------------------------------------
        private void ToolStrip_Settings_Click(object sender, EventArgs e)
        { SetEnvironment.Settings(); }
        private void ToolStrip_Search_Click(object sender, EventArgs e)
        { SetEnvironment.Search(); }
        private void ToolStrip_ExtractToStage_Click(object sender, EventArgs e)
        { ExtractionMethods.ExtractToStage(); }
        private void ToolStrip_ExtractAsAscii_Click(object sender, EventArgs e)
        { ExtractionMethods.ExtractAsASCII(); }
        private void ToolStrip_ExtractAsDDS_Click(object sender, EventArgs e)
        { ExtractionMethods.ExtractAsDDS(); }
        private void ToolStrip_CopyPath_Click(object sender, EventArgs e)
        { ToolUtils.copyToClipboard(GetCurrent.AssetsFullPath()); }
        private void ToolStrip_CopyHash_Click(object sender, EventArgs e)
        { ToolUtils.copyToClipboard(GetCurrent.AssetsHashes()); }
        private void ToolStrip_ReplaceAsset_Click(object sender, EventArgs e)
        {
            var selected = GetCurrent.AssetsIDs().Count();
            if (selected != 1) return;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file to replace asset with...";
            dialog.Multiselect = false;
            dialog.RestoreDirectory = true;
            dialog.Filter = "All files(*.*) | *.*";

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = dialog.FileName;

            string assetPath = GetCurrent.AssetsFullPath()[0];
            string assetName = GetCurrent.AssetsNames()[0];
            byte assetSpan = GetCurrent.AssetsSpans()[0];
            ulong assetID = GetCurrent.AssetsIDs()[0];
            string assetArchive = GetCurrent.AssetsArchives()[0];

            Asset asset = new Asset();
            asset.Span = assetSpan;
            asset.Id = assetID;
            asset.Name = assetName;
            asset.FullPath = assetPath;
            asset.Archive = assetArchive;

            _replacedAssets.Set(asset, path);
        }

        // Extract all selected asset rows
        //--------------------------------------------------------------------------------------
        private void ToolStrip_ExtractSelected_Click(object sender, EventArgs e)
        {
            var dataGridView = GetCurrent.DataGridView();

            if (dataGridView.SelectedRows.Count == 1)
            {
                string assetPath = GetCurrent.AssetsNames()[0];
                ulong assetID = GetCurrent.AssetsIDs()[0];
                byte assetSpan = GetCurrent.AssetsSpans()[0];

                ExtractOneAssetDialog(assetPath, assetSpan, assetID);
            }
            else
            {
                ExtractMultipleAssetsDialog(GetCurrent.AssetsNames(), GetCurrent.AssetsSpans(), GetCurrent.AssetsIDs());
            }
        }

        // Menu Item
        //--------------------------------------------------------------------------------------
        private void menuItem_PackAsStage_Click(object sender, EventArgs e)
        {
            var window = new PackStageWindow(_replacedAssets, _addedAssets, _toc);
            window.ShowDialog();
        }
        private void menuItem_ClearAll_Click(object sender, EventArgs e)
        {
            _replacedAssets.Clear();
            _addedAssets.Clear();
        }
        private void menuItem_AddFromStage_Click(object sender, EventArgs e)
        {
            var window = new StageSelectorWindow();
            window.OnlyExisting = true;
            window.ShowDialog();

            if (window.Stage == null) return;

            var cwd = Directory.GetCurrentDirectory();
            var path = Path.Combine(cwd, "stages");
            var stagePath = Path.Combine(path, window.Stage);

            for (var spanIndex = 0; spanIndex < 256; ++spanIndex)
            {
                var spanDir = Path.Combine(stagePath, $"{spanIndex}");
                if (!Directory.Exists(spanDir)) continue;

                var files = Directory.GetFiles(spanDir, "*", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var relpath = Path.GetRelativePath(spanDir, file);
                    string fullpath = null;
                    ulong assetId;
                    if (Regex.IsMatch(relpath, "^[0-9A-Fa-f]{16}$"))
                    {
                        assetId = ulong.Parse(relpath, NumberStyles.HexNumber);
                    }
                    else
                    {
                        assetId = CRC64.Hash(relpath);
                        fullpath = relpath;
                    }

                    var assetIndex = _toc.FindAssetIndex((byte)spanIndex, assetId);
                    if (assetIndex != -1)
                    {
                        var asset = _assets[assetIndex];
                        _replacedAssets.Set(asset, file);
                        continue;
                    }

                    // record to _addedAssets, updating the record if it's already present
                    Asset newAsset = null;

                    foreach (var addedAsset in _addedAssets.Keys)
                    {
                        if (addedAsset.Span == spanIndex && addedAsset.Id == assetId)
                        {
                            newAsset = addedAsset;
                            break;
                        }
                    }

                    var adding = (newAsset == null);
                    if (adding) newAsset = new Asset();

                    newAsset.Span = (byte)spanIndex;
                    newAsset.Id = assetId;
                    newAsset.Size = 0; // TODO?
                    newAsset.HasHeader = true;
                    newAsset.Name = Path.GetFileName(relpath);
                    newAsset.Archive = "-";
                    newAsset.FullPath = fullpath;

                    if (adding)
                    {
                        _addedAssets.Add(newAsset, file);
                    }
                    else
                    {
                        _addedAssets.Set(newAsset, file);
                    }
                }
            }
        }
        private void SaveAsWWPROJ()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "WWProj Files (*.wwproj)|*.wwproj",
                DefaultExt = ".wwproj",
                AddExtension = true
            };

            // Show the dialog and check if the user selected a file
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Create a list to hold the serialized assets with their associated strings
                var assetsToSave = new List<AssetWWPROJHelper>();

                // Serialize _replacedAssets (Asset and associated string)
                foreach (var entry in _replacedAssets)
                {
                    assetsToSave.Add(new AssetWWPROJHelper(entry.Key, entry.Value));
                }

                // Serialize _addedAssets (Asset and associated string)
                foreach (var entry in _addedAssets)
                {
                    assetsToSave.Add(new AssetWWPROJHelper(entry.Key, entry.Value));
                }

                // Serialize the data to a file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(fileStream))
                    {
                        foreach (var assetData in assetsToSave)
                        {
                            // Write data in a custom format (binary for efficiency)
                            writer.Write(assetData.Span);
                            writer.Write(assetData.Id);
                            writer.Write(assetData.Name);
                            writer.Write(assetData.Archive);
                            writer.Write(assetData.FullPath ?? string.Empty);
                            writer.Write(assetData.RefPath);
                            writer.Write(assetData.AssociatedString ?? string.Empty);  // Save the associated string
                        }
                    }
                }
            }
        }
        private void AddFromWWPROJ()
        {
            // Create the open file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "WWProj Files (*.wwproj)|*.wwproj",
                DefaultExt = ".wwproj",
                AddExtension = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                var assetsFromFile = new List<AssetWWPROJHelper>();

                using (var fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (var reader = new BinaryReader(fileStream))
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            byte span = reader.ReadByte();
                            ulong id = reader.ReadUInt64();
                            string name = reader.ReadString();
                            string archive = reader.ReadString();
                            string fullPath = reader.ReadString();
                            string refPath = reader.ReadString();
                            string associatedString = reader.ReadString();

                            Asset asset = new Asset
                            {
                                Span = span,
                                Id = id,
                                Name = name,
                                Archive = archive,
                                FullPath = fullPath,
                                // RefPath is not needed here as it's calculated from Span and Id
                            };

                            // Add the asset and associated string to the appropriate dictionary
                            // Here, you can decide if it's to go into _replacedAssets or _addedAssets
                            _addedAssets[asset] = associatedString;
                        }
                    }
                }
            }
        }
        private void menuItem_ModWWPROJ_Click(object sender, EventArgs e)
        {
            if (_replacedAssets.Count > 0 || _addedAssets.Count > 0)
            {
                SaveAsWWPROJ();
            }
            else
            {
                AddFromWWPROJ();
            }
        }
        private void discordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://discord.gg/9B2Rf2AP",
                UseShellExecute = true
            });
        }


        // Handle right click for context menu
        //------------------------------------------------------------------------------------------
        public void OpenContextMenu(object sender, MouseEventArgs e)
        {
            var dataGridView = GetCurrent.DataGridView();

            int selectedRows = dataGridView.SelectedRows.Count;
            var hitTestInfo = dataGridView.HitTest(e.X, e.Y);

            ToolStrip_ExtractAsAscii.Visible = false;
            ToolStrip_ExtractAsDDS.Visible = false;

            if (e.Button == MouseButtons.Right && hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0)
            {
                if (selectedRows == 0)
                {
                    dataGridView.ClearSelection();
                    dataGridView.Rows[hitTestInfo.RowIndex].Selected = true;
                    selectedRows = 1;
                }

                dataGridView.CurrentCell = dataGridView[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];

                string assetType = Path.GetExtension(dataGridView.CurrentCell.Value?.ToString() ?? string.Empty);

                ToolStrip_ExtractAsAscii.Visible = assetType == ".model";
                ToolStrip_ExtractAsDDS.Visible = assetType == ".texture";

                ToolStrip_AssetsSelected.Text = selectedRows > 1
                    ? $"{selectedRows} assets selected"
                    : $"{selectedRows} asset selected";

                ToolStrip_CopyPath.Text = selectedRows > 1 ? "Copy paths" : "Copy path";
                ToolStrip_CopyHash.Text = selectedRows > 1 ? "Copy hashes" : "Copy hash";

                contextMenuStrip1.Show(dataGridView, new Point(e.X, e.Y));
            }
        }

        #region Open additional tools

        // Open Tools
        //------------------------------------------------------------------------------------------
        private void ToolStrip_HashTool_Click(object sender, EventArgs e)
        { SetEnvironment.HashTool(); }
        private void ToolStrip_SpandexTool_Click(object sender, EventArgs e)
        { SetEnvironment.Spandex(); }
        private void ToolStrip_SilkTextureTool_Click(object sender, EventArgs e)
        { SetEnvironment.SilkTexture(); }
        private void ToolStrip_ModdingTool_Click(object sender, EventArgs e)
        { SetEnvironment.ModdingTool(); }

        // Open with file
        private void ToolStrip_OpenMaterial_Click(object sender, EventArgs e)
        {
            SetEnvironment.Spandex();
            SetEnvironment.spandexForm.Open();
        }
        private void ToolStrip_OpenTexture_Click(object sender, EventArgs e)
        {
            SetEnvironment.SilkTexture();
            SetEnvironment.silkTextureForm.Open();
        }

        #endregion

        #endregion

        #region Extraction dialogs
        // Extract a single asset dialog
        //------------------------------------------------------------------------------------------
        private void ExtractOneAssetDialog(string assetpath, byte span, ulong assetID)
        {
            // Build the dialog
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Extract asset...",
                RestoreDirectory = true,
                Filter = "All files (*.*)|*.*",
                FileName = Path.GetFileName(assetpath)  // Set the initial file name based on assetpath
            };

            // If OK, continue
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = dialog.FileName;

                ExtractionMethods.ExtractAsset(assetID, span, selectedPath, _toc);
            }
        }

        // Extract multiple assets dialog
        //------------------------------------------------------------------------------------------
        private void ExtractMultipleAssetsDialog(string[] assets, byte[] spans, ulong[] assetIDs)
        {
            // Build folder dialog
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = true
            };

            Activate();

            // If OK, continue
            if (dialog.ShowDialog() != DialogResult.OK) return;

            var path = dialog.SelectedPath;

            if (!Directory.Exists(path)) return;

            // Extract assets with corresponding span
            for (int i = 0; i < assets.Length; i++)
            {
                string assetPath = assets[i];
                ulong assetID = assetIDs[i];
                byte span = spans[i];
                string outputPath = Path.Combine(path, Path.GetFileName(assetPath));

                ExtractionMethods.ExtractAsset(assetID, span, outputPath, _toc);
            }
        }

        #endregion

        // Handle form closing and loading
        //------------------------------------------------------------------------------------------
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            // Load user settings
            LoadPreferences();

            SetEnvironment.Home();
        }

        private void home_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetEnvironment.Home();
        }

        private void ToolStrip_Information_Click(object sender, EventArgs e)
        {
            SetEnvironment.Information();
        }

        
    }
}
