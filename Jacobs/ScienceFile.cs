using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
//using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
 
namespace JacobsDesktopApp
{
    public partial class ScienceFile : Form
    {
        public int ClassNo { get; set; }
        public string SchlName { get; set; }

        public string SubjectName { get; set; }


      
        public ScienceFile()
        {
            InitializeComponent();
        }
         

        private void ScienceFile_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            lblSchl.Text = "Jacobs Educare";
            lblSchl.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblSchl.ForeColor = Color.RoyalBlue;
            lblSchl.Visible = true;

            lblSchl.Left = (this.ClientSize.Width - lblSchl.Width) / 2;
            lblSchl.Top = 20;
           


            btnLogout.Visible = false;

            lbllesson.Text = SubjectName + " Lessons";
            lbllesson.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lbllesson.ForeColor = Color.RoyalBlue;

            lbllesson.Left = (this.ClientSize.Width - lbllesson.Width) / 2;
            lbllesson.Top = 100;

            grpLesson.Left = 20;
            grpLesson.Top = 180;

            grpLesson.Width = 1400;
            grpLesson.Height = 630;

            grpLesson.Left = (this.ClientSize.Width - grpLesson.Width) / 2;
            grpLesson.Top = 180;

            this.BackColor = Color.FromArgb(245, 248, 252);

            grpLesson.BackColor = Color.White;
            grpLesson.FlatStyle = FlatStyle.Flat;
            grpLesson.Text = "";

            pictureBox1.Dock = DockStyle.None;
            pictureBox1.Size = new Size(140, 100);
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // ===== BACK BUTTON DESIGN =====
            btnBack.Text = "← Back";
            btnBack.Width = 110;
            btnBack.Height = 35;

            btnBack.BackColor = Color.FromArgb(37, 99, 235);
            btnBack.ForeColor = Color.White;

            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;

            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);

           // SetRoundedButton(btnBack, 12);

            LoadDocumentsForClass(ClassNo);

            this.Text = "";
        }

        

        private void LoadDocumentsForClass(int classNo)
        {
            //string subjectFolder = Path.Combine(
            //    Application.StartupPath,
            //     @"..\..\Files",
            //    $"Class {classNo}",
            //    SubjectName);

            //subjectFolder = Path.GetFullPath(subjectFolder);
            string filesPath = Path.GetFullPath(
    Path.Combine(Application.StartupPath, "Files"));

            // Find the class folder dynamically
            string classFolder = Directory.GetDirectories(filesPath)
                .FirstOrDefault(d =>
                {
                    string folderName = Path.GetFileName(d);

                    Match match = Regex.Match(folderName, @"^\d+");

                    return match.Success &&
                           match.Value == classNo.ToString();
                });

            if (classFolder == null)
            {
                MessageBox.Show("Class folder not found.");
                return;
            }

            // Build subject folder
            string subjectFolder = Path.Combine(classFolder, SubjectName);

            if (!Directory.Exists(subjectFolder))
            {
                MessageBox.Show(SubjectName + " folder not found:\n" + subjectFolder);
                return;
            }

            if (!Directory.Exists(subjectFolder))
            {
                MessageBox.Show(SubjectName + " folder not found:\n" + subjectFolder);
                return;
            }

            grpLesson.Controls.Clear();

            FlowLayoutPanel flowPanel = new FlowLayoutPanel();
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.AutoScroll = true;
            flowPanel.WrapContents = true;
            flowPanel.FlowDirection = FlowDirection.LeftToRight;
            flowPanel.Padding = new Padding(40);
            flowPanel.BackColor = Color.White;

            string[] lessonFolders = Directory.GetDirectories(subjectFolder);

            foreach (string lessonFolder in lessonFolders)
            {
                string lessonName = Path.GetFileName(lessonFolder);

                // Card
                Panel card = new Panel();
                card.Width = 240;
                card.Height = 220;
                card.BackColor = Color.White;
                card.Margin = new Padding(25);
                card.BorderStyle = BorderStyle.None;
                card.Cursor = Cursors.Hand;

                SetRoundedPanel(card, 20);

                // Circle Background
                Panel circle = new Panel();
                circle.Width = 130;
                circle.Height = 130;
                circle.BackColor = Color.FromArgb(245, 248, 252);

                circle.Left = (card.Width - circle.Width) / 2;
                circle.Top = 15;

                SetRoundedPanel(circle, 65);

                // Folder Icon
                PictureBox folderIcon = new PictureBox();
                folderIcon.Image = Jacobs.Properties.Resources.logofolde;
                folderIcon.Size = new Size(80, 80);
                folderIcon.SizeMode = PictureBoxSizeMode.Zoom;

                folderIcon.Left = (circle.Width - folderIcon.Width) / 2;
                folderIcon.Top = (circle.Height - folderIcon.Height) / 2;

                folderIcon.Tag = lessonName;
                folderIcon.Cursor = Cursors.Hand;
                folderIcon.Click += FolderIcon_Click;

                circle.Controls.Add(folderIcon);

                // Lesson Label
                Label lblLesson = new Label();
                lblLesson.Text = lessonName;
                lblLesson.Width = card.Width;
                lblLesson.Height = 40;
                lblLesson.TextAlign = ContentAlignment.MiddleCenter;
                lblLesson.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                lblLesson.ForeColor = Color.FromArgb(15, 23, 42);
                lblLesson.Location = new Point(0, 150);

                // Blue Underline
                Panel underline = new Panel();
                underline.Width = 60;
                underline.Height = 3;
                underline.BackColor = Color.RoyalBlue;
                underline.Left = (card.Width - underline.Width) / 2;
                underline.Top = 190;

                card.Controls.Add(circle);
                card.Controls.Add(lblLesson);
                card.Controls.Add(underline);

                flowPanel.Controls.Add(card);
            }

            grpLesson.Controls.Add(flowPanel);
        }

        private void SetRoundedPanel(Control control, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path =
                new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
        }
        private void FolderIcon_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                string lessonName = pic.Tag.ToString();

                LessonsList openPPTFile = new LessonsList
                {
                    LessonName = lessonName,
                    SubjectName = SubjectName,
                    ClassNo = ClassNo,
                    SchlName = SchlName
                };

                openPPTFile.Show();
                this.Hide();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Subjects englishFiles = new Subjects();
            englishFiles.ClassNo = ClassNo;
            englishFiles.SchlName = SchlName;
            englishFiles.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Subjects englishFiles = new Subjects();
            englishFiles.ClassNo = ClassNo;
            englishFiles.SchlName = SchlName;
            englishFiles.Show();
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
