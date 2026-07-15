using System;
using System.Drawing;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    /// <summary>
    /// Central place for the app's colours, fonts and control styling so every
    /// screen shares one consistent look and feel.
    /// </summary>
    internal static class Theme
    {
        // Palette
        public static readonly Color Primary = Color.FromArgb(37, 99, 235);   // blue
        public static readonly Color PrimaryDark = Color.FromArgb(29, 78, 216);
        public static readonly Color Accent = Color.FromArgb(59, 130, 246);
        public static readonly Color Background = Color.FromArgb(245, 248, 252);
        public static readonly Color Surface = Color.White;
        public static readonly Color TextDark = Color.FromArgb(15, 23, 42);
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);
        public static readonly Color Border = Color.FromArgb(203, 213, 225);

        public const string FontName = "Segoe UI";

        public static Font Font(float size, FontStyle style = FontStyle.Regular)
            => new Font(FontName, size, style);

        /// <summary>Solid blue call-to-action button.</summary>
        public static void PrimaryButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = PrimaryDark;
            b.BackColor = Primary;
            b.ForeColor = Color.White;
            b.Font = Font(11, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
        }

        /// <summary>White button with a blue outline (secondary action / back).</summary>
        public static void SecondaryButton(Button b)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderColor = Primary;
            b.FlatAppearance.BorderSize = 1;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 244, 255);
            b.BackColor = Color.White;
            b.ForeColor = Primary;
            b.Font = Font(10, FontStyle.Bold);
            b.Cursor = Cursors.Hand;
            b.UseVisualStyleBackColor = false;
        }

        /// <summary>
        /// Styles and positions a screen's "← Back" button consistently at the
        /// top-left of the content area (same place on every screen and viewer).
        /// </summary>
        public static void TopLeftBack(Button b)
        {
            PrimaryButton(b);
            b.Text = "← Back";
            b.Size = new Size(120, 40);
            b.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            b.Location = new Point(30, 85);
            b.BringToFront();
        }

        /// <summary>Flat single-line text/password input.</summary>
        public static void Input(TextBox t)
        {
            t.BorderStyle = BorderStyle.FixedSingle;
            t.Font = Font(12);
            t.BackColor = Color.White;
            t.ForeColor = TextDark;
        }

        /// <summary>
        /// Adds the shared blue viewer header (Back button + centred title) to a
        /// document/media viewer form and hides its old ad-hoc Back button, so
        /// every viewer looks the same. The new Back button reuses the form's
        /// existing back-navigation.
        /// </summary>
        public static Panel ApplyViewerChrome(Form form, string title, EventHandler onBack, Control oldBack)
        {
            form.BackColor = Background;

            Panel bar = new Panel();
            bar.Dock = DockStyle.Top;
            bar.Height = 48;
            bar.BackColor = Primary;

            Button back = new Button();
            back.Text = "← Back";
            back.Size = new Size(110, 34);
            back.Location = new Point(10, 7);
            back.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            back.FlatStyle = FlatStyle.Flat;
            back.FlatAppearance.BorderSize = 0;
            back.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 244, 255);
            back.BackColor = Color.White;
            back.ForeColor = Primary;
            back.Font = Font(10, FontStyle.Bold);
            back.Cursor = Cursors.Hand;
            back.UseVisualStyleBackColor = false;
            if (onBack != null)
                back.Click += onBack;

            // Fill label centres the title across the whole bar regardless of the
            // window width; the Back button sits on top at the left.
            Label lbl = new Label();
            lbl.Text = title ?? "";
            lbl.ForeColor = Color.White;
            lbl.Font = Font(12, FontStyle.Bold);
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;

            bar.Controls.Add(lbl);
            bar.Controls.Add(back);
            back.BringToFront();

            form.Controls.Add(bar);
            bar.BringToFront();

            if (oldBack != null)
                oldBack.Visible = false;

            return bar;
        }
    }
}
