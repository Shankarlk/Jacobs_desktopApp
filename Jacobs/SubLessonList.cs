using Jacobs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jacobs
{
    public partial class SubLessonList : Form
    {
        public int ClassNo { get; set; }

        public string SchlName { get; set; }

        public string SubjectName { get; set; }

        public string LessonName { get; set; }

        public string CurrentFolder { get; set; }
        public SubLessonList()
        {
            InitializeComponent();
            this.Load += SubLessonList_Load;
        }

        private void SubLessonList_Load(object sender, EventArgs e)
        {
            string[] folders = Directory.GetDirectories(CurrentFolder);

            foreach (string folder in folders)
            {
                MessageBox.Show(Path.GetFileName(folder));
            }
        }
    }
}
