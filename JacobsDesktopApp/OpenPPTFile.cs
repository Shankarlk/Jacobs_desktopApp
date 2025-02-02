using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    public partial class OpenPPTFile : Form
    {
        private Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
        private Presentation pptPresentation;
        private int slideIndex = 1;
        private int slideIndexs = 1;
        public string DocName { get; set; }
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        private SpeechSynthesizer speechSynthesizer;
        private bool isSpeechPlaying;
        public OpenPPTFile()
        {
            InitializeComponent();
            speechSynthesizer = new SpeechSynthesizer();
            isSpeechPlaying = false;
        }

        private void OpenPPTFile_Load(object sender, EventArgs e)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string opf = Path.Combine(baseDirectory, "Files", DocName);
            string playimages = Path.Combine(baseDirectory, "Files", "play.png");
            Image playImage = Image.FromFile(playimages);
            Bitmap resizedPlayImage = new Bitmap(playImage, new Size(22, 22));
            btnPlayPause.Image = resizedPlayImage;
            pptPresentation = pptApplication.Presentations.Open(opf, MsoTriState.msoFalse, MsoTriState.msoFalse, MsoTriState.msoFalse);
            
            DisplaySlide();
        }

        private float zoomFactor = 1.0f;
        private void DisplaySlide()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string outputFolder = Path.Combine(baseDirectory, "output_images");
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
            // Export the slide as an png file
            string tempHtmlFile = Path.Combine(outputFolder, "temp.png");
            try
            {
                pptPresentation.Slides[slideIndexs].Export(tempHtmlFile, "png", 1024, 768);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }
            // Load the HTML file into the picture box control
            //pictureBox1.ImageLocation = tempHtmlFile;
            RenderZoomedImage(tempHtmlFile);
        }
        private void RenderZoomedImage(string imagePath)
        {
            if (!File.Exists(imagePath)) return;

            // Load the original image
            using (System.Drawing.Image originalImage = System.Drawing.Image.FromFile(imagePath))
            {
                // Calculate the zoomed dimensions
                int newWidth = (int)(originalImage.Width * zoomFactor);
                int newHeight = (int)(originalImage.Height * zoomFactor);

                // Create a new bitmap with the zoomed dimensions
                Bitmap zoomedImage = new Bitmap(newWidth, newHeight);
                using (Graphics graphics = Graphics.FromImage(zoomedImage))
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight));
                }

                pictureBox1.Image = zoomedImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            zoomFactor += 0.1f; // Increase zoom factor
            DisplaySlide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zoomFactor = Math.Max(0.1f, zoomFactor - 0.1f); 
            DisplaySlide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            speechSynthesizer.SpeakAsyncCancelAll();
            EnglishFiles englishFiles = new EnglishFiles();
            englishFiles.ClassNo = ClassNo;
            englishFiles.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {


            int currentSlideIndex = slideIndexs;
            if (currentSlideIndex < pptPresentation.Slides.Count)
            {
                slideIndexs = slideIndexs + 1;
                DisplaySlide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int currentSlideIndex = slideIndexs;
            if (currentSlideIndex > 1)
            {
                slideIndexs = slideIndexs - 1;
                DisplaySlide();
            }
        }
        int pp = 0;

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (pp == 0)
            {
                pp = 1;
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string opf = Path.Combine(baseDirectory, "Files", DocName);
                string playimages = Path.Combine(baseDirectory, "Files", "pause.png");
                Image playImage = Image.FromFile(playimages);
                Bitmap resizedPlayImage = new Bitmap(playImage, new Size(22, 22));
                btnPlayPause.Image = resizedPlayImage;
                    ReadSlideText(slideIndex);
                speechSynthesizer.Resume();
            } else if (pp == 1)
            {
                pp = 0;
                //speechSynthesizer.SpeakAsyncCancelAll();
                speechSynthesizer.Pause();
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string opf = Path.Combine(baseDirectory, "Files", DocName);
                string playimages = Path.Combine(baseDirectory, "Files", "play.png");
                Image playImage = Image.FromFile(playimages);
                Bitmap resizedPlayImage = new Bitmap(playImage, new Size(22, 22));
                btnPlayPause.Image = resizedPlayImage;
            }
            //if (isSpeechPlaying)
            //{
            //    speechSynthesizer.SpeakAsyncCancelAll();
            //    isSpeechPlaying = false;
            //}
            //else
            //{
            //    isSpeechPlaying = true;
            //    ReadSlideText(slideIndex);
            //}
        }
        private void ReadSlideText(int slideIndex)
        {
            // Get the text from the current slide
            string slideText = GetSlideText(slideIndex);

            if (!string.IsNullOrEmpty(slideText))
            {
                // Speak the text of the slide
                speechSynthesizer.SpeakAsync(slideText);

                // Once speech is finished, go to the next slide
                speechSynthesizer.SpeakCompleted += (s, args) =>
                {
                    // Check if there are more slides
                    if (slideIndex < pptPresentation.Slides.Count)
                    {
                        slideIndex++; 
                        int currentSlideIndex = slideIndexs;
                        if (currentSlideIndex < pptPresentation.Slides.Count)
                        {
                            slideIndexs = slideIndexs + 1;
                            DisplaySlide();
                        }
                        ReadSlideText(slideIndex);  // Continue to the next slide
                    }
                    else
                    {
                        // All slides have been spoken
                        isSpeechPlaying = false; // Set flag to stop speech
                    }
                };
            }
        }
        private string GetSlideText(int slideIndex)
        {
            string slideText = "";
            try
            {
                var slide = pptPresentation.Slides[slideIndex];

                foreach (Microsoft.Office.Interop.PowerPoint.Shape shape in slide.Shapes)
                {
                    if (shape.HasTextFrame == MsoTriState.msoTrue && shape.TextFrame.HasText == MsoTriState.msoTrue)
                    {
                        slideText += shape.TextFrame.TextRange.Text + " ";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error extracting text: " + ex.Message);
            }
            return slideText;
        }

    }
}


//private void btnPrev_Click(object sender, EventArgs e)
//{
//    int currentSlideIndex = slideIndex;
//    if (currentSlideIndex > 1)
//    {
//        slideIndex = slideIndex - 1;
//        DisplaySlide();
//    }
//}

//private void btnNext_Click(object sender, EventArgs e)
//{

//    int currentSlideIndex = slideIndex;
//    if (currentSlideIndex < pptPresentation.Slides.Count)
//    {
//        slideIndex = slideIndex + 1;
//        DisplaySlide();
//    }
//}