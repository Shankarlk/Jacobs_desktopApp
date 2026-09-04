using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json.Linq;

namespace JacobsDesktopApp
{
    public partial class OpenPdfFile : Form
    {
        public string DocName { get; set; }

        public int ClassNo { get; set; }
        public string SchlName { get; set; }
        public string LessonName { get; set; }
        public string SubjectName { get; set; }

        private const string VirtualHost = "jacobspdf.local";

        private readonly AudioPlayer _player = new AudioPlayer();
        private string _workDir;

        // Read-aloud state
        private List<string> _sentences;
        private int _curSentence;
        private bool _reading;
        private bool _paused;
        private bool _preparing;
        private string _curWav;
        private Task<string> _prefetchTask;
        private int _prefetchIndex = -1;
        private Timer _pollTimer;

        public OpenPdfFile()
        {
            InitializeComponent();
        }

        private async void OpenPdfFile_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DocName) || !File.Exists(DocName))
            {
                MessageBox.Show("PDF file not found:\n" + DocName, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblTitle.Text = string.IsNullOrEmpty(LessonName) ? "Lesson Document" : LessonName;
            btnRead.Visible = PiperTts.IsAvailable;
            LayoutHeaderButtons();
            topBar.SizeChanged += (s, ev) => LayoutHeaderButtons();

            try
            {
                string userDataFolder = Path.Combine(Path.GetTempPath(), "JacobsDesktopApp", "WebView2");
                CoreWebView2Environment env =
                    await CoreWebView2Environment.CreateAsync(null, userDataFolder);
                if (IsClosingOrGone()) return;

                await webView1.EnsureCoreWebView2Async(env);
                if (IsClosingOrGone() || webView1.CoreWebView2 == null) return;

                webView1.CoreWebView2.WebMessageReceived += OnWebMessage;

                // Assemble a self-contained folder (viewer assets + this PDF) and
                // serve it over a virtual https host so PDF.js and its worker load
                // without file:// restrictions.
                string viewerSrc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PdfViewer");
                _workDir = Path.Combine(Path.GetTempPath(), "JacobsDesktopApp", "pdfview", Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(_workDir);
                File.Copy(Path.Combine(viewerSrc, "viewer.html"), Path.Combine(_workDir, "viewer.html"), true);
                File.Copy(Path.Combine(viewerSrc, "pdf.min.js"), Path.Combine(_workDir, "pdf.min.js"), true);
                File.Copy(Path.Combine(viewerSrc, "pdf.worker.min.js"), Path.Combine(_workDir, "pdf.worker.min.js"), true);
                File.Copy(DocName, Path.Combine(_workDir, "doc.pdf"), true);

                webView1.CoreWebView2.SetVirtualHostNameToFolderMapping(
                    VirtualHost, _workDir, CoreWebView2HostResourceAccessKind.Allow);

                webView1.CoreWebView2.Navigate("https://" + VirtualHost + "/viewer.html?file=doc.pdf");
            }
            catch (WebView2RuntimeNotFoundException)
            {
                MessageBox.Show(
                    "The Microsoft Edge WebView2 Runtime is not installed on this system.\n" +
                    "Please install it to view PDF documents.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                if (IsClosingOrGone() || IsBenignShutdown(ex)) return;
                MessageBox.Show("Error loading PDF:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnWebMessage(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string json;
            try { json = e.TryGetWebMessageAsString(); }
            catch { return; }
            if (string.IsNullOrEmpty(json)) return;

            try
            {
                JObject o = JObject.Parse(json);
                if ((string)o["type"] == "ready")
                    _sentences = o["sentences"].ToObject<List<string>>();
            }
            catch { }
        }

        private void PostToJs(string json)
        {
            try
            {
                if (webView1 != null && webView1.CoreWebView2 != null)
                    webView1.CoreWebView2.PostWebMessageAsString(json);
            }
            catch { }
        }

        // ===== Read-aloud (bundled Piper neural voice + PDF.js word highlighting) =====

        private void LayoutHeaderButtons()
        {
            const int pad = 15, gap = 8, top = 7;
            btnRead.Top = top;
            btnStop.Top = top;
            btnRead.Left = topBar.ClientSize.Width - pad - btnRead.Width;
            btnStop.Left = btnRead.Left - gap - btnStop.Width;
            btnRead.BringToFront();
            btnStop.BringToFront();
        }

        private async void btnRead_Click(object sender, EventArgs e)
        {
            if (_preparing) return;

            if (_reading)
            {
                if (!_paused)
                {
                    _player.Pause();
                    PostToJs("{\"type\":\"pause\"}");
                    _paused = true;
                    btnRead.Text = "Resume";
                }
                else
                {
                    _player.Resume();
                    PostToJs("{\"type\":\"resume\"}");
                    _paused = false;
                    btnRead.Text = "Pause";
                }
                return;
            }

            if (_sentences == null || _sentences.Count == 0)
            {
                MessageBox.Show(
                    "The document is still loading, or has no readable text. Please try again in a moment.",
                    "Read aloud", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _reading = true;
            _paused = false;
            _curSentence = 0;
            await PlayCurrentAsync();
        }

        private async Task PlayCurrentAsync()
        {
            if (IsClosingOrGone() || !_reading) return;

            if (_curSentence >= _sentences.Count)
            {
                FinishReading();
                return;
            }

            string wav;
            _preparing = true;
            if (_curSentence == 0) { btnRead.Text = "Preparing…"; btnRead.Enabled = false; }
            try
            {
                if (_prefetchTask != null && _prefetchIndex == _curSentence)
                    wav = await _prefetchTask;
                else
                    wav = await Task.Run(() => PiperTts.Synthesize(_sentences[_curSentence]));
            }
            finally
            {
                _preparing = false;
                btnRead.Enabled = true;
            }

            if (IsClosingOrGone() || !_reading) return;

            if (string.IsNullOrEmpty(wav))
            {
                _curSentence++;
                await PlayCurrentAsync();
                return;
            }

            _curWav = wav;
            int durationMs = WavDurationMs(wav);
            PostToJs("{\"type\":\"play\",\"index\":" + _curSentence + ",\"duration\":" + durationMs + "}");
            _player.Play(wav);
            btnRead.Text = "Pause";
            btnStop.Visible = true;
            StartPoll();
            PrefetchNext();
        }

        private void PrefetchNext()
        {
            int next = _curSentence + 1;
            if (next < _sentences.Count)
            {
                _prefetchIndex = next;
                string text = _sentences[next];
                _prefetchTask = Task.Run(() => PiperTts.Synthesize(text));
            }
            else
            {
                _prefetchTask = null;
                _prefetchIndex = -1;
            }
        }

        private void StartPoll()
        {
            if (_pollTimer == null)
            {
                _pollTimer = new Timer { Interval = 300 };
                _pollTimer.Tick += PollPlayback;
            }
            _pollTimer.Start();
        }

        private async void PollPlayback(object sender, EventArgs e)
        {
            if (_paused || !_reading) return;

            string m = _player.Mode();
            if (m == "playing" || m == "paused") return;

            // Current sentence finished — move to the next one.
            _pollTimer.Stop();
            TryDelete(_curWav);
            _curSentence++;
            await PlayCurrentAsync();
        }

        private void FinishReading()
        {
            if (_pollTimer != null) _pollTimer.Stop();
            _reading = false;
            _paused = false;
            PostToJs("{\"type\":\"stop\"}");
            ResetReadUi();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_pollTimer != null) _pollTimer.Stop();
            _reading = false;
            _paused = false;
            _player.Stop();
            TryDelete(_curWav);
            PostToJs("{\"type\":\"stop\"}");
            ResetReadUi();
        }

        private void ResetReadUi()
        {
            btnRead.Text = "Read aloud";
            btnRead.Enabled = true;
            btnStop.Visible = false;
        }

        private static int WavDurationMs(string path)
        {
            try
            {
                byte[] hdr = new byte[44];
                using (FileStream fs = File.OpenRead(path))
                    fs.Read(hdr, 0, 44);

                int sampleRate = BitConverter.ToInt32(hdr, 24);
                short channels = BitConverter.ToInt16(hdr, 22);
                short bits = BitConverter.ToInt16(hdr, 34);
                long dataSize = new FileInfo(path).Length - 44;

                double bytesPerSec = sampleRate * channels * (bits / 8);
                if (bytesPerSec <= 0) return 3000;
                return (int)(dataSize / bytesPerSec * 1000.0);
            }
            catch { return 3000; }
        }

        private static void TryDelete(string path)
        {
            try { if (!string.IsNullOrEmpty(path) && File.Exists(path)) File.Delete(path); }
            catch { }
        }

        // ===== Navigation / lifecycle =====

        private void btnBack_Click(object sender, EventArgs e)
        {
            LessonsList lessons = new LessonsList
            {
                LessonName = LessonName,
                SubjectName = SubjectName,
                ClassNo = ClassNo,
                SchlName = SchlName
            };
            lessons.Show();
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try { if (_pollTimer != null) _pollTimer.Stop(); } catch { }
            try { _player.Dispose(); } catch { }
            try { TryDelete(_curWav); } catch { }
            try { if (_workDir != null && Directory.Exists(_workDir)) Directory.Delete(_workDir, true); } catch { }
            base.OnFormClosed(e);
        }

        private bool IsClosingOrGone()
        {
            return IsDisposed || Disposing || !IsHandleCreated;
        }

        private static bool IsBenignShutdown(Exception ex)
        {
            if (ex is ObjectDisposedException) return true;
            const int E_ABORT = unchecked((int)0x80004004);
            return ex is System.Runtime.InteropServices.COMException com && com.HResult == E_ABORT;
        }
    }
}
