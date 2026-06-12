using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace JacobsDesktopApp
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string SchoolName = "School Name";
        public string SchlName { get; set; }
        public string Board { get; set; }
        private void Home_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            btnLogout.Visible = false;

            lblSchl.Text = "Welcome to Jacob Educare";

            
            groupBox2.Controls.Clear();

            groupBox2.BackColor = Color.FromArgb(245, 248, 252);
            groupBox2.Text = "";

            groupBox2.Width = 700;
            groupBox2.Height = 700;

            groupBox2.Left =
                (this.ClientSize.Width - groupBox2.Width) / 2;

            groupBox2.Top = 120;
            foreach (Control ctrl in groupBox2.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.MouseEnter += Button_MouseEnter;
                    btn.MouseLeave += Button_MouseLeave;
                }
            }
            try
            {
                 string encryptedFilePath = Path.Combine(baseDirectory, "LicenseKey", "License_protected.txt");
                //string encryptedFilePath = Path.Combine(baseDirectory, @"..\..\LicenseKey", "License_protected.txt");
                string password = "SmsTeacher@123"; // Your encryption password To open pdf 
              
                string decryptedFilePath = DecryptTxtWithPassword(encryptedFilePath, password);

                if (decryptedFilePath == null)
                {
                    MessageBox.Show("Failed to decrypt the TXT file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                // Read the decrypted TXT content
                string txtContent = File.ReadAllText(decryptedFilePath);
                 

                // Validate the license
                if (!ValidateLicense(txtContent))
                {
                    MessageBox.Show("Invalid or expired license. The application will now close.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                if (File.Exists(decryptedFilePath))
                {
                    File.Delete(decryptedFilePath);
                }
                LoadClassCards();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during license validation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit(); // Close the application if there's an error
            }


        }

       

        private void LoadClassCards()
        {
            int cardWidth = 170;
            int cardHeight = 140;

            int startX = 80;
            int startY = 40;

            int gapX = 30;
            int gapY = 25;

            int classNo = 1;

            for (int row = 0; row < 4; row++)
            {
                int cardsInRow = (row == 3) ? 1 : 3;

                int rowStartX = startX;

                if (row == 3)
                    rowStartX = startX + cardWidth + gapX;

                for (int col = 0; col < cardsInRow; col++)
                {
                    Panel card = new Panel();
                    card.Width = cardWidth;
                    card.Height = cardHeight;
                    card.BackColor = Color.White;
                    card.Cursor = Cursors.Hand;

                    card.Location = new Point(
                        rowStartX + (col * (cardWidth + gapX)),
                        startY + (row * (cardHeight + gapY)));

                    card.Tag = classNo;

                    card.BackColor = Color.White;
                    card.Padding = new Padding(10);

                    card.BorderStyle = BorderStyle.None;

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(60, 60);
                    pic.Location = new Point(55, 20);

                    pic.SizeMode = PictureBoxSizeMode.Zoom;

                    pic.Image = Properties.Resources.bookicon; // Add your blue book icon

                    pic.Tag = classNo;

                    Label lbl = new Label();
                    lbl.Text = "Class " + classNo;
                    lbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);

                    lbl.ForeColor = Color.FromArgb(30, 58, 138);

                    lbl.Width = cardWidth;
                    lbl.Height = 30;

                    lbl.Location = new Point(0, 90);

                    lbl.TextAlign = ContentAlignment.MiddleCenter;

                    lbl.Tag = classNo;

                    card.Controls.Add(pic);
                    card.Controls.Add(lbl);

                    card.Click += ClassCard_Click;
                    pic.Click += ClassCard_Click;
                    lbl.Click += ClassCard_Click;

                    groupBox2.Controls.Add(card);

                    classNo++;

                    if (classNo > 10)
                        return;
                }
            }
        }

        private void ClassCard_Click(object sender, EventArgs e)
        {
            int classNo = 0;

            if (sender is Panel)
                classNo = Convert.ToInt32(((Panel)sender).Tag);

            else if (sender is PictureBox)
                classNo = Convert.ToInt32(((PictureBox)sender).Tag);

            else if (sender is Label)
                classNo = Convert.ToInt32(((Label)sender).Tag);

            Subjects frm = new Subjects();

            frm.ClassNo = classNo;
            frm.SchlName = SchoolName;

            frm.Show();

            this.Hide();
        }

        private void ReplaceLicenseFile()
        {
            LicenseKeyReplacement subjects = new LicenseKeyReplacement();
            subjects.SchlName = SchoolName;
            subjects.Show();
            this.Close();
        }
        public static string DecryptPdfWithPassword(string encryptedFilePath, string password)
        {
            try
            {
                // Convert the password string to byte array
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

                // Create a temporary file for the decrypted PDF
                string decryptedFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(encryptedFilePath));

                // Try opening the encrypted PDF using the password
                using (PdfReader reader = new PdfReader(encryptedFilePath, new ReaderProperties().SetPassword(passwordBytes)))
                {
                    using (PdfWriter writer = new PdfWriter(decryptedFilePath))
                    {
                        using (PdfDocument pdfDoc = new PdfDocument(reader, writer))
                        {
                            // No need to add a new paragraph; just copy the content
                        }
                    }
                }

                return decryptedFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error decrypting PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static string ReadDecryptedPdf(string decryptedFilePath)
        {
            using (PdfReader reader = new PdfReader(decryptedFilePath))
            {
                using (PdfDocument pdfDoc = new PdfDocument(reader))
                {
                    StringBuilder text = new StringBuilder();
                    int numberOfPages = pdfDoc.GetNumberOfPages();
                    for (int i = 1; i <= numberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
                    }
                    return text.ToString();
                }
            }
        }
        //bool ValidateLicense(string pdfContent)
        //{
        //    string pattern = @"License Key:\s*(?<key>[A-Z0-9\-]+)\s*Expiration Date:\s*(?<date>\d{4}-\d{2}-\d{2})";

        //    Match match = Regex.Match(pdfContent, pattern);

        //    if (match.Success)
        //    {
        //        string licenseKey = match.Groups["key"].Value;
        //        DateTime expirationDate = DateTime.Parse(match.Groups["date"].Value);
        //        if (DateTime.Now <= expirationDate)
        //        {
        //            int monthsUntilExpiration = ((expirationDate.Year - DateTime.Now.Year) * 12) + expirationDate.Month - DateTime.Now.Month;

        //            if (monthsUntilExpiration <= 1) // Change this number to set the threshold (e.g., 1 month)
        //            {
        //                int daysUntilExpiration = (expirationDate - DateTime.Now).Days;
        //                MessageBox.Show($"Warning: Your license will expire in {daysUntilExpiration} days!",
        //                   "License Expiration Warning",
        //                   MessageBoxButtons.OK,
        //                   MessageBoxIcon.Warning);
        //            }
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public static string DecryptTxtWithPassword(string encryptedFilePath, string password)
        {
            try
            {
                 

                byte[] key = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));

                // Read the encrypted file
                byte[] fileBytes = File.ReadAllBytes(encryptedFilePath);

                // Extract IV (first 16 bytes)
                byte[] IV = new byte[16];
                byte[] encryptedBytes = new byte[fileBytes.Length - 16];

                Array.Copy(fileBytes, 0, IV, 0, 16); // Copy IV
                Array.Copy(fileBytes, 16, encryptedBytes, 0, encryptedBytes.Length); // Copy encrypted data

                using (Aes aes = Aes.Create())
                {
                    aes.Key = key;
                    aes.IV = IV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7; 

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                        // Create a temporary file for the decrypted content
                        string tempFilePath = Path.Combine(Path.GetTempPath(), "Decrypted_License.txt");
                        File.WriteAllText(tempFilePath, decryptedText);
                        return tempFilePath;
                    }
                }
            }
            catch (CryptographicException ce)
            {
                MessageBox.Show($"Decryption failed: Invalid password or corrupted file.\n{ce.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error decrypting TXT: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        public static bool ValidateLicense(string txtContent)
        {
            string pattern = @"Expiration Date:\s*(?<date>\d{4}-\d{2}-\d{2})";

            Match match = Regex.Match(txtContent, pattern);

            if (match.Success)
            {
                string licenseKey = match.Groups["key"].Value;
                DateTime expirationDate = DateTime.Parse(match.Groups["date"].Value);

                if (DateTime.Now <= expirationDate)
                {
                    int daysUntilExpiration = (expirationDate - DateTime.Now).Days;

                    if (daysUntilExpiration <= 15) // Threshold for expiration warning
                    {
                        MessageBox.Show($"Warning: Your license will expire in {daysUntilExpiration} days!",
                            "License Expiration Warning",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    return true;
                }
            }
            return false;
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.RoyalBlue;
            btn.ForeColor = Color.White;
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.RoyalBlue;
        }
        private void cLass1_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 1;
            subjects.Show();
            this.Hide();
        }

        private void class2_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 2;
            subjects.Show();
            this.Hide();
        }

        private void class3_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 3;
            subjects.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 4;
            subjects.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 5;
            subjects.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 6;
            subjects.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 7;
            subjects.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 8;
            subjects.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 9;
            subjects.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Subjects subjects = new Subjects();
            subjects.SchlName = SchoolName;
            subjects.ClassNo = 10;
            subjects.Show();
            this.Hide();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {
            btnLogout.Visible = true;
        }

        private void labelArrow_Click(object sender, EventArgs e)
        {
            btnLogout.Visible = true;
        }

        private void Home_Click(object sender, EventArgs e)
        {
            Point mousePos = this.PointToClient(Cursor.Position);

            // Check if the click is outside labelArrow and btnLogout
            if (!labelArrow.Bounds.Contains(mousePos) && !btnLogout.Bounds.Contains(mousePos))
            {
                btnLogout.Visible = false;
            }

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 home = new Form1();
            home.Show();
            this.Hide();
        }
    }
}
