using Microsoft.VisualBasic;
using Newtonsoft.Json;
using SpideyToolbox.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpideyToolbox
{
    public partial class SettingsWindow : Form
    {
        //------------------------------------------------------------------------------------------
        public SettingsWindow()
        {
            InitializeComponent();
            ToolboxStyle.ApplyToolBoxStyle(this, Handle);

            ShowIcon = false;
            MaximizeBox = false;
            MinimizeBox = false;

            // Assign controls
            AppSettings settings = LoadSettings();
            
            check_AutoLoadToc.Checked = settings._autoloadRecent;
            textBox_AuthorName.Text = settings._authorName;
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
            AppSettings settings = new AppSettings
            {
                _autoloadRecent = check_AutoLoadToc.Checked,
                _authorName = textBox_AuthorName.Text,
            };

            SaveSettings(settings);

            Close();
        }
    }
}
