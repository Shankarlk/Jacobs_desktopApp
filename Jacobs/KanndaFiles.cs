using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace JacobsDesktopApp
{
    public partial class KanndaFiles : Form
    {
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        private Dictionary<int, List<string>> lessons = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Kannada_Class1_Lesson1", "Kannada_Class1_Lesson2", "Kannada_Class1_Lesson3", "Kannada_Class1_Lesson4", "Kannada_Class1_Lesson5", "Kannada_Class1_Lesson6", "Kannada_Class1_Lesson7", "Kannada_Class1_Lesson8", "Kannada_Class1_Lesson9", "Kannada_Class1_Lesson10", "Kannada_Class1_Lesson11", "Kannada_Class1_Lesson12" } },
        { 2, new List<string> { "Kannada_Class2_Lesson1", "Kannada_Class2_Lesson2", "Kannada_Class2_Lesson3", "Kannada_Class2_Lesson4", "Kannada_Class2_Lesson5", "Kannada_Class2_Lesson6", "Kannada_Class2_Lesson7", "Kannada_Class2_Lesson8", "Kannada_Class2_Lesson9", "Kannada_Class2_Lesson10", "Kannada_Class2_Lesson11", "Kannada_Class2_Lesson12" } },
        { 3, new List<string> { "Kannada_Class3_Lesson1", "Kannada_Class3_Lesson2", "Kannada_Class3_Lesson3", "Kannada_Class3_Lesson4", "Kannada_Class3_Lesson5", "Kannada_Class3_Lesson6", "Kannada_Class3_Lesson7", "Kannada_Class3_Lesson8", "Kannada_Class3_Lesson9", "Kannada_Class3_Lesson10", "Kannada_Class3_Lesson11", "Kannada_Class3_Lesson12" } },
        { 4, new List<string> { "Kannada_Class4_Lesson1", "Kannada_Class4_Lesson2", "Kannada_Class4_Lesson3", "Kannada_Class4_Lesson4", "Kannada_Class4_Lesson5", "Kannada_Class4_Lesson6", "Kannada_Class4_Lesson7", "Kannada_Class4_Lesson8", "Kannada_Class4_Lesson9", "Kannada_Class4_Lesson10", "Kannada_Class4_Lesson11", "Kannada_Class4_Lesson12" } },
        { 5, new List<string> { "Kannada_Class5_Lesson1", "Kannada_Class5_Lesson2", "Kannada_Class5_Lesson3", "Kannada_Class5_Lesson4", "Kannada_Class5_Lesson5", "Kannada_Class5_Lesson6", "Kannada_Class5_Lesson7", "Kannada_Class5_Lesson8", "Kannada_Class5_Lesson9", "Kannada_Class5_Lesson10", "Kannada_Class5_Lesson11", "Kannada_Class5_Lesson12" } },
        { 6, new List<string> { "Kannada_Class6_Lesson1", "Kannada_Class6_Lesson2", "Kannada_Class6_Lesson3", "Kannada_Class6_Lesson4", "Kannada_Class6_Lesson5", "Kannada_Class6_Lesson6", "Kannada_Class6_Lesson7", "Kannada_Class6_Lesson8", "Kannada_Class6_Lesson9", "Kannada_Class6_Lesson10", "Kannada_Class6_Lesson11", "Kannada_Class6_Lesson12" } },
        { 7, new List<string> { "Kannada_Class7_Lesson1", "Kannada_Class7_Lesson2", "Kannada_Class7_Lesson3", "Kannada_Class7_Lesson4", "Kannada_Class7_Lesson5", "Kannada_Class7_Lesson6", "Kannada_Class7_Lesson7", "Kannada_Class7_Lesson8", "Kannada_Class7_Lesson9", "Kannada_Class7_Lesson10", "Kannada_Class7_Lesson11", "Kannada_Class7_Lesson12" } },
        { 8, new List<string> { "Kannada_Class8_Lesson1", "Kannada_Class8_Lesson2", "Kannada_Class8_Lesson3", "Kannada_Class8_Lesson4", "Kannada_Class8_Lesson5", "Kannada_Class8_Lesson6", "Kannada_Class8_Lesson7", "Kannada_Class8_Lesson8", "Kannada_Class8_Lesson9", "Kannada_Class8_Lesson10", "Kannada_Class8_Lesson11", "Kannada_Class8_Lesson12" } },
        { 9, new List<string> { "Kannada_Class9_Lesson1", "Kannada_Class9_Lesson2", "Kannada_Class9_Lesson3", "Kannada_Class9_Lesson4", "Kannada_Class9_Lesson5", "Kannada_Class9_Lesson6", "Kannada_Class9_Lesson7", "Kannada_Class9_Lesson8", "Kannada_Class9_Lesson9", "Kannada_Class9_Lesson10", "Kannada_Class9_Lesson11", "Kannada_Class9_Lesson12" } },
        { 10, new List<string> { "Kannada_Class10_Lesson1", "Kannada_Class10_Lesson2", "Kannada_Class10_Lesson3", "Kannada_Class10_Lesson4", "Kannada_Class10_Lesson5", "Kannada_Class10_Lesson6", "Kannada_Class10_Lesson7", "Kannada_Class10_Lesson8", "Kannada_Class10_Lesson9", "Kannada_Class10_Lesson10", "Kannada_Class10_Lesson11", "Kannada_Class10_Lesson12" } }
    };

        public KanndaFiles()
        {
            InitializeComponent();
        }

        private void KanndaFiles_Load(object sender, EventArgs e)
        {
            btnLogout.Visible = false;
            lblSchl.Visible = false;
            lbllesson.Left = (this.ClientSize.Width - lbllesson.Width) / 2;
            grpLesson.Left = (this.ClientSize.Width - grpLesson.Width) / 2;
            grpLesson.Top = (this.ClientSize.Height - grpLesson.Height) / 3;
            LoadDocumentsForClass(ClassNo);
            lblSchl.Text = "              " + SchlName + "          ";
        }
        private void LoadDocumentsForClass(int classNo)
        {
            if (lessons.TryGetValue(classNo, out List<string> documents))
            {
                grpLesson.Controls.Clear();

                FlowLayoutPanel flowPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    WrapContents = true,
                    FlowDirection = FlowDirection.LeftToRight,
                    Padding = new Padding(20),
                };

                foreach (var doc in documents)
                {
                    Panel folderItem = new Panel
                    {
                        Width = 100,
                        Height = 100,
                        Margin = new Padding(10)
                    };

                    PictureBox folderIcon = new PictureBox
                    {
                        Image = Jacobs.Properties.Resources.folder1, // Replace with your folder image in Resources
                        Size = new Size(64, 64),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(18, 0),
                        Cursor = Cursors.Hand,
                        Tag = doc
                    };
                    folderIcon.Click += FolderIcon_Click;

                    Label folderLabel = new Label
                    {
                        Text = doc,
                        AutoSize = false,
                        Width = 100,
                        Height = 30,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(0, 70)
                    };

                    folderItem.Controls.Add(folderIcon);
                    folderItem.Controls.Add(folderLabel);
                    flowPanel.Controls.Add(folderItem);
                }

                grpLesson.Controls.Add(flowPanel); // Add the flow layout to your GroupBox
            }
            else
            {
                MessageBox.Show($"No documents found for this subject of class {classNo}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FolderIcon_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                string lessonName = pic.Tag.ToString();

                LessonsList openPPTFile = new LessonsList
                {
                    LessonName = lessonName,
                    SubjectName = "Kannada",
                    ClassNo = ClassNo,
                    SchlName = SchlName
                };

                openPPTFile.Show();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

//private Dictionary<int, List<string>> classDocuments = new Dictionary<int, List<string>>
//    {
//        { 1, new List<string> { "Kannada_Class1.pdf", "Kannada_Class1.pptx", "Kannada_Class1.mp4" } },
//        { 2, new List<string> { "Kannada_Class2.pdf", "Kannada_Class2.pptx", "Kannada_Class2.mp4" } },
//        { 3, new List<string> { "Kannada_Class3.pdf", "Kannada_Class3.pptx", "Kannada_Class3.mp4" } },
//        { 4, new List<string> { "Kannada_Class4.pdf", "Kannada_Class4.pptx", "Kannada_Class4.mp4" } },
//        { 5, new List<string> { "Kannada_Class5.pdf", "Kannada_Class5.pptx", "Kannada_Class5.mp4" } },
//        { 6, new List<string> { "Kannada_Class6.pdf", "Kannada_Class6.pptx", "Kannada_Class6.mp4" } },
//        { 7, new List<string> { "Kannada_Class7.pdf", "Kannada_Class7.pptx", "Kannada_Class7.mp4" } },
//        { 8, new List<string> { "Kannada_Class8.pdf", "Kannada_Class8.pptx", "Kannada_Class8.mp4" } },
//        { 9, new List<string> { "Kannada_Class9.pdf", "Kannada_Class9.pptx", "Kannada_Class9.mp4" } },
//        { 10, new List<string> { "Kannada_Class10.pdf", "Kannada_Class10.pptx", "Kannada_Class10.mp4" } }
//    };
//private Dictionary<int, List<string>> exercise = new Dictionary<int, List<string>>
//    {
//        { 1, new List<string> { "Hindi_Lesson1_Exercise1.pdf", "Hindi_Lesson1_Exercise2.pdf", "Hindi_Lesson1_Exercise3.pdf" } },
//        { 2, new List<string> { "Hindi_Lesson2_Exercise1.pdf", "Hindi_Lesson2_Exercise2.pdf", "Hindi_Lesson2_Exercise3.pdf" } },
//        { 3, new List<string> { "Hindi_Lesson3_Exercise1.pdf", "Hindi_Lesson3_Exercise2.pdf", "Hindi_Lesson3_Exercise3.pdf" } },
//        { 4, new List<string> { "Hindi_Lesson4_Exercise1.pdf", "Hindi_Lesson4_Exercise2.pdf", "Hindi_Lesson4_Exercise3.pdf" } },
//        { 5, new List<string> { "Hindi_Lesson5_Exercise1.pdf", "Hindi_Lesson5_Exercise2.pdf", "Hindi_Lesson5_Exercise3.pdf" } },
//        { 6, new List<string> { "Hindi_Lesson6_Exercise1.pdf", "Hindi_Lesson6_Exercise2.pdf", "Hindi_Lesson6_Exercise3.pdf" } },
//        { 7, new List<string> { "Hindi_Lesson7_Exercise1.pdf", "Hindi_Lesson7_Exercise2.pdf", "Hindi_Lesson7_Exercise3.pdf" } },
//        { 8, new List<string> { "Hindi_Lesson8_Exercise1.pdf", "Hindi_Lesson8_Exercise2.pdf", "Hindi_Lesson8_Exercise3.pdf" } },
//        { 9, new List<string> { "Hindi_Lesson9_Exercise1.pdf", "Hindi_Lesson9_Exercise2.pdf", "Hindi_Lesson9_Exercise3.pdf" } },
//        { 10, new List<string> { "Hindi_Lesson10_Exercise1.pdf", "Hindi_Lesson10_Exercise2.pdf", "Hindi_Lesson10_Exercise3.pdf" } }
//    };