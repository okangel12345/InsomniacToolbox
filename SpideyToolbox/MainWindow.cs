using DAT1;
using SpideyTextureScaler;
using SpideyToolbox.Utilities;
using SpideyToolbox.Windows;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using WebWorks.Windows;
using WebWorks.Utilities;

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
        public MainWindow()
        {
            InitializeComponent();
            LoadSpideyPanel();

            // Apply Style
            ModdingLab.ToolboxStyle.ApplyToolBoxStyle(this, Handle, menuStrip1, contextMenuStrip1);
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
        private void LoadSpideyPanel()
        {
            splitContainer1.Visible = false;

            ToolUtils toolUtils = new ToolUtils();
            SpideyHome spideyHome = new SpideyHome();

            panel_Main.Dock = DockStyle.Fill;

            toolUtils.LoadFormIntoPanel(spideyHome, panel_Main, true);
        }
        private void LoadFormIntoPanel(Form form, Panel panel)
        {
            splitContainer1.Visible = false;

            form.FormBorderStyle = FormBorderStyle.None;

            form.TopLevel = false;

            panel.Controls.Clear();
            panel.Controls.Add(form);
            panel.Dock = DockStyle.Fill;
            panel.Visible = true;

            form.Dock = DockStyle.Fill;

            form.Show();
        }
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

        private bool isRunning = true;  // Flag to control the running state
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

            // Recent path handling
            SaveRecentTXT(path);
            LoadRecentMenus();

            // Start a new thread for TOC loading
            Thread thread = new(() =>
            {
                LoadTOC(tocPath);

                //try
                //{
                //    LoadTOC(tocPath);
                //    this.Invoke(() =>
                //    {
                //        //panel_Main.Dock = DockStyle.None;
                //        //panel_Main.Visible = false;
                //    });
                //}
                //catch (Exception ex)
                //{
                //    try
                //    {
                //        this.Invoke(() =>
                //        {
                //            toolUtils.ToolMessage($"An error occurred while loading the TOC: {ex.Message}", "Error", 0, 1);
                //        });
                //    }
                //    catch { }

                //}
            });

            _taskThreads.Add(thread);
            thread.Start();

            SetEnvironment("ModdingTool");
        }

        // Start loading the TOC file
        //------------------------------------------------------------------------------------------
        private void LoadTOC(string path)
        {

            this.Invoke(() =>
            {
                OverlayHeaderLabel.Text = "Loading 'toc'...";
                OverlayOperationLabel.Text = "-";
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
                            OverlayHeaderLabel.Text = "Loading 'toc'...";
                            OverlayOperationLabel.Text = $"{progress}/{progressTotal} assets";
                        });
                    }
                }
                ++spanIndex;
            }

            this.Invoke(() => OverlayOperationLabel.Text = "-");

            // Load known hashes
            //--------------------------------------------------------------------------------------

            // TODO: Let the user select which hashes to use, depending on the select hashes in the main strip menu
            // "hashes" or files with "hashes_" at the beginning will be loaded.

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

            // Add paths to build the tree
            //--------------------------------------------------------------------------------------
            var root = new TreeNode("Root");

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

            // Sort all nodes to make it easier to find files
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

                    dataGridView_Files.Rows.Add(asset.Name, displaySize, archive, span, asset.Id);
                }
            }

            dataGridView_Files.ResumeLayout();
            dataGridView_Files.ClearSelection();
        }

        #endregion

        #region User Input

        // Load TOC menu item, Ctrl + O
        //------------------------------------------------------------------------------------------
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

        // Additional menu item buttons
        //------------------------------------------------------------------------------------------
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = new SettingsWindow();
            settings.ShowDialog();
        }
        private void searchByNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow(_assets, _assetsByPath);
            searchWindow.Show();
        }
        private void extractToStageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var window = new StageSelector();
            //window.ShowDialog();

            //if (window.Stage == null) return;

            var cwd = Directory.GetCurrentDirectory();
            var path = Path.Combine(cwd, "stages");
            var stagePath = Path.Combine(path, "test");
            if (!Directory.Exists(stagePath)) Directory.CreateDirectory(stagePath);

            string[] assetNames = getSelectedAssetsNames();
            ulong[] assetIDs = getSelectedAssetsIDs();
            byte[] spans = getSelectedAssetsSpans();

            for (int i = 0; i < assetNames.Length; i++)
            {
                string assetPath = Path.Combine(stagePath, $"{spans[i]}", assetNames[i]);

                Debug.WriteLine($"Extracting to stage {assetNames[i]}, with span {spans[i]}, to {assetPath}");
                ExtractionMethods.ExtractAsset(assetIDs[i], spans[i], assetPath, _toc);
            }
        }
        private void extractAsasciiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool toci29;

            if (_toc is TOC_I29)
            { toci29 = true; }
            else
            { toci29 = false; }

            string assetPath = getSelectedAssetsNames()[0];
            ulong assetID = getSelectedAssetsIDs()[0];
            byte assetSpan = getSelectedAssetsSpans()[0];

            ExtractAsciiWindow extractAsciiWindow = new ExtractAsciiWindow(assetPath, toci29, assetID, assetSpan, _toc);

            extractAsciiWindow.ShowDialog();
        }
        private void extractAsddsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string outputPath;

            string assetNameFinalDDS = Path.GetFileNameWithoutExtension(getSelectedAssetsNames()[0]) + ".dds";
            string assetNameTexture = Path.GetFileNameWithoutExtension(getSelectedAssetsNames()[0]);

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save .dds file";
                saveFileDialog.Filter = "DDS Files (*.dds)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FileName = assetNameFinalDDS;
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputPath = saveFileDialog.FileName;

                    string choosenName = Path.GetFileName(outputPath);

                    string silkFolder = Path.GetDirectoryName(outputPath);
                    string silkInput = Path.Combine(silkFolder, assetNameTexture);
                    string silkOutput = Path.Combine(silkFolder, assetNameFinalDDS);
                    string silkRename = Path.Combine(silkFolder, choosenName);

                    ExtractionMethods.ExtractAsset(getSelectedAssetsIDs()[0], getSelectedAssetsSpans()[0], silkInput, _toc);

                    var p = new SpideyTextureScaler.Program();

                    DirectoryInfo outputdirInfo = new DirectoryInfo(silkFolder);
                    FileInfo sourceInfo = new FileInfo(silkInput);

                    p.texturestats = new List<TextureBase>()
                    {
                        new Source(),
                        new DDS(),
                        new Output(),
                    };

                    p.Extract(sourceInfo, outputdirInfo, true);

                    // Cleanup and rename

                    if (File.Exists(silkInput))
                    {
                        File.Delete(silkInput);
                    }
                    if (File.Exists(silkOutput))
                    {
                        File.Move(silkOutput, silkRename);
                    }
                }
            }
        }

        // Extract all selected asset rows
        //--------------------------------------------------------------------------------------
        private void ExtractSelectedtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Files.SelectedRows.Count == 1)
            {
                string assetPath = getSelectedAssetsNames()[0];
                ulong assetID = getSelectedAssetsIDs()[0];

                ExtractOneAssetDialog(assetPath, getSelectedAssetsSpans()[0], assetID);
            }
            else
            {
                ExtractMultipleAssetsDialog(getSelectedAssetsNames(), getSelectedAssetsSpans(), getSelectedAssetsIDs());
            }
        }

        // Handle right click for context menu
        //------------------------------------------------------------------------------------------
        private void dataGridView_Files_MouseClick(object sender, MouseEventArgs e)
        {
            int selectedRows = dataGridView_Files.SelectedRows.Count;
            var hitTestInfo = dataGridView_Files.HitTest(e.X, e.Y);

            extractAsasciiToolStripMenuItem.Visible = false;
            extractAsddsToolStripMenuItem.Visible = false;

            // Check if it's a valid right-click and within the DataGridView
            if (e.Button == MouseButtons.Right && hitTestInfo.RowIndex >= 0 && hitTestInfo.ColumnIndex >= 0)
            {
                // If no rows are selected, select the row under the cursor
                if (selectedRows == 0)
                {
                    dataGridView_Files.ClearSelection();
                    dataGridView_Files.Rows[hitTestInfo.RowIndex].Selected = true;
                    selectedRows = 1; // Update the count after selecting
                }

                // Set the current cell for single-row actions
                dataGridView_Files.CurrentCell = dataGridView_Files[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex];

                string assetType = Path.GetExtension(dataGridView_Files.CurrentCell.Value?.ToString() ?? string.Empty);

                // Toggle menu items based on the asset type
                extractAsasciiToolStripMenuItem.Visible = assetType == ".model";
                extractAsddsToolStripMenuItem.Visible = assetType == ".texture";

                // Update the context menu text based on the selection count
                assetSelectedToolStripMenuItem.Text = selectedRows > 1
                    ? $"{selectedRows} assets selected"
                    : $"{selectedRows} asset selected";

                copyPathToolStripMenuItem.Text = selectedRows > 1 ? "Copy paths" : "Copy path";
                copyHashToolStripMenuItem.Text = selectedRows > 1 ? "Copy hashes" : "Copy hash";

                contextMenuStrip1.Show(dataGridView_Files, new Point(e.X, e.Y));
            }

        }

        #region Open additional tools

        // Set environment and open Tools
        //------------------------------------------------------------------------------------------
        private Spandex.Form1 spandexForm;
        private SpideyTexture silkTextureForm;
        private void SetEnvironment(string Program, bool OpenWith = false)
        {
            if (Program == "Spandex")
            {
                if (spandexForm == null || spandexForm.IsDisposed)
                {
                    string[] args = { "" };

                    spandexForm = new Spandex.Form1(args, OpenWith);
                }

                LoadFormIntoPanel(spandexForm, panel_Main);
                this.Text = "WebWorks - Spandex";
            }
            if (Program == "SilkTexture")
            {
                if (silkTextureForm == null || silkTextureForm.IsDisposed)
                {
                    var program = new SpideyTextureScaler.Program
                    {
                        texturestats = new List<TextureBase>
                    {
                        new Source(),
                        new DDS(),
                        new Output()
                    }
                    };

                    silkTextureForm = new SpideyTexture(program, OpenWith);
                }

                LoadFormIntoPanel(silkTextureForm, panel_Main);
                this.Text = "WebWorks - Silk Texture";
            }
            if (Program == "HashTool")
            {
                HashTool hashTool = new HashTool();
                hashTool.Show();
            }
            if (Program == "ModdingTool")
            {
                panel_Main.Visible = false;
                splitContainer1.Visible = true;
                this.Text = "WebWorks - Modding Tool";
            }
        }

        private void spandexToolStripMenuItem_Click(object sender, EventArgs e)
        { SetEnvironment("Spandex"); }
        private void silkTextureToolStripMenuItem_Click(object sender, EventArgs e)
        { SetEnvironment("SilkTexture"); }
        private void calculateHashToolStripMenuItem_Click(object sender, EventArgs e)
        { SetEnvironment("HashTool"); }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        { SetEnvironment("ModdingTool"); }
        private void openMaterial_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetEnvironment("Spandex", false);
            spandexForm.Open();
        }
        private void OpenTexture_toolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetEnvironment("SilkTexture", false);
            silkTextureForm.Open();
        }

        #endregion

        #endregion

        #region Extraction methods

        // Misc
        //------------------------------------------------------------------------------------------
        private ulong[] getSelectedAssetsIDs()
        {
            ulong[] assetIDs = new ulong[dataGridView_Files.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView_Files.SelectedRows)
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

        private string[] getSelectedAssetsNames()
        {
            string[] assetNames = new string[dataGridView_Files.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView_Files.SelectedRows)
            {
                assetNames[i] = OverlayHeaderLabel.Text.Replace("Current directory: ", "") + "\\" + selectedRow.Cells[0].Value?.ToString();
                i++;
            }
            return assetNames;
        }

        private byte[] getSelectedAssetsSpans()
        {
            byte[] spans = new byte[dataGridView_Files.SelectedRows.Count];
            int i = 0;

            foreach (DataGridViewRow selectedRow in dataGridView_Files.SelectedRows)
            {
                // Get the value of the 4th column (span as byte)
                spans[i] = Convert.ToByte(selectedRow.Cells[3].Value);
                i++;
            }
            return spans;
        }

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
        }
    }
}
