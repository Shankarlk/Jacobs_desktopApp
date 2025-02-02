using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    public partial class OpenMp4 : Form
    {
        public string DocName { get; set; }
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public OpenMp4()
        {
            InitializeComponent();
        }

        private void OpenMp4_Load(object sender, EventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string opf = System.IO.Path.Combine(baseDirectory, "Files", DocName);
            axWindowsMediaPlayer1.URL = opf;
            axWindowsMediaPlayer1.Ctlcontrols.play();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnglishFiles englishFiles = new EnglishFiles();
            englishFiles.ClassNo = ClassNo;
            englishFiles.Show();
            this.Hide();
        }
    }
}
