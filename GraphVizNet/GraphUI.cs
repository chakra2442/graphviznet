using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace GraphVizNet
{
    class GraphUI : Form
    {
        internal string inputDotFile { get; set; }
        internal string outputPng { get; set; }
        internal string pathToDotExe { get; set; }

        private string formatParam = "-Tbmp";
        private PictureBox pictureBox1;
        private int waitMs = 500;

        internal GraphUI()
        {
            InitializeComponent();
        }

        internal void DrawGraph()
        {
            if (string.IsNullOrEmpty(inputDotFile) || string.IsNullOrEmpty(pathToDotExe))
            {
                throw new ArgumentException("Incomplete parameters");
            }

            ProcessStartInfo procInfo = new ProcessStartInfo(pathToDotExe, string.Format("{0} {1}", formatParam, inputDotFile));
            procInfo.UseShellExecute = false;
            procInfo.RedirectStandardError = true;
            procInfo.RedirectStandardOutput = true;

            Trace.TraceInformation("Starting dot.exe");
            var proc = Process.Start(procInfo);
            proc.WaitForExit(waitMs);
            Trace.TraceInformation("Finished dot.exe");

            //var error = proc.StandardError.ReadToEnd();
            //if (!string.IsNullOrEmpty(error))
            //{
            //    throw new Exception(error);
            //}

            MemoryStream ms = new MemoryStream();
            proc.StandardOutput.BaseStream.CopyTo(ms);
            var image = Image.FromStream(ms, true, false);

            this.ClientSize = image.Size;
            this.pictureBox1.Size = image.Size;
            this.pictureBox1.Image = image;

            Trace.TraceInformation("Starting UI thread");
            Thread uiThread = new Thread(new ThreadStart(() => { Application.Run(this); }));
            uiThread.Start();

            Trace.TraceInformation("Graph rendering complete");
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // GraphViz
            // 
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(148, 100);
            this.Controls.Add(this.pictureBox1);
            this.Name = "GraphViz";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
