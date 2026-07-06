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
    public partial class MathFiles : Form
    {
        public int ClassNo { get; set; }
        public string SchlName { get; set; }

        private Dictionary<int, List<string>> lessons = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Math_Class1_Lesson1", "Math_Class1_Lesson2", "Math_Class1_Lesson3", "Math_Class1_Lesson4", "Math_Class1_Lesson5", "Math_Class1_Lesson6", "Math_Class1_Lesson7", "Math_Class1_Lesson8", "Math_Class1_Lesson9", "Math_Class1_Lesson10", "Math_Class1_Lesson11", "Math_Class1_Lesson12" } },
        { 2, new List<string> { "Math_Class2_Lesson1", "Math_Class2_Lesson2", "Math_Class2_Lesson3", "Math_Class2_Lesson4", "Math_Class2_Lesson5", "Math_Class2_Lesson6", "Math_Class2_Lesson7", "Math_Class2_Lesson8", "Math_Class2_Lesson9", "Math_Class2_Lesson10", "Math_Class2_Lesson11", "Math_Class2_Lesson12" } },
        { 3, new List<string> { "Math_Class3_Lesson1", "Math_Class3_Lesson2", "Math_Class3_Lesson3", "Math_Class3_Lesson4", "Math_Class3_Lesson5", "Math_Class3_Lesson6", "Math_Class3_Lesson7", "Math_Class3_Lesson8", "Math_Class3_Lesson9", "Math_Class3_Lesson10", "Math_Class3_Lesson11", "Math_Class3_Lesson12" } },
        { 4, new List<string> { "Math_Class4_Lesson1", "Math_Class4_Lesson2", "Math_Class4_Lesson3", "Math_Class4_Lesson4", "Math_Class4_Lesson5", "Math_Class4_Lesson6", "Math_Class4_Lesson7", "Math_Class4_Lesson8", "Math_Class4_Lesson9", "Math_Class4_Lesson10", "Math_Class4_Lesson11", "Math_Class4_Lesson12" } },
        { 5, new List<string> { "Math_Class5_Lesson1", "Math_Class5_Lesson2", "Math_Class5_Lesson3", "Math_Class5_Lesson4", "Math_Class5_Lesson5", "Math_Class5_Lesson6", "Math_Class5_Lesson7", "Math_Class5_Lesson8", "Math_Class5_Lesson9", "Math_Class5_Lesson10", "Math_Class5_Lesson11", "Math_Class5_Lesson12" } },
        { 6, new List<string> { "Math_Class6_Lesson1", "Math_Class6_Lesson2", "Math_Class6_Lesson3", "Math_Class6_Lesson4", "Math_Class6_Lesson5", "Math_Class6_Lesson6", "Math_Class6_Lesson7", "Math_Class6_Lesson8", "Math_Class6_Lesson9", "Math_Class6_Lesson10", "Math_Class6_Lesson11", "Math_Class6_Lesson12" } },
        { 7, new List<string> { "Math_Class7_Lesson1", "Math_Class7_Lesson2", "Math_Class7_Lesson3", "Math_Class7_Lesson4", "Math_Class7_Lesson5", "Math_Class7_Lesson6", "Math_Class7_Lesson7", "Math_Class7_Lesson8", "Math_Class7_Lesson9", "Math_Class7_Lesson10", "Math_Class7_Lesson11", "Math_Class7_Lesson12" } },
        { 8, new List<string> { "Math_Class8_Lesson1", "Math_Class8_Lesson2", "Math_Class8_Lesson3", "Math_Class8_Lesson4", "Math_Class8_Lesson5", "Math_Class8_Lesson6", "Math_Class8_Lesson7", "Math_Class8_Lesson8", "Math_Class8_Lesson9", "Math_Class8_Lesson10", "Math_Class8_Lesson11", "Math_Class8_Lesson12" } },
        { 9, new List<string> { "Math_Class9_Lesson1", "Math_Class9_Lesson2", "Math_Class9_Lesson3", "Math_Class9_Lesson4", "Math_Class9_Lesson5", "Math_Class9_Lesson6", "Math_Class9_Lesson7", "Math_Class9_Lesson8", "Math_Class9_Lesson9", "Math_Class9_Lesson10", "Math_Class9_Lesson11", "Math_Class9_Lesson12" } },
        { 10, new List<string> { "Math_Class10_Lesson1", "Math_Class10_Lesson2", "Math_Class10_Lesson3", "Math_Class10_Lesson4", "Math_Class10_Lesson5", "Math_Class10_Lesson6", "Math_Class10_Lesson7", "Math_Class10_Lesson8", "Math_Class10_Lesson9", "Math_Class10_Lesson10", "Math_Class10_Lesson11", "Math_Class10_Lesson12" } }
    };
        public MathFiles()
        {
            InitializeComponent();
        }

        private void MathFiles_Load(object sender, EventArgs e)
        {
            LoadDocumentsForClass(ClassNo);
            lblSchl.Text = "              " + SchlName + "          ";
            button2.Margin = new Padding(10, 10, 10, 50);
            button2.Location = new Point(button2.Location.X, button2.Location.Y - 40);
            btnLogout.Visible = false;
            lblSchl.Visible = false;
            lbllesson.Left = (this.ClientSize.Width - lbllesson.Width) / 2;
            grpLesson.Left = (this.ClientSize.Width - grpLesson.Width) / 2;
            grpLesson.Top = (this.ClientSize.Height - grpLesson.Height) / 3;
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
                    SubjectName = "Math",
                    ClassNo = ClassNo,
                    SchlName = SchlName
                };

                openPPTFile.Show();
                this.Hide();
            }
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
