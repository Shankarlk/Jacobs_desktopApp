using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    public partial class OpenMp4 : Form
    {
        public string DocName { get; set; }
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
        public OpenMp4()
        {
            InitializeComponent();
        }

        private void OpenMp4_Load(object sender, EventArgs e)
        {
            //button1.Visible = false;
            string tempPath = Path.Combine(Path.GetTempPath(), DocName);

            // Extract MP4 from embedded resources
            ExtractEmbeddedResource("JacobsDesktopApp.Files." + DocName, tempPath);

            // Check if extraction was successful
            if (!File.Exists(tempPath))
            {
                MessageBox.Show("Failed to extract MP4 file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Play the extracted MP4 file
            axWindowsMediaPlayer1.URL = tempPath;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void ExtractEmbeddedResource(string resourceName, string outputPath)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                    {
                        MessageBox.Show("Resource not found: " + resourceName);
                        return;
                    }

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error extracting file: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LessonsList LessonsList = new LessonsList();
            LessonsList.LessonName = LessonName;
            LessonsList.SubjectName = SubjectName;
            LessonsList.ClassNo = ClassNo;
            LessonsList.SchlName = SchlName;
            LessonsList.Show();
            this.Hide();
        }
    }
}
