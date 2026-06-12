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

namespace JacobsDesktopApp
{
    public partial class EnglishFiles : Form
    {
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
    //    private Dictionary<int, List<string>> classDocuments = new Dictionary<int, List<string>>
    //{
    //    { 1, new List<string> { "English_Class1.pdf", "English_Class1.pptx", "English_Class1.mp4" } },
    //    { 2, new List<string> { "English_Class2.pdf", "English_Class2.pptx", "English_Class2.mp4" } },
    //    { 3, new List<string> { "English_Class3.pdf", "English_Class3.pptx", "English_Class3.mp4" } },
    //    { 4, new List<string> { "English_Class4.pdf", "English_Class4.pptx", "English_Class4.mp4" } },
    //    { 5, new List<string> { "English_Class5.pdf", "English_Class5.pptx", "English_Class5.mp4" } },
    //    { 6, new List<string> { "English_Class6.pdf", "English_Class6.pptx", "English_Class6.mp4" } },
    //    { 7, new List<string> { "English_Class7.pdf", "English_Class7.pptx", "English_Class7.mp4" } },
    //    { 8, new List<string> { "English_Class8.pdf", "English_Class8.pptx", "English_Class8.mp4" } },
    //    { 9, new List<string> { "English_Class9.pdf", "English_Class9.pptx", "English_Class9.mp4" } },
    //    { 10, new List<string> { "English_Class10.pdf", "English_Class10.pptx", "English_Class10.mp4" } }
    //};
    //    private Dictionary<int, List<string>> lessons = new Dictionary<int, List<string>>
    //{
    //    { 1, new List<string> { "English_Class1_Lesson1", "English_Class1_Lesson2", "English_Class1_Lesson3", "English_Class1_Lesson4", "English_Class1_Lesson5", "English_Class1_Lesson6", "English_Class1_Lesson7", "English_Class1_Lesson8", "English_Class1_Lesson9", "English_Class1_Lesson10", "English_Class1_Lesson11", "English_Class1_Lesson12" } },
    //    { 2, new List<string> { "English_Class2_Lesson1", "English_Class2_Lesson2", "English_Class2_Lesson3", "English_Class2_Lesson4", "English_Class2_Lesson5", "English_Class2_Lesson6", "English_Class2_Lesson7", "English_Class2_Lesson8", "English_Class2_Lesson9", "English_Class2_Lesson10", "English_Class2_Lesson11", "English_Class2_Lesson12" } },
    //    { 3, new List<string> { "English_Class3_Lesson1", "English_Class3_Lesson2", "English_Class3_Lesson3", "English_Class3_Lesson4", "English_Class3_Lesson5", "English_Class3_Lesson6", "English_Class3_Lesson7", "English_Class3_Lesson8", "English_Class3_Lesson9", "English_Class3_Lesson10", "English_Class3_Lesson11", "English_Class3_Lesson12" } },
    //    { 4, new List<string> { "English_Class4_Lesson1", "English_Class4_Lesson2", "English_Class4_Lesson3", "English_Class4_Lesson4", "English_Class4_Lesson5", "English_Class4_Lesson6", "English_Class4_Lesson7", "English_Class4_Lesson8", "English_Class4_Lesson9", "English_Class4_Lesson10", "English_Class4_Lesson11", "English_Class4_Lesson12" } },
    //    { 5, new List<string> { "English_Class5_Lesson1", "English_Class5_Lesson2", "English_Class5_Lesson3", "English_Class5_Lesson4", "English_Class5_Lesson5", "English_Class5_Lesson6", "English_Class5_Lesson7", "English_Class5_Lesson8", "English_Class5_Lesson9", "English_Class5_Lesson10", "English_Class5_Lesson11", "English_Class5_Lesson12" } },
    //    { 6, new List<string> { "English_Class6_Lesson1", "English_Class6_Lesson2", "English_Class6_Lesson3", "English_Class6_Lesson4", "English_Class6_Lesson5", "English_Class6_Lesson6", "English_Class6_Lesson7", "English_Class6_Lesson8", "English_Class6_Lesson9", "English_Class6_Lesson10", "English_Class6_Lesson11", "English_Class6_Lesson12" } },
    //    { 7, new List<string> { "English_Class7_Lesson1", "English_Class7_Lesson2", "English_Class7_Lesson3", "English_Class7_Lesson4", "English_Class7_Lesson5", "English_Class7_Lesson6", "English_Class7_Lesson7", "English_Class7_Lesson8", "English_Class7_Lesson9", "English_Class7_Lesson10", "English_Class7_Lesson11", "English_Class7_Lesson12" } },
    //    { 8, new List<string> { "English_Class8_Lesson1", "English_Class8_Lesson2", "English_Class8_Lesson3", "English_Class8_Lesson4", "English_Class8_Lesson5", "English_Class8_Lesson6", "English_Class8_Lesson7", "English_Class8_Lesson8", "English_Class8_Lesson9", "English_Class8_Lesson10", "English_Class8_Lesson11", "English_Class8_Lesson12" } },
    //    { 9, new List<string> { "English_Class9_Lesson1", "English_Class9_Lesson2", "English_Class9_Lesson3", "English_Class9_Lesson4", "English_Class9_Lesson5", "English_Class9_Lesson6", "English_Class9_Lesson7", "English_Class9_Lesson8", "English_Class9_Lesson9", "English_Class9_Lesson10", "English_Class9_Lesson11", "English_Class9_Lesson12" } },
    //    { 10, new List<string> { "English_Class10_Lesson1", "English_Class10_Lesson2", "English_Class10_Lesson3", "English_Class10_Lesson4", "English_Class10_Lesson5", "English_Class10_Lesson6", "English_Class10_Lesson7", "English_Class10_Lesson8", "English_Class10_Lesson9", "English_Class10_Lesson10", "English_Class10_Lesson11", "English_Class10_Lesson12" } }
    //};
        //        "English_Lesson1_Exercise2.pdf", "English_Lesson1_Exercise3.pdf" 
        //"English_Lesson2_Exercise2.pdf", "English_Lesson2_Exercise3.pdf" 
        //"English_Lesson3_Exercise2.pdf", "English_Lesson3_Exercise3.pdf" 
        //"English_Lesson4_Exercise2.pdf", "English_Lesson4_Exercise3.pdf" 
        //"English_Lesson5_Exercise2.pdf", "English_Lesson5_Exercise3.pdf" 
        //"English_Lesson6_Exercise2.pdf", "English_Lesson6_Exercise3.pdf" 
        //"English_Lesson7_Exercise2.pdf", "English_Lesson7_Exercise3.pdf" 
        //"English_Lesson8_Exercise2.pdf", "English_Lesson8_Exercise3.pdf" 
        //"English_Lesson9_Exercise2.pdf", "English_Lesson9_Exercise3.pdf" 
        //"English_Lesson10_Exercise2.pdf", "English_Lesson10_Exercise3.pdf

