using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Timers;


namespace KeplerGames //HÖF: Ólafur Jón Valgeirsson 
{
    public partial class LoginWindow : Form
    {
        DatabaseClass callDB = new DatabaseClass();
       
        
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static string uniuser;
        public static int level;
        public static string[] userInfo; /*Nýtir Upplýsingarnar frá LoginWindow*/
        

        private void bt_Login_login_Click(object sender, EventArgs e)
        {
            
            string user = tb_login_user.Text;
            string pass = tb_login_pass.Text;
            
            userInfo = new string[3];

            tb_login_pass.BackColor = Color.White;
            tb_login_user.BackColor = Color.White;



            if (user == "" || pass == "")
            {
                MessageBox.Show("You need to fill in the blanks");

            }
            else
            {


                try
                {

                    userInfo = callDB.Userloggingin(user, pass);
                    uniuser = userInfo[0];
                    level = Convert.ToInt32(userInfo[2]);
                    UserInterFace UIF = new UserInterFace();
                    if (userInfo[1].ToLower() == "false")
                    {
                        tb_login_pass.BackColor = Color.Red;
                        tb_login_user.BackColor = Color.Red;
                    }
                    else
                    {
                        UIF.Show();
                        base.Hide();
                    }
                    


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        protected override void OnClosed(EventArgs e)/*Stundum ef maður er að Show, hide getur setið eftir process í task manager, þetta kemur í veg fyrir það*/
        {
            base.OnClosed(e);

            Environment.Exit(0);
        }

        private void tb_login_user_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_login_pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

   

       
        
    }
}
