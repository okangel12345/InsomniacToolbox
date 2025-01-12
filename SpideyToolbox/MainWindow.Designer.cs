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
            statusStrip1 = new StatusStrip();
            OverlayHeaderLabel = new ToolStripStatusLabel();
            OverlayOperationLabel = new ToolStripStatusLabel();
            TreeView_Assets = new TreeView();
            dataGridView_Files = new DataGridView();
            AssetSelected = new DataGridViewCheckBoxColumn();
            FileName = new DataGridViewTextBoxColumn();
            Size = new DataGridViewTextBoxColumn();
            Archive = new DataGridViewTextBoxColumn();
            Span = new DataGridViewTextBoxColumn();
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
            searchToolStripMenuItem = new ToolStripMenuItem();
            searchByNameToolStripMenuItem = new ToolStripMenuItem();
            advancedSearchToolStripMenuItem = new ToolStripMenuItem();
            jumpToHashToolStripMenuItem = new ToolStripMenuItem();
            modToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            calculateHashToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            informationToolStripMenuItem = new ToolStripMenuItem();
            discordToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Files).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            TreeView_Assets.NodeMouseClick += TreeView_Assets_NodeMouseClick;
            // 
            // dataGridView_Files
            // 
            dataGridView_Files.AllowUserToAddRows = false;
            dataGridView_Files.AllowUserToDeleteRows = false;
            dataGridView_Files.AllowUserToResizeRows = false;
            dataGridView_Files.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Files.Columns.AddRange(new DataGridViewColumn[] { AssetSelected, FileName, Size, Archive, Span });
            dataGridView_Files.Dock = DockStyle.Fill;
            dataGridView_Files.Location = new Point(0, 0);
            dataGridView_Files.Name = "dataGridView_Files";
            dataGridView_Files.RowHeadersVisible = false;
            dataGridView_Files.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_Files.Size = new Size(624, 508);
            dataGridView_Files.TabIndex = 2;
            // 
            // AssetSelected
            // 
            AssetSelected.FillWeight = 5F;
            AssetSelected.HeaderText = "";
            AssetSelected.Name = "AssetSelected";
            AssetSelected.Width = 20;
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
            splitContainer1.Panel2.Controls.Add(panel_Main);
            splitContainer1.Panel2.Controls.Add(dataGridView_Files);
            splitContainer1.Size = new Size(939, 508);
            splitContainer1.SplitterDistance = 311;
            splitContainer1.TabIndex = 3;
            // 
            // panel_Main
            // 
            panel_Main.Location = new Point(515, 429);
            panel_Main.Name = "panel_Main";
            panel_Main.Size = new Size(97, 76);
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadTOCToolStripMenuItem, loadRecentToolStripMenuItem, hashesToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadTOCToolStripMenuItem
            // 
            loadTOCToolStripMenuItem.Name = "loadTOCToolStripMenuItem";
            loadTOCToolStripMenuItem.Size = new Size(148, 22);
            loadTOCToolStripMenuItem.Text = "Load TOC...";
            loadTOCToolStripMenuItem.Click += loadTOCToolStripMenuItem_Click;
            // 
            // loadRecentToolStripMenuItem
            // 
            loadRecentToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6 });
            loadRecentToolStripMenuItem.Name = "loadRecentToolStripMenuItem";
            loadRecentToolStripMenuItem.Size = new Size(148, 22);
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
            hashesToolStripMenuItem.Size = new Size(148, 22);
            hashesToolStripMenuItem.Text = "Hashes... >";
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { searchByNameToolStripMenuItem, advancedSearchToolStripMenuItem, jumpToHashToolStripMenuItem });
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(54, 20);
            searchToolStripMenuItem.Text = "Search";
            // 
            // searchByNameToolStripMenuItem
            // 
            searchByNameToolStripMenuItem.Name = "searchByNameToolStripMenuItem";
            searchByNameToolStripMenuItem.Size = new Size(195, 22);
            searchByNameToolStripMenuItem.Text = "Search...";
            // 
            // advancedSearchToolStripMenuItem
            // 
            advancedSearchToolStripMenuItem.Name = "advancedSearchToolStripMenuItem";
            advancedSearchToolStripMenuItem.Size = new Size(195, 22);
            advancedSearchToolStripMenuItem.Text = "Advanced Search...";
            // 
            // jumpToHashToolStripMenuItem
            // 
            jumpToHashToolStripMenuItem.Name = "jumpToHashToolStripMenuItem";
            jumpToHashToolStripMenuItem.Size = new Size(195, 22);
            jumpToHashToolStripMenuItem.Text = "Jump to path or hash...";
            // 
            // modToolStripMenuItem
            // 
            modToolStripMenuItem.Name = "modToolStripMenuItem";
            modToolStripMenuItem.Size = new Size(44, 20);
            modToolStripMenuItem.Text = "Mod";
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
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { calculateHashToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // calculateHashToolStripMenuItem
            // 
            calculateHashToolStripMenuItem.Name = "calculateHashToolStripMenuItem";
            calculateHashToolStripMenuItem.Size = new Size(160, 22);
            calculateHashToolStripMenuItem.Text = "Calculate hash...";
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
            informationToolStripMenuItem.Size = new Size(137, 22);
            informationToolStripMenuItem.Text = "Information";
            // 
            // discordToolStripMenuItem
            // 
            discordToolStripMenuItem.Name = "discordToolStripMenuItem";
            discordToolStripMenuItem.Size = new Size(137, 22);
            discordToolStripMenuItem.Text = "Discord";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(939, 560);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            ForeColor = SystemColors.Control;
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "WebWorks";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Files).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private DataGridViewCheckBoxColumn AssetSelected;
        private DataGridViewTextBoxColumn FileName;
        private DataGridViewTextBoxColumn Size;
        private DataGridViewTextBoxColumn Archive;
        private DataGridViewTextBoxColumn Span;
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
        private ToolStripMenuItem advancedSearchToolStripMenuItem;
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
    }
}
