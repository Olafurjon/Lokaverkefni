using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace KeplerGames // HÖF Ólafur Jón, þetta er enn í vinnslu, virkar eins og staðan er en ekki 100% eins og ég vill, endar alltaf í bakgrunni eða getur opnað ooft
{
    public partial class VerifyPass : Form
    {
        public VerifyPass()
        {
            InitializeComponent();
            
        }
        


        private void bt_verifypass_Click(object sender, EventArgs e)
        {
            
            string pass = LoginWindow.userInfo[1];
            string user = LoginWindow.userInfo[0];

            label1.Text = "Verify " + user + " Password";
           
            if (tb_verify_pass.Text == pass)
            {
                UserInterFace.letmein = 1;
                MessageBox.Show("Password For "+user+" Verified");
                this.Close();
            }
            else if (tb_verify_pass.Text == "")
            {
                tb_verify_pass.BackColor = Color.Red;
                MessageBox.Show("Please Verify The Password");
                UserInterFace.letmein = 0;
            }
            else
            {
                UserInterFace.letmein = 0;
                MessageBox.Show("Wrong  Password");
            }


            

        }
    }
}
