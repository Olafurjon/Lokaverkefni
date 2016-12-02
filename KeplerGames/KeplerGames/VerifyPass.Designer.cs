namespace KeplerGames
{
    partial class VerifyPass
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
            this.tb_verify_pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_verifypass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_verify_pass
            // 
            this.tb_verify_pass.Location = new System.Drawing.Point(58, 38);
            this.tb_verify_pass.Name = "tb_verify_pass";
            this.tb_verify_pass.PasswordChar = '*';
            this.tb_verify_pass.Size = new System.Drawing.Size(100, 20);
            this.tb_verify_pass.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Verify Password";
            // 
            // bt_verifypass
            // 
            this.bt_verifypass.Location = new System.Drawing.Point(58, 64);
            this.bt_verifypass.Name = "bt_verifypass";
            this.bt_verifypass.Size = new System.Drawing.Size(100, 35);
            this.bt_verifypass.TabIndex = 2;
            this.bt_verifypass.Text = "Verify";
            this.bt_verifypass.UseVisualStyleBackColor = true;
            this.bt_verifypass.Click += new System.EventHandler(this.bt_verifypass_Click);
            // 
            // VerifyPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 111);
            this.Controls.Add(this.bt_verifypass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_verify_pass);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VerifyPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VerifyPass";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_verify_pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_verifypass;
    }
}