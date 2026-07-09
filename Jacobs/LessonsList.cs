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
using static System.Windows.Forms.AxHost;
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
    //    private Dictionary<int, List<string>> EnglishclassDocuments = new Dictionary<int, List<string>>
    //{
    //    { 1, new List<string> { "flame_test_simulator.html", "periodic_table_quiz.html", "A_human_brain__3d.glb" } },
    //    { 2, new List<string> { "English_Class1.pdf", "English_Class1.pptx", "English_Class1.mp4" } },
    //    { 3, new List<string> { "English_Class3.pdf", "English_Class3.pptx", "English_Class3.mp4" } },
    //    { 4, new List<string> { "English_Class4.pdf", "English_Class4.pptx", "English_Class4.mp4" } },
    //    { 5, new List<string> { "English_Class5.pdf", "English_Class5.pptx", "English_Class5.mp4" } },
    //    { 6, new List<string> { "English_Class6.pdf", "English_Class6.pptx", "English_Class6.mp4" } },
    //    { 7, new List<string> { "English_Class7.pdf", "English_Class7.pptx", "English_Class7.mp4" } },
    //    { 8, new List<string> { "English_Class8.pdf", "English_Class8.pptx", "English_Class8.mp4" } },
    //    { 9, new List<string> { "English_Class9.pdf", "English_Class9.pptx", "English_Class9.mp4" } },
    //    { 10, new List<string> { "English_Class10.pdf", "English_Class10.pptx", "English_Class10.mp4" } }
    //};
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
    //    private Dictionary<int, List<string>> ScienceclassDocuments = new Dictionary<int, List<string>>
    //{
    //    { 1, new List<string> { "Science_Class1.pdf", "Science_Class1.pptx", "Science_Class1.mp4" } },
    //    { 2, new List<string> { "Science_Class2.pdf", "Science_Class2.pptx", "Science_Class2.mp4" } },
    //    { 3, new List<string> { "Science_Class3.pdf", "Science_Class3.pptx", "Science_Class3.mp4" } },
    //    { 4, new List<string> { "Science_Class4.pdf", "Science_Class4.pptx", "Science_Class4.mp4" } },
    //    { 5, new List<string> { "Science_Class5.pdf", "Science_Class5.pptx", "Science_Class5.mp4" } },
    //    { 6, new List<string> { "Science_Class6.pdf", "Science_Class6.pptx", "Science_Class6.mp4" } },
    //    { 7, new List<string> { "Science_Class7.pdf", "Science_Class7.pptx", "Science_Class7.mp4" } },
    //    { 8, new List<string> { "Science_Class8.pdf", "Science_Class8.pptx", "Science_Class8.mp4" } },
    //    { 9, new List<string> { "Science_Class9.pdf", "Science_Class9.pptx", "Science_Class9.mp4" } },
    //    { 10, new List<string> { "Science_Class10.pdf", "Science_Class10.pptx", "Science_Class10.mp4" } }
    //};
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
        //private void LoadDocumentsForClass(int classNo, string lessonNumber)
        //{
        //    if (SubjectName == "English")
        //        classDocuments = new Dictionary<int, List<string>>(EnglishclassDocuments);
        //    else if (SubjectName == "Hindi")
        //        classDocuments = new Dictionary<int, List<string>>(HindiclassDocuments);
        //    else if (SubjectName == "Kannada")
        //        classDocuments = new Dictionary<int, List<string>>(KannadaclassDocuments);
        //    else if (SubjectName == "Math")
        //        classDocuments = new Dictionary<int, List<string>>(MathclassDocuments);
        //    else if (SubjectName == "Science")
        //        classDocuments = new Dictionary<int, List<string>>(ScienceclassDocuments);
        //    else if (SubjectName == "Social")
        //        classDocuments = new Dictionary<int, List<string>>(SocialclassDocuments);

        //    // Load documents as folders
        //    groupBox2.Controls.Clear();
        //    FlowLayoutPanel docFlow = CreateFolderFlowPanel();
        //    if (classDocuments.TryGetValue(classNo, out List<string> documents))
        //    {
        //        foreach (var doc in documents)
        //        {
        //            docFlow.Controls.Add(CreateFolderItem(doc));
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show($"No documents found for this subject of class {classNo}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    groupBox2.Controls.Add(docFlow);

        //    // Load exercises as folders
        //    groupBox4.Controls.Clear();
        //    FlowLayoutPanel exFlow = CreateFolderFlowPanel();
        //    if (exercise.TryGetValue(lessonNumber, out List<string> exercises))
        //    {
        //        foreach (var ex in exercises)
        //        {
        //            exFlow.Controls.Add(CreateFolderItem(ex));
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show($"No exercise found for this subject of lesson {lessonNumber}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    groupBox4.Controls.Add(exFlow);
        //}
        private void SetRoundedPanel(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);

            path.CloseFigure();

            control.Region = new Region(path);
        }
        private void LoadDocumentsForClass(int classNo, string lessonNumber)
        {


            //string lessonFolder = Path.Combine(
            //    Application.StartupPath,
            //      @"..\..\Files",
            //     $"Class {classNo}",
            //    SubjectName,
            //    lessonNumber);

            //lessonFolder = Path.GetFullPath(lessonFolder);


            //string filesPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Files"));
            string filesPath = Path.GetFullPath(Path.Combine(Application.StartupPath, "Files"));

            // Find the class folder
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

            // Subject folder
            string subjectFolder = Path.Combine(classFolder, SubjectName);

            if (!Directory.Exists(subjectFolder))
            {
                MessageBox.Show("Subject folder not found.");
                return;
            }

            // Lesson folder
            string lessonFolder = Path.Combine(subjectFolder, lessonNumber);

            if (!Directory.Exists(lessonFolder))
            {
                MessageBox.Show("Lesson folder not found.");
                return;
            }

            //groupBox2.Controls.Clear();
            //groupBox4.Controls.Clear();

            FlowLayoutPanel docFlow = CreateFolderFlowPanel();
            FlowLayoutPanel exFlow = CreateFolderFlowPanel();

            // string[] files = Directory.GetFiles(lessonFolder);

            string[] files = Directory.GetFiles(lessonFolder,"*.enc");
            string[] folders = Directory.GetDirectories(lessonFolder);
            // MessageBox.Show(files.Length.ToString());
            foreach (string folder in folders)
            {
                docFlow.Controls.Add(CreateFolderItem(folder));
            }

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                 
                if (fileName.ToLower().Contains("exercise"))
                {


                    docFlow.Controls.Add(CreateFileItem(file));
                }
                else
                {
                     
                    docFlow.Controls.Add(CreateFileItem(file));
                }
            }

            groupBox2.Controls.Add(docFlow);
            //groupBox4.Controls.Add(exFlow);
        }

       
        private FlowLayoutPanel CreateFolderFlowPanel()
        {
            FlowLayoutPanel flow = new FlowLayoutPanel();

            flow.Dock = DockStyle.Fill;
            flow.AutoScroll = true;
            flow.WrapContents = true;
            flow.FlowDirection = FlowDirection.LeftToRight;

            flow.Padding = new Padding(25, 70, 25, 25);
            flow.BackColor = Color.White;

            return flow;
        }



        private Panel CreateFolderItem(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            fileName = Path.GetFileNameWithoutExtension(fileName);

            Panel panel = new Panel();
            panel.Width = 220;
            panel.Height = 250;
            panel.Margin = new Padding(20);

            panel.BackColor = Color.White;

          

            // Rounded card
            SetRoundedPanel(panel, 18);

            // Circle background
            Panel circle = new Panel();
            circle.Width = 120;
            circle.Height = 120;
            circle.BackColor = Color.FromArgb(245, 248, 252);

            circle.Left = (panel.Width - circle.Width) / 2;
            circle.Top = 20;

            SetRoundedPanel(circle, 60);

            // Folder Image
            PictureBox folderIcon = new PictureBox();

            folderIcon.Image = Jacobs.Properties.Resources.logofolde;

            folderIcon.Size = new Size(75, 75);

            folderIcon.SizeMode = PictureBoxSizeMode.Zoom;

            folderIcon.Left = (circle.Width - folderIcon.Width) / 2;
            folderIcon.Top = (circle.Height - folderIcon.Height) / 2;

            folderIcon.Cursor = Cursors.Hand;

            folderIcon.Tag = filePath;

            circle.Controls.Add(folderIcon);

            // File Name
            Label lbl = new Label();

            lbl.Text = fileName;

            lbl.Width = panel.Width;

            lbl.Height = 40;

            lbl.Location = new Point(0, 150);

            lbl.TextAlign = ContentAlignment.MiddleCenter;

            lbl.ForeColor = Color.FromArgb(15, 23, 42);

            lbl.Font = new Font(
                "Segoe UI",
                11,
                FontStyle.Bold);

            // File Count
            Label lblCount = new Label();

            lblCount.Text = "1 File";

            lblCount.Width = panel.Width;

            lblCount.Height = 25;

            lblCount.Location = new Point(0, 190);

            lblCount.TextAlign = ContentAlignment.MiddleCenter;

            lblCount.ForeColor = Color.RoyalBlue;

            lblCount.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Regular);

            panel.Controls.Add(circle);
            panel.Controls.Add(lbl);
            panel.Controls.Add(lblCount);

            folderIcon.Click += FolderIcon_Click;
            lbl.Click += FolderIcon_Click;

            return panel;
        }
        private void FolderIcon_Click(object sender, EventArgs e)
        {
            //if (sender is PictureBox pic)
            //{
            //     string documentPath = pic.Tag.ToString();
            //    string tempFile =FileSecurity.DecryptToTemp(documentPath,"SmsTeacher@123");
            //    string originalFileName = Path.GetFileNameWithoutExtension(documentPath);
            //    string extension = Path.GetExtension(originalFileName).ToLower();

            //    if (extension == ".pdf")
            //    {
            //        OpenPdfFile pdfViewer = new OpenPdfFile
            //        {
            //            DocName = tempFile,
            //            ClassNo = ClassNo,
            //            SchlName = SchlName,
            //            LessonName = LessonName,
            //            SubjectName = SubjectName
            //        };
            //        pdfViewer.Show();
            //        this.Hide();
            //    }
            //    else if (extension == ".pptx")
            //    {
            //        OpenPPTFile pptViewer = new OpenPPTFile
            //        {
            //            DocName = tempFile,
            //            ClassNo = ClassNo,
            //            SchlName = SchlName,
            //            LessonName = LessonName,
            //            SubjectName = SubjectName
            //        };
            //        pptViewer.Show();
            //        this.Hide();
            //    }
            //    else if (extension == ".mp4")
            //    {
            //        OpenMp4 mp4Viewer = new OpenMp4
            //        {
            //            DocName = tempFile,
            //            ClassNo = ClassNo,
            //            SchlName = SchlName,
            //            LessonName = LessonName,
            //            SubjectName = SubjectName
            //        };
            //        mp4Viewer.Show();
            //        this.Hide();
            //    }
            //    else if (extension == ".html" || extension == ".htm")
            //    {
            //        OpenHtml htmlViewer = new OpenHtml();
            //        htmlViewer.DocName = tempFile;
            //        htmlViewer.ClassNo = ClassNo;
            //        htmlViewer.SchlName = SchlName;
            //        htmlViewer.LessonName = LessonName;
            //        htmlViewer.SubjectName = SubjectName;
            //        htmlViewer.Show();
            //        this.Hide();
            //    }
            //    else if (extension == ".glb")
            //    {
            //        OpenGlb glbViewer = new OpenGlb
            //        {
            //            DocName = tempFile,
            //            ClassNo = ClassNo,
            //            SchlName = SchlName,
            //            LessonName = LessonName,
            //            SubjectName = SubjectName
            //        };
            //        glbViewer.Show();
            //        this.Hide();
            //    }
            //}

            string path = "";

            if (sender is PictureBox pic)
                path = pic.Tag.ToString();
            else if (sender is Label lbl)
                path = lbl.Tag.ToString();

            if (Directory.Exists(path))
            {
                LoadFolderContents(path);
            }
        }
        private void LoadFolderContents(string folderPath)
        {
            groupBox2.Controls.Clear();
            groupBox4.Controls.Clear();

            FlowLayoutPanel docFlow = CreateFolderFlowPanel();
            FlowLayoutPanel exFlow = CreateFolderFlowPanel();

            string[] folders = Directory.GetDirectories(folderPath);
            string[] files = Directory.GetFiles(folderPath, "*.enc");

            foreach (string folder in folders)
            {
                docFlow.Controls.Add(CreateFolderItem(folder));
            }

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);

                if (fileName.ToLower().Contains("exercise"))
                    docFlow.Controls.Add(CreateFileItem(file));
                else
                    docFlow.Controls.Add(CreateFileItem(file));
            }

            groupBox2.Controls.Add(docFlow);
            groupBox4.Controls.Add(exFlow);

            // Optional: Add the headers again
            //AddHeaders();
        }
        private Panel CreateFileItem(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // Remove .enc
            fileName = Path.GetFileNameWithoutExtension(fileName);

            Panel pnl = new Panel();
            pnl.Width = 480;
            pnl.Height = 50;
            pnl.Margin = new Padding(5);
            pnl.BackColor = Color.White;
            pnl.BorderStyle = BorderStyle.FixedSingle;

            Label lblName = new Label();
            lblName.Text = fileName;
            lblName.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblName.AutoSize = false;
            lblName.Width = 450;
            lblName.Height = 30;
            lblName.Location = new Point(15, 10);
            lblName.Cursor = Cursors.Hand;

            pnl.Tag = filePath;
            lblName.Tag = filePath;

            pnl.Controls.Add(lblName);

            pnl.Click += File_Click;
            lblName.Click += File_Click;

            return pnl;
        }
        private void File_Click(object sender, EventArgs e)
        {
            string filePath = ((Control)sender).Tag.ToString();

            string ext = Path.GetExtension(
                Path.GetFileNameWithoutExtension(filePath))
                .ToLower();

            string tempFile =
                FileSecurity.DecryptToTemp(
                    filePath,
                    "SmsTeacher@123");

            if (ext == ".html")
            {
                OpenHtml frm = new OpenHtml();

                frm.DocName = tempFile;
                frm.ClassNo = ClassNo;
                frm.SchlName = SchlName;
                frm.LessonName = LessonName;
                frm.SubjectName = SubjectName;

                frm.Show();
            }
            else if (ext == ".pdf")
            {
                OpenPdfFile frm = new OpenPdfFile();

                frm.DocName = tempFile;
                frm.ClassNo = ClassNo;
                frm.SchlName = SchlName;
                frm.LessonName = LessonName;
                frm.SubjectName = SubjectName;

                frm.Show();
            }
            else if (ext == ".pptx")
            {
                OpenPPTFile frm = new OpenPPTFile();

                frm.DocName = tempFile;
                frm.ClassNo = ClassNo;
                frm.SchlName = SchlName;
                frm.LessonName = LessonName;
                frm.SubjectName = SubjectName;

                frm.Show();
            }
            else if (ext == ".mp4")
            {
                OpenMp4 frm = new OpenMp4();

                frm.DocName = tempFile;
                frm.ClassNo = ClassNo;
                frm.SchlName = SchlName;
                frm.LessonName = LessonName;
                frm.SubjectName = SubjectName;

                frm.Show();
            }
            else if (ext == ".glb")
            {
                OpenGlb frm = new OpenGlb();

                frm.DocName = tempFile;
                frm.ClassNo = ClassNo;
                frm.SchlName = SchlName;
                frm.LessonName = LessonName;
                frm.SubjectName = SubjectName;

                frm.Show();
            }

            this.Hide();
        }
        private void LessonsList_Load(object sender, EventArgs e)
        {
            // Full Screen
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(245, 248, 252);

            // School Name
            lblSchl.Text = "Jacobs Educare";
            lblSchl.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblSchl.ForeColor = Color.RoyalBlue;
            lblSchl.AutoSize = true;
            lblSchl.Left = (this.ClientSize.Width - lblSchl.Width) / 2;
            lblSchl.Top = 20;

            // Lesson Name
            lblLesson.Text = LessonName;
            lblLesson.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblLesson.ForeColor = Color.RoyalBlue;
            lblLesson.AutoSize = true;
            lblLesson.Left = (this.ClientSize.Width - lblLesson.Width) / 2;
            lblLesson.Top = 100;

            btnLogout.Visible = false;

            // GroupBoxes
            int gap = 30;
            int sideMargin = 50;

            int availableWidth = this.ClientSize.Width - (sideMargin * 2) - gap;

            groupBox2.Width = availableWidth / 2;
            groupBox4.Width = availableWidth / 2;

            groupBox2.Height = 550;
            groupBox4.Height = 550;

            groupBox2.Top = 180;
            groupBox4.Top = 180;

            groupBox2.Left = sideMargin;
            groupBox4.Left = groupBox2.Right + gap;

            groupBox2.BackColor = Color.White;
            groupBox4.BackColor = Color.White;

            groupBox2.Text = "";
            groupBox4.Text = "";


            btnBack.Text = "← Back";
            btnBack.Width = 110;
            btnBack.Height = 35;

            btnBack.BackColor = Color.FromArgb(37, 99, 235);
            btnBack.ForeColor = Color.White;

            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;

            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);

          

            // Load folders first
            LoadDocumentsForClass(ClassNo, LessonName);

            // Document Header
            Label lblDoc = new Label();
            lblDoc.Text = "📄 Lesson Documents";
            lblDoc.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblDoc.ForeColor = Color.FromArgb(20, 40, 90);
            lblDoc.AutoSize = true;
            lblDoc.Location = new Point(15, 15);

            groupBox2.Controls.Add(lblDoc);
            lblDoc.BringToFront();

            // Blue Line
            Panel line1 = new Panel();
            line1.BackColor = Color.RoyalBlue;
            line1.Size = new Size(50, 3);
            line1.Location = new Point(15, 50);

            groupBox2.Controls.Add(line1);
            line1.BringToFront();

            // Exercise Header
            Label lblEx = new Label();
            lblEx.Text = "✏ Lesson Exercises";
            lblEx.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblEx.ForeColor = Color.FromArgb(20, 40, 90);
            lblEx.AutoSize = true;
            lblEx.Location = new Point(15, 15);

            groupBox4.Controls.Add(lblEx);
            lblEx.BringToFront();

            // Blue Line
            Panel line2 = new Panel();
            line2.BackColor = Color.RoyalBlue;
            line2.Size = new Size(50, 3);
            line2.Location = new Point(15, 50);

            groupBox4.Controls.Add(line2);
            line2.BringToFront();
            this.Text = "";
        }


        private void GroupBoxBlueBorder(object sender, PaintEventArgs e)
        {
            GroupBox grp = sender as GroupBox;

            using (Pen pen = new Pen(Color.RoyalBlue, 3))
            {
                e.Graphics.DrawLine( pen,
                    0,
                    1,
                    grp.Width,
                    1);
            }
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

        private void btnLogout_Click(object sender, EventArgs e)
        {

            Form1 home = new Form1();
            home.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

             
            ScienceFile frm = new ScienceFile();

            frm.SubjectName = SubjectName;
            frm.ClassNo = ClassNo;
            frm.SchlName = SchlName;

            frm.Show();
            this.Hide();
        }

        private void labelArrow_Click(object sender, EventArgs e)
        {

            btnLogout.Visible = true;
        }
    }
}

 