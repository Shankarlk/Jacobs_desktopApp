using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing.Drawing2D;
using System.Speech.Synthesis;
namespace JacobsDesktopApp
{
    public partial class OpenPdfFile : Form
    {
        public string DocName { get; set; }
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        private float zoomFactor = 0.5f;
        PdfReader reader;
        //int totalPages = 0;
        private Spire.Pdf.PdfDocument pdfDocument;
        private int currentPage = 0;
        private SpeechSynthesizer speechSynthesizer;
        private bool isSpeechPlaying = false;
        public OpenPdfFile()
        {
            InitializeComponent();
            speechSynthesizer = new SpeechSynthesizer();
        }

        private void OpenPdfFile_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(baseDirectory, "Files", DocName);

            string playimages = System.IO.Path.Combine(baseDirectory, "Files", "play.png");
            System.Drawing.Image playImage = System.Drawing.Image.FromFile(playimages);
            Bitmap resizedPlayImage = new Bitmap(playImage, new Size(22, 22));
            btnPlayPause.Image = resizedPlayImage;
            //// Load the PDF
            //pdfDocument = new Spire.Pdf.PdfDocument();
            //pdfDocument.LoadFromFile(filePath); 
            try
            {
                pdfDocument = new Spire.Pdf.PdfDocument();
                pdfDocument.LoadFromFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF: {ex.Message}", "Error");
            }

            DisplayPage(0);
        }
        private void DisplayPage(int pageIndex)
        {
            if (pdfDocument != null && pageIndex >= 0 && pageIndex < pdfDocument.Pages.Count)
            {
                using (var pageStream = pdfDocument.SaveAsImage(pageIndex))
                {
                    System.Drawing.Image pageImage = System.Drawing.Image.FromStream(pageStream);

                    pictureBox1.Image = pageImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    RenderZoomedImage(pageImage);
                }
                currentPage = pageIndex;
            }
        }
        private void ReadPageText(int pageIndex)
        {
            if (pdfDocument != null && pageIndex >= 0 && pageIndex < pdfDocument.Pages.Count)
            {
                string text = pdfDocument.Pages[pageIndex].ExtractText();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    speechSynthesizer.SpeakAsync(text);
                }
                else
                {
                    MessageBox.Show("No text found on this page.");
                }
            }
        }


        private void SpeechSynthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (currentPage < pdfDocument.Pages.Count - 1)
            {
                currentPage++;
                DisplayPage(currentPage);
                ReadPageText(currentPage);
            }
            else
            {
                isSpeechPlaying = false;
            }
        }

        private void RenderZoomedImage(System.Drawing.Image originalImage)
        {
            int newWidth = (int)(originalImage.Width * zoomFactor);
            int newHeight = (int)(originalImage.Height * zoomFactor);

            Bitmap zoomedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(zoomedImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, newWidth, newHeight));
            }

            pictureBox1.Image = zoomedImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                DisplayPage(currentPage - 1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (currentPage < pdfDocument.Pages.Count - 1)
            {
                DisplayPage(currentPage + 1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            speechSynthesizer.SpeakAsyncCancelAll();
            EnglishFiles englishFiles = new EnglishFiles();
            englishFiles.ClassNo = ClassNo;
            englishFiles.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            zoomFactor += 0.1f;
            DisplayPage(currentPage);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zoomFactor = Math.Max(zoomFactor - 0.1f, 0.1f);
            DisplayPage(currentPage);
        }

        int pp = 0;
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (pp == 0)
            {
                pp = 1;
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string opf = System.IO.Path.Combine(baseDirectory, "Files", DocName);
                string playimages = System.IO.Path.Combine(baseDirectory, "Files", "pause.png");
                System.Drawing.Image playImage = System.Drawing.Image.FromFile(playimages);
                Bitmap resizedPlayImage = new Bitmap(playImage, new Size(22, 22));
                btnPlayPause.Image = resizedPlayImage;
                ReadPageText(currentPage);
                speechSynthesizer.Resume();
            }
            else if (pp == 1)
            {
                pp = 0;
                speechSynthesizer.Pause();
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string opf = System.IO.Path.Combine(baseDirectory, "Files", DocName);
                string playimages = System.IO.Path.Combine(baseDirectory, "Files", "play.png");
                System.Drawing.Image playImage = System.Drawing.Image.FromFile(playimages);
                Bitmap resizedPlayImage = new Bitmap(playImage, new Size(22, 22));
                btnPlayPause.Image = resizedPlayImage;
            }
        }
    }
}
