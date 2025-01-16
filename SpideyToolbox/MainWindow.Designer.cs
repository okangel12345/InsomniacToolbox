namespace SpideyToolbox
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            statusStrip1 = new StatusStrip();
            OverlayHeaderLabel = new ToolStripStatusLabel();
            OverlayOperationLabel = new ToolStripStatusLabel();
            TreeView_Assets = new TreeView();
            dataGridView_Files = new DataGridView();
            FileName = new DataGridViewTextBoxColumn();
            Size = new DataGridViewTextBoxColumn();
            Archive = new DataGridViewTextBoxColumn();
            Span = new DataGridViewTextBoxColumn();
            assetID = new DataGridViewTextBoxColumn();
            splitContainer1 = new SplitContainer();
            panel_Main = new Panel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadTOCToolStripMenuItem = new ToolStripMenuItem();
            loadRecentToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            hashesToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            openMaterial_toolStripMenuItem = new ToolStripMenuItem();
            OpenTexture_toolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            searchByNameToolStripMenuItem = new ToolStripMenuItem();
            jumpToHashToolStripMenuItem = new ToolStripMenuItem();
            modToolStripMenuItem = new ToolStripMenuItem();
            menuItem_ReplacedAssets = new ToolStripMenuItem();
            menuItem_ClearAll = new ToolStripMenuItem();
            menuItem_AddFromStage = new ToolStripMenuItem();
            menuItem_PackAsStage = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            calculateHashToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            spandexToolStripMenuItem = new ToolStripMenuItem();
            silkTextureToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            informationToolStripMenuItem = new ToolStripMenuItem();
            discordToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            assetSelectedToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            ExtractSelectedtoolStripMenuItem = new ToolStripMenuItem();
            extractAsasciiToolStripMenuItem = new ToolStripMenuItem();
            extractAsddsToolStripMenuItem = new ToolStripMenuItem();
            extractToStageToolStripMenuItem = new ToolStripMenuItem();
            replaceAssetToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            copyPathToolStripMenuItem = new ToolStripMenuItem();
            copyHashToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Files).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.BackColor = Color.FromArgb(12, 12, 12);
            statusStrip1.Items.AddRange(new ToolStripItem[] { OverlayHeaderLabel, OverlayOperationLabel });
            statusStrip1.Location = new Point(0, 538);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(939, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // OverlayHeaderLabel
            // 
            OverlayHeaderLabel.Name = "OverlayHeaderLabel";
            OverlayHeaderLabel.Size = new Size(118, 17);
            OverlayHeaderLabel.Text = "toolStripStatusLabel1";
            // 
            // OverlayOperationLabel
            // 
            OverlayOperationLabel.Name = "OverlayOperationLabel";
            OverlayOperationLabel.Size = new Size(118, 17);
            OverlayOperationLabel.Text = "toolStripStatusLabel1";
            // 
            // TreeView_Assets
            // 
            TreeView_Assets.BackColor = Color.FromArgb(22, 22, 22);
            TreeView_Assets.BorderStyle = BorderStyle.FixedSingle;
            TreeView_Assets.Dock = DockStyle.Fill;
            TreeView_Assets.ForeColor = SystemColors.Control;
            TreeView_Assets.Location = new Point(0, 0);
            TreeView_Assets.Name = "TreeView_Assets";
            TreeView_Assets.Size = new Size(311, 508);
            TreeView_Assets.TabIndex = 1;
            TreeView_Assets.AfterSelect += TreeView_Assets_AfterSelect;
            // 
            // dataGridView_Files
            // 
            dataGridView_Files.AllowUserToAddRows = false;
            dataGridView_Files.AllowUserToDeleteRows = false;
            dataGridView_Files.AllowUserToResizeRows = false;
            dataGridView_Files.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Files.Columns.AddRange(new DataGridViewColumn[] { FileName, Size, Archive, Span, assetID });
            dataGridView_Files.Dock = DockStyle.Fill;
            dataGridView_Files.Location = new Point(0, 0);
            dataGridView_Files.Name = "dataGridView_Files";
            dataGridView_Files.RowHeadersVisible = false;
            dataGridView_Files.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Files.Size = new Size(624, 508);
            dataGridView_Files.TabIndex = 2;
            dataGridView_Files.MouseClick += dataGridView_Files_MouseClick;
            // 
            // FileName
            // 
            FileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FileName.FillWeight = 45F;
            FileName.HeaderText = "Assets";
            FileName.MinimumWidth = 50;
            FileName.Name = "FileName";
            FileName.ReadOnly = true;
            // 
            // Size
            // 
            Size.FillWeight = 25F;
            Size.HeaderText = "Size";
            Size.Name = "Size";
            Size.ReadOnly = true;
            Size.Width = 75;
            // 
            // Archive
            // 
            Archive.FillWeight = 19F;
            Archive.HeaderText = "Archive";
            Archive.Name = "Archive";
            Archive.ReadOnly = true;
            // 
            // Span
            // 
            Span.FillWeight = 6F;
            Span.HeaderText = "Span";
            Span.Name = "Span";
            Span.ReadOnly = true;
            Span.Width = 75;
            // 
            // assetID
            // 
            assetID.HeaderText = "assetID";
            assetID.Name = "assetID";
            assetID.Visible = false;
            assetID.Width = 5;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(0, 27);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(TreeView_Assets);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView_Files);
            splitContainer1.Size = new Size(939, 508);
            splitContainer1.SplitterDistance = 311;
            splitContainer1.TabIndex = 3;
            // 
            // panel_Main
            // 
            panel_Main.BackColor = Color.FromArgb(12, 12, 12);
            panel_Main.Location = new Point(891, 372);
            panel_Main.Name = "panel_Main";
            panel_Main.Size = new Size(21, 335);
            panel_Main.TabIndex = 5;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, searchToolStripMenuItem, modToolStripMenuItem, optionsToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(939, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadTOCToolStripMenuItem, loadRecentToolStripMenuItem, hashesToolStripMenuItem, toolStripMenuItem9, openMaterial_toolStripMenuItem, OpenTexture_toolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadTOCToolStripMenuItem
            // 
            loadTOCToolStripMenuItem.Name = "loadTOCToolStripMenuItem";
            loadTOCToolStripMenuItem.ShortcutKeyDisplayString = "";
            loadTOCToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            loadTOCToolStripMenuItem.Size = new Size(212, 22);
            loadTOCToolStripMenuItem.Text = "Load TOC...";
            loadTOCToolStripMenuItem.Click += loadTOCToolStripMenuItem_Click;
            // 
            // loadRecentToolStripMenuItem
            // 
            loadRecentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6 });
            loadRecentToolStripMenuItem.Name = "loadRecentToolStripMenuItem";
            loadRecentToolStripMenuItem.Size = new Size(212, 22);
            loadRecentToolStripMenuItem.Text = "Load Recent...";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(80, 22);
            toolStripMenuItem2.Text = "1";
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(80, 22);
            toolStripMenuItem3.Text = "2";
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(80, 22);
            toolStripMenuItem4.Text = "3";
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(80, 22);
            toolStripMenuItem5.Text = "4";
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(80, 22);
            toolStripMenuItem6.Text = "5";
            // 
            // hashesToolStripMenuItem
            // 
            hashesToolStripMenuItem.Name = "hashesToolStripMenuItem";
            hashesToolStripMenuItem.Size = new Size(212, 22);
            hashesToolStripMenuItem.Text = "Hashes...";
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Enabled = false;
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(212, 22);
            toolStripMenuItem9.Text = "───────────────────────";
            // 
            // openMaterial_toolStripMenuItem
            // 
            openMaterial_toolStripMenuItem.Name = "openMaterial_toolStripMenuItem";
            openMaterial_toolStripMenuItem.ShortcutKeys = Keys.Control | Keys.M;
            openMaterial_toolStripMenuItem.Size = new Size(212, 22);
            openMaterial_toolStripMenuItem.Text = "Open .material...";
            openMaterial_toolStripMenuItem.Click += openMaterial_toolStripMenuItem_Click;
            // 
            // OpenTexture_toolStripMenuItem
            // 
            OpenTexture_toolStripMenuItem.Name = "OpenTexture_toolStripMenuItem";
            OpenTexture_toolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;
            OpenTexture_toolStripMenuItem.Size = new Size(212, 22);
            OpenTexture_toolStripMenuItem.Text = "Open .texture...";
            OpenTexture_toolStripMenuItem.Click += OpenTexture_toolStripMenuItem_Click;
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { searchByNameToolStripMenuItem, jumpToHashToolStripMenuItem });
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(54, 20);
            searchToolStripMenuItem.Text = "Search";
            // 
            // searchByNameToolStripMenuItem
            // 
            searchByNameToolStripMenuItem.Name = "searchByNameToolStripMenuItem";
            searchByNameToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            searchByNameToolStripMenuItem.Size = new Size(195, 22);
            searchByNameToolStripMenuItem.Text = "Search...";
            searchByNameToolStripMenuItem.Click += searchByNameToolStripMenuItem_Click;
            // 
            // jumpToHashToolStripMenuItem
            // 
            jumpToHashToolStripMenuItem.Name = "jumpToHashToolStripMenuItem";
            jumpToHashToolStripMenuItem.Size = new Size(195, 22);
            jumpToHashToolStripMenuItem.Text = "Jump to path or hash...";
            // 
            // modToolStripMenuItem
            // 
            modToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuItem_ReplacedAssets, menuItem_ClearAll, menuItem_AddFromStage, menuItem_PackAsStage });
            modToolStripMenuItem.Name = "modToolStripMenuItem";
            modToolStripMenuItem.Size = new Size(44, 20);
            modToolStripMenuItem.Text = "Mod";
            modToolStripMenuItem.Click += modToolStripMenuItem_Click;
            // 
            // menuItem_ReplacedAssets
            // 
            menuItem_ReplacedAssets.Enabled = false;
            menuItem_ReplacedAssets.Name = "menuItem_ReplacedAssets";
            menuItem_ReplacedAssets.Size = new Size(180, 22);
            menuItem_ReplacedAssets.Text = "0 replaced, 0 new";
            // 
            // menuItem_ClearAll
            // 
            menuItem_ClearAll.Enabled = false;
            menuItem_ClearAll.Name = "menuItem_ClearAll";
            menuItem_ClearAll.Size = new Size(180, 22);
            menuItem_ClearAll.Text = "Clear all";
            menuItem_ClearAll.Click += menuItem_ClearAll_Click;
            // 
            // menuItem_AddFromStage
            // 
            menuItem_AddFromStage.Enabled = false;
            menuItem_AddFromStage.Name = "menuItem_AddFromStage";
            menuItem_AddFromStage.Size = new Size(180, 22);
            menuItem_AddFromStage.Text = "Add from stage...";
            // 
            // menuItem_PackAsStage
            // 
            menuItem_PackAsStage.Name = "menuItem_PackAsStage";
            menuItem_PackAsStage.Size = new Size(180, 22);
            menuItem_PackAsStage.Text = "Pack as stage...";
            menuItem_PackAsStage.Click += menuItem_PackAsStage_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Settings";
            optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { calculateHashToolStripMenuItem, toolStripMenuItem11, toolStripMenuItem1, spandexToolStripMenuItem, silkTextureToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // calculateHashToolStripMenuItem
            // 
            calculateHashToolStripMenuItem.Name = "calculateHashToolStripMenuItem";
            calculateHashToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;
            calculateHashToolStripMenuItem.Size = new Size(215, 22);
            calculateHashToolStripMenuItem.Text = "Calculate hash...    ";
            calculateHashToolStripMenuItem.Click += calculateHashToolStripMenuItem_Click;
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Enabled = false;
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(215, 22);
            toolStripMenuItem11.Text = "───────────────────────";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.ShortcutKeys = Keys.Control | Keys.D1;
            toolStripMenuItem1.Size = new Size(215, 22);
            toolStripMenuItem1.Text = "Modding Tool";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // spandexToolStripMenuItem
            // 
            spandexToolStripMenuItem.Image = WebWorks.Windows.FormIcons.Spiderman_Symbol;
            spandexToolStripMenuItem.Name = "spandexToolStripMenuItem";
            spandexToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D2;
            spandexToolStripMenuItem.Size = new Size(215, 22);
            spandexToolStripMenuItem.Text = "Spandex";
            spandexToolStripMenuItem.Click += spandexToolStripMenuItem_Click;
            // 
            // silkTextureToolStripMenuItem
            // 
            silkTextureToolStripMenuItem.Name = "silkTextureToolStripMenuItem";
            silkTextureToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D3;
            silkTextureToolStripMenuItem.Size = new Size(215, 22);
            silkTextureToolStripMenuItem.Text = "Silk Texture";
            silkTextureToolStripMenuItem.Click += silkTextureToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { informationToolStripMenuItem, discordToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // informationToolStripMenuItem
            // 
            informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            informationToolStripMenuItem.ShortcutKeys = Keys.F11;
            informationToolStripMenuItem.Size = new Size(180, 22);
            informationToolStripMenuItem.Text = "Information";
            // 
            // discordToolStripMenuItem
            // 
            discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            discordToolStripMenuItem.ShortcutKeys = Keys.F12;
            discordToolStripMenuItem.Size = new Size(180, 22);
            discordToolStripMenuItem.Text = "Discord";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { assetSelectedToolStripMenuItem, toolStripMenuItem7, ExtractSelectedtoolStripMenuItem, extractAsasciiToolStripMenuItem, extractAsddsToolStripMenuItem, extractToStageToolStripMenuItem, replaceAssetToolStripMenuItem, toolStripMenuItem8, copyPathToolStripMenuItem, copyHashToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(215, 224);
            // 
            // assetSelectedToolStripMenuItem
            // 
            assetSelectedToolStripMenuItem.BackColor = Color.Black;
            assetSelectedToolStripMenuItem.ForeColor = SystemColors.Control;
            assetSelectedToolStripMenuItem.Name = "assetSelectedToolStripMenuItem";
            assetSelectedToolStripMenuItem.Size = new Size(214, 22);
            assetSelectedToolStripMenuItem.Text = "N assets selected";
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.BackColor = Color.Black;
            toolStripMenuItem7.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripMenuItem7.Enabled = false;
            toolStripMenuItem7.ForeColor = SystemColors.Control;
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(214, 22);
            toolStripMenuItem7.Text = "____________________________";
            // 
            // ExtractSelectedtoolStripMenuItem
            // 
            ExtractSelectedtoolStripMenuItem.BackColor = Color.Black;
            ExtractSelectedtoolStripMenuItem.ForeColor = SystemColors.Control;
            ExtractSelectedtoolStripMenuItem.Name = "ExtractSelectedtoolStripMenuItem";
            ExtractSelectedtoolStripMenuItem.Size = new Size(214, 22);
            ExtractSelectedtoolStripMenuItem.Text = "Extract selected...";
            ExtractSelectedtoolStripMenuItem.Click += ExtractSelectedtoolStripMenuItem_Click;
            // 
            // extractAsasciiToolStripMenuItem
            // 
            extractAsasciiToolStripMenuItem.BackColor = Color.Black;
            extractAsasciiToolStripMenuItem.ForeColor = SystemColors.Control;
            extractAsasciiToolStripMenuItem.Name = "extractAsasciiToolStripMenuItem";
            extractAsasciiToolStripMenuItem.Size = new Size(214, 22);
            extractAsasciiToolStripMenuItem.Text = "Extract as .ascii...";
            extractAsasciiToolStripMenuItem.Click += extractAsasciiToolStripMenuItem_Click;
            // 
            // extractAsddsToolStripMenuItem
            // 
            extractAsddsToolStripMenuItem.BackColor = Color.Black;
            extractAsddsToolStripMenuItem.ForeColor = SystemColors.Control;
            extractAsddsToolStripMenuItem.Name = "extractAsddsToolStripMenuItem";
            extractAsddsToolStripMenuItem.Size = new Size(214, 22);
            extractAsddsToolStripMenuItem.Text = "Extract as .dds...";
            extractAsddsToolStripMenuItem.Click += extractAsddsToolStripMenuItem_Click;
            // 
            // extractToStageToolStripMenuItem
            // 
            extractToStageToolStripMenuItem.BackColor = Color.Black;
            extractToStageToolStripMenuItem.ForeColor = SystemColors.Control;
            extractToStageToolStripMenuItem.Name = "extractToStageToolStripMenuItem";
            extractToStageToolStripMenuItem.Size = new Size(214, 22);
            extractToStageToolStripMenuItem.Text = "Extract to stage...";
            extractToStageToolStripMenuItem.Click += extractToStageToolStripMenuItem_Click;
            // 
            // replaceAssetToolStripMenuItem
            // 
            replaceAssetToolStripMenuItem.BackColor = Color.Black;
            replaceAssetToolStripMenuItem.ForeColor = SystemColors.Control;
            replaceAssetToolStripMenuItem.Name = "replaceAssetToolStripMenuItem";
            replaceAssetToolStripMenuItem.Size = new Size(214, 22);
            replaceAssetToolStripMenuItem.Text = "Replace selected...";
            replaceAssetToolStripMenuItem.Click += replaceAssetToolStripMenuItem_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.BackColor = Color.Black;
            toolStripMenuItem8.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripMenuItem8.Enabled = false;
            toolStripMenuItem8.ForeColor = SystemColors.Control;
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(214, 22);
            toolStripMenuItem8.Text = "____________________________";
            // 
            // copyPathToolStripMenuItem
            // 
            copyPathToolStripMenuItem.BackColor = Color.Black;
            copyPathToolStripMenuItem.ForeColor = SystemColors.Control;
            copyPathToolStripMenuItem.Name = "copyPathToolStripMenuItem";
            copyPathToolStripMenuItem.Size = new Size(214, 22);
            copyPathToolStripMenuItem.Text = "Copy path";
            // 
            // copyHashToolStripMenuItem
            // 
            copyHashToolStripMenuItem.BackColor = Color.Black;
            copyHashToolStripMenuItem.ForeColor = SystemColors.Control;
            copyHashToolStripMenuItem.Name = "copyHashToolStripMenuItem";
            copyHashToolStripMenuItem.Size = new Size(214, 22);
            copyHashToolStripMenuItem.Text = "Copy hash";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(939, 560);
            Controls.Add(panel_Main);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.Control;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "WebWorks";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Files).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel OverlayHeaderLabel;
        private ToolStripStatusLabel OverlayOperationLabel;
        private TreeView TreeView_Assets;
        private DataGridView dataGridView_Files;
        private SplitContainer splitContainer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem loadTOCToolStripMenuItem;
        private ToolStripMenuItem loadRecentToolStripMenuItem;
        private ToolStripMenuItem hashesToolStripMenuItem;
        private ToolStripMenuItem hashestxtToolStripMenuItem;
        private ToolStripMenuItem hashesi30txtToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem searchByNameToolStripMenuItem;
        private ToolStripMenuItem jumpToHashToolStripMenuItem;
        private ToolStripMenuItem modToolStripMenuItem;
        private ToolStripMenuItem calculateHashToolStripMenuItem;
        private ToolStripMenuItem informationToolStripMenuItem;
        private ToolStripMenuItem discordToolStripMenuItem;
        private Panel panel_Main;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem ExtractSelectedtoolStripMenuItem;
        private ToolStripMenuItem assetSelectedToolStripMenuItem;
        private ToolStripMenuItem extractAsasciiToolStripMenuItem;
        private ToolStripMenuItem extractAsddsToolStripMenuItem;
        private ToolStripMenuItem extractToStageToolStripMenuItem;
        private ToolStripMenuItem replaceAssetToolStripMenuItem;
        private ToolStripMenuItem copyPathToolStripMenuItem;
        private ToolStripMenuItem copyHashToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem spandexToolStripMenuItem;
        private ToolStripMenuItem silkTextureToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn Size;
        private DataGridViewTextBoxColumn Archive;
        private DataGridViewTextBoxColumn Span;
        private DataGridViewTextBoxColumn assetID;
        private ToolStripMenuItem openMaterial_toolStripMenuItem;
        private ToolStripMenuItem OpenTexture_toolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem menuItem_ReplacedAssets;
        private ToolStripMenuItem menuItem_ClearAll;
        private ToolStripMenuItem menuItem_AddFromStage;
        private ToolStripMenuItem menuItem_PackAsStage;
    }
}
