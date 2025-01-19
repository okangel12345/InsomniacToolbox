using SpideyToolbox.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebWorks.Windows
{
    public partial class InformationWindow : Form
    {
        public InformationWindow()
        {
            InitializeComponent();

            ToolUtils.ApplyStyle(this, Handle);

            MaximizeBox = false;
            MinimizeBox = false;
        }
    }
}
