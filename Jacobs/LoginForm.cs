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
    public partial class LoginForm : Form
    {
        private readonly string username = "User";
        private readonly string adminuname = "Admin";
        private readonly string password = "user";
        private readonly string admin = "admin";
        public LoginForm()
        {
            InitializeComponent();
            this.Width = 500;
            this.Height = 500;
            this.Size = new Size(500, 500);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            lblBoard.Visible = false;
            cmbBoard.Visible = false;

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string uname = txtUsername.Text;
            string pwd = txtPassword.Text;
            //string board = cmbBoard.SelectedItem?.ToString();
            if (uname == "")
            {
                MessageBox.Show("Please Enter The Username");
            }
            else if (pwd == "")
            {
                MessageBox.Show("Please Enter The Password");
            }
            else
            {
                //MessageBox.Show("";
                //lblMsg.ForeColor = Color.Red;
            }

            if (uname != adminuname && uname != username)
            {
                MessageBox.Show("Invalid Username");
            }

            else if (pwd == admin)
            {
                //MessageBox.Show(""); 
                LicenseKeyReplacement subjects = new LicenseKeyReplacement();
                subjects.SchlName = "School Name";
                subjects.Show();
                this.Hide();
            }
            //else if ()
            //{
            //    MessageBox.Show("Invalid Username";
            //    lblMsg.ForeColor = Color.Red;
            //}
            else if (pwd != password)
            {
                MessageBox.Show("Invalid Password");
            }
            //else if (board == null)
            //{
            //    MessageBox.Show("Please select the board.");
            //}
            else
            {
                //MessageBox.Show("";
                Home home = new Home();
                //home.Board = board;
                home.Show();
                this.Hide();
            }

        }

        
    }
}
