﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpideyToolbox.Windows;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace SpideyToolbox.Utilities
{
    internal class ToolUtils
    {
        // Load form into panel
        //------------------------------------------------------------------------------------------
        public void LoadPanel(Form Form, Panel Main_Panel)
        {
            Main_Panel.Controls.Clear();
            Form.TopLevel = false;
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.Dock = DockStyle.Fill;

            Main_Panel.Controls.Add(Form);

            // Show Form1
            Form.Show();
        }

        public void LoadFormIntoPanel(UserControl userControl, Panel panel, bool fillPanel = false)
        {
            if (fillPanel)
            {
                panel.Dock = DockStyle.Fill;
            }

            panel.Controls.Clear();

            userControl.Dock = DockStyle.Fill;
            panel.Controls.Add(userControl);

        }

        // Load form into panel
        //------------------------------------------------------------------------------------------
        public static void CloseWithCtrlW(Form form, object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.W)
            {
                form.Close();
            }
        }

        // Load Style with settings
        //------------------------------------------------------------------------------------------
        public static void ApplyStyle(Control parent, IntPtr hwnd, MenuStrip strip = null, ContextMenuStrip context = null)
        {
            SettingsWindow sets = new SettingsWindow();
            AppSettings settings = sets.LoadSettings();
            ModdingLab.ToolboxStyle.ApplyToolBoxStyle(parent, hwnd, strip, context, settings._accentColor, settings._accentColorGrid);
        }

        // Copy to clipboard
        //------------------------------------------------------------------------------------------
        public static void copyToClipboard(string[] paths)
        {
            string clipboardText = paths.Length == 1 ? paths[0] : string.Join(", ", paths);
            Clipboard.SetText(clipboardText);
        }
    }

    // Modding Tool classes
    //----------------------------------------------------------------------------------------------
    public static class Extensions
    {
        public static void Set<K, V>(this Dictionary<K, V> d, K k, V v)
        {
            if (d.ContainsKey(k))
                d[k] = v;
            else
                d.Add(k, v);
        }

        public static void Update<K, V>(this Dictionary<K, V> d, K k, V v, Func<V, V, V> update)
        {
            if (d.ContainsKey(k))
                d[k] = update(d[k], v);
            else
                d.Add(k, v);
        }
    }

    public static class SizeFormat
    {
        public static string FormatSize(uint bytesCount)
        {
            var v = bytesCount;
            var r = "";
            var u = "B";

            if (v > 1024)
            {
                r = Remainder(v);
                v /= 1024;
                u = "KB";

                if (v > 1024)
                {
                    r = Remainder(v);
                    v /= 1024;
                    u = "MB";

                    if (v > 1024)
                    {
                        r = Remainder(v);
                        v /= 1024;
                        u = "GB";
                    }
                }
            }

            return $"{v}{r} {u}";
        }

        private static string Remainder(uint v)
        {
            if (v % 1024 == 0) return "";
            var v2 = (v % 1024) / 1024.0;
            int v3 = (int)(v2 * 10);
            if (v3 == 0) return ".1";
            return "." + v3;
        }
    }

    public class Asset
    {
        public byte Span { get; set; }
        public ulong Id;
        public uint Size { get; set; }
        public string SizeFormatted { get => SizeFormat.FormatSize(Size); }
        public bool HasHeader;

        public string Name { get; set; }
        public string Archive { get; set; }
        public string FullPath = null;
        public string RefPath { get => $"{Span}/{Id:X016}"; }
    }

    public class AssetWWPROJHelper
    {
        public byte Span { get; }
        public ulong Id { get; }
        public string Name { get; }
        public string Archive { get; }
        public string FullPath { get; }
        public string RefPath { get; }
        public string AssociatedString { get; }

        // Constructor to map Asset to AssetDataWithString and include the associated string
        public AssetWWPROJHelper(Asset asset, string associatedString)
        {
            Span = asset.Span;
            Id = asset.Id;
            Name = asset.Name;
            Archive = asset.Archive;
            FullPath = asset.FullPath;
            RefPath = $"{Span}/{Id:X016}";
            AssociatedString = associatedString;  // Store the associated string
        }
    }
}
