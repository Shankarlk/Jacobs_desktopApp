using System;
using System.Drawing;
using System.Windows.Forms;

namespace JacobsDesktopApp
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.bgImage = new System.Windows.Forms.PictureBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblBoard = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkAgree = new System.Windows.Forms.CheckBox();
            this.linkForgot = new System.Windows.Forms.LinkLabel();
            this.linkSignUp = new System.Windows.Forms.LinkLabel();
            this.lblFooter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bgImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // bgImage
            // 
            this.bgImage.ErrorImage = ((System.Drawing.Image)(resources.GetObject("bgImage.ErrorImage")));
            this.bgImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("bgImage.InitialImage")));
            this.bgImage.Location = new System.Drawing.Point(0, 0);
            this.bgImage.Name = "bgImage";
            this.bgImage.Size = new System.Drawing.Size(500, 600);
            this.bgImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bgImage.TabIndex = 0;
            this.bgImage.TabStop = false;
            // 
            // logo
            // 
            this.logo.ErrorImage = ((System.Drawing.Image)(resources.GetObject("logo.ErrorImage")));
            this.logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("logo.InitialImage")));
            this.logo.Location = new System.Drawing.Point(650, 30);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(150, 80);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 1;
            this.logo.TabStop = false;
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Arial", 10F);
            this.lblUsername.ForeColor = System.Drawing.Color.Gray;
            this.lblUsername.Location = new System.Drawing.Point(580, 150);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(100, 23);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "User Name";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(580, 175);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(300, 22);
            this.txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Arial", 10F);
            this.lblPassword.ForeColor = System.Drawing.Color.Gray;
            this.lblPassword.Location = new System.Drawing.Point(580, 220);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 23);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(580, 245);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(300, 22);
            this.txtPassword.TabIndex = 5;
            // 
            // lblBoard
            // 
            this.lblBoard.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblBoard.Font = new System.Drawing.Font("Arial", 10F);
            this.lblBoard.ForeColor = System.Drawing.Color.Gray;
            this.lblBoard.Location = new System.Drawing.Point(580, 290);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(100, 23);
            this.lblBoard.TabIndex = 6;
            this.lblBoard.Text = "Board";
            this.lblBoard.Visible = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Black;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(580, 345);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(300, 31);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "LOGIN";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkAgree
            // 
            this.chkAgree.AutoSize = true;
            this.chkAgree.Font = new System.Drawing.Font("Arial", 9F);
            this.chkAgree.Location = new System.Drawing.Point(580, 420);
            this.chkAgree.Name = "chkAgree";
            this.chkAgree.Size = new System.Drawing.Size(321, 21);
            this.chkAgree.TabIndex = 9;
            this.chkAgree.Text = "Agree to our Terms of use and Privacy Policy";
            // 
            // linkForgot
            // 
            this.linkForgot.AutoSize = true;
            this.linkForgot.Location = new System.Drawing.Point(800, 460);
            this.linkForgot.Name = "linkForgot";
            this.linkForgot.Size = new System.Drawing.Size(119, 16);
            this.linkForgot.TabIndex = 10;
            this.linkForgot.TabStop = true;
            this.linkForgot.Text = "Forgot Password ?";
            // 
            // linkSignUp
            // 
            this.linkSignUp.AutoSize = true;
            this.linkSignUp.Location = new System.Drawing.Point(580, 460);
            this.linkSignUp.Name = "linkSignUp";
            this.linkSignUp.Size = new System.Drawing.Size(149, 16);
            this.linkSignUp.TabIndex = 11;
            this.linkSignUp.TabStop = true;
            this.linkSignUp.Text = "Don\'t have an account ?";
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Arial", 8F);
            this.lblFooter.ForeColor = System.Drawing.Color.Gray;
            this.lblFooter.Location = new System.Drawing.Point(598, 500);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(327, 32);
            this.lblFooter.TabIndex = 12;
            this.lblFooter.Text = "© 2025 JACOBS EDUCARE, All Rights Reserved\nPowered by EVOLUTION SOFTWARE SOLUTION" +
    "S";
            // 
            // LoginForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.bgImage);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblBoard);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkAgree);
            this.Controls.Add(this.linkForgot);
            this.Controls.Add(this.linkSignUp);
            this.Controls.Add(this.lblFooter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jacobs Educare - Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bgImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox bgImage;
        private PictureBox logo;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblBoard;
        private ComboBox cmbBoard;
        private Button btnLogin;
        private CheckBox chkAgree;
        private LinkLabel linkForgot;
        private LinkLabel linkSignUp;
        private Label lblFooter;
    }
}