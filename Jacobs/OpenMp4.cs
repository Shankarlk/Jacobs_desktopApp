using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
        public OpenMp4()
        {
            InitializeComponent();
            this.FormClosing += OpenMp4_FormClosing;
        }

        



        private void OpenMp4_Load(object sender, EventArgs e)
        {
            if (!File.Exists(DocName))
            {
                MessageBox.Show("File not found:\n" + DocName);
                return;
            }

            axWindowsMediaPlayer1.URL = DocName;
            axWindowsMediaPlayer1.Ctlcontrols.play();
         
        }

        private void OpenMp4_FormClosing(object sender, FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            axWindowsMediaPlayer1.close();
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            LessonsList LessonsList = new LessonsList();
            LessonsList.LessonName = LessonName;
            LessonsList.SubjectName = SubjectName;
            LessonsList.ClassNo = ClassNo;
            LessonsList.SchlName = SchlName;
            LessonsList.Show();
            this.Hide();
        }
    }
}