        public EnglishFiles()
        {
            InitializeComponent();
        }

        private void EnglishFiles_Load(object sender, EventArgs e)
        {
            lblSchl.Text = "              " + SchlName + "          ";
            button2.Margin = new Padding(10, 10, 10, 50);
            button2.Location = new Point(button2.Location.X, button2.Location.Y - 40);
            btnLogout.Visible = false;
            lblSchl.Visible = false;
            lbllesson.Left = (this.ClientSize.Width - lbllesson.Width) / 2;
            grpLesson.Left = (this.ClientSize.Width - grpLesson.Width) / 2;
            grpLesson.Top = (this.ClientSize.Height - grpLesson.Height) / 3;
            LoadDocumentsForClass(ClassNo);
        }
        //private void LoadDocumentsForClass(int classNo)
        //{
        //    if (lessons.TryGetValue(classNo, out List<string> documents))
        //    {
        //        grpLesson.Controls.Clear();

        //        FlowLayoutPanel flowPanel = new FlowLayoutPanel
        //        {
        //            Dock = DockStyle.Fill,
        //            AutoScroll = true,
        //            WrapContents = true,
        //            FlowDirection = FlowDirection.LeftToRight,
        //            Padding = new Padding(20),
        //        };

        //        foreach (var doc in documents)
        //        {
        //            Panel folderItem = new Panel
        //            {
        //                Width = 100,
        //                Height = 100,
        //                Margin = new Padding(10)
        //            };

        //            PictureBox folderIcon = new PictureBox
        //            {
        //                Image = Properties.Resources.folder, // Replace with your folder image in Resources
        //                Size = new Size(64, 64),
        //                SizeMode = PictureBoxSizeMode.StretchImage,
        //                Location = new Point(18, 0),
        //                Cursor = Cursors.Hand,
        //                Tag = doc
        //            };
        //            folderIcon.Click += FolderIcon_Click;

        //            Label folderLabel = new Label
        //            {
        //                Text = doc,
        //                AutoSize = false,
        //                Width = 100,
        //                Height = 30,
        //                TextAlign = ContentAlignment.MiddleCenter,
        //                Location = new Point(0, 70)
        //            };

        //            folderItem.Controls.Add(folderIcon);
        //            folderItem.Controls.Add(folderLabel);
        //            flowPanel.Controls.Add(folderItem);
        //        }

