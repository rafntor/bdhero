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

namespace BDHero.Plugin.FFmpegMuxer
{
    public partial class PluginInfoForm : Form
    {
        Thread m_thread1, m_thread2;
        public PluginInfoForm(IJobObjectManager jobObjectManager)
        {
            InitializeComponent();

            lblFFMpegVer.Text = lblMkvPropEditVer.Text = "looking up version...";

            m_thread1 = new Thread(MakeFFMpegVersion);
            m_thread1.Start(jobObjectManager);

            m_thread2 = new Thread(MakeMkvPropEditVersion);
            m_thread2.Start(jobObjectManager);
        }
        private void PluginInfoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lblMkvPropEditVer.Text = "waiting to close lookup request...";
            m_thread2.Join();
            lblMkvPropEditVer.Text = "ready";

            lblFFMpegVer.Text = "waiting to close lookup request...";
            m_thread1.Join();
            lblFFMpegVer.Text = "ready";
        }
        void MakeFFMpegVersion(object jobObjectManager)
        {
            string version = FFmpeg.ExeVersion(jobObjectManager as IJobObjectManager);

            lblFFMpegVer.Invoke((MethodInvoker)delegate { lblFFMpegVer.Text = version; });
        }
        void MakeMkvPropEditVersion(object jobObjectManager)
        {
            string version = GetMkvPropEditVersion(jobObjectManager as IJobObjectManager);

            lblMkvPropEditVer.Invoke((MethodInvoker)delegate { lblMkvPropEditVer.Text = version; });

        }
        string GetMkvPropEditVersion(IJobObjectManager jobObjectManager)
        {
            string result = "";
            var mkvpropedit = new MkvPropEdit(jobObjectManager, null);
            mkvpropedit.Arguments = new ProcessUtils.ArgumentList("--version");
            mkvpropedit.StdOut += delegate (string line) { result += line; };
            mkvpropedit.Start(); // sync
            return result;
        }
    }
}
