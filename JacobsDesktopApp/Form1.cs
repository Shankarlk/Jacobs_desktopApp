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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = txtUsername.Text;
            string pwd = txtPwd.Text; 
            string board = cmbBoard.SelectedItem?.ToString();
            if (uname == "")
            {
                lblMsg.Text = "Please Enter The Username";
                lblMsg.ForeColor = Color.Red;
            }
            else if(pwd == "")
            {
                lblMsg.Text = "Please Enter The Password";
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.Text = "";
                lblMsg.ForeColor = Color.Red;
            }

            if (uname != adminuname && uname != username)
            {
                lblMsg.Text = "Invalid Username";
                lblMsg.ForeColor = Color.Red;
            }
            else if (pwd == admin)
            {
                lblMsg.Text = "";
                lblMsg.ForeColor = Color.Red;
                LicenseKeyReplacement subjects = new LicenseKeyReplacement();
                subjects.SchlName = "School Name";
                subjects.Show();
                this.Hide();
            }
            //else if ()
            //{
            //    lblMsg.Text = "Invalid Username";
            //    lblMsg.ForeColor = Color.Red;
            //}
            else if (pwd != password)
            {
                lblMsg.Text = "Invalid Password";
                lblMsg.ForeColor = Color.Red;
            }
            else if (board == null)
            {
                lblMsg.Text = "Please select the board.";
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.Text = "";
                lblMsg.ForeColor = Color.Red;
                Home home = new Home();
                home.Board = board;
                home.Show();
                this.Hide(); 
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }
    }
}
