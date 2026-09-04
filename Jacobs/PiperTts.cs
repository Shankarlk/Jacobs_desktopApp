using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace JacobsDesktopApp
{
    /// <summary>
    /// Thin wrapper around the bundled Piper neural text-to-speech engine.
    /// Runs piper.exe as a short-lived process and returns a WAV file. Piper
    /// runs out-of-process, so it is unaffected by this app being 32-bit.
    /// </summary>
    internal static class PiperTts
    {
        public static string PiperDir
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Piper"); }
        }

        public static bool IsAvailable
        {
            get
            {
                return File.Exists(Path.Combine(PiperDir, "piper.exe"))
                    && File.Exists(Path.Combine(PiperDir, "voice.onnx"));
            }
        }

        /// <summary>
        /// Synthesizes text to a WAV file and returns its path (or null on
        /// failure). Blocking call — invoke from a background thread.
        /// </summary>
        public static string Synthesize(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || !IsAvailable)
                return null;

            string piperExe = Path.Combine(PiperDir, "piper.exe");
            string model = Path.Combine(PiperDir, "voice.onnx");

            string outDir = Path.Combine(Path.GetTempPath(), "JacobsDesktopApp", "tts");
            Directory.CreateDirectory(outDir);
            string outWav = Path.Combine(outDir, Guid.NewGuid().ToString() + ".wav");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = piperExe,
                Arguments = "--model \"" + model + "\" --output_file \"" + outWav + "\"",
                WorkingDirectory = PiperDir,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardInput = true,
                RedirectStandardError = true
            };

            using (Process p = Process.Start(psi))
            {
                // Piper reads UTF-8 text from stdin; write raw bytes so any
                // punctuation survives regardless of the console code page.
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                p.StandardInput.BaseStream.Write(bytes, 0, bytes.Length);
                p.StandardInput.BaseStream.Flush();
                p.StandardInput.Close();

                p.StandardError.ReadToEnd(); // drain so the process can exit
                p.WaitForExit();
            }

            return File.Exists(outWav) ? outWav : null;
        }
    }
}
