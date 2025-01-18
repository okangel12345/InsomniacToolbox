using DAT1;
using Newtonsoft.Json.Linq;
using Spiderman;
using SpideyToolbox;
using SpideyToolbox.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebWorks.Windows
{
    public partial class PackStageWindow : Form
    {
        private bool _initializing = true;
        private Dictionary<Asset, string> _mainWindowReplacedAssets;
        private Dictionary<Asset, string> _mainWindowAddedAssets;

        private string _modName;
        private string _author;
        private string _gameId;
        private string _description;
        private string _cover;

        private List<Game> _games = new();
        private BindingList<AssetReplace> _assets = new BindingList<AssetReplace>();

        class Game
        {
            public string Name { get; set; }
            public string Id;
        }

        class AssetReplace
        {
            public Asset Asset;
            public bool IsNew = false;

            public string OriginalAssetName { get => (IsNew ? $"new ({Asset.RefPath})" : Asset.Name); }
            public string OriginalAssetNameToolTip { get => (Asset.FullPath == null ? "" : $"Path: {Asset.FullPath}\n") + $"ID: {Asset.Id:X016}\nSpan: {Asset.Span}\nArchive: {Asset.Archive}"; }

            public string ReplacingFileName { get; set; }
            public string ReplacingFileNameToolTip { get; set; }
        }

        public PackStageWindow(Dictionary<Asset, string> replacedAssets, Dictionary<Asset, string> addedAssets, TOCBase toc)
        {
            InitializeComponent();
            ToolUtils.ApplyStyle(this, Handle);

            _initializing = false;

            SettingsWindow sets = new SettingsWindow();
            AppSettings settings = sets.LoadSettings();

            string defaultAuthorName = settings._authorName;

            if (defaultAuthorName != null)
            {
                _author = defaultAuthorName;
                AuthorTextBox.Text = defaultAuthorName;
            }


            MakeGamesSelector(toc);

            _mainWindowReplacedAssets = replacedAssets;
            _mainWindowAddedAssets = addedAssets;
            UpdateAssetsList();
        }

        private void MakeGamesSelector(TOCBase toc)
        {
            _games.Clear();
            _games.Add(new Game() { Name = "Marvel's Spider-Man Remastered", Id = "MSMR" });
            _games.Add(new Game() { Name = "Marvel's Spider-Man: Miles Morales", Id = "MM" });
            _games.Add(new Game() { Name = "Ratchet & Clank: Rift Apart", Id = "RCRA" });
            _games.Add(new Game() { Name = "Marvel's Spider-Man 2", Id = "MSM2" });
            _games.Add(new Game() { Name = "i33", Id = "i33" });

            GameComboBox.DataSource = _games;
            GameComboBox.DisplayMember = "Name";

            var selected = _games[0];
            if (toc is TOC_I29)
            {
                selected = _games[2];
            }

            _gameId = selected.Id;
            GameComboBox.SelectedItem = selected;
        }

        private void UpdateAssetsList()
        {
            _assets.Clear();

            foreach (var asset in _mainWindowReplacedAssets.Keys)
            {
                var path = _mainWindowReplacedAssets[asset];
                _assets.Add(new AssetReplace
                {
                    Asset = asset,
                    ReplacingFileName = Path.GetFileName(path),
                    ReplacingFileNameToolTip = path
                });
            }

            foreach (var asset in _mainWindowAddedAssets.Keys)
            {
                var path = _mainWindowAddedAssets[asset];
                _assets.Add(new AssetReplace
                {
                    Asset = asset,
                    IsNew = true,
                    ReplacingFileName = Path.GetFileName(path),
                    ReplacingFileNameToolTip = path
                });
            }

            AssetsList.DataSource = _assets;
        }

        private void RefreshButton()
        {
            var isEmpty = (string s) => { return (s == null || s == ""); };

            SaveStageButton.Visible = (!isEmpty(_modName) && !isEmpty(_author));
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            _modName = NameTextBox.Text;
            RefreshButton();
        }

        private void AuthorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            _author = AuthorTextBox.Text;
            RefreshButton();
        }

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            _description = DescriptionTextBox.Text;
            RefreshButton();
        }

        private void GameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            _gameId = ((Game)GameComboBox.SelectedItem).Id;
        }

        private void SaveStageButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = "Save .stage...";
            dialog.Filter = "Stage (*.stage)|*.stage|All files (*.*)|*.*";
            dialog.DefaultExt = "*.stage";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(AuthorTextBox.Text) || string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("There are one or two missing required fields.");
                return;
            }

            var tocHasTextureSections = (_gameId != "MSMR" && _gameId != "MM");
            if (_mainWindowAddedAssets.Count > 0 && tocHasTextureSections)
            {
                MessageBox.Show($"Warning: adding new .texture assets is not implemented.\n\nThe game might work incorrectly with these or even crash because of them.", "Warning", MessageBoxButtons.OK);
            }

            var headerless = new JArray();
            var stageFileName = dialog.FileName;
            try
            {
                using var f = new FileStream(stageFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                using var zip = new ZipArchive(f, ZipArchiveMode.Create);

                void WriteAssets(Dictionary<Asset, string> assets)
                {
                    foreach (var asset in assets.Keys)
                    {
                        var path = assets[asset];
                        var bytes = File.ReadAllBytes(path);

                        var assetPath = asset.RefPath;
                        if (asset.FullPath != null)
                            assetPath = $"{asset.Span}/" + DAT1.Utils.Normalize(asset.FullPath);

                        if (!asset.HasHeader)
                        {
                            headerless.Add(assetPath);
                        }

                        var entry = zip.CreateEntry(assetPath);
                        using var ef = entry.Open();
                        ef.Write(bytes, 0, bytes.Length);
                    }
                }

                void MoveCover()
                {
                    if (_cover != null)
                    {
                        try { 
                        var coverBytes = File.ReadAllBytes(_cover);

                        var entry = zip.CreateEntry("cover.png");
                        using var ef = entry.Open();
                        ef.Write(coverBytes, 0, coverBytes.Length);
                        }
                        catch { }
                    }
                }

                MoveCover();

                WriteAssets(_mainWindowReplacedAssets);
                WriteAssets(_mainWindowAddedAssets);

                {
                    JObject j = new()
                    {
                        ["game"] = _gameId,
                        ["name"] = _modName,
                        ["author"] = _author,
                        ["description"] = _description,
                        ["headerless"] = headerless
                    };

                    var text = j.ToString();
                    var data = Encoding.UTF8.GetBytes(text);

                    var entry = zip.CreateEntry("info.json");
                    using var ef = entry.Open();
                    ef.Write(data, 0, data.Length);
                }
            }
            catch
            {
                MessageBox.Show($"Error: failed to write '{stageFileName}'!", "Error", MessageBoxButtons.OK);
                return;
            }

            Close();

        }

        private void AssetsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow row in AssetsList.SelectedRows)
                {
                    // Assuming the row is bound to an object of type AssetReplace
                    var assetReplace = row.DataBoundItem as AssetReplace;

                    if (assetReplace != null)
                    {
                        if (assetReplace.IsNew)
                        {
                            _mainWindowAddedAssets.Remove(assetReplace.Asset);
                        }
                        else
                        {
                            _mainWindowReplacedAssets.Remove(assetReplace.Asset);
                        }
                    }
                }
                UpdateAssetsList();
            }
        }

        private void SelectCoverButton_Click(object sender, EventArgs e)
        {
            if (_initializing) return;
            RefreshButton();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PNG Files (*.png)|*.png";
                openFileDialog.Title = "Select a Cover Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var selectedImage = new Bitmap(openFileDialog.FileName);

                    if (selectedImage.Width == 320 && selectedImage.Height == 180)
                    {
                        CoverPictureBox.Image = selectedImage;

                        _cover = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("The image must be 320x180 pixels.", "Invalid Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
