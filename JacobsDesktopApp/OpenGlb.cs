using Microsoft.Web.WebView2.Core;
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
    public partial class OpenGlb : Form
    {
        public string DocName { get; set; }
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
        public OpenGlb()
        {
            InitializeComponent();
        }

        private async void OpenGlb_Load(object sender, EventArgs e)
        {
            // --- Phase 1: Extract the GLB file to the temp directory ---
            string glbFileName = DocName; // e.g., "A_human_brain_3d.glb"
            string tempDir = Path.GetTempPath(); // The root folder where the file will be
            string glbTempPath = Path.Combine(tempDir, glbFileName);

            // Extract the GLB file
            ExtractEmbeddedResource("JacobsDesktopApp.Files." + glbFileName, glbTempPath);

            if (!File.Exists(glbTempPath))
            {
                MessageBox.Show("Failed to extract GLB file.", "Error");
                return;
            }

            // --- Phase 2: Setup WebView2 Environment and Virtual Host Mapping ---

            // Define the virtual host details
            const string virtualHostName = "model.local";

            // 1. Initialize WebView2
            await webView21.EnsureCoreWebView2Async(null);

            // 2. Set the Virtual Host Mapping
            // This tells WebView2 to serve files from the 'tempDir' (C:\Users\...\AppData\Local\Temp\)
            // under the domain 'https://model.local/'
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping(
                virtualHostName,
                tempDir,
                CoreWebView2HostResourceAccessKind.Allow
            );

            // --- Phase 3: Read, Modify, and Load the HTML String ---

            string htmlResourceName = "JacobsDesktopApp.AppFiles.GlbViewer.html";
            Assembly assembly = Assembly.GetExecutingAssembly();
            string htmlContent;

            try
            {
                using (Stream resourceStream = assembly.GetManifestResourceStream(htmlResourceName))
                // ... (Error handling and reading stream remain the same) ...
                {
                    if (resourceStream == null)
                    {
                        MessageBox.Show($"HTML Resource not found: {htmlResourceName}", "Error");
                        return;
                    }
                    using (StreamReader reader = new StreamReader(resourceStream, Encoding.UTF8))
                    {
                        htmlContent = reader.ReadToEnd();
                    }
                }

                // The URI is now the VIRTUAL HTTPS path to the file!
                string glbVirtualUri = $"https://{virtualHostName}/{glbFileName}";

                // Inject the VIRTUAL URI into the HTML placeholder
                string finalHtml = htmlContent.Replace("###GLB_FILE_PATH###", glbVirtualUri);

                // Load the HTML content directly
                webView21.CoreWebView2.NavigateToString(finalHtml);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading 3D model: {ex.Message}", "Fatal Error");
            }
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
                // Handle or log the error
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
