using System;
using System.Runtime.InteropServices;
using System.Text;

namespace JacobsDesktopApp
{
    /// <summary>
    /// Minimal WAV player built on the Windows MCI API. Supports
    /// play / pause / resume / stop with no external dependency, which is all
    /// the read-aloud feature needs.
    /// </summary>
    internal sealed class AudioPlayer : IDisposable
    {
        [DllImport("winmm.dll", CharSet = CharSet.Auto)]
        private static extern int mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr callback);

        private const string Alias = "jacobsTts";
        private bool _open;

        public void Play(string wavPath)
        {
            Stop();
            mciSendString("open \"" + wavPath + "\" type waveaudio alias " + Alias, null, 0, IntPtr.Zero);
            _open = true;
            mciSendString("play " + Alias, null, 0, IntPtr.Zero);
        }

        public void Pause()
        {
            if (_open) mciSendString("pause " + Alias, null, 0, IntPtr.Zero);
        }

        public void Resume()
        {
            if (_open) mciSendString("resume " + Alias, null, 0, IntPtr.Zero);
        }

        public void Stop()
        {
            if (!_open) return;
            mciSendString("stop " + Alias, null, 0, IntPtr.Zero);
            mciSendString("close " + Alias, null, 0, IntPtr.Zero);
            _open = false;
        }

        /// <summary>Returns "playing", "paused", "stopped", etc.</summary>
        public string Mode()
        {
            if (!_open) return "stopped";
            StringBuilder sb = new StringBuilder(64);
            mciSendString("status " + Alias + " mode", sb, sb.Capacity, IntPtr.Zero);
            return sb.ToString();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
