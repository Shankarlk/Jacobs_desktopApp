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
using System.Drawing.Text;
using com.itextpdf.text.pdf;
using Spire.Pdf;
//using System.Reflection;
namespace JacobsDesktopApp
{
    public partial class OpenPdfFile : Form
    {
        public string DocName { get; set; }
        private System.Drawing.Image originalImage;
        private System.Drawing.Image backupImage;
        private bool isCursive = false;

        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
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
            lblExtractedTexts = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Brush Script MT", 18, FontStyle.Italic),
                ForeColor = Color.Black,
                Location = new Point(20, 20)
            };
        }

        private void OpenPdfFile_Load(object sender, EventArgs e)
        {
             

            try
            {
                if (!File.Exists(DocName))
                {
                    MessageBox.Show("PDF file not found:\n" + DocName);
                    return;
                }

                pdfDocument = new Spire.Pdf.PdfDocument();
                pdfDocument.LoadFromFile(DocName);
               // MessageBox.Show(pdfDocument.Pages.Count.ToString());
                if (DocName.Contains("Exercise"))
                    button7.Visible = true;
                else
                    button7.Visible = false;

                string playimages = System.IO.Path.Combine(
                         Application.StartupPath,
                         @"..\..\Files\play.png");

                playimages = System.IO.Path.GetFullPath(playimages);

                if (System.IO.File.Exists(playimages))
                {
                    System.Drawing.Image playImage = System.Drawing.Image.FromFile(playimages);

                    Bitmap resizedPlayImage = new Bitmap(playImage, 22, 22);

                    btnPlayPause.Image = resizedPlayImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading PDF:\n" + ex.Message);
            }

            DisplayPage(0);
        }
        


        private void DisplayPage(int pageIndex)
        {
            if (pdfDocument != null && pageIndex >= 0 && pageIndex < pdfDocument.Pages.Count)
            {
                using (System.IO.Stream pageStream = pdfDocument.SaveAsImage(pageIndex))
                {
                    System.Drawing.Image pageImage =
                        System.Drawing.Image.FromStream(pageStream);

                    originalImage = (System.Drawing.Image)pageImage.Clone();

                    if (backupImage != null)
                        backupImage.Dispose();

                    backupImage = (System.Drawing.Image)originalImage.Clone();

                    pictureBox1.Image = originalImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    lblExtractedTexts.Text = "";

                    currentPage = pageIndex;
                    isCursive = false;
                }
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }
        private void ReadPageText(int pageIndex)
        {
            if (pdfDocument != null && pageIndex >= 0 && pageIndex < pdfDocument.Pages.Count)
            {
                //string text = pdfDocument.Pages[pageIndex].ExtractText();
                //if (!string.IsNullOrWhiteSpace(text))
                //{
                //    //string cursiveText = ConvertToCursive(text);
                //    //DrawCursiveText(text); // Show joined cursive text in PictureBox
                //    ////lblExtractedTexts.Text = cursiveText;
                //    //lblExtractedTexts.Text = text;
                //    //lblExtractedTexts.Font = new System.Drawing.Font("Brush Script MT", 25, FontStyle.Italic);
                //    speechSynthesizer.SpeakAsync(text);  // Speak original text
                //}
                //else
                //{
                //    MessageBox.Show("No text found on this page.");
                //}
                return;
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
            LessonsList openPPTFile = new LessonsList();
            openPPTFile.LessonName = LessonName;
            openPPTFile.SubjectName = SubjectName;
            openPPTFile.ClassNo = ClassNo;
            openPPTFile.SchlName = SchlName;
            openPPTFile.Show();
            //englishFiles.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            zoomFactor = Math.Min(zoomFactor + 0.1f, 3.0f); // Limit max zoom to 3x
            ApplyZoom();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            zoomFactor = Math.Max(zoomFactor - 0.1f, 0.5f); // Limit min zoom to 0.5x
            ApplyZoom();
        }
        private void ApplyZoom()
        {
            if (backupImage == null) return; // Ensure we always zoom from the correct base image

            System.Drawing.Image sourceImage = isCursive ? pictureBox1.Image : backupImage;

            int newWidth = (int)(sourceImage.Width * zoomFactor);
            int newHeight = (int)(sourceImage.Height * zoomFactor);

            Bitmap zoomedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(zoomedImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(sourceImage, new System.Drawing.Rectangle(0, 0, newWidth, newHeight));
            }

            // Properly replace the image in pictureBox1
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }

            pictureBox1.Image = zoomedImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        int pp = 0;
        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (pp == 0)
            {
                pp = 1;
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                //string opf = System.IO.Path.Combine(baseDirectory, "Files", DocName);

                //string playimages = System.IO.Path.Combine(baseDirectory, "Files", "pause.png");
                string playimages = System.IO.Path.Combine( Application.StartupPath,@"..\..\Files\pause.png");

                playimages = System.IO.Path.GetFullPath(playimages);

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
                //string playimages = System.IO.Path.Combine(baseDirectory, "Files", "play.png");
                string playimages = System.IO.Path.Combine(Application.StartupPath,@"..\..\Files\play.png");
                playimages = System.IO.Path.GetFullPath(playimages);
                System.Drawing.Image playImage = System.Drawing.Image.FromFile(playimages);
                Bitmap resizedPlayImage = new Bitmap(playImage, 22, 22);
                //Bitmap resizedPlayImage = new Bitmap(playImage, 22, 22);
                btnPlayPause.Image = resizedPlayImage;
            }
        }

        private System.Drawing.Image DrawCursiveTextOnImage(System.Drawing.Image originalImage, string text)
        {
            if (originalImage == null || string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Error: Original image is null or text is empty.", "Debug");
                return originalImage;
            }

            try
            {
                Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);

                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    // Clear the background to white (or another color if needed)
                    graphics.Clear(Color.White);

                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    using (System.Drawing.Font cursiveFont = new System.Drawing.Font("Brush Script MT", 25, FontStyle.Italic))
                    using (SolidBrush textBrush = new SolidBrush(Color.Black))
                    {
                        RectangleF rect = new RectangleF(20, 20, newImage.Width - 40, newImage.Height - 40);
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Near,
                            LineAlignment = StringAlignment.Near,
                            FormatFlags = StringFormatFlags.LineLimit
                        };

                        graphics.DrawString(text, cursiveFont, textBrush, rect, format);
                    }
                }

                return newImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error drawing text: {ex.Message}", "Error");
                return originalImage;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplyZoom();
            if (backupImage == null || string.IsNullOrWhiteSpace(lblExtractedTexts.Text))
            {
                MessageBox.Show("No text available to convert.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isCursive)
            {
                // Restore from backup instead of originalImage
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose(); // Free memory before replacing image
                    pictureBox1.Image = null;    // Ensure it's cleared
                }

                pictureBox1.Image = (System.Drawing.Image)backupImage.Clone();
                isCursive = false;
                button7.Text = "Joining Letters";
            }
            else
            {
                // Generate the cursive image
                System.Drawing.Image cursiveImage = DrawCursiveTextOnImage(backupImage, lblExtractedTexts.Text);
                if (cursiveImage != null)
                {
                    // Properly replace the image
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }

                    pictureBox1.Image = cursiveImage;
                    isCursive = true;
                    button7.Text = "Normal Text";
                }
                else
                {
                    MessageBox.Show("Failed to generate cursive text image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}


/*
        private void DrawCursiveText(string text)
        {
            if (pictureBox1.Width == 0 || pictureBox1.Height == 0)
                return;

            // Create a new bitmap
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);  // Background color
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Use a script font that supports joined letters
                using (System.Drawing.Font cursiveFont = new System.Drawing.Font("Brush Script MT", 25, FontStyle.Italic))
                using (SolidBrush textBrush = new SolidBrush(Color.Black))
                {
                    graphics.DrawString(text, cursiveFont, textBrush, new PointF(10, 10));
                }
            }

            pictureBox1.Image = bitmap;
        }



        private string ConvertToCursive(string input)
        {
            Dictionary<char, string> cursiveMap = new Dictionary<char, string>
    {
        {'A', "𝒜"}, {'B', "ℬ"}, {'C', "𝒞"}, {'D', "𝒟"}, {'E', "ℰ"}, {'F', "ℱ"},
        {'G', "𝒢"}, {'H', "ℋ"}, {'I', "ℐ"}, {'J', "𝒥"}, {'K', "𝒦"}, {'L', "ℒ"},
        {'M', "ℳ"}, {'N', "𝒩"}, {'O', "𝒪"}, {'P', "𝒫"}, {'Q', "𝒬"}, {'R', "ℛ"},
        {'S', "𝒮"}, {'T', "𝒯"}, {'U', "𝒰"}, {'V', "𝒱"}, {'W', "𝒲"}, {'X', "𝒳"},
        {'Y', "𝒴"}, {'Z', "𝒵"}, {'a', "𝒶"}, {'b', "𝒷"}, {'c', "𝒸"}, {'d', "𝒹"},
        {'e', "ℯ"}, {'f', "𝒻"}, {'g', "ℊ"}, {'h', "𝒽"}, {'i', "𝒾"}, {'j', "𝒿"},
        {'k', "𝓀"}, {'l', "𝓁"}, {'m', "𝓂"}, {'n', "𝓃"}, {'o', "ℴ"}, {'p', "𝓅"},
        {'q', "𝓆"}, {'r', "𝓇"}, {'s', "𝓈"}, {'t', "𝓉"}, {'u', "𝓊"}, {'v', "𝓋"},
        {'w', "𝓌"}, {'x', "𝓍"}, {'y', "𝓎"}, {'z', "𝓏"}
    };

            return string.Concat(input.Select(c => cursiveMap.ContainsKey(c) ? cursiveMap[c] : c.ToString()));
        }


 */