using Org.BouncyCastle.Utilities;
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
    public partial class Subjects : Form
    {
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public Subjects()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnglishFiles english = new EnglishFiles();
            english.SchlName = SchlName;
            english.ClassNo = ClassNo;
            english.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HindiFiles english = new HindiFiles();
            english.SchlName = SchlName;
            english.ClassNo = ClassNo;
            english.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Home englishFiles = new Home();
            //subjects.SchlName = SchoolName;
            englishFiles.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MathFiles english = new MathFiles();
            english.SchlName = SchlName;
            english.ClassNo = ClassNo;
            english.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ScienceFile english = new ScienceFile();
            english.SchlName = SchlName;
            english.ClassNo = ClassNo;
            english.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            KanndaFiles english = new KanndaFiles();
            english.SchlName = SchlName;
            english.ClassNo = ClassNo;
            english.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SocialFiles english = new SocialFiles();
            english.SchlName = SchlName;
            english.ClassNo = ClassNo;
            english.Show();
            this.Hide();
        }

        private void Subjects_Load(object sender, EventArgs e)
        {
            lblSchl.Text = "              " + SchlName + "          ";
        }
    }
}
