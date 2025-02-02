using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace JacobsDesktopApp
{
    public partial class LessonsList : Form
    {
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
        public string Board { get; set; }
        private Dictionary<int, List<string>> classDocuments;
        private Dictionary<int, List<string>> EnglishclassDocuments = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "English_Class1.pdf", "English_Class1.pptx", "English_Class1.mp4" } },
        { 2, new List<string> { "English_Class2.pdf", "English_Class2.pptx", "English_Class2.mp4" } },
        { 3, new List<string> { "English_Class3.pdf", "English_Class3.pptx", "English_Class3.mp4" } },
        { 4, new List<string> { "English_Class4.pdf", "English_Class4.pptx", "English_Class4.mp4" } },
        { 5, new List<string> { "English_Class5.pdf", "English_Class5.pptx", "English_Class5.mp4" } },
        { 6, new List<string> { "English_Class6.pdf", "English_Class6.pptx", "English_Class6.mp4" } },
        { 7, new List<string> { "English_Class7.pdf", "English_Class7.pptx", "English_Class7.mp4" } },
        { 8, new List<string> { "English_Class8.pdf", "English_Class8.pptx", "English_Class8.mp4" } },
        { 9, new List<string> { "English_Class9.pdf", "English_Class9.pptx", "English_Class9.mp4" } },
        { 10, new List<string> { "English_Class10.pdf", "English_Class10.pptx", "English_Class10.mp4" } }
    };
        private Dictionary<int, List<string>> HindiclassDocuments = new Dictionary<int, List<string>>
        {
            { 1, new List<string> { "Hindi_Class1.pdf", "Hindi_Class1.pptx", "Hindi_Class1.mp4" } },
            { 2, new List<string> { "Hindi_Class2.pdf", "Hindi_Class2.pptx", "Hindi_Class2.mp4" } },
            { 3, new List<string> { "Hindi_Class3.pdf", "Hindi_Class3.pptx", "Hindi_Class3.mp4" } },
            { 4, new List<string> { "Hindi_Class4.pdf", "Hindi_Class4.pptx", "Hindi_Class4.mp4" } },
            { 5, new List<string> { "Hindi_Class5.pdf", "Hindi_Class5.pptx", "Hindi_Class5.mp4" } },
            { 6, new List<string> { "Hindi_Class6.pdf", "Hindi_Class6.pptx", "Hindi_Class6.mp4" } },
            { 7, new List<string> { "Hindi_Class7.pdf", "Hindi_Class7.pptx", "Hindi_Class7.mp4" } },
            { 8, new List<string> { "Hindi_Class8.pdf", "Hindi_Class8.pptx", "Hindi_Class8.mp4" } },
            { 9, new List<string> { "Hindi_Class9.pdf", "Hindi_Class9.pptx", "Hindi_Class9.mp4" } },
            { 10, new List<string> { "Hindi_Class10.pdf", "Hindi_Class10.pptx", "Hindi_Class10.mp4" } }
        };
        private Dictionary<int, List<string>> MathclassDocuments = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Math_Class1.pdf", "Math_Class1.pptx", "Math_Class1.mp4" } },
        { 2, new List<string> { "Math_Class2.pdf", "Math_Class2.pptx", "Math_Class2.mp4" } },
        { 3, new List<string> { "Math_Class3.pdf", "Math_Class3.pptx", "Math_Class3.mp4" } },
        { 4, new List<string> { "Math_Class4.pdf", "Math_Class4.pptx", "Math_Class4.mp4" } },
        { 5, new List<string> { "Math_Class5.pdf", "Math_Class5.pptx", "Math_Class5.mp4" } },
        { 6, new List<string> { "Math_Class6.pdf", "Math_Class6.pptx", "Math_Class6.mp4" } },
        { 7, new List<string> { "Math_Class7.pdf", "Math_Class7.pptx", "Math_Class7.mp4" } },
        { 8, new List<string> { "Math_Class8.pdf", "Math_Class8.pptx", "Math_Class8.mp4" } },
        { 9, new List<string> { "Math_Class9.pdf", "Math_Class9.pptx", "Math_Class9.mp4" } },
        { 10, new List<string> { "Math_Class10.pdf", "Math_Class10.pptx", "Math_Class10.mp4" } }
    };
        private Dictionary<int, List<string>> KannadaclassDocuments = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Kannada_Class1.pdf", "Kannada_Class1.pptx", "Kannada_Class1.mp4" } },
        { 2, new List<string> { "Kannada_Class2.pdf", "Kannada_Class2.pptx", "Kannada_Class2.mp4" } },
        { 3, new List<string> { "Kannada_Class3.pdf", "Kannada_Class3.pptx", "Kannada_Class3.mp4" } },
        { 4, new List<string> { "Kannada_Class4.pdf", "Kannada_Class4.pptx", "Kannada_Class4.mp4" } },
        { 5, new List<string> { "Kannada_Class5.pdf", "Kannada_Class5.pptx", "Kannada_Class5.mp4" } },
        { 6, new List<string> { "Kannada_Class6.pdf", "Kannada_Class6.pptx", "Kannada_Class6.mp4" } },
        { 7, new List<string> { "Kannada_Class7.pdf", "Kannada_Class7.pptx", "Kannada_Class7.mp4" } },
        { 8, new List<string> { "Kannada_Class8.pdf", "Kannada_Class8.pptx", "Kannada_Class8.mp4" } },
        { 9, new List<string> { "Kannada_Class9.pdf", "Kannada_Class9.pptx", "Kannada_Class9.mp4" } },
        { 10, new List<string> { "Kannada_Class10.pdf", "Kannada_Class10.pptx", "Kannada_Class10.mp4" } }
    };
        private Dictionary<int, List<string>> SocialclassDocuments = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Social_Class1.pdf", "Social_Class1.pptx", "Social_Class1.mp4" } },
        { 2, new List<string> { "Social_Class2.pdf", "Social_Class2.pptx", "Social_Class2.mp4" } },
        { 3, new List<string> { "Social_Class3.pdf", "Social_Class3.pptx", "Social_Class3.mp4" } },
        { 4, new List<string> { "Social_Class4.pdf", "Social_Class4.pptx", "Social_Class4.mp4" } },
        { 5, new List<string> { "Social_Class5.pdf", "Social_Class5.pptx", "Social_Class5.mp4" } },
        { 6, new List<string> { "Social_Class6.pdf", "Social_Class6.pptx", "Social_Class6.mp4" } },
        { 7, new List<string> { "Social_Class7.pdf", "Social_Class7.pptx", "Social_Class7.mp4" } },
        { 8, new List<string> { "Social_Class8.pdf", "Social_Class8.pptx", "Social_Class8.mp4" } },
        { 9, new List<string> { "Social_Class9.pdf", "Social_Class9.pptx", "Social_Class9.mp4" } },
        { 10, new List<string> { "Social_Class10.pdf", "Social_Class10.pptx", "Social_Class10.mp4" } }
    };
        private Dictionary<int, List<string>> ScienceclassDocuments = new Dictionary<int, List<string>>
    {
        { 1, new List<string> { "Science_Class1.pdf", "Science_Class1.pptx", "Science_Class1.mp4" } },
        { 2, new List<string> { "Science_Class2.pdf", "Science_Class2.pptx", "Science_Class2.mp4" } },
        { 3, new List<string> { "Science_Class3.pdf", "Science_Class3.pptx", "Science_Class3.mp4" } },
        { 4, new List<string> { "Science_Class4.pdf", "Science_Class4.pptx", "Science_Class4.mp4" } },
        { 5, new List<string> { "Science_Class5.pdf", "Science_Class5.pptx", "Science_Class5.mp4" } },
        { 6, new List<string> { "Science_Class6.pdf", "Science_Class6.pptx", "Science_Class6.mp4" } },
        { 7, new List<string> { "Science_Class7.pdf", "Science_Class7.pptx", "Science_Class7.mp4" } },
        { 8, new List<string> { "Science_Class8.pdf", "Science_Class8.pptx", "Science_Class8.mp4" } },
        { 9, new List<string> { "Science_Class9.pdf", "Science_Class9.pptx", "Science_Class9.mp4" } },
        { 10, new List<string> { "Science_Class10.pdf", "Science_Class10.pptx", "Science_Class10.mp4" } }
    };
        private Dictionary<string, List<string>> exercise = new Dictionary<string, List<string>>
    {
        { "English_Class1_Lesson1", new List<string> { "English_Lesson1_Exercise1.pdf"} },
        { "English_Class1_Lesson2", new List<string> { "English_Lesson2_Exercise1.pdf"} },
        { "English_Class1_Lesson3", new List<string> { "English_Lesson3_Exercise1.pdf"} },
        { "English_Class1_Lesson4", new List<string> { "English_Lesson4_Exercise1.pdf"} },
        { "English_Class1_Lesson5", new List<string> { "English_Lesson5_Exercise1.pdf"} },
        { "English_Class1_Lesson6", new List<string> { "English_Lesson6_Exercise1.pdf"} },
        { "English_Class1_Lesson7", new List<string> { "English_Lesson7_Exercise1.pdf"} },
        { "English_Class1_Lesson8", new List<string> { "English_Lesson8_Exercise1.pdf"} },
        { "English_Class1_Lesson9", new List<string> { "English_Lesson9_Exercise1.pdf"} },
        { "English_Class1_Lesson10", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class1_Lesson11", new List<string> { "English_Lesson11_Exercise1.pdf" } },
        { "English_Class1_Lesson12", new List<string> { "English_Lesson12_Exercise1.pdf" } },
        { "English_Class2_Lesson1", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson2", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson3", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson4", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson5", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson6", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson7", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson8", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson9", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class2_Lesson10", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "English_Class3_Lesson1", new List<string> { "English_Lesson1_Exercise1.pdf"} },
        { "English_Class3_Lesson2", new List<string> { "English_Lesson2_Exercise1.pdf"} },
        { "English_Class3_Lesson3", new List<string> { "English_Lesson3_Exercise1.pdf"} },
        { "English_Class3_Lesson4", new List<string> { "English_Lesson4_Exercise1.pdf"} },
        { "English_Class3_Lesson5", new List<string> { "English_Lesson5_Exercise1.pdf"} },
        { "English_Class3_Lesson6", new List<string> { "English_Lesson6_Exercise1.pdf"} },
        { "English_Class3_Lesson7", new List<string> { "English_Lesson7_Exercise1.pdf"} },
        { "English_Class3_Lesson8", new List<string> { "English_Lesson8_Exercise1.pdf"} },
        { "English_Class3_Lesson9", new List<string> { "English_Lesson9_Exercise1.pdf"} },
        { "English_Class3_Lesson10", new List<string> { "English_Lesson10_Exercise1.pdf" } },
        { "Hindi_Class1_Lesson1", new List<string> { "Hindi_Lesson1_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson2", new List<string> { "Hindi_Lesson2_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson3", new List<string> { "Hindi_Lesson3_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson4", new List<string> { "Hindi_Lesson4_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson5", new List<string> { "Hindi_Lesson5_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson6", new List<string> { "Hindi_Lesson6_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson7", new List<string> { "Hindi_Lesson7_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson8", new List<string> { "Hindi_Lesson8_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson9", new List<string> { "Hindi_Lesson9_Exercise1.pdf"} },
        { "Hindi_Class1_Lesson10", new List<string> { "Hindi_Lesson10_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson1", new List<string> { "Hindi_Lesson1_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson2", new List<string> { "Hindi_Lesson2_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson3", new List<string> { "Hindi_Lesson3_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson4", new List<string> { "Hindi_Lesson4_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson5", new List<string> { "Hindi_Lesson5_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson6", new List<string> { "Hindi_Lesson6_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson7", new List<string> { "Hindi_Lesson7_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson8", new List<string> { "Hindi_Lesson8_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson9", new List<string> { "Hindi_Lesson9_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson10", new List<string> { "Hindi_Lesson10_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson11", new List<string> { "Hindi_Lesson11_Exercise1.pdf" } },
        { "Hindi_Class2_Lesson12", new List<string> { "Hindi_Lesson12_Exercise1.pdf" } },
        { "Social_Class1_Lesson1", new List<string> { "Social_Lesson1_Exercise1.pdf" } },
        { "Social_Class1_Lesson2", new List<string> { "Social_Lesson2_Exercise1.pdf" } },
        { "Social_Class1_Lesson3", new List<string> { "Social_Lesson3_Exercise1.pdf" } },
        { "Social_Class1_Lesson4", new List<string> { "Social_Lesson4_Exercise1.pdf" } },
        { "Social_Class1_Lesson5", new List<string> { "Social_Lesson5_Exercise1.pdf" } },
        { "Social_Class1_Lesson6", new List<string> { "Social_Lesson6_Exercise1.pdf" } },
        { "Social_Class1_Lesson7", new List<string> { "Social_Lesson7_Exercise1.pdf" } },
        { "Social_Class1_Lesson8", new List<string> { "Social_Lesson8_Exercise1.pdf" } },
        { "Social_Class1_Lesson9", new List<string> { "Social_Lesson9_Exercise1.pdf" } },
        { "Social_Class1_Lesson10", new List<string> { "Social_Lesson10_Exercise1.pdf" } },
        { "Social_Class1_Lesson11", new List<string> { "Social_Lesson11_Exercise1.pdf" } },
        { "Social_Class1_Lesson12", new List<string> { "Social_Lesson12_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson1", new List<string> { "Kannada_Lesson1_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson2", new List<string> { "Kannada_Lesson2_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson3", new List<string> { "Kannada_Lesson3_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson4", new List<string> { "Kannada_Lesson4_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson5", new List<string> { "Kannada_Lesson5_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson6", new List<string> { "Kannada_Lesson6_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson7", new List<string> { "Kannada_Lesson7_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson8", new List<string> { "Kannada_Lesson8_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson9", new List<string> { "Kannada_Lesson9_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson10", new List<string> { "Kannada_Lesson10_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson11", new List<string> { "Kannada_Lesson11_Exercise1.pdf" } },
        { "Kannada_Class1_Lesson12", new List<string> { "Kannada_Lesson12_Exercise1.pdf" } },
        { "Science_Class1_Lesson1", new List<string> { "Science_Lesson1_Exercise1.pdf" } },
        { "Science_Class1_Lesson2", new List<string> { "Science_Lesson2_Exercise1.pdf" } },
        { "Science_Class1_Lesson3", new List<string> { "Science_Lesson3_Exercise1.pdf" } },
        { "Science_Class1_Lesson4", new List<string> { "Science_Lesson4_Exercise1.pdf" } },
        { "Science_Class1_Lesson5", new List<string> { "Science_Lesson5_Exercise1.pdf" } },
        { "Science_Class1_Lesson6", new List<string> { "Science_Lesson6_Exercise1.pdf" } },
        { "Science_Class1_Lesson7", new List<string> { "Science_Lesson7_Exercise1.pdf" } },
        { "Science_Class1_Lesson8", new List<string> { "Science_Lesson8_Exercise1.pdf" } },
        { "Science_Class1_Lesson9", new List<string> { "Science_Lesson9_Exercise1.pdf" } },
        { "Science_Class1_Lesson10", new List<string> { "Science_Lesson10_Exercise1.pdf" } },
        { "Science_Class1_Lesson11", new List<string> { "Science_Lesson11_Exercise1.pdf" } },
        { "Science_Class1_Lesson12", new List<string> { "Science_Lesson12_Exercise1.pdf" } },
        { "Math_Class1_Lesson1", new List<string> { "Math_Lesson1_Exercise1.pdf" } },
        { "Math_Class1_Lesson2", new List<string> { "Math_Lesson2_Exercise1.pdf" } },
        { "Math_Class1_Lesson3", new List<string> { "Math_Lesson3_Exercise1.pdf" } },
        { "Math_Class1_Lesson4", new List<string> { "Math_Lesson4_Exercise1.pdf" } },
        { "Math_Class1_Lesson5", new List<string> { "Math_Lesson5_Exercise1.pdf" } },
        { "Math_Class1_Lesson6", new List<string> { "Math_Lesson6_Exercise1.pdf" } },
        { "Math_Class1_Lesson7", new List<string> { "Math_Lesson7_Exercise1.pdf" } },
        { "Math_Class1_Lesson8", new List<string> { "Math_Lesson8_Exercise1.pdf" } },
        { "Math_Class1_Lesson9", new List<string> { "Math_Lesson9_Exercise1.pdf" } },
        { "Math_Class1_Lesson10", new List<string> { "Math_Lesson10_Exercise1.pdf" } },
        { "Math_Class1_Lesson11", new List<string> { "Math_Lesson11_Exercise1.pdf" } },
        { "Math_Class1_Lesson12", new List<string> { "Math_Lesson12_Exercise1.pdf" } }
    };
        public LessonsList()
        {
            InitializeComponent();
        }

        private void LoadDocumentsForClass(int classNo, string lessonNumber)
        {
            if(SubjectName == "English")
            {
                classDocuments = new Dictionary<int, List<string>>(EnglishclassDocuments);
            }else if(SubjectName == "Hindi")
            {
                classDocuments = new Dictionary<int, List<string>>(HindiclassDocuments);
            }else if( SubjectName == "Kannada")
            {
                classDocuments = new Dictionary<int, List<string>>(KannadaclassDocuments);
            }
            else if( SubjectName == "Math")
            {
                classDocuments = new Dictionary<int, List<string>>(MathclassDocuments);
            }
            else if( SubjectName == "Science")
            {
                classDocuments = new Dictionary<int, List<string>>(ScienceclassDocuments);
            }
            else if( SubjectName == "Social")
            {
                classDocuments = new Dictionary<int, List<string>>(SocialclassDocuments);
            }
            if (classDocuments.TryGetValue(classNo, out List<string> documents))
            {
                int buttonHeight = 30;
                int buttonWidth = 200;
                int spacing = 10;
                int startY = 20;
                groupBox2.Controls.Clear();
                for (int i = 0; i < documents.Count; i++)
                {
                    Button documentButton = new Button
                    {
                        Text = documents[i],
                        Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                        Location = new System.Drawing.Point(10, startY + (i * (buttonHeight + spacing))),
                        Tag = documents[i] // Store the document path in the Tag property
                    };
                    documentButton.Click += DocumentButton_Click;
                    groupBox2.Controls.Add(documentButton);
                }
            }
            else
            {
                MessageBox.Show($"No documents found for this subject of class {classNo}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (exercise.TryGetValue(lessonNumber, out List<string> exercises))
            {
                int buttonHeight = 30;
                int buttonWidth = 250;
                int spacing = 10;
                int startY = 20;
                groupBox4.Controls.Clear();
                for (int i = 0; i < exercises.Count; i++)
                {
                    Button documentButton = new Button
                    {
                        Text = exercises[i],
                        Size = new System.Drawing.Size(buttonWidth, buttonHeight),
                        Location = new System.Drawing.Point(10, startY + (i * (buttonHeight + spacing))),
                        Tag = exercises[i] 
                    };
                    documentButton.Click += DocumentButton_Click;
                    groupBox4.Controls.Add(documentButton);
                }
            }
            else
            {
                MessageBox.Show($"No exercise found for this subject of lesson {lessonNumber}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void DocumentButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string documentPath = clickedButton.Tag.ToString();

                string fileExtension = System.IO.Path.GetExtension(documentPath);
                if (fileExtension == ".pdf")
                {
                    OpenPdfFile english = new OpenPdfFile();
                    english.DocName = documentPath;
                    english.ClassNo = ClassNo;
                    english.SchlName = SchlName;
                    english.Show();
                    this.Hide();
                }
                else if (fileExtension == ".pptx")
                {
                    OpenPPTFile openPPTFile = new OpenPPTFile();
                    openPPTFile.DocName = documentPath;
                    openPPTFile.ClassNo = ClassNo;
                    openPPTFile.SchlName = SchlName;
                    openPPTFile.Show();
                    this.Hide();
                }
                else if (fileExtension == ".mp4")
                {
                    OpenMp4 openPPTFile = new OpenMp4();
                    openPPTFile.DocName = documentPath;
                    openPPTFile.ClassNo = ClassNo;
                    openPPTFile.SchlName = SchlName;
                    openPPTFile.Show();
                    this.Hide();
                }
            }
        }

        private void LessonsList_Load(object sender, EventArgs e)
        {
            lblSchl.Text = "              " + SchlName + "          ";
            lblLesson.Text = "              " + LessonName + "          ";
            LoadDocumentsForClass(ClassNo, LessonName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SubjectName == "English")
            {
                EnglishFiles englishFiles = new EnglishFiles();
                englishFiles.ClassNo = ClassNo;
                englishFiles.SchlName = SchlName;
                englishFiles.Show();
                this.Hide();
            }
            else if (SubjectName == "Hindi")
            {
                HindiFiles englishFiles = new HindiFiles();
                englishFiles.ClassNo = ClassNo;
                englishFiles.SchlName = SchlName;
                englishFiles.Show();
                this.Hide();
            }
            else if (SubjectName == "Kannada")
            {
                KanndaFiles englishFiles = new KanndaFiles();
                englishFiles.ClassNo = ClassNo;
                englishFiles.SchlName = SchlName;
                englishFiles.Show();
                this.Hide();
            }
            else if (SubjectName == "Math")
            {
                MathFiles englishFiles = new MathFiles();
                englishFiles.ClassNo = ClassNo;
                englishFiles.SchlName = SchlName;
                englishFiles.Show();
                this.Hide();
            }
            else if (SubjectName == "Science")
            {
                ScienceFile englishFiles = new ScienceFile();
                englishFiles.ClassNo = ClassNo;
                englishFiles.SchlName = SchlName;
                englishFiles.Show();
                this.Hide();
            }
            else if (SubjectName == "Social")
            {
                SocialFiles englishFiles = new SocialFiles();
                englishFiles.ClassNo = ClassNo;
                englishFiles.SchlName = SchlName;
                englishFiles.Show();
                this.Hide();
            }
        }
    }
}
