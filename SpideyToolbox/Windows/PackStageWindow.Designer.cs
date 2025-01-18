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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)CoverPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AssetsList).BeginInit();
            SuspendLayout();
            // 
            // CoverPictureBox
            // 
            CoverPictureBox.Image = FormIcons.DefaultCover;
            CoverPictureBox.Location = new Point(12, 30);
            CoverPictureBox.Name = "CoverPictureBox";
            CoverPictureBox.Size = new Size(320, 180);
            CoverPictureBox.TabIndex = 0;
            CoverPictureBox.TabStop = false;
            // 
            // NameTextBox
            // 
            NameTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            NameTextBox.BackColor = Color.FromArgb(22, 22, 22);
            NameTextBox.BorderStyle = BorderStyle.FixedSingle;
            NameTextBox.ForeColor = SystemColors.Control;
            NameTextBox.Location = new Point(385, 30);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(362, 23);
            NameTextBox.TabIndex = 1;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
            // 
            // AuthorTextBox
            // 
            AuthorTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AuthorTextBox.BackColor = Color.FromArgb(22, 22, 22);
            AuthorTextBox.BorderStyle = BorderStyle.FixedSingle;
            AuthorTextBox.ForeColor = SystemColors.Control;
            AuthorTextBox.Location = new Point(385, 59);
            AuthorTextBox.Name = "AuthorTextBox";
            AuthorTextBox.Size = new Size(362, 23);
            AuthorTextBox.TabIndex = 4;
            AuthorTextBox.TextChanged += AuthorTextBox_TextChanged;
            // 
            // GameComboBox
            // 
            GameComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            GameComboBox.BackColor = Color.FromArgb(22, 22, 22);
            GameComboBox.FlatStyle = FlatStyle.Flat;
            GameComboBox.ForeColor = SystemColors.Control;
            GameComboBox.FormattingEnabled = true;
            GameComboBox.Location = new Point(385, 88);
            GameComboBox.Name = "GameComboBox";
            GameComboBox.Size = new Size(362, 23);
            GameComboBox.TabIndex = 5;
            GameComboBox.SelectedIndexChanged += GameComboBox_SelectedIndexChanged;
            // 
            // AssetsList
            // 
            AssetsList.AllowUserToAddRows = false;
            AssetsList.AllowUserToResizeColumns = false;
            AssetsList.AllowUserToResizeRows = false;
            AssetsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AssetsList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AssetsList.Columns.AddRange(new DataGridViewColumn[] { OriginalAssetName, ReplacingFileName, OriginalAssetNameToolTip, ReplacingFileNameToolTip });
            AssetsList.Location = new Point(12, 279);
            AssetsList.Name = "AssetsList";
            AssetsList.RowHeadersVisible = false;
            AssetsList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AssetsList.Size = new Size(735, 338);
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
            DescriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            DescriptionTextBox.BackColor = Color.FromArgb(22, 22, 22);
            DescriptionTextBox.BorderStyle = BorderStyle.None;
            DescriptionTextBox.ForeColor = SystemColors.ActiveBorder;
            DescriptionTextBox.Location = new Point(338, 117);
            DescriptionTextBox.Name = "DescriptionTextBox";
            DescriptionTextBox.Size = new Size(409, 93);
            DescriptionTextBox.TabIndex = 7;
            DescriptionTextBox.Text = "Description...";
            DescriptionTextBox.TextChanged += DescriptionTextBox_TextChanged;
            // 
            // SaveStageButton
            // 
            SaveStageButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            SaveStageButton.Location = new Point(338, 220);
            SaveStageButton.Name = "SaveStageButton";
            SaveStageButton.Size = new Size(409, 23);
            SaveStageButton.TabIndex = 8;
            SaveStageButton.Text = "Save stage...";
            SaveStageButton.UseVisualStyleBackColor = true;
            SaveStageButton.Click += SaveStageButton_Click;
            // 
            // SelectCoverButton
            // 
            SelectCoverButton.Location = new Point(12, 220);
            SelectCoverButton.Name = "SelectCoverButton";
            SelectCoverButton.Size = new Size(320, 23);
            SelectCoverButton.TabIndex = 9;
            SelectCoverButton.Text = "Select cover...";
            SelectCoverButton.UseVisualStyleBackColor = true;
            SelectCoverButton.Click += SelectCoverButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ActiveBorder;
            label1.Location = new Point(335, 38);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 10;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ActiveBorder;
            label2.Location = new Point(335, 67);
            label2.Name = "label2";
            label2.Size = new Size(47, 15);
            label2.TabIndex = 11;
            label2.Text = "Author:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ActiveBorder;
            label3.Location = new Point(335, 96);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 12;
            label3.Text = "Game:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ActiveBorder;
            label4.Location = new Point(12, 261);
            label4.Name = "label4";
            label4.Size = new Size(222, 15);
            label4.TabIndex = 13;
            label4.Text = "Assets to replace... (Press DEL to remove)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ActiveBorder;
            label5.Location = new Point(12, 12);
            label5.Name = "label5";
            label5.Size = new Size(98, 15);
            label5.TabIndex = 14;
            label5.Text = "Cover (Optional):";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(637, 250);
            button1.Name = "button1";
            button1.Size = new Size(110, 23);
            button1.TabIndex = 15;
            button1.Text = "Add new asset...";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.Location = new Point(521, 250);
            button2.Name = "button2";
            button2.Size = new Size(110, 23);
            button2.TabIndex = 16;
            button2.Text = "Clear all..";
            button2.UseVisualStyleBackColor = true;
            // 
            // PackStageWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 12, 12);
            ClientSize = new Size(759, 629);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SelectCoverButton);
            Controls.Add(SaveStageButton);
            Controls.Add(DescriptionTextBox);
            Controls.Add(AssetsList);
            Controls.Add(GameComboBox);
            Controls.Add(AuthorTextBox);
            Controls.Add(NameTextBox);
            Controls.Add(CoverPictureBox);
            Name = "PackStageWindow";
            Text = "Pack mod as stage";
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
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button button1;
        private Button button2;
    }
}