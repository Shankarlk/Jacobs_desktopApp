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
    }
}
