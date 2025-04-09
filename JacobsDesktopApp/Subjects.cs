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
            //this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            button7.Margin = new Padding(10, 10, 10, 50);
            button7.Location = new Point(button7.Location.X, button7.Location.Y - 40);
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
            btnLogout.Visible = false;
            lblSchl.Visible = false;
            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 40;
            footerPanel.BackColor = Color.LightGray;

            // Create Label for Footer Text
            Label lblFooter = new Label();
            lblFooter.Text = "© 2025 JACOBS EDUCARE, All Rights Reserved  |  Terms of Service  |  Privacy Policy";
            lblFooter.AutoSize = true;
            lblFooter.Font = new Font("Arial", 10, FontStyle.Regular);
            lblFooter.ForeColor = Color.Black;

            // Position Footer Text
            lblFooter.Left = 10;
            lblFooter.Top = (footerPanel.Height - lblFooter.Height) / 2;

            // Add Label to Footer
            footerPanel.Controls.Add(lblFooter);

            // Add Footer Panel to Form
            this.Controls.Add(footerPanel);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

            Form1 home = new Form1();
            home.Show();
            this.Hide();
        }

        private void labelArrow_Click(object sender, EventArgs e)
        {

            btnLogout.Visible = true;
        }
    }
}
