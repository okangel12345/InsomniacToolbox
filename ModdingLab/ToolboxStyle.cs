using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ModdingLab
{
    public class ToolboxStyle
    {
        // Public static method to apply all styles to a form
        //------------------------------------------------------------------------------------------
        static Color accent = Color.FromArgb(119, 204, 221);
        static Color accentGrid = Color.FromArgb(17, 130, 135);
        public static void ApplyToolBoxStyle(Control parent, IntPtr hwnd, MenuStrip strip = null, ContextMenuStrip context = null, string accentColor = null, string accentColorGrid = null)
        {
            if (!string.IsNullOrEmpty(accentColor))
            {
                accentColor = accentColor.TrimStart('#');
                if (accentColor.Length == 6)
                {
                    accent = Color.FromArgb(
                        Convert.ToInt32(accentColor.Substring(0, 2), 16),
                        Convert.ToInt32(accentColor.Substring(2, 2), 16),
                        Convert.ToInt32(accentColor.Substring(4, 2), 16)
                    );
                }
            }

            if (!string.IsNullOrEmpty(accentColorGrid))
            {
                accentColorGrid = accentColorGrid.TrimStart('#');
                if (accentColorGrid.Length == 6)
                {
                    accentGrid = Color.FromArgb(
                        Convert.ToInt32(accentColorGrid.Substring(0, 2), 16),
                        Convert.ToInt32(accentColorGrid.Substring(2, 2), 16),
                        Convert.ToInt32(accentColorGrid.Substring(4, 2), 16)
                    );
                }
            }

            ApplySplitContainerStyle(parent);
            ApplyDataGridViewStyle(parent);
            ApplyGroupBoxStyle(parent);
            ApplyButtonStyle(parent);
            SetTitleBarColor(hwnd);
            ApplyMenuStripDark(strip);
            ApplyContextMenuStripDark(context);
            ApplyTreeViewStyle(parent);
        }

        //------------------------------------------------------------------------------------------

        public class CustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                // Draw a black border explicitly
                using (Pen blackPen = new Pen(Color.FromArgb(24, 24, 24)))
                {
                    e.Graphics.DrawRectangle(blackPen, new Rectangle(Point.Empty, e.ToolStrip.Size - new Size(1, 1)));
                }
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                // Ensure the background is fully black
                using (SolidBrush blackBrush = new SolidBrush(Color.FromArgb(18, 18, 18)))
                {
                    e.Graphics.FillRectangle(blackBrush, e.AffectedBounds);
                }
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                // Set menu item background explicitly
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                using (SolidBrush blackBrush = new SolidBrush(Color.FromArgb(18, 18, 18)))
                {
                    e.Graphics.FillRectangle(blackBrush, rect);
                }

                // Highlight selected or hovered items with a gray shade
                if (e.Item.Selected)
                {
                    using (SolidBrush grayBrush = new SolidBrush(accent))
                    {
                        e.Graphics.FillRectangle(grayBrush, rect);
                    }
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    e.Item.ForeColor = Color.White;
                }
            }
        }

        #region Controls

        public static void ApplySplitContainerStyle(Control parent)
        {
            Color splitterColor = Color.FromArgb(64, 64, 64); // Dark splitter color
            int splitterWidth = 6; // Thicker splitter for better visibility
            Color panelBackgroundColor = Color.FromArgb(40, 40, 40); // Dark panel background color

            // Recursively apply styles to all SplitContainer controls in the parent container
            void ApplyStyleRecursive(Control parentControl)
            {
                foreach (Control control in parentControl.Controls)
                {
                    if (control is SplitContainer splitContainer)
                    {
                        // Style the SplitContainer itself
                        splitContainer.BackColor = splitterColor;
                        splitContainer.SplitterWidth = splitterWidth;

                        // Style the panels
                        splitContainer.Panel1.BackColor = panelBackgroundColor;
                        splitContainer.Panel2.BackColor = panelBackgroundColor;

                        // Add custom painting for the splitter
                        splitContainer.Paint += (sender, e) =>
                        {
                            // Define the bounds for the splitter
                            Rectangle splitterBounds = splitContainer.Orientation == Orientation.Horizontal
                                ? new Rectangle(0, splitContainer.SplitterDistance, splitContainer.Width, splitterWidth)
                                : new Rectangle(splitContainer.SplitterDistance, 0, splitterWidth, splitContainer.Height);

                            // Fill the splitter with the specified color
                            using (var brush = new SolidBrush(splitterColor))
                            {
                                e.Graphics.FillRectangle(brush, splitterBounds);
                            }
                        };

                        // Trigger repaint when size or contents change
                        splitContainer.SplitterMoved += (sender, e) => splitContainer.Invalidate();
                    }

                    // Apply recursively to child controls
                    if (control.HasChildren)
                    {
                        ApplyStyleRecursive(control);
                    }
                }
            }

            // Start applying styles from the parent control
            ApplyStyleRecursive(parent);
        }

        public static void ApplyDataGridViewStyle(Control parent)
        {
            Color backgroundColor = Color.FromArgb(12, 12, 12);
            Color gridLineColor = Color.FromArgb(64, 64, 64);
            Color headerBackColor = Color.FromArgb(30, 30, 30);
            Color headerForeColor = Color.Gainsboro; // Slightly dimmer than WhiteSmoke
            Color rowBackColor = Color.FromArgb(20, 20, 20);
            Color rowForeColor = Color.Gainsboro;
            Color selectedRowBackColor = Color.FromArgb(0, 0, 0);
            Color selectedRowForeColor = Color.LightGray; // Slightly dimmer than White
            Color alternatingRowBackColor = Color.FromArgb(25, 25, 25);
            Color customSelectedRowBackColor = accentGrid; // Custom color for selected row background
            Color customSelectedRowForeColor = Color.FromArgb(230, 230, 230); // Custom color for selected row foreground

            void ApplyStyleRecursive(Control parentControl)
            {
                foreach (Control control in parentControl.Controls)
                {
                    if (control is DataGridView dataGridView)
                    {
                        dataGridView.BackgroundColor = backgroundColor;
                        dataGridView.GridColor = gridLineColor;
                        dataGridView.BorderStyle = BorderStyle.None;

                        dataGridView.EnableHeadersVisualStyles = false;
                        dataGridView.ColumnHeadersDefaultCellStyle.BackColor = headerBackColor;
                        dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = headerForeColor;
                        dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
                        dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                        dataGridView.RowsDefaultCellStyle.BackColor = rowBackColor;
                        dataGridView.RowsDefaultCellStyle.ForeColor = rowForeColor;
                        dataGridView.RowsDefaultCellStyle.SelectionBackColor = customSelectedRowBackColor; // Use custom selected row background
                        dataGridView.RowsDefaultCellStyle.SelectionForeColor = customSelectedRowForeColor; // Use custom selected row foreground

                        dataGridView.AlternatingRowsDefaultCellStyle.BackColor = alternatingRowBackColor;

                        dataGridView.RowTemplate.DefaultCellStyle.BackColor = rowBackColor;

                        dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

                        dataGridView.RowHeadersDefaultCellStyle.BackColor = headerBackColor;
                        dataGridView.RowHeadersDefaultCellStyle.ForeColor = headerForeColor;
                        dataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                        dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView.MultiSelect = true;
                    }

                    if (control.HasChildren)
                    {
                        ApplyStyleRecursive(control);
                    }
                }
            }

            ApplyStyleRecursive(parent);
        }

        public static void ApplyTreeViewStyle(Control parent)
        {
            Color backgroundColor = Color.FromArgb(12, 12, 12);
            Color rowForeColor = Color.Gainsboro;
            Color gridLineColor = Color.FromArgb(64, 64, 64);
            Color headerBackColor = Color.FromArgb(30, 30, 30);
            Color headerForeColor = Color.Gainsboro; // Slightly dimmer than WhiteSmoke
            Color rowBackColor = Color.FromArgb(20, 20, 20);
            Color alternatingRowBackColor = Color.FromArgb(25, 25, 25);

            void ApplyStyleRecursive(Control parentControl)
            {
                foreach (Control control in parentControl.Controls)
                {
                    if (control is TreeView treeView)
                    {
                        // General TreeView styling
                        treeView.BackColor = backgroundColor;
                        treeView.ForeColor = rowForeColor;

                        // Style for the node labels
                        treeView.ItemHeight = 25; // Customize node height to make it look more spacious and consistent with the style

                        // Alternate row styling (for every other row)
                        treeView.ForeColor = rowForeColor;
                        treeView.BackColor = backgroundColor;

                        // Set the border color and grid style (if applicable, though TreeView doesn't have a grid)
                        treeView.BorderStyle = BorderStyle.None;

                        // Optional: Set the TreeView's indent for better aesthetics
                        treeView.Indent = 20;
                    }

                    if (control.HasChildren)
                    {
                        ApplyStyleRecursive(control);
                    }
                }
            }

            ApplyStyleRecursive(parent);
        }

        public static void ApplyGroupBoxStyle(Control parent)
        {
            // Configuration for GroupBox styling
            Color borderColor = Color.FromArgb(72, 72, 72);
            Color backgroundColor = Color.FromArgb(30, 30, 30);
            Color textColor = Color.WhiteSmoke;
            Font textFont = new Font("Segoe UI", 9, FontStyle.Regular);

            // Recursively apply styles to all GroupBoxes in the parent container
            void ApplyStyleRecursive(Control parentControl)
            {
                foreach (Control control in parentControl.Controls)
                {
                    if (control is GroupBox groupBox)
                    {
                        // General appearance
                        groupBox.BackColor = backgroundColor;
                        groupBox.ForeColor = textColor;
                        groupBox.Font = textFont;

                        // Attach custom paint event for drawing borders and header
                        groupBox.Paint += (sender, e) => DrawCustomGroupBox(groupBox, e, borderColor);
                    }

                    // Apply recursively to child controls
                    if (control.HasChildren)
                    {
                        ApplyStyleRecursive(control);
                    }
                }
            }

            // Start applying styles from the parent control
            ApplyStyleRecursive(parent);
        }

        private static void DrawCustomGroupBox(GroupBox groupBox, PaintEventArgs e, Color borderColor)
        {
            // Measure the text header
            SizeF textSize = e.Graphics.MeasureString(groupBox.Text, groupBox.Font);
            Rectangle bounds = groupBox.ClientRectangle;

            // Adjust bounds for the text header
            Rectangle textRect = new Rectangle(bounds.X + 6, bounds.Y, (int)textSize.Width + 10, (int)textSize.Height);
            Rectangle borderRect = new Rectangle(bounds.X, bounds.Y + (int)(textSize.Height / 2), bounds.Width - 1, bounds.Height - (int)(textSize.Height / 2) - 1);

            // Draw background
            using (SolidBrush backgroundBrush = new SolidBrush(groupBox.BackColor))
            {
                e.Graphics.FillRectangle(backgroundBrush, bounds);
            }

            // Create rounded rectangle for the border
            using (GraphicsPath borderPath = CreateRoundedRectanglePath(borderRect, 10)) // 10 is the corner radius
            {
                // Draw border
                using (Pen borderPen = new Pen(borderColor))
                {
                    e.Graphics.DrawPath(borderPen, borderPath);
                }
            }

            // Draw text
            TextRenderer.DrawText(e.Graphics, groupBox.Text, groupBox.Font, textRect, groupBox.ForeColor, TextFormatFlags.Left);
        }

        // Button style will be the same for all forms
        // It's based on QT's button design.
        public static void ApplyButtonStyle(Control parent)
        {
            // Configuration
            bool roundedButtons = true;
            FlatStyle flatStyle = FlatStyle.Flat;
            int borderSize = 2;
            Color borderColor = Color.FromArgb(72, 72, 72);

            //Color topColor = Color.FromArgb(64,64,64);
            //Color bottomColor = Color.FromArgb(54,54,54);

            Color topColor = Color.FromArgb(54, 54, 54);
            Color bottomColor = Color.FromArgb(44, 44, 44);

            // Recursively apply styles to all buttons
            void ApplyStyleRecursive(Control parentControl)
            {
                foreach (Control control in parentControl.Controls)
                {
                    if (control is Button button)
                    {
                        // Set basic button styles
                        button.FlatStyle = flatStyle;
                        button.FlatAppearance.BorderSize = borderSize;
                        button.FlatAppearance.BorderColor = borderColor;
                        button.ForeColor = Color.WhiteSmoke;

                        // Attach custom paint handlers based on configuration
                        button.Paint += roundedButtons
                            ? ((sender, e) => ApplyGradientRounded(button, e, topColor, bottomColor))
                            : (sender, e) => ApplyGradient(button, e, topColor, bottomColor);

                        // Invalidate button to handle hover effects
                        button.MouseEnter += (sender, e) => button.Invalidate();
                        button.MouseLeave += (sender, e) => button.Invalidate();
                    }

                    // Apply recursively to child controls
                    if (control.HasChildren)
                    {
                        ApplyStyleRecursive(control);
                    }
                }
            }

            // Start applying styles from the parent control
            ApplyStyleRecursive(parent);
        }

        private static void ApplyGradient(Button button, PaintEventArgs e, Color topColor, Color bottomColor)
        {
            // Hover effect colors
            bool isHovered = button.ClientRectangle.Contains(button.PointToClient(Cursor.Position));
            Color currentTopColor = isHovered ? ControlPaint.Light(topColor, 0.2f) : topColor;
            Color currentBottomColor = isHovered ? ControlPaint.Light(bottomColor, 0.2f) : bottomColor;

            // Fill background with gradient
            using (var brush = new LinearGradientBrush(button.ClientRectangle, currentTopColor, currentBottomColor, 90f))
            {
                e.Graphics.FillRectangle(brush, button.ClientRectangle);
            }

            // Draw button text
            TextRenderer.DrawText(e.Graphics, button.Text, button.Font, button.ClientRectangle, button.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // Draw border
            ControlPaint.DrawBorder(e.Graphics, button.ClientRectangle, button.FlatAppearance.BorderColor, ButtonBorderStyle.Solid);
        }

        private static void ApplyGradientRounded(Button button, PaintEventArgs e, Color topColor, Color bottomColor)
        {
            // Rounded corners radius
            int cornerRadius = 4; // Adjust to change roundness

            // Hover effect colors
            bool isHovered = button.ClientRectangle.Contains(button.PointToClient(Cursor.Position));
            Color currentTopColor = isHovered ? ControlPaint.Light(topColor, 0.2f) : topColor;
            Color currentBottomColor = isHovered ? ControlPaint.Light(bottomColor, 0.2f) : bottomColor;

            // Adjust bounds slightly inward to avoid clipping the border
            Rectangle adjustedBounds = new Rectangle(0, 0, button.Width - 1, button.Height - 1);

            // Create a rounded rectangle path
            using (var path = CreateRoundedRectanglePath(adjustedBounds, cornerRadius))
            {
                // Clear the button background for transparency
                e.Graphics.Clear(button.Parent.BackColor);

                // Clip the drawing area to the rounded path
                e.Graphics.SetClip(path);

                // Fill the button background with a gradient
                using (var brush = new LinearGradientBrush(adjustedBounds, currentTopColor, currentBottomColor, 90f))
                {
                    e.Graphics.FillPath(brush, path);
                }

                // Reset clipping to default for drawing text and border
                e.Graphics.ResetClip();

                // Draw the button's text
                TextRenderer.DrawText(
                    e.Graphics,
                    button.Text,
                    button.Font,
                    adjustedBounds,
                    button.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                // Draw the rounded border
                using (var borderPen = new Pen(button.FlatAppearance.BorderColor, 1))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Ensures smooth rounded edges
                    e.Graphics.DrawPath(borderPen, path);
                }
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            // Top-left corner
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);

            // Top-right corner
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);

            // Bottom-right corner
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);

            // Bottom-left corner
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure(); // Closes the path to form the rounded rectangle
            return path;
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_CAPTION_COLOR = 35;

        // Static method to set title bar color
        public static void SetTitleBarColor(IntPtr hwnd)
        {
            Color color = Color.FromArgb(26, 26, 26);

            // Swap the Red and Blue components of the color
            Color swappedColor = Color.FromArgb(color.A, color.B, color.G, color.R);

            // Multiply each RGB component by 0.7 to darken the color
            int red = (int)(swappedColor.R * 0.7);
            int green = (int)(swappedColor.G * 0.7);
            int blue = (int)(swappedColor.B * 0.7);

            // Ensure that the values are within the valid range [0, 255]
            red = Math.Max(0, Math.Min(255, red));
            green = Math.Max(0, Math.Min(255, green));
            blue = Math.Max(0, Math.Min(255, blue));

            // Create a new color with the modified values
            Color finalColor = Color.FromArgb(swappedColor.A, red, green, blue);

            // Convert the modified color to ARGB format and set the window title bar color
            int finalColorValue = finalColor.ToArgb() & 0x00FFFFFF;

            // Call DwmSetWindowAttribute to change the title bar color
            DwmSetWindowAttribute(hwnd, DWMWA_CAPTION_COLOR, ref finalColorValue, sizeof(int));
        }

        public static void ApplyContextMenuStripDark(ContextMenuStrip contextMenuStrip, Color? accentColor = null)
        {
            if (contextMenuStrip != null)
            {
                contextMenuStrip.BackColor = Color.FromArgb(25, 25, 25); // ContextMenuStrip background set to black
                contextMenuStrip.ForeColor = Color.White;

                contextMenuStrip.Renderer = new CustomToolStripRenderer();

                // Loop through all items in the ContextMenuStrip
                foreach (var item in contextMenuStrip.Items)
                {
                    // Check if the item is a ToolStripMenuItem
                    if (item is ToolStripMenuItem menuItem)
                    {
                        ApplyMenuItemStyle(menuItem); // Apply style only if it's a ToolStripMenuItem
                    }
                }
            }
        }

        public static void ApplyMenuStripDark(MenuStrip MainMenuStrip, Color? accentColor = null)
        {
            if (MainMenuStrip != null)
            {
                MainMenuStrip.BackColor = Color.FromArgb(25, 25, 25); // MenuStrip background set to black
                MainMenuStrip.ForeColor = Color.White;

                // Set custom renderer to ensure black border and full control styling
                MainMenuStrip.Renderer = new CustomToolStripRenderer();

                foreach (ToolStripMenuItem menuItem in MainMenuStrip.Items)
                {
                    ApplyMenuItemStyle(menuItem);
                }
            }
        }

        private static void ApplyMenuItemStyle(ToolStripMenuItem menuItem)
        {
            menuItem.BackColor = Color.FromArgb(0, 0, 0); // Menu items background set to black
            menuItem.ForeColor = Color.White;

            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                subItem.BackColor = Color.FromArgb(25, 25, 25); // Submenu items background set to black
                subItem.ForeColor = Color.White;
            }
        }

        public class BCustomToolStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                // Draw a black border explicitly
                using (Pen blackPen = new Pen(Color.FromArgb(24, 24, 24)))
                {
                    e.Graphics.DrawRectangle(blackPen, new Rectangle(Point.Empty, e.ToolStrip.Size - new Size(1, 1)));
                }
            }

            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                // Ensure the background is fully black
                using (SolidBrush blackBrush = new SolidBrush(Color.FromArgb(18, 18, 18)))
                {
                    e.Graphics.FillRectangle(blackBrush, e.AffectedBounds);
                }
            }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                // Set menu item background explicitly
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                using (SolidBrush blackBrush = new SolidBrush(Color.FromArgb(18, 18, 18)))
                {
                    e.Graphics.FillRectangle(blackBrush, rect);
                }

                if (e.Item.Selected)
                {
                    using (SolidBrush grayBrush = new SolidBrush(Color.FromArgb(119, 204, 221)))
                    {
                        e.Graphics.FillRectangle(grayBrush, rect);
                    }
                    e.Item.ForeColor = Color.Black;
                }
                else
                {
                    e.Item.ForeColor = Color.White;
                }
            }

            #endregion Controls
        }
    }
}