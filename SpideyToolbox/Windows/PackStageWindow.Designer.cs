namespace WebWorks.Windows
{
    partial class PackStageWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CoverPictureBox = new PictureBox();
            NameTextBox = new TextBox();
            AuthorTextBox = new TextBox();
            GameComboBox = new ComboBox();
            AssetsList = new DataGridView();
            OriginalAssetName = new DataGridViewTextBoxColumn();
            ReplacingFileName = new DataGridViewTextBoxColumn();
            OriginalAssetNameToolTip = new DataGridViewTextBoxColumn();
            ReplacingFileNameToolTip = new DataGridViewTextBoxColumn();
            DescriptionTextBox = new RichTextBox();
            SaveStageButton = new Button();
            SelectCoverButton = new Button();
            ((System.ComponentModel.ISupportInitialize)CoverPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AssetsList).BeginInit();
            SuspendLayout();
            // 
            // CoverPictureBox
            // 
            CoverPictureBox.Location = new Point(12, 12);
            CoverPictureBox.Name = "CoverPictureBox";
            CoverPictureBox.Size = new Size(320, 180);
            CoverPictureBox.TabIndex = 0;
            CoverPictureBox.TabStop = false;
            // 
            // NameTextBox
            // 
            NameTextBox.BackColor = Color.FromArgb(22, 22, 22);
            NameTextBox.BorderStyle = BorderStyle.FixedSingle;
            NameTextBox.ForeColor = SystemColors.Control;
            NameTextBox.Location = new Point(338, 12);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(263, 23);
            NameTextBox.TabIndex = 1;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // AuthorTextBox
            // 
            AuthorTextBox.BackColor = Color.FromArgb(22, 22, 22);
            AuthorTextBox.BorderStyle = BorderStyle.FixedSingle;
            AuthorTextBox.ForeColor = SystemColors.Control;
            AuthorTextBox.Location = new Point(338, 41);
            AuthorTextBox.Name = "AuthorTextBox";
            AuthorTextBox.Size = new Size(263, 23);
            AuthorTextBox.TabIndex = 4;
            AuthorTextBox.TextChanged += AuthorTextBox_TextChanged;
            // 
            // GameComboBox
            // 
            GameComboBox.BackColor = Color.FromArgb(22, 22, 22);
            GameComboBox.FlatStyle = FlatStyle.Flat;
            GameComboBox.ForeColor = SystemColors.Control;
            GameComboBox.FormattingEnabled = true;
            GameComboBox.Location = new Point(338, 70);
            GameComboBox.Name = "GameComboBox";
            GameComboBox.Size = new Size(263, 23);
            GameComboBox.TabIndex = 5;
            GameComboBox.SelectedIndexChanged += GameComboBox_SelectedIndexChanged;
            // 
            // AssetsList
            // 
            AssetsList.AllowUserToAddRows = false;
            AssetsList.AllowUserToResizeColumns = false;
            AssetsList.AllowUserToResizeRows = false;
            AssetsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AssetsList.Columns.AddRange(new DataGridViewColumn[] { OriginalAssetName, ReplacingFileName, OriginalAssetNameToolTip, ReplacingFileNameToolTip });
            AssetsList.Location = new Point(12, 231);
            AssetsList.Name = "AssetsList";
            AssetsList.RowHeadersVisible = false;
            AssetsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AssetsList.Size = new Size(589, 386);
            AssetsList.TabIndex = 6;
            AssetsList.KeyDown += AssetsList_KeyDown;
            // 
            // OriginalAssetName
            // 
            OriginalAssetName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            OriginalAssetName.DataPropertyName = "OriginalAssetName";
            OriginalAssetName.HeaderText = "Original Asset Name";
            OriginalAssetName.Name = "OriginalAssetName";
            // 
            // ReplacingFileName
            // 
            ReplacingFileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ReplacingFileName.DataPropertyName = "ReplacingFileName";
            ReplacingFileName.HeaderText = "Replacing File Name";
            ReplacingFileName.Name = "ReplacingFileName";
            // 
            // OriginalAssetNameToolTip
            // 
            OriginalAssetNameToolTip.DataPropertyName = "OriginalAssetNameToolTip";
            OriginalAssetNameToolTip.HeaderText = "OriginalAssetNameToolTip";
            OriginalAssetNameToolTip.Name = "OriginalAssetNameToolTip";
            OriginalAssetNameToolTip.Visible = false;
            // 
            // ReplacingFileNameToolTip
            // 
            ReplacingFileNameToolTip.DataPropertyName = "ReplacingFileNameToolTip";
            ReplacingFileNameToolTip.HeaderText = "ReplacingFileNameToolTip";
            ReplacingFileNameToolTip.Name = "ReplacingFileNameToolTip";
            ReplacingFileNameToolTip.Visible = false;
            // 
            // DescriptionTextBox
            // 
            DescriptionTextBox.BackColor = Color.FromArgb(22, 22, 22);
            DescriptionTextBox.BorderStyle = BorderStyle.None;
            DescriptionTextBox.ForeColor = SystemColors.Control;
            DescriptionTextBox.Location = new Point(338, 99);
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.Size = new Size(263, 93);
            DescriptionTextBox.TabIndex = 7;
            DescriptionTextBox.Text = "";
            DescriptionTextBox.TextChanged += DescriptionTextBox_TextChanged;
            // 
            // SaveStageButton
            // 
            SaveStageButton.Location = new Point(504, 202);
            SaveStageButton.Name = "SaveStageButton";
            SaveStageButton.Size = new Size(97, 23);
            SaveStageButton.TabIndex = 8;
            SaveStageButton.Text = "Save stage...";
            SaveStageButton.UseVisualStyleBackColor = true;
            SaveStageButton.Click += SaveStageButton_Click;
            // 
            // SelectCoverButton
            // 
            SelectCoverButton.Location = new Point(235, 202);
            SelectCoverButton.Name = "SelectCoverButton";
            SelectCoverButton.Size = new Size(97, 23);
            SelectCoverButton.TabIndex = 9;
            SelectCoverButton.Text = "Select cover...";
            SelectCoverButton.UseVisualStyleBackColor = true;
            SelectCoverButton.Click += SelectCoverButton_Click;
            // 
            // PackStageWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(613, 629);
            Controls.Add(SelectCoverButton);
            Controls.Add(SaveStageButton);
            Controls.Add(DescriptionTextBox);
            Controls.Add(AssetsList);
            Controls.Add(GameComboBox);
            Controls.Add(AuthorTextBox);
            Controls.Add(NameTextBox);
            Controls.Add(CoverPictureBox);
            Name = "PackStageWindow";
            Text = "PackStageWindow";
            ((System.ComponentModel.ISupportInitialize)CoverPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)AssetsList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox CoverPictureBox;
        private TextBox NameTextBox;
        private TextBox AuthorTextBox;
        private ComboBox GameComboBox;
        private DataGridView AssetsList;
        private RichTextBox DescriptionTextBox;
        private DataGridViewTextBoxColumn OriginalAssetName;
        private DataGridViewTextBoxColumn ReplacingFileName;
        private DataGridViewTextBoxColumn OriginalAssetNameToolTip;
        private DataGridViewTextBoxColumn ReplacingFileNameToolTip;
        private Button SaveStageButton;
        private Button SelectCoverButton;
    }
}