        //        grpLesson.Controls.Add(flowPanel); // Add the flow layout to your GroupBox
        //    }
        //    else
        //    {
        //        MessageBox.Show($"No documents found for this subject of class {classNo}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void LoadDocumentsForClass(int classNo)
        {
            string englishFolder = Path.Combine(
                Application.StartupPath,
                @"..\..\Files",
                $"Class {classNo}",
                "English");

            englishFolder = Path.GetFullPath(englishFolder);

            if (!Directory.Exists(englishFolder))
            {
                MessageBox.Show("English folder not found:\n" + englishFolder);
                return;
            }

            grpLesson.Controls.Clear();

            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(20)
            };

            string[] lessonFolders = Directory.GetDirectories(englishFolder);

            foreach (string lessonFolder in lessonFolders)
            {
                string lessonName = Path.GetFileName(lessonFolder);

                Panel folderItem = new Panel
                {
                    Width = 100,
                    Height = 100,
                    Margin = new Padding(10)
                };

                PictureBox folderIcon = new PictureBox
                {
                    Image = Properties.Resources.folder1,
                    Size = new Size(64, 64),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(18, 0),
                    Cursor = Cursors.Hand,
                    Tag = lessonName
                };

                folderIcon.Click += FolderIcon_Click;

                Label folderLabel = new Label
                {
                    Text = lessonName,
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

            grpLesson.Controls.Add(flowPanel);
        }
        private void FolderIcon_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pic)
            {
                string lessonName = pic.Tag.ToString();

                LessonsList openPPTFile = new LessonsList
                {
                    LessonName = lessonName,
                    SubjectName = "English",
                    ClassNo = ClassNo,
                    SchlName = SchlName
                };

                openPPTFile.Show();
                this.Hide();
            }
        }

        private void DocumentButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string documentPath = clickedButton.Tag.ToString();

                string fileExtension = System.IO.Path.GetExtension(documentPath);
                if(fileExtension == ".pdf")
                {
                    OpenPdfFile english = new OpenPdfFile();
                    english.DocName = documentPath;
                    english.ClassNo = ClassNo;
                    english.Show();
                    this.Hide();
                }else if(fileExtension == ".pptx")
                {
                    OpenPPTFile openPPTFile = new OpenPPTFile();
                    openPPTFile.DocName = documentPath;
                    openPPTFile.ClassNo = ClassNo;
                    openPPTFile.Show();
                    this.Hide();
                }else if(fileExtension == ".mp4")
                {
                    OpenMp4 openPPTFile = new OpenMp4();
                    openPPTFile.DocName = documentPath;
                    openPPTFile.ClassNo = ClassNo;
                    openPPTFile.Show();
                    this.Hide();
                }
                //MessageBox.Show($"Opening document: {documentPath}", "Document Open", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Subjects englishFiles = new Subjects();
            englishFiles.ClassNo = ClassNo;
            englishFiles.SchlName = SchlName;
            englishFiles.Show();
            this.Hide();
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

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void labelArrow_Click(object sender, EventArgs e)
        {

            btnLogout.Visible = true;
        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int lessonNumber =Convert.ToInt32(comboBox1.SelectedItem?.ToString()) ;
        //    LoadDocumentsForClass(ClassNo, lessonNumber);
        //}
    }
}


/*
 
        private void LoadDocumentsForClass(int classNo)
        {
            if (lessons.TryGetValue(classNo, out List<string> documents))
            {
                int linkLabelHeight = 30;
                int spacing = 20;
                int startX1 = 50;  
                int startX2 = 280; 
                int startY = 50;
                int midIndex = (documents.Count + 1) / 2;

                grpLesson.Controls.Clear();

                for (int i = 0; i < documents.Count; i++)
                {
                    int column = i < midIndex ? 0 : 1; 
                    int xPosition = column == 0 ? startX1 : startX2;
                    int yPosition = startY + ((i % midIndex) * (linkLabelHeight + spacing));

                    LinkLabel documentLinkLabel = new LinkLabel
                    {
                        Text = $"• {documents[i]}",
                        AutoSize = true,
                        Location = new System.Drawing.Point(xPosition, yPosition),
                        Tag = documents[i],
                        BackColor = Color.LightGray
                    };

                    documentLinkLabel.LinkClicked += DocumentLinkLabel_LinkClicked;
                    grpLesson.Controls.Add(documentLinkLabel);
                }
            }
            else
            {
                MessageBox.Show($"No documents found for this subject of class {classNo}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void DocumentLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel linkLabel)
            {
                LessonsList openPPTFile = new LessonsList();
                openPPTFile.LessonName = linkLabel.Text.Substring(2);
                openPPTFile.SubjectName = "English";
                openPPTFile.ClassNo = ClassNo;
                openPPTFile.SchlName = SchlName;
                openPPTFile.Show();
                this.Hide();
            }

        } 
 
 */