using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    public partial class OpenHtml : Form
    {
        public string DocName { get; set; }
        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }
        public OpenHtml()
        {
            InitializeComponent();
        }

        private async void OpenHtml_Load(object sender, EventArgs e)
        {
            Theme.ApplyViewerChrome(this, LessonName, button1_Click, groupBox1);
            // 1. Determine the path where the HTML file will be temporarily extracted
         //string tempPath = Path.Combine(Path.GetTempPath(), DocName);

            // 2. Use your existing method to extract the HTML file from embedded resources
            //ExtractEmbeddedResource("JacobsDesktopApp.Files." + DocName, tempPath);

            // 3. Check if extraction was successful
            //if (!File.Exists(tempPath))
            //{
            //    MessageBox.Show("Failed to extract HTML file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            string tempPath = DocName;

            if (!File.Exists(tempPath))
            {
                MessageBox.Show("HTML file not found:\n" + tempPath);
                return;
            }

            try
            {
                await webView21.EnsureCoreWebView2Async(null);

                string fileUri = new Uri(tempPath).AbsoluteUri;

                webView21.CoreWebView2.Navigate(fileUri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // 4. Load the extracted HTML file into the WebBrowser control
            // *** NEW: Initialize the CoreWebView2 component ***
            try
            {
                // This call ensures the browser environment is initialized before navigation
                await webView21.EnsureCoreWebView2Async(null);

                // Check if initialization was successful
                if (webView21.CoreWebView2 != null)
                {
                    // 3. Convert the local file path to a Uri
                    string fileUri = new Uri(tempPath).AbsoluteUri;

                    // 4. Load the local HTML file using CoreWebView2.Navigate()
                    webView21.CoreWebView2.Navigate(fileUri);
                }
                else
                {
                    MessageBox.Show("WebView2 failed to initialize.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (WebView2RuntimeNotFoundException)
            {
                MessageBox.Show("The Edge WebView2 Runtime is not installed on this system. Please install it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during WebView2 loading: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //LessonsList LessonsList = new LessonsList();
            //LessonsList.LessonName = LessonName;
            //LessonsList.SubjectName = SubjectName;
            //LessonsList.ClassNo = ClassNo;
            //LessonsList.SchlName = SchlName;
            //LessonsList.Show();
            //this.Hide();
            LessonsList frm = new LessonsList();

            frm.ClassNo = ClassNo;
            frm.SubjectName = SubjectName;
            frm.LessonName = LessonName;
            frm.SchlName = SchlName;

            frm.Show();
            this.Hide();

        }
    }
}
