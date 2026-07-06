using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace JacobsDesktopApp
{
    public partial class LicenseKeyReplacement : Form
    {
        public string SchlName { get; set; }
        public LicenseKeyReplacement()
        {
            InitializeComponent();
        }

        private void LicenseKeyReplacement_Load(object sender, EventArgs e)
        {
            lblSchl.Text = "              " + SchlName + "          ";
            btnLogout.Visible = false;
            lblSchl.Visible = false;
            button2.Visible = false; // For Encription make it true 
            button3.Visible = false;   

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select a TXT File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string destinationPath = Path.Combine(outputDirectory, "LicenseKey", "License_protected.txt");

                    string licenseFolder = Path.Combine(outputDirectory, "LicenseKey");
                    if (!Directory.Exists(licenseFolder))
                    {
                        Directory.CreateDirectory(licenseFolder);
                    }
                    File.Copy(filePath, destinationPath, true);
                    MessageBox.Show("File uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }

        }




         

        private void button1_Click_1(object sender, EventArgs e) 
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.Title = "Select a TXT File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string destinationPath = Path.Combine(outputDirectory, "LicenseKey", "License_protected.txt");

                    string licenseFolder = Path.Combine(outputDirectory, "LicenseKey");
                    if (!Directory.Exists(licenseFolder))
                    {
                        Directory.CreateDirectory(licenseFolder);
                    }
                    File.Copy(filePath, destinationPath, true);
                    MessageBox.Show("File uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text files (*.txt)|*.txt";
                    openFileDialog.Title = "Select License TXT File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        string licenseFolder = Path.Combine(
                            Application.StartupPath,
                            "LicenseKey");

                        if (!Directory.Exists(licenseFolder))
                        {
                            Directory.CreateDirectory(licenseFolder);
                        }

                        string destinationPath = Path.Combine(
                            licenseFolder,
                            "License_protected.txt");

                        EncryptTxtWithPassword(
                            filePath,
                            destinationPath,
                            "SmsTeacher@123");

                        MessageBox.Show(
                            "License Encrypted Successfully",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void EncryptTxtWithPassword(
        string inputFilePath,
        string outputFilePath,
        string password)
        {
            byte[] key = new SHA256Managed()
                .ComputeHash(Encoding.UTF8.GetBytes(password));

            byte[] plainBytes = File.ReadAllBytes(inputFilePath);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor =
                    aes.CreateEncryptor())
                {
                    byte[] encryptedBytes =
                        encryptor.TransformFinalBlock(
                            plainBytes,
                            0,
                            plainBytes.Length);

                    using (FileStream fs =
                        new FileStream(outputFilePath,
                        FileMode.Create))
                    {
                        fs.Write(aes.IV, 0, aes.IV.Length);
                        fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    }
                }
            }
        }
        //this for relese working code
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string basePath = Application.StartupPath;

                string sourcePdf = Path.Combine(
                    basePath,
                    "LicenseKey",
                    "License_protected.pdf");

                string protectedPdf = Path.Combine(
                    basePath,
                    "LicenseKey",
                    "License_protected_new.pdf");

                if (!File.Exists(sourcePdf))
                {
                    MessageBox.Show("PDF File Not Found:\n" + sourcePdf);
                    return;
                }

                WriterProperties props = new WriterProperties();

                props.SetStandardEncryption(
                    Encoding.UTF8.GetBytes("SmsTeacher@123"),
                    Encoding.UTF8.GetBytes("OwnerPassword123"),
                    EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_128);

                PdfDocument pdfDoc = new PdfDocument(
                    new PdfReader(sourcePdf),
                    new PdfWriter(protectedPdf, props));

                pdfDoc.Close();

                MessageBox.Show(
                    "Password Protected PDF Created Successfully\n\n" +
                    protectedPdf,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FileSecurity frm = new FileSecurity();
            frm.Show();
        }


        //private void button3_Click(object sender, EventArgs e)  -- this code is work for project folder
        //{
        //    try
        //    {
        //        string projectPath = Directory.GetParent(Application.StartupPath)
        //                                      .Parent
        //                                      .Parent
        //                                      .FullName;

        //        string sourcePdf = Path.Combine(
        //            projectPath,
        //            "LicenseKey",
        //            "License_protected.pdf");

        //        string protectedPdf = Path.Combine(
        //            projectPath,
        //            "LicenseKey",
        //            "License_protected_new.pdf");

        //        if (!File.Exists(sourcePdf))
        //        {
        //            MessageBox.Show("PDF File Not Found:\n" + sourcePdf);
        //            return;
        //        }

        //        WriterProperties props = new WriterProperties();

        //        props.SetStandardEncryption(
        //            Encoding.UTF8.GetBytes("SmsTeacher@123"),      // Open Password
        //            Encoding.UTF8.GetBytes("OwnerPassword123"),    // Owner Password
        //            EncryptionConstants.ALLOW_PRINTING,
        //            EncryptionConstants.ENCRYPTION_AES_128);

        //        PdfDocument pdfDoc = new PdfDocument(
        //            new PdfReader(sourcePdf),
        //            new PdfWriter(protectedPdf, props));

        //        pdfDoc.Close();

        //        MessageBox.Show(
        //            "Password Protected PDF Created Successfully\n\n" +
        //            protectedPdf,
        //            "Success",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            ex.Message,
        //            "Error",
        //            MessageBoxButtons.OK,
        //            MessageBoxIcon.Error);
        //    }
        //}
    }
}
