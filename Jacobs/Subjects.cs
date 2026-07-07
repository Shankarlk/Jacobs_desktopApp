using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
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
            this.WindowState = FormWindowState.Maximized;
        }


        private void SetRoundedPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();

            panel.Region = new Region(path);
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
            //ScienceFile english = new ScienceFile();
            //english.SchlName = SchlName;
            //english.ClassNo = ClassNo;
            //english.Show();
            //this.Hide();


            ScienceFile frm = new ScienceFile();

            frm.SubjectName = "Science";
            frm.SchlName = SchlName;
            frm.ClassNo = ClassNo;

            frm.Show();
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
            this.WindowState = FormWindowState.Maximized;

            btnLogout.Visible = false;

            lblSchl.Visible = true;
            lblSchl.Text = "Jacobs Educare";
            lblSchl.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblSchl.ForeColor = Color.RoyalBlue;

            lblSchl.Left = (this.ClientSize.Width - lblSchl.Width) / 2;
            lblSchl.Top = 20;

            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 90;

            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 40;
            footerPanel.BackColor = Color.LightGray;

            Label lblFooter = new Label();
            lblFooter.Text = "© 2025 JACOBS EDUCARE, All Rights Reserved";
            lblFooter.AutoSize = true;
            lblFooter.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblFooter.ForeColor = Color.Black;
            lblFooter.Left = 10;
            lblFooter.Top = 10;

            footerPanel.Controls.Add(lblFooter);
            this.Controls.Add(footerPanel);
            button7.Text = "← Back";
            button7.BackColor = Color.FromArgb(37, 99, 235);
            button7.ForeColor = Color.White;
            button7.FlatStyle = FlatStyle.Flat;
            button7.FlatAppearance.BorderSize = 0;
            button7.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button7.Width = 120;
            button7.Height = 40;
            button7.BringToFront();

            Label lblWelcome = new Label();
            lblWelcome.Text = "Welcome, Teacher";
            lblWelcome.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(37, 99, 235);
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(300, 80);


            // Subjects Card
            Panel pnlSubjects = CreateStatCard("Subjects", "7", Color.RoyalBlue);
            pnlSubjects.Location = new Point(750, 70);

            // Chapters Card
            Panel pnlChapters = CreateStatCard("Chapters", "56", Color.SeaGreen);
            pnlChapters.Location = new Point(930, 70);

            // Lessons Card
            Panel pnlLessons = CreateStatCard("Lessons", "124", Color.MediumPurple);
            pnlLessons.Location = new Point(1110, 70);

            this.Controls.Add(pnlSubjects);
            this.Controls.Add(pnlChapters);
            this.Controls.Add(pnlLessons);

            // Description Label
            Label lblDesc = new Label();
            lblDesc.Text = "Select a subject to manage its chapters and lessons";
            lblDesc.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblDesc.ForeColor = Color.Gray;
            lblDesc.AutoSize = true;
            lblDesc.Location = new Point(300, 115);

            this.Controls.Add(lblWelcome);
            this.Controls.Add(lblDesc);

            LoadSubjects();
            this.Text = "";

        }

        private Panel CreateStatCard(string title, string value, Color valueColor)
        {
            Panel pnl = new Panel();
            pnl.Size = new Size(150, 70);
            pnl.BackColor = Color.White;
            pnl.BorderStyle = BorderStyle.FixedSingle;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 10);
            lblTitle.ForeColor = Color.Gray;
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(0, 95);

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblValue.ForeColor = valueColor;
            lblValue.AutoSize = true;
            lblValue.Location = new Point(15, 30);

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblValue);

            return pnl;
        }

        


        private void LoadSubjects()
            {
                groupBox2.Controls.Clear();

                groupBox2.BackColor = Color.FromArgb(245, 247, 250);
                groupBox2.FlatStyle = FlatStyle.Flat;

                groupBox2.Width = 1150;
                groupBox2.Height = 600;

                groupBox2.Left = (this.ClientSize.Width - groupBox2.Width) / 2;
                groupBox2.Top = 150;

            //    string classFolder = Path.Combine(
            //        Application.StartupPath,
            //        @"..\..\Files",
            //        $"Class {ClassNo}");

            //classFolder = Path.GetFullPath(classFolder);


            string filesPath = Path.GetFullPath(Path.Combine(Application.StartupPath, "Files"));

            string classFolder = Directory.GetDirectories(filesPath)
                .FirstOrDefault(d =>
                {
                    string folderName = Path.GetFileName(d);

                    Match match = Regex.Match(folderName, @"^\d+");

                    return match.Success &&
                           match.Value == ClassNo.ToString();
                });

            if (classFolder == null)
            {
                MessageBox.Show("Class folder not found.");
                return;
            }

            if (!Directory.Exists(classFolder))
                    return;

                string[] subjects = Directory.GetDirectories(classFolder);

                int x = 30;
                int y = 30;

                foreach (string subject in subjects)
                {
                    string subjectName = Path.GetFileName(subject);

                    Panel card = new Panel();

                    card.Width = 260;
                    card.Height = 230;

                    card.Location = new Point(x, y);

                    card.BackColor = Color.White;
                    card.Cursor = Cursors.Hand;
                    card.Tag = subjectName;

                    card.Padding = new Padding(15);

                    SetRoundedPanel(card, 20);

                    PictureBox pic = new PictureBox();

                    pic.Size = new Size(80, 80);

                    pic.Location = new Point(
                        (card.Width - 80) / 2,
                        15);

                    pic.SizeMode = PictureBoxSizeMode.Zoom;

                    pic.BackColor = Color.Transparent;

                    pic.Tag = subjectName;

                switch (subjectName.ToLower())
                    {
                        case "computer":
                            pic.Image = Jacobs.Properties.Resources.download;
                            break;

                        case "english":
                            pic.Image = Jacobs.Properties.Resources.eng;
                            break;

                        case "hindi":
                            pic.Image = Jacobs.Properties.Resources.hindi;
                            break;

                        case "kannada":
                            pic.Image = Jacobs.Properties.Resources.kannada;
                            break;

                        case "maths":
                            pic.Image = Jacobs.Properties.Resources.maths;
                            break;

                        case "science":
                            pic.Image = Jacobs.Properties.Resources.science;
                            break;

                        case "social science":
                            pic.Image = Jacobs.Properties.Resources.social;
                            break;

                        case "biology":
                            pic.Image = Jacobs.Properties.Resources.Biology;
                            break;
                }

                        Label lblTitle = new Label();

                        lblTitle.Text = subjectName;

                        lblTitle.Font = new Font(
                            "Segoe UI",
                            14,
                            FontStyle.Bold);

                        lblTitle.ForeColor = Color.FromArgb(30, 30, 30);

                        lblTitle.Width = card.Width;

                        lblTitle.Height = 35;

                     lblTitle.Location = new Point(0, 105);
               

                lblTitle.TextAlign =
                            ContentAlignment.MiddleCenter;

                        lblTitle.Tag = subjectName;

                    Label lblDesc = new Label();

                    lblDesc.Width = 220;

                    lblDesc.Height = 40;

                lblDesc.Location = new Point(20, 148);

                lblDesc.TextAlign =
                        ContentAlignment.TopCenter;
                lblDesc.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblDesc.ForeColor = Color.Black;
                lblDesc.ForeColor =
                        Color.Gray;

                    lblDesc.Tag = subjectName;

                switch (subjectName.ToLower())
                    {
                        case "computer":
                            lblDesc.Text = "Explore Computer concepts";
                            break;

                        case "english":
                            lblDesc.Text = "Improve English language skills";
                            break;

                        case "hindi":
                            lblDesc.Text = "Learn Hindi language";
                            break;

                        case "kannada":
                            lblDesc.Text = "Learn Kannada language";
                            break;

                        case "maths":
                            lblDesc.Text = "Learn Mathematics concepts";
                            break;

                        case "science":
                            lblDesc.Text = "Explore Science and experiments";
                            break;

                        case "social science":
                            lblDesc.Text = "Learn about Society and Environment";
                            break;

                        case "biology":
                            lblDesc.Text = "Learn about Biology";
                            break;
                }

                        Label arrow = new Label();

                        arrow.Text = "➜";

                        arrow.Font = new Font(
                            "Segoe UI",
                            16,
                            FontStyle.Bold);

                        arrow.ForeColor =
                            Color.RoyalBlue;

                        arrow.AutoSize = true;

                        arrow.Location = new Point(
                            card.Width - 35,
                            185);

                        arrow.Tag = subjectName;

                card.Controls.Add(pic);
                    card.Controls.Add(lblTitle);
                    card.Controls.Add(lblDesc);
                    card.Controls.Add(arrow);

                    card.Click += Subject_Click;
                    pic.Click += Subject_Click;
                    lblTitle.Click += Subject_Click;
                    lblDesc.Click += Subject_Click;
                    arrow.Click += Subject_Click;

                    card.MouseEnter += (s, e) =>
                    {
                        card.BackColor = Color.FromArgb(248, 250, 255);
                    };

                    card.MouseLeave += (s, e) =>
                    {
                        card.BackColor = Color.White;
                    };

                groupBox2.Controls.Add(card);

                    x += 275;

                    if (x + 250 > groupBox2.Width)
                    {
                        x = 30;
                        y += 250;
                    }
                }
            }
        private void Subject_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.RoyalBlue;
            btn.ForeColor = Color.White;
        }

        private void Subject_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.RoyalBlue;
        }

        private void Subject_Click(object sender, EventArgs e)
        {
            string subjectName = "";

            if (sender is Panel)
                subjectName = ((Panel)sender).Tag.ToString();

            else if (sender is PictureBox)
                subjectName = ((PictureBox)sender).Tag.ToString();

            else if (sender is Label)
                subjectName = ((Label)sender).Tag.ToString();

            ScienceFile frm = new ScienceFile();

            frm.SubjectName = subjectName;
            frm.ClassNo = ClassNo;
            frm.SchlName = SchlName;

            frm.Show();
            this.Hide();
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
