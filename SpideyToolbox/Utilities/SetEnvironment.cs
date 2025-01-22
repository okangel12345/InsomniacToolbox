using SpideyTextureScaler;
using SpideyToolbox;
using SpideyToolbox.Utilities;
using SpideyToolbox.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebWorks.Windows;
using WebWorks.Windows.Search;
using WebWorks.Windows.Tools;

namespace WebWorks.Utilities
{
    // Different environments of InsomniacToolbox
    //----------------------------------------------------------------------------------------------
    internal class SetEnvironment {

        // Initialize forms

        public static Spandex.Form1 spandexForm;
        public static SpideyTexture silkTextureForm;
        public static QuickGameLaunch quickGameLaunchForm;
        static MainWindow mainWindow = MainWindow.Instance;

        // Main tools
        //------------------------------------------------------------------------------------------
        public static void Spandex()
        {
            if (spandexForm == null || spandexForm.IsDisposed)
            {
                string[] args = { "" };
                spandexForm = new Spandex.Form1(args, mainWindow._selectedHashes);
            }

            LoadFormIntoPanel(spandexForm, mainWindow.panel_Main);
            mainWindow.Text = "WebWorks - Spandex";
        }
        public static void SilkTexture()
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

                silkTextureForm = new SpideyTexture(program);
            }

            LoadFormIntoPanel(silkTextureForm, mainWindow.panel_Main);
            mainWindow.Text = "WebWorks - Silk Texture";
        }
        public static void ModdingTool()
        {
            mainWindow.panel_Main.Visible = false;
            mainWindow.splitContainer1.Visible = true;
            mainWindow.Text = "WebWorks - Modding Tool";
        }
        public static void QuickGameLaunch()
        {
            if (quickGameLaunchForm == null || quickGameLaunchForm.IsDisposed)
            {
                quickGameLaunchForm = new QuickGameLaunch();
            }

            LoadFormIntoPanel(quickGameLaunchForm, mainWindow.panel_Main);
            mainWindow.Text = "WebWorks - Quick Game Launch (Experimental)";
        }

        // Search and jump to
        //------------------------------------------------------------------------------------------
        public static void Search()
        {
            List<SpideyToolbox.Utilities.Asset> _assets = mainWindow._assets;
            Dictionary<string, List<int>> _assetsByPath = mainWindow._assetsByPath;

            SearchWindow searchWindow = new SearchWindow(_assets, _assetsByPath);
            searchWindow.Show();
        }
        public static void JumpTo()
        {
            var window = new JumpToWindow();
            window.ShowDialog();

            if (!window.Jumped) return;
            mainWindow.JumpTo(window.Path.Trim());
        }

        // Two lines
        //------------------------------------------------------------------------------------------
        public static void HashTool()
        {
            HashTool hashTool = new HashTool();
            hashTool.Show();
        }
        public static void Information()
        {
            InformationWindow information = new InformationWindow();
            information.ShowDialog();
        }
        public static void Settings()
        {
            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();
        }

        // Home
        //------------------------------------------------------------------------------------------
        public static void Home()
        {
            mainWindow.splitContainer1.Visible = false;

            ToolUtils toolUtils = new ToolUtils();
            SpideyHome spideyHome = new SpideyHome();

            LoadFormIntoPanel(spideyHome, mainWindow.panel_Main);
        }

        // Helper methods
        //------------------------------------------------------------------------------------------
        public static void LoadFormIntoPanel(Form form, Panel panel)
        {
            mainWindow.splitContainer1.Visible = false;

            form.FormBorderStyle = FormBorderStyle.None;

            form.TopLevel = false;

            panel.Controls.Clear();
            panel.Controls.Add(form);
            panel.Dock = DockStyle.Fill;
            panel.Visible = true;

            form.Dock = DockStyle.Fill;

            form.Show();
        }
    }
}
