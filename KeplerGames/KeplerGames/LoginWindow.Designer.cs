namespace KeplerGames
{
    partial class LoginWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            this.tb_login_user = new System.Windows.Forms.TextBox();
            this.tb_login_pass = new System.Windows.Forms.TextBox();
            this.bt_Login_login = new System.Windows.Forms.Button();
            this.pb_login = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pb_login)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_login_user
            // 
            this.tb_login_user.Location = new System.Drawing.Point(140, 223);
            this.tb_login_user.Name = "tb_login_user";
            this.tb_login_user.Size = new System.Drawing.Size(113, 20);
            this.tb_login_user.TabIndex = 0;
            this.tb_login_user.TextChanged += new System.EventHandler(this.tb_login_user_TextChanged);
            // 
            // tb_login_pass
            // 
            this.tb_login_pass.Location = new System.Drawing.Point(320, 226);
            this.tb_login_pass.Name = "tb_login_pass";
            this.tb_login_pass.PasswordChar = '*';
            this.tb_login_pass.Size = new System.Drawing.Size(113, 20);
            this.tb_login_pass.TabIndex = 1;
            this.tb_login_pass.TextChanged += new System.EventHandler(this.tb_login_pass_TextChanged);
            // 
            // bt_Login_login
            // 
            this.bt_Login_login.Location = new System.Drawing.Point(211, 252);
            this.bt_Login_login.Name = "bt_Login_login";
            this.bt_Login_login.Size = new System.Drawing.Size(101, 39);
            this.bt_Login_login.TabIndex = 2;
            this.bt_Login_login.Text = "Login";
            this.bt_Login_login.UseVisualStyleBackColor = true;
            this.bt_Login_login.Click += new System.EventHandler(this.bt_Login_login_Click);
            // 
            // pb_login
            // 
            this.pb_login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pb_login.BackgroundImage")));
            this.pb_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_login.Location = new System.Drawing.Point(3, 0);
            this.pb_login.Name = "pb_login";
            this.pb_login.Size = new System.Drawing.Size(510, 207);
            this.pb_login.TabIndex = 3;
            this.pb_login.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 226);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // LoginWindow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(518, 323);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_Login_login);
            this.Controls.Add(this.tb_login_pass);
            this.Controls.Add(this.tb_login_user);
            this.Controls.Add(this.pb_login);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kepler Games Login";
            ((System.ComponentModel.ISupportInitialize)(this.pb_login)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_login_user;
        private System.Windows.Forms.TextBox tb_login_pass;
        private System.Windows.Forms.Button bt_Login_login;
        private System.Windows.Forms.PictureBox pb_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

