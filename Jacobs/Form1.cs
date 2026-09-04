using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    public partial class Form1 : Form
    {
        private readonly string username = "User";
        private readonly string adminuname = "Admin";
        private readonly string password = "user";
        private readonly string admin = "admin";
        public Form1()
        {
            InitializeComponent();
            this.Width = 500;
            this.Height = 500;
            this.Size = new Size(500, 500);
            groupBox6.Margin = new Padding(10, 10, 10, 50);
            groupBox6.Location = new Point(groupBox6.Location.X, groupBox6.Location.Y - 0);  
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = txtUsername.Text.Trim();
            string pwd = txtPwd.Text;

            if (uname == "")
            {
                MessageBox.Show("Please Enter The Username");
                txtUsername.Focus();
                return;
            }

            if (pwd == "")
            {
                MessageBox.Show("Please Enter The Password");
                txtPwd.Focus();
                return;
            }

            if (uname != adminuname && uname != username)
            {
                MessageBox.Show("Invalid Username");
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            if (pwd == admin)
            {
                LicenseKeyReplacement subjects = new LicenseKeyReplacement();
                subjects.SchlName = "School Name";
                subjects.Show();
                this.Hide();
                return;
            }

            if (pwd != password)
            {
                MessageBox.Show("Invalid Password");
                txtPwd.Focus();
                txtPwd.SelectAll();
                return;
            }

            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnCancel.Visible=false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            label4.Visible = false;
            cmbBoard.Visible = false;
            this.Text = "";

            ApplyLoginTheme();

            // Pressing Enter anywhere on the login form triggers Login.
            this.AcceptButton = btnLogin;
            txtUsername.Focus();
        }

        // Give the login card the shared look: clean inputs, blue primary button
        // and consistent Segoe UI typography, laid out in a tidy vertical stack.
        private void ApplyLoginTheme()
        {
            groupBox2.BackColor = Theme.Surface;
            groupBox3.BackColor = Theme.Surface;

            // These were anchored Top|Bottom in the designer, which stretches the
            // inputs vertically. Pin them to the top so they keep a fixed height.
            label2.Anchor = AnchorStyles.Top;
            label3.Anchor = AnchorStyles.Top;
            txtUsername.Anchor = AnchorStyles.Top;
            txtPwd.Anchor = AnchorStyles.Top;

            int panelWidth = groupBox3.ClientSize.Width;
            int fieldWidth = 300;
            int left = (panelWidth - fieldWidth) / 2;
            if (left < 20) left = 20;

            label2.Text = "Username";
            label2.Font = Theme.Font(11, System.Drawing.FontStyle.Bold);
            label2.ForeColor = Theme.TextDark;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(left, 30);

            Theme.Input(txtUsername);
            txtUsername.Size = new System.Drawing.Size(fieldWidth, 32);
            txtUsername.Location = new System.Drawing.Point(left, 58);

            label3.Text = "Password";
            label3.Font = Theme.Font(11, System.Drawing.FontStyle.Bold);
            label3.ForeColor = Theme.TextDark;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(left, 108);

            Theme.Input(txtPwd);
            txtPwd.Size = new System.Drawing.Size(fieldWidth, 32);
            txtPwd.Location = new System.Drawing.Point(left, 136);

            Theme.PrimaryButton(btnLogin);
            btnLogin.Text = "Login";
            btnLogin.Size = new System.Drawing.Size(fieldWidth, 42);
            btnLogin.Location = new System.Drawing.Point(left, 196);
            btnLogin.Anchor = AnchorStyles.Top;
        }
    }
}
