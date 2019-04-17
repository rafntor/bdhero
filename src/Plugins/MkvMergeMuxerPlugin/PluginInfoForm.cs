using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using OSUtils.JobObjects;

namespace BDHero.Plugin.MkvMergeMuxer
{
    public partial class PluginInfoForm : Form
    {
        Thread m_thread;
        public PluginInfoForm(IJobObjectManager jobObjectManager)
        {
            InitializeComponent();

            lblVersion.Text = "looking up version...";

            m_thread = new Thread(MakeVersion);
            m_thread.Start(jobObjectManager);
        }
        void MakeVersion(object jobObjectManager)
        {
            string version = MkvMerge.ExeVersion(jobObjectManager as IJobObjectManager);

            lblVersion.Invoke((MethodInvoker)delegate { lblVersion.Text = version; });
        }
        private void PluginInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblVersion.Text = "waiting to close lookup request...";
            m_thread.Join();
        }
    }
}
