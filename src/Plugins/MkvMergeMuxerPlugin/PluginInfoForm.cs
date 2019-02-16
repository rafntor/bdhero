using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDHero.Plugin.MkvMergeMuxer
{
    public partial class PluginInfoForm : Form
    {
        public PluginInfoForm()
        {
            InitializeComponent();
        }
        public string MkvMergeVersion
        {
            get { return lblVersion.Text; }
            set { lblVersion.Text = value; }
        }
    }
}
