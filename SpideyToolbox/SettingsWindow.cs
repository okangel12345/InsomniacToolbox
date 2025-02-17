﻿using Newtonsoft.Json;
using SpideyToolbox.Utilities;

namespace SpideyToolbox
{
    public partial class SettingsWindow : Form
    {
        //------------------------------------------------------------------------------------------
        public SettingsWindow()
        {
            InitializeComponent();
            ModdingLab.ToolboxStyle.ApplyToolBoxStyle(this, Handle);

            ShowIcon = false;
            MaximizeBox = false;
            MinimizeBox = false;

            // Assign controls
            AppSettings settings = LoadSettings();

            textBox_AuthorName.Text = settings._authorName;

            check_AutoLoadToc.Checked = settings._autoloadRecent;
            check_LoadModToc.Checked = settings._loadtocModded;
            check_ExperimentalFeatures.Checked = settings._experimentalFeatures;
        }

        // Save and load methods
        //------------------------------------------------------------------------------------------
        public void SaveSettings(AppSettings settings)
        {
            string settingsFile = "settings.json";
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);

            File.WriteAllText(settingsFile, json);
        }

        public AppSettings LoadSettings()
        {
            string settingsFile = "settings.json";
            if (File.Exists(settingsFile))
            {
                string json = File.ReadAllText(settingsFile);
                return JsonConvert.DeserializeObject<AppSettings>(json);
            }
            return new AppSettings();
        }

        // User input to save the settings
        //------------------------------------------------------------------------------------------
        private void btn_Save_Click_1(object sender, EventArgs e)
        {
            AppSettings currentSettings = LoadSettings();
            AppSettings settings = new AppSettings
            {
                _autoloadRecent = check_AutoLoadToc.Checked,
                _authorName = textBox_AuthorName.Text,
                _loadtocModded = check_LoadModToc.Checked,
                _experimentalFeatures = check_ExperimentalFeatures.Checked,

                _recentTOC1 = currentSettings._recentTOC1,
                _recentTOC2 = currentSettings._recentTOC2,
                _recentTOC3 = currentSettings._recentTOC3,
                _recentTOC4 = currentSettings._recentTOC4,
                _recentTOC5 = currentSettings._recentTOC5,

                _accentColor = currentSettings._accentColor,
                _accentColorGrid = currentSettings._accentColorGrid,
            };

            SaveSettings(settings);

            Close();
        }
    }
}
