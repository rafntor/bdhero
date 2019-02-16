using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDHero.Plugin.FFmpegMuxer
{
    public partial class PluginInfoForm : Form
    {
        public PluginInfoForm()
        {
            InitializeComponent();
        }
        public string FFMpegVersion
        {
            get { return lblFFMpegVer.Text; }
            set { lblFFMpegVer.Text = value; }
        }
        public string MkvPropEditVersion
        {
            get { return lblMkvPropEditVer.Text; }
            set { lblMkvPropEditVer.Text = value; }
        }
    }
}
