using AxWMPLib;
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
    public partial class FileSecurity : Form
    {
        public FileSecurity()
        {
            InitializeComponent();
            //button2.Visible = false;
        }

        public static void EncryptFile(
        string inputFile,
        string outputFile,
        string password)
        {
            byte[] key = new SHA256Managed()
                .ComputeHash(
                    Encoding.UTF8.GetBytes(password));

            byte[] fileBytes =
                File.ReadAllBytes(inputFile);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (ICryptoTransform encryptor =
                    aes.CreateEncryptor())
                {
                    byte[] encryptedBytes =
                        encryptor.TransformFinalBlock(
                            fileBytes,
                            0,
                            fileBytes.Length);

                    using (FileStream fs =
                        new FileStream(
                            outputFile,
                            FileMode.Create))
                    {
                        fs.Write(aes.IV, 0, aes.IV.Length);
                        fs.Write(
                            encryptedBytes,
                            0,
                            encryptedBytes.Length);
                    }
                }
            }
        }
        public static void DecryptFile(
        string inputFile,
        string outputFile,
        string password)
        {
            byte[] key = new SHA256Managed()
                .ComputeHash(
                    Encoding.UTF8.GetBytes(password));

            byte[] fileBytes =
                File.ReadAllBytes(inputFile);

            byte[] iv = new byte[16];

            Array.Copy(fileBytes, 0, iv, 0, 16);

            byte[] encryptedBytes =
                new byte[fileBytes.Length - 16];

            Array.Copy(
                fileBytes,
                16,
                encryptedBytes,
                0,
                encryptedBytes.Length);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                using (ICryptoTransform decryptor =
                    aes.CreateDecryptor())
                {
                    byte[] decryptedBytes =
                        decryptor.TransformFinalBlock(
                            encryptedBytes,
                            0,
                            encryptedBytes.Length);

                    File.WriteAllBytes(
                        outputFile,
                        decryptedBytes);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                string rootFolder = Path.Combine(Application.StartupPath, @"..\..\Files");
                

                string[] lessonFolders = Directory.GetDirectories(rootFolder,"Lesson *",SearchOption.AllDirectories);

                foreach (string lessonFolder in lessonFolders)
                {
                    foreach (string file in Directory.GetFiles(lessonFolder))
                    {
                        if (file.EndsWith(".enc"))
                        { 
                            continue;
                        }
                        
                        string extension = Path.GetExtension(file).ToLower();

                        if (extension == ".pdf" ||extension == ".pptx" || extension == ".mp4" ||extension == ".glb" ||extension == ".html")
                        {
                            string encryptedFile = file + ".enc";

                            if (!File.Exists(encryptedFile))
                            {
                                EncryptFile(file,encryptedFile,"SmsTeacher@123");

                               
                            }
                        }
                    }
                }

                MessageBox.Show("All new files encrypted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string encryptedFile =
                 @"E:\JacobDesktop1\JacobDesktop\JacobsDesktopApp\Files\Class 1\English\Lesson 1\English_Class1.pdf.enc";

                string outputFile =
                    @"E:\JacobDesktop1\JacobDesktop\JacobsDesktopApp\Files\Class 1\English\Lesson 1\English_Class1_Test.pdf";

                DecryptFile(
                    encryptedFile,
                     outputFile,
                    "SmsTeacher@123");

                MessageBox.Show(
                    "File Decrypted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string DecryptToTemp(string encryptedFile,string password)
        {
            string extension = Path.GetExtension( Path.GetFileNameWithoutExtension(encryptedFile));

            string tempFolder =Path.Combine(Path.GetTempPath(),"JacobsDesktopApp");

            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            string tempFile =Path.Combine(tempFolder,Guid.NewGuid().ToString() + extension);

            DecryptFile(encryptedFile,tempFile,password);

            return tempFile;
        }




    }
}
