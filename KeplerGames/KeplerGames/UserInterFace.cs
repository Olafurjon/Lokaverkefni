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
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace KeplerGames
{
    public partial class UserInterFace : Form
    {
      
        DatabaseClass callDB = new DatabaseClass();
        
        public UserInterFace()
        {
            InitializeComponent();
            int access = 0;
            string loggedinUser = "Nobody";
            loggedinUser = LoginWindow.uniuser;
            access = LoginWindow.level;
            int userloggedin = callDB.IsUserLoggedIN(LoginWindow.userInfo[0]);


            if (access == 1)
            {
                tabcontrol.TabPages.Remove(tab_admin);
                tabcontrol.TabPages.Remove(tab_Programmers);
                tabcontrol.TabPages.Remove(tab_review);
            }
            if (access == 2)
            {
                tabcontrol.TabPages.Remove(tab_admin);
                tabcontrol.TabPages.Remove(tab_Programmers);
            }
            if (access == 3)
            {
                tabcontrol.TabPages.Remove(tab_admin);
            }

            if (userloggedin == 0)
            {
                bt_logout.Text = "Start Working";
            }
            else
            {
                bt_logout.Text = "Quit Working";
            }


            try
            {
                callDB.Initialize();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        public void ClearLabels()
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label3, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };


            for (int i = 0; i < labelarray.Count(); i++)
            {
                this.tab_admin.Controls.Remove(labelarray[i]);
                this.tab_admin.Controls.Remove(tbarray[i]);
                this.tab_games.Controls.Remove(labelarray[i]);
                this.tab_games.Controls.Remove(tbarray[i]);
                this.tab_Programmers.Controls.Remove(labelarray[i]);
                this.tab_Programmers.Controls.Remove(tbarray[i]);
                this.tab_review.Controls.Remove(labelarray[i]);
                this.tab_review.Controls.Remove(tbarray[i]);
                tbarray[4].PasswordChar = '\0';
                labelarray[i].Visible = false;
                tbarray[i].Visible = false;
            }
        } //*** Hreinsar alla labels, kalla ´þa þetta til að gera allt klárt
        public void HideDynamic()
        {
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            
            bt_execute2.Visible = false;
            tb_dynamic1.Visible = false;
            tb_dynamic2.Visible = false;
            tb_dynamic3.Visible = false;
            tb_dynamic4.Visible = false;
            tb_dynamic5.Visible = false;
            tb_dynamic6.PasswordChar = '\0';
            tb_dynamic6.Visible = false;
            tb_dynamic7.Visible = false;
            tb_dynamic8.Visible = false;
            tb_dynamic9.Visible = false;
            tb_dynamic10.Visible = false;
            tb_dynamic11.Visible = false;
            tb_dynamic12.Visible = false;
            tb_dynamic13.Visible = false;
            tb_dynamic14.Visible = false;
            tb_dynamic15.Visible = false;



        } //*** Hreinsar öll tb
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Environment.Exit(0);

        } //*** Passar að þetta verði ekki keyrandi í bakgrunn ***
        public static int letmein; //*** í vinnslu, checkar password verify fyrir delete, verður klárað ef tími gefst til
        public string gamepath;

        

        /*Tab-Games*/
        private void tab_games_Enter(object sender, EventArgs e)
        {
            int userloggedin = callDB.IsUserLoggedIN(LoginWindow.userInfo[0]);
            this.tab_games.Controls.Add(bt_logout);
            dgv_Games.DataSource = null;
            dgv_Games.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT game_id,Games.name,Developers.name AS Developers, path, dateadded, games.description FROM games JOIN Developers ON games.dev_id = Developers.dev_id"); /*Bý ég til dataset töflu úr SQL queryinu*/
            dgv_Games.DataSource = table.Tables[0]; /*Dúndrar dataset töflunni inní datagriddið*/
            dgv_Games.Rows[0].Selected = true; /*Velur fyrstu röðina*/
            HideDynamic();/*Felur textboxin*/
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label3, label14, label15, label16 }; /*Mögulega er ég að kalla í þetta allt óþarflega oft og með meira en ég þarf en það sparaði tíma að bara copy paste-a þetta*/
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };/*Mögulega er ég að kalla í þetta allt óþarflega oft og með meira en ég þarf en það sparaði tíma að bara copy paste-a þetta*/
            ClearLabels();/*Remove-ar öll controlin svo ég geti kallað í ný án vandræða*/

            for (int i = 0; i < labelarray.Count(); i++) /*Bætir við tbarray og labelarrya í viðeigandi Tab stjórnun*/
            {
                this.tab_games.Controls.Add(labelarray[i]);
                this.tab_games.Controls.Add(tbarray[i]);
            }


            if (dgv_Games.SelectedRows.Count > 0) /*Ef er valið eitthvað í röðinni þá refreshast gögnin*/
            {


                for (int i = 0; i < dgv_Games.ColumnCount; i++) /*Keyrir í gegnum jafnoft og dálkarnir eru í datagriddinu, það er svolítið mikið af þessu...*/
                {

                    string[] data;
                    data = new string[dgv_Games.ColumnCount];
                    string[] info;
                    info = new string[dgv_Games.ColumnCount];
                    info[i] = dgv_Games.Columns[i].Name;
                    data[i] = dgv_Games.SelectedRows[0].Cells[i].Value + string.Empty; /*Þessi sér um að setja gildin úr töflunni í array svo ég geti notað*/
                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;
                }
                bt_rungame.Visible = true;


            }



        }
        

        private void dgv_Games_SelectionChanged(object sender, EventArgs e)
        {
            
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label3, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            ClearLabels();

            for (int i = 0; i < labelarray.Count(); i++)
            {
                this.tab_games.Controls.Add(labelarray[i]);
                this.tab_games.Controls.Add(tbarray[i]);
            }


            if (dgv_Games.SelectedRows.Count > 0)
            {


                for (int i = 0; i < dgv_Games.ColumnCount; i++)
                {

                    string[] data;
                    data = new string[dgv_Games.ColumnCount];
                    string[] info;
                    info = new string[dgv_Games.ColumnCount];
                    

                    info[i] = dgv_Games.Columns[i].Name;

                    data[i] = dgv_Games.SelectedRows[0].Cells[i].Value + string.Empty;
                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;
                    

                }
                bt_rungame.Visible = true;


            }
        } /*Hérna geri ég það sama og í Enter til að refresha þegar það er beytt um röð*/
        private void bt_rungame_Click(object sender, EventArgs e)
        {
            if (tb_dynamic1.Text == "")
            {
                MessageBox.Show("Choose A Game From The List");
            }
            else if (gamepath == "")
            {
                MessageBox.Show("Path is invalid");
            }
           /*magnaða við SQL maður verður að gera \\\\WIN3A-20\\GAMES\\leikur.exe þar sem tsuts tekur alltaf 1x\fyrir hvert \ sem er sett...*/
      
            else
            {

                gamepath = tb_dynamic4.Text;
                ProcessStartInfo startinfo = new ProcessStartInfo(gamepath);
                Process StartGame = new Process();
                StartGame.StartInfo = startinfo;
                
                
                if (!File.Exists(gamepath))
                {
                    MessageBox.Show("Path Error");
                }
                else
                {
                    Process.Start(gamepath); /*Keyrir exe skránna frá pathinu*/
                }
                
                try
                {
                   
                }
                catch(ArgumentException ex)
                {
                    MessageBox.Show("Failed due to: "+ ex.Message );
                }

            }
        }

        /* END Tab-Games*/


        /*Tab-Reviews*/


        private void tab_review_Enter(object sender, EventArgs e) /*Þegar er ENTERAÐ þá refreshast taflan og sýnir allt úr games og gerir það sem refreshar*/
        {
            int userloggedin = callDB.IsUserLoggedIN(LoginWindow.userInfo[0]);
            this.tab_review.Controls.Add(bt_logout);
            ClearLabels();
            HideDynamic();
            dgv_reviews.DataSource = null;
            dgv_reviews.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT game_id,Games.name,Developers.name AS Developers, path, dateadded, games.description FROM games JOIN Developers ON games.dev_id = Developers.dev_id");
            dgv_reviews.DataSource = table.Tables[0];

            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label3, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
          
            dgv_reviews.Rows[0].Selected = true;
            for (int i = 0; i < labelarray.Count(); i++)
            {
                this.tab_review.Controls.Add(labelarray[i]);
                this.tab_review.Controls.Add(tbarray[i]);
            }

            if (dgv_reviews.SelectedRows.Count > 0)
            {

                for (int i = 0; i < dgv_reviews.ColumnCount; i++)
                {

                    string[] data;
                    data = new string[dgv_reviews.ColumnCount];
                    string[] info;
                    info = new string[dgv_reviews.ColumnCount];

                    info[i] = dgv_reviews.Columns[i].Name;

                    data[i] = dgv_reviews.SelectedRows[0].Cells[i].Value + string.Empty;
                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;
                }
                bt_rungame_review.Visible = true;


            }
        }
        private void dgv_reviews_SelectionChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            ClearLabels();
            for (int i = 0; i < labelarray.Count(); i++)
            {

                this.tab_review.Controls.Add(labelarray[i]);
                this.tab_review.Controls.Add(tbarray[i]);
            }

            if (dgv_reviews.SelectedRows.Count > 0)
            {


                for (int i = 0; i < dgv_reviews.ColumnCount; i++)
                {

                    string[] data;
                    data = new string[dgv_reviews.ColumnCount];
                    string[] info;
                    info = new string[dgv_reviews.ColumnCount];


                    info[i] = dgv_reviews.Columns[i].Name;

                    data[i] = dgv_reviews.SelectedRows[0].Cells[i].Value + string.Empty;

                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;

                }
                bt_rungame_review.Visible = true;

            }
        } /*Refreshar labels og tb þegar fært er um röð*/

        private void bt_rungame_review_Click(object sender, EventArgs e)
        {
            if (tb_dynamic1.Text == "")
            {
                MessageBox.Show("Choose A Game From The List");
            }
            else if (gamepath == "")
            {
                MessageBox.Show("Path is invalid");
            }
            /*magnaða við SQL maður verður að gera \\\\WIN3A-20\\GAMES\\leikur.exe þar sem tsuts tekur alltaf 1x\fyrir hvert \ sem er sett...*/
         
            else
            {
                gamepath = tb_dynamic4.Text;
                try
                {
                    Process.Start(gamepath);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Failed due to: " + ex.Message);
                }

            }
        } /*Sama og áðan*/
      
        private void bt_review_commit_Click(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };

            string user = LoginWindow.userInfo[0];
            string gamename = "";
            MessageBox.Show(user);
            foreach (var label in labelarray)
            {
                int i = 0;
                i++;
                if (label.Text.ToLower() == "name")
                {
                    gamename = tbarray[i].Text;
                    MessageBox.Show(gamename);
                }
            }

            if (tb_gamereview.Text == "")
            {
                MessageBox.Show("Dont Post Empty Reviews...");
            }
            else if (gamename == "")
            {
                MessageBox.Show("Choose A Game first");
            }
            else
            {
                try
                {
                    callDB.PostReview(gamename, user, tb_gamereview.Text);
                    MessageBox.Show("Success: Review for "+gamename+" Posted");
                }
                catch (ArgumentException ex)
                {

                    MessageBox.Show("Error Due To: " + ex.Message);
                }
            }
        }


        /*End Tab Review*/


        /*Tab Programmers*/
        /*Access Level 3, geta sett inn sýna eigin leiki í gagnagrunninn ásamt því að spila leikina */
        private void tab_Programmers_Enter(object sender, EventArgs e)
        {
            HideDynamic();
            int userloggedin = callDB.IsUserLoggedIN(LoginWindow.userInfo[0]);
            this.tab_Programmers.Controls.Add(bt_logout);
            dgv_Programmes.DataSource = null;
            dgv_Programmes.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT game_id,Games.name,Developers.name AS Developers, path, dateadded, games.description FROM games JOIN Developers ON games.dev_id = Developers.dev_id");
            dgv_Programmes.DataSource = table.Tables[0];
            dgv_Programmes.Rows[0].Selected = true;
            
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            ClearLabels();
            for (int i = 0; i < labelarray.Count(); i++)
            {

                this.tab_Programmers.Controls.Add(labelarray[i]);
                this.tab_Programmers.Controls.Add(tbarray[i]);
            }

            if (dgv_Programmes.SelectedRows.Count > 0)
            {


                for (int i = 0; i < dgv_Programmes.ColumnCount; i++)
                {

                    string[] data;
                    data = new string[dgv_Programmes.ColumnCount];
                    string[] info;
                    info = new string[dgv_Programmes.ColumnCount];


                    info[i] = dgv_Programmes.Columns[i].Name;

                    data[i] = dgv_Programmes.SelectedRows[0].Cells[i].Value + string.Empty;

                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;

                }

            }
        } /*Ekkert nýtt hér*/
        private void dgv_Programmes_SelectionChanged(object sender, EventArgs e)
        {
        


            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            ClearLabels();
            for (int i = 0; i < labelarray.Count(); i++)
            {

                this.tab_Programmers.Controls.Add(labelarray[i]);
                this.tab_Programmers.Controls.Add(tbarray[i]);
            }
           

            if (dgv_Programmes.SelectedRows.Count > 0)
            {
                

                for (int i = 0; i < dgv_Programmes.ColumnCount; i++)
                {

                    string[] data;
                    data = new string[dgv_Programmes.ColumnCount];
                    string[] info;
                    info = new string[dgv_Programmes.ColumnCount];


                    info[i] = dgv_Programmes.Columns[i].Name;

                    data[i] = dgv_Programmes.SelectedRows[0].Cells[i].Value + string.Empty;

                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;

                }

            }
        }/*Ekkert nýtt hér*/

        private void bt_programmers_Click(object sender, EventArgs e)
        {
            HideDynamic();
            bt_programmers.Visible = false;
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_Programmes.DataSource = null;
            dgv_Programmes.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name,path,description FROM games");
            dgv_Programmes.DataSource = table.Tables[0];
            
            bt_programmers.Visible = false;
            if (dgv_Programmes.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_Programmes.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_Programmes.ColumnCount];
                string[] info;
                info = new string[dgv_Programmes.ColumnCount];
                info[i] = dgv_Programmes.Columns[i].Name;
                tbarray[i].Clear();
                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;
                tbarray[i].Visible = true;

            }
            bt_executehelper = 33;
            bt_programmer2.Visible = true;
            bt_programmer2.Text = "Add Game";

        }/*Ekkert nýtt hér*/

        private void bt_programmer2_Click(object sender, EventArgs e)/*Ekkert nýtt hér*/
        {
            if (bt_executehelper == 33) /*þegar ég er með takka yfir takka*/
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_Programmes.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_Programmes.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.AddGameProgrammer(text[0], text[1], text[2], LoginWindow.userInfo[0]);
                        MessageBox.Show("Success");
                        bt_programmer2.Visible = false;
                        bt_programmers.Visible = true;
                        dgv_Programmes.DataSource = null;
                        dgv_Programmes.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT game_id,Games.name,Developers.name AS Developers, path, dateadded, games.description FROM games JOIN Developers ON games.dev_id = Developers.dev_id");
                        dgv_Programmes.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
        private void bt_programmers_rungame_Click(object sender, EventArgs e)
        {
            if (tb_dynamic1.Text == "" || tb_dynamic4.Text == "")
            {
                MessageBox.Show("Choose A Game From The List");
            }
            else if (gamepath == "")
            {
                MessageBox.Show("Path is invalid");
            }
            /*magnaða við SQL maður verður að gera \\\\WIN3A-20\\GAMES\\leikur.exe þar sem tsuts tekur alltaf 1x\fyrir hvert \ sem er sett...*/

            else
            {
                gamepath = tb_dynamic4.Text;
                
                try
                {
                    Process.Start(gamepath);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Failed due to: " + ex.Message);
                }

            }
        }

        /*Tab Programmers End*/

        public static string olduser; /*Geymir upplýsingarnar fyrir update ef það er uppfært username eða nafn*/
        public static string oldname; /*Geymir upplýsingarnar fyrir update ef það er uppfært username eða nafn*/


        /*Admin Panel*/ 
        /*Getur basically manipulate-að allt í gagnagrunninum*/
        private void tab_admin_Enter(object sender, EventArgs e)
        {
            HideDynamic();
            int userloggedin = callDB.IsUserLoggedIN(LoginWindow.userInfo[0]);
            this.tab_admin.Controls.Add(bt_logout);
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT * FROM USERS");
            dgv_admin_users.DataSource = table.Tables[0];



            
        } /**/
        public int bt_executehelper = 0;

        private void bt_queryrun_Click(object sender, EventArgs e) /*Ákvað að hafa þetta af einhverri ástæðu, get gert þetta þannig að maður getir deletað, og update-að og þannig en svona til vonar og vara er bara hægt að gera SELECT*/
        {
            string query = tb_customquery.Text;
            if (tb_customquery.Text == "")
            {
                MessageBox.Show("Please fill in the textbox");
            }

            else if (tb_customquery.Text.ToLower().Contains("update") || tb_customquery.Text.ToLower().Contains("delete") || tb_customquery.Text.ToLower().Contains("insert")) // geri tolower svo ég þurfi ekki að spá í þsesu
            {
                MessageBox.Show("You can only run SELECT commands");
            }
            else
            {
                try
                {
                    DataSet table = callDB.InfoToDataGrid(query);
                    dgv_admin_users.DataSource = table.Tables[0];

                }
                catch (Exception)
                {

                    MessageBox.Show("Error in your SQL statement");
                }
            }
        }

        public void dgv_admin_users_SelectionChanged(object sender, EventArgs e)
        {
            HideDynamic();
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            ClearLabels();
            for (int i = 0; i < labelarray.Count(); i++)
            {

                this.tab_admin.Controls.Add(labelarray[i]);
                this.tab_admin.Controls.Add(tbarray[i]);
            }

            if (dgv_admin_users.SelectedRows.Count > 0)
            {


                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {

                    string[] data;
                    data = new string[dgv_admin_users.ColumnCount];
                    string[] info;
                    info = new string[dgv_admin_users.ColumnCount];


                    info[i] = dgv_admin_users.Columns[i].Name;

                    data[i] = dgv_admin_users.SelectedRows[0].Cells[i].Value + string.Empty;

                    labelarray[i].Text = info[i].ToUpper();
                    labelarray[i].Visible = true;
                    if (labelarray[i].Text == "PASS")
                    {
                        tbarray[i].PasswordChar = '*';
                    }

                    tbarray[i].Text = data[i];
                    tbarray[i].Visible = true;

                }

            }

        }

        private void rb_users_CheckedChanged(object sender, EventArgs e)
        {
            DataSet table = callDB.InfoToDataGrid("SELECT user_id,name,username,access_level FROM users");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_clear_CheckedChanged(object sender, EventArgs e)
        {

            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
        }

        private void rb_showdev_CheckedChanged(object sender, EventArgs e)
        {
            DataSet table = callDB.InfoToDataGrid("SELECT * FROM Developers");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_show_devmemb_CheckedChanged(object sender, EventArgs e)
        {
            DataSet table = callDB.InfoToDataGrid("SELECT users.name, users.username, developers.name AS Developers,developermembers.dev_title FROM users JOIN DeveLOPERMEMBERS ON Developermembers.user_id = users.user_id JOIN developers ON  developers.dev_id = DEVELOPERmembers.dev_id");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_showloggedin_CheckedChanged(object sender, EventArgs e)
        {
            DataSet table = callDB.InfoToDataGrid("SELECT name, username,loggedin  FROM users WHERE loggedin = 1");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_showGames_CheckedChanged(object sender, EventArgs e)
        {
            /*SELECT game_id,Games.name,Developers.name AS Developers, path, dateadded, games.description , developers.description AS Dev.Description FROM games
             JOIN Developers ON games.dev_id = Developers.dev_id
              */
           
            DataSet table = callDB.InfoToDataGrid("SELECT game_id,Games.name,Developers.name AS Developers, path, dateadded, games.description  FROM games JOIN Developers ON games.dev_id = Developers.dev_id");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_show_dep_CheckedChanged(object sender, EventArgs e)
        {
            DataSet table = callDB.InfoToDataGrid("SELECT * FROM Departments");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_show_genres_CheckedChanged(object sender, EventArgs e)
        {
            DataSet table = callDB.InfoToDataGrid("SELECT * FROM Genres");
            dgv_admin_users.DataSource = table.Tables[0];
        }

        private void rb_add_user_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT dep_id,name,email,username,pass,access_level,title FROM USERS");
            dgv_admin_users.DataSource = table.Tables[0];
            HideDynamic();
            if (dgv_admin_users.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_admin_users.ColumnCount];
                string[] info;
                info = new string[dgv_admin_users.ColumnCount];
                info[i] = dgv_admin_users.Columns[i].Name;

                tbarray[i].Clear();

                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;
                if (labelarray[i].Text == "PASS")
                {
                    tbarray[i].PasswordChar = '*';

                }

                tbarray[i].Visible = true;

            }

            bt_executehelper = 1; //Segir hey þú ert í Add user
            bt_execute.Text = "Add User";
            bt_execute.Visible = true;


        }

        private void bt_execute_Click(object sender, EventArgs e)
        {
            
            if (bt_executehelper == 1) /* Add User */
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.CreateUser(Convert.ToInt32(text[0]), text[1], text[2], text[3], text[4], Convert.ToInt32(text[5]), text[6]);
                        rb_add_user.Checked = false;
                        bt_execute.Visible = false;
                        MessageBox.Show("Success");
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT dep_id,name,email,username,pass,access_level,title FROM USERS");
                        dgv_admin_users.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }

            }/*End if Add users*/

            if (bt_executehelper == 2) /*Begin Add Developers*/
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.CreateDeveloper(text[0], text[1]);
                        rb_add_dev.Checked = false;
                        bt_execute.Visible = false;
                        MessageBox.Show("Success");
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name,description FROM Developers");
                        dgv_admin_users.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }


            }/*End Add Developers*/

            if (bt_executehelper == 3) /*Add Departments*/
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.CreateDepartments(text[0]);
                        MessageBox.Show("Success");
                        rb_add_dep.Checked = false;
                        bt_execute.Visible = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name FROM Departments");
                        dgv_admin_users.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }

            } /*End Add Dep*/

            if (bt_executehelper == 4)/*Add Genre*/
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.CreateGenre(text[0]);
                        MessageBox.Show("Success");
                        rb_add_genre.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name FROM Genres");
                        dgv_admin_users.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }


            } /*End Add Genre*/
            
            if (bt_executehelper == 5) /*Add Game*/
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.AddGame(text[0], Convert.ToInt32(text[1]), text[2], text[3]);
                        MessageBox.Show("Success");
                        bt_execute.Visible = false;
                        rb_add_game.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name,description, ,dateadded dev_id,path,dateadded FROM games");
                        dgv_admin_users.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            } /*End Add Game*/
            if (bt_executehelper == 6) /*Update Users*/
            {
                string olduser = "";
                olduser = tb_dynamic4.Text;
                oldname = olduser;
                HideDynamic();
                bt_execute2.Visible = true;

                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                dgv_admin_users.DataSource = null;
                dgv_admin_users.Rows.Clear();
                DataSet table = callDB.InfoToDataGrid("SELECT dep_id,name,email,username,pass,access_level,loggedin,title FROM USERS WHERE username = '" + olduser + "'");
                dgv_admin_users.DataSource = table.Tables[0];
                
                if (dgv_admin_users.FirstDisplayedCell.Value + string.Empty == "")
                {
                    MessageBox.Show(olduser + " Does Not Exist");
                    rb_update_department.Checked = false;
                    rb_update_department.Checked = true;
                }
                else
                {
                    if (dgv_admin_users.ColumnCount > 13)
                    {
                        bt_execute.Location = new Point(466, 471);
                    }
                    else
                    {
                        bt_execute.Location = new Point(729, 469);
                    }
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {

                        string[] data;
                        data = new string[dgv_admin_users.ColumnCount];
                        string[] info;
                        info = new string[dgv_admin_users.ColumnCount];
                        info[i] = dgv_admin_users.Columns[i].Name;

                        dgv_admin_users.Rows[0].Selected = true;
                        data[i] = dgv_admin_users.SelectedRows[0].Cells[i].Value + string.Empty;

                        tbarray[i].Clear();

                        labelarray[i].Text = info[i].ToUpper();
                        labelarray[i].Visible = true;
                        tbarray[i].Text = data[i];
                        if (labelarray[i].Text == "PASS")
                        {
                            tbarray[i].PasswordChar = '*';

                        }
                        tbarray[i].Visible = true;

                    }

                    bt_execute.Visible = false;
                    bt_execute2.Visible = true;
                    bt_executehelper = 61;
                    bt_execute2.Text = "Update User";
                }


            } /*End Update Users*/
            if (bt_executehelper == 7) /*Update Departments*/
            {
                string oldep = "";
                oldep = tb_dynamic1.Text;
                oldname = oldep;
                

                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                dgv_admin_users.DataSource = null;
                dgv_admin_users.Rows.Clear();
                DataSet table = callDB.InfoToDataGrid("SELECT name FROM departments WHERE name = '" + oldep + "'");
                dgv_admin_users.DataSource = table.Tables[0];
                HideDynamic();
                bt_execute2.Visible = true;
                if (dgv_admin_users.FirstDisplayedCell.Value + string.Empty == "")
                {
                    MessageBox.Show(olduser + " Does Not Exist");
                    rb_update_department.Checked = false;
                    rb_update_department.Checked = true;
                }
                else
                {
                    if (dgv_admin_users.ColumnCount > 13)
                    {
                        bt_execute.Location = new Point(466, 471);
                    }
                    else
                    {
                        bt_execute.Location = new Point(729, 469);
                    }
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {

                        string[] data;
                        data = new string[dgv_admin_users.ColumnCount];
                        string[] info;
                        info = new string[dgv_admin_users.ColumnCount];
                        info[i] = dgv_admin_users.Columns[i].Name;

                        dgv_admin_users.Rows[0].Selected = true;
                        data[i] = dgv_admin_users.SelectedRows[0].Cells[i].Value + string.Empty;

                        tbarray[i].Clear();

                        labelarray[i].Text = info[i].ToUpper();
                        labelarray[i].Visible = true;
                        tbarray[i].Text = data[i];
                        if (labelarray[i].Text == "NAME")
                        {
                            labelarray[i].Text = "NEW NAME";

                        }
                        tbarray[i].Visible = true;

                    }

                    bt_execute.Visible = false;
                    bt_execute2.Visible = true;
                    bt_executehelper = 71;
                    bt_execute2.Text = "Update Department";
                }
            }/*End Update dept*/
            if (bt_executehelper == 8) /*Update Game*/
            {
                
                oldname = tb_dynamic1.Text;
               


                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                dgv_admin_users.DataSource = null;
                dgv_admin_users.Rows.Clear();
                DataSet table = callDB.InfoToDataGrid("SELECT name,dev_id,path,description FROM games WHERE name = '" + oldname + "'");
                dgv_admin_users.DataSource = table.Tables[0];
                HideDynamic();
                bt_execute2.Visible = true;
                if (dgv_admin_users.FirstDisplayedCell.Value + string.Empty == "")
                {
                    MessageBox.Show(olduser + " Does Not Exist");
                    rb_update_game.Checked = false;
                    rb_update_game.Checked = true;
                }
                else
                {
                    if (dgv_admin_users.ColumnCount > 13)
                    {
                        bt_execute.Location = new Point(466, 471);
                    }
                    else
                    {
                        bt_execute.Location = new Point(729, 469);
                    }
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {

                        string[] data;
                        data = new string[dgv_admin_users.ColumnCount];
                        string[] info;
                        info = new string[dgv_admin_users.ColumnCount];
                        info[i] = dgv_admin_users.Columns[i].Name;

                        dgv_admin_users.Rows[0].Selected = true;
                        data[i] = dgv_admin_users.SelectedRows[0].Cells[i].Value + string.Empty;

                        tbarray[i].Clear();

                        labelarray[i].Text = info[i].ToUpper();
                        labelarray[i].Visible = true;
                        tbarray[i].Text = data[i];
                       
                        tbarray[i].Visible = true;

                    }

                    bt_execute.Visible = false;
                    bt_execute2.Visible = true;
                    bt_executehelper = 81;
                    bt_execute2.Text = "Update Game Information";
                }
            }/*End Update Game*/
            if (bt_executehelper == 9) /*Update Developer*/
            {

                oldname = tb_dynamic1.Text;



                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                dgv_admin_users.DataSource = null;
                dgv_admin_users.Rows.Clear();
                DataSet table = callDB.InfoToDataGrid("SELECT name,description FROM Developers WHERE name = '" + oldname + "'");
                dgv_admin_users.DataSource = table.Tables[0];
                HideDynamic();
                bt_execute2.Visible = true;
                if (dgv_admin_users.FirstDisplayedCell.Value + string.Empty == "")
                {
                    MessageBox.Show(olduser + " Does Not Exist");
                    rb_update_developr.Checked = false;
                    rb_update_developr.Checked = true;
                }
                else
                {
                    if (dgv_admin_users.ColumnCount > 13)
                    {
                        bt_execute.Location = new Point(466, 471);
                    }
                    else
                    {
                        bt_execute.Location = new Point(729, 469);
                    }
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {

                        string[] data;
                        data = new string[dgv_admin_users.ColumnCount];
                        string[] info;
                        info = new string[dgv_admin_users.ColumnCount];
                        info[i] = dgv_admin_users.Columns[i].Name;

                        dgv_admin_users.Rows[0].Selected = true;
                        data[i] = dgv_admin_users.SelectedRows[0].Cells[i].Value + string.Empty;

                        tbarray[i].Clear();

                        labelarray[i].Text = info[i].ToUpper();
                        labelarray[i].Visible = true;
                        tbarray[i].Text = data[i];

                        tbarray[i].Visible = true;

                    }

                    bt_execute.Visible = false;
                    bt_execute2.Visible = true;
                    bt_executehelper = 91;
                    bt_execute2.Text = "Update Developer";
                }
            }/*End Update Developer*/
            if (bt_executehelper == 10) /*Update Genre*/
            {

                oldname = tb_dynamic1.Text;



                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                dgv_admin_users.DataSource = null;
                dgv_admin_users.Rows.Clear();
                DataSet table = callDB.InfoToDataGrid("SELECT name FROM genres WHERE name = '" + oldname + "'");
                dgv_admin_users.DataSource = table.Tables[0];
                HideDynamic();
                bt_execute2.Visible = true;
                if (dgv_admin_users.FirstDisplayedCell.Value + string.Empty == "")
                {
                    MessageBox.Show(olduser + " Does Not Exist");
                    rb_update_developr.Checked = false;
                    rb_update_developr.Checked = true;
                }
                else
                {
                    if (dgv_admin_users.ColumnCount > 13)
                    {
                        bt_execute.Location = new Point(466, 471);
                    }
                    else
                    {
                        bt_execute.Location = new Point(729, 469);
                    }
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {

                        string[] data;
                        data = new string[dgv_admin_users.ColumnCount];
                        string[] info;
                        info = new string[dgv_admin_users.ColumnCount];
                        info[i] = dgv_admin_users.Columns[i].Name;

                        dgv_admin_users.Rows[0].Selected = true;
                        data[i] = dgv_admin_users.SelectedRows[0].Cells[i].Value + string.Empty;

                        tbarray[i].Clear();

                        labelarray[i].Text = info[i].ToUpper();
                        labelarray[i].Visible = true;
                        tbarray[i].Text = data[i];

                        tbarray[i].Visible = true;

                    }

                    bt_execute.Visible = false;
                    bt_execute2.Visible = true;
                    bt_executehelper = 101;
                    bt_execute2.Text = "Update Genre";
                }
            }/*End Update Genre*/
            if (bt_executehelper == 11)/*Delete Genre*/
            {
                DialogResult result1 = MessageBox.Show("Confirm: Delete "+tb_dynamic1.Text+" Permanently?", "Welcome Box", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {


                    TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                    string[] text = new string[10];
                    bool error = false;
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        tbarray[i].BackColor = Color.White;
                    }

                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        text[i] = tbarray[i].Text;
                        if (tbarray[i].Text == "")
                        {
                            tbarray[i].BackColor = Color.Red;
                            error = true;

                        }

                    }
                    if (error == true)
                    {
                        MessageBox.Show("Please fill inn all the boxes");
                    }
                    else
                    {

                        try
                        {
                            callDB.DeleteGenre(text[0]);
                            MessageBox.Show("Success");
                            rb_delete_genre.Checked = false;
                            bt_execute.Visible = false;
                            dgv_admin_users.DataSource = null;
                            dgv_admin_users.Rows.Clear();
                            DataSet table = callDB.InfoToDataGrid("SELECT name FROM Genres");
                            dgv_admin_users.DataSource = table.Tables[0];
                        }
                        catch (MySqlException ex)
                        {
                            callDB.CloseConnection();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Deletion Canceled");
                }
            }/*End Delete Genre*/
            if (bt_executehelper == 12)/*Delete user*/
            {
                DialogResult result1 = MessageBox.Show("Confirm: Delete " + tb_dynamic1.Text + " Permanently?", "Welcome Box", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {


                    TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                    string[] text = new string[10];
                    bool error = false;
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        tbarray[i].BackColor = Color.White;
                    }

                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        text[i] = tbarray[i].Text;
                        if (tbarray[i].Text == "")
                        {
                            tbarray[i].BackColor = Color.Red;
                            error = true;

                        }

                    }
                    if (error == true)
                    {
                        MessageBox.Show("Please fill inn all the boxes");
                    }
                    else
                    {

                        try
                        {
                            callDB.DeleteUser(text[0]);
                            MessageBox.Show("Success");
                            rb_delete_user.Checked = false;
                            bt_execute.Visible = false;
                            dgv_admin_users.DataSource = null;
                            dgv_admin_users.Rows.Clear();
                            DataSet table = callDB.InfoToDataGrid("SELECT dep_id,name,email,username,pass,access_level,title FROM USERS");
                            dgv_admin_users.DataSource = table.Tables[0];
                            
                        }
                        catch (MySqlException ex)
                        {
                            callDB.CloseConnection();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Deletion Canceled");
                }
            }/*End Delete User*/
            if (bt_executehelper == 13)/*Delete Game*/
            {
                DialogResult result1 = MessageBox.Show("Confirm: Delete " + tb_dynamic1.Text + " Permanently?", "Welcome Box", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {


                    TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                    string[] text = new string[10];
                    bool error = false;
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        tbarray[i].BackColor = Color.White;
                    }

                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        text[i] = tbarray[i].Text;
                        if (tbarray[i].Text == "")
                        {
                            tbarray[i].BackColor = Color.Red;
                            error = true;

                        }

                    }
                    if (error == true)
                    {
                        MessageBox.Show("Please fill inn all the boxes");
                    }
                    else
                    {

                        try
                        {
                            callDB.DeleteGame(text[0]);
                            MessageBox.Show("Success");
                            bt_execute.Visible = false;
                            rb_delete_game.Checked = false;
                            dgv_admin_users.DataSource = null;
                            dgv_admin_users.Rows.Clear();
                            DataSet table = callDB.InfoToDataGrid("SELECT name,description,dev_id,path,dateadded FROM games");
                            dgv_admin_users.DataSource = table.Tables[0];
                        }
                        catch (MySqlException ex)
                        {
                            callDB.CloseConnection();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Deletion Canceled");
                }
            }/*End Delete Game*/
            if (bt_executehelper == 14)/*Delete Developer*/
            {
                DialogResult result1 = MessageBox.Show("Confirm: Delete " + tb_dynamic1.Text + " Permanently?", "Welcome Box", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {


                    TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                    string[] text = new string[10];
                    bool error = false;
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        tbarray[i].BackColor = Color.White;
                    }

                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        text[i] = tbarray[i].Text;
                        if (tbarray[i].Text == "")
                        {
                            tbarray[i].BackColor = Color.Red;
                            error = true;

                        }

                    }
                    if (error == true)
                    {
                        MessageBox.Show("Please fill inn all the boxes");
                    }
                    else
                    {

                        try
                        {
                            callDB.DeleteDeveloper(text[0]);
                            MessageBox.Show("Success");
                            rb_delete_developer.Checked = false;
                            bt_execute.Visible = false;
                            dgv_admin_users.DataSource = null;
                            dgv_admin_users.Rows.Clear();
                            DataSet table = callDB.InfoToDataGrid("SELECT name,description FROM Developers");
                            dgv_admin_users.DataSource = table.Tables[0];
                        }
                        catch (MySqlException ex)
                        {
                            callDB.CloseConnection();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Deletion Canceled");
                }
            }/*End Delete Developer*/
            if (bt_executehelper == 15)/*Delete Department*/
            {
                DialogResult result1 = MessageBox.Show("Confirm: Delete " + tb_dynamic1.Text + " Permanently?", "Welcome Box", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {


                    TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                    string[] text = new string[10];
                    bool error = false;
                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        tbarray[i].BackColor = Color.White;
                    }

                    for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                    {
                        text[i] = tbarray[i].Text;
                        if (tbarray[i].Text == "")
                        {
                            tbarray[i].BackColor = Color.Red;
                            error = true;

                        }

                    }
                    if (error == true)
                    {
                        MessageBox.Show("Please fill inn all the boxes");
                    }
                    else
                    {

                        try
                        {
                            callDB.DeleteDepartments(text[0]);
                            MessageBox.Show("Success");
                            bt_execute.Visible = false;
                            rb_delete_department.Checked = false;
                            dgv_admin_users.DataSource = null;
                            dgv_admin_users.Rows.Clear();
                            DataSet table = callDB.InfoToDataGrid("SELECT name FROM Departments");
                            dgv_admin_users.DataSource = table.Tables[0];
                        }
                        catch (MySqlException ex)
                        {
                            callDB.CloseConnection();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Deletion Canceled");
                }
            }/*End Delete Department*/

            if (bt_executehelper == 16) /* Add dev_member */
            {
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.AddDevMember(Convert.ToInt32(text[0]), Convert.ToInt32(text[1]), text[2]);
                        rb_adddevmember.Checked = false;
                        bt_execute.Visible = false;
                        MessageBox.Show("Success");
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT user_id,dev_id,title FROM developermembers");
                        dgv_admin_users.DataSource = table.Tables[0];
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }

            }/* end Add dev_member */

        }

        private void rb_add_dev_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name,description FROM Developers");
            dgv_admin_users.DataSource = table.Tables[0];
            HideDynamic();
            if (dgv_admin_users.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_admin_users.ColumnCount];
                string[] info;
                info = new string[dgv_admin_users.ColumnCount];
                info[i] = dgv_admin_users.Columns[i].Name;

                tbarray[i].Clear();

                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;

                tbarray[i].Visible = true;

            }

            bt_executehelper = 2; //Segir hey þú ert í Add Developers
            bt_execute.Text = "Add Developers";
            bt_execute.Visible = true;
        }

        private void rb_add_dep_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM Departments");
            dgv_admin_users.DataSource = table.Tables[0];
            HideDynamic();
            if (dgv_admin_users.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_admin_users.ColumnCount];
                string[] info;
                info = new string[dgv_admin_users.ColumnCount];
                info[i] = dgv_admin_users.Columns[i].Name;

                tbarray[i].Clear();

                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;

                tbarray[i].Visible = true;

            }

            bt_executehelper = 3; //Segir hey þú ert í Add Departments
            bt_execute.Text = "Add Department";
            bt_execute.Visible = true;
        }

        private void rb_add_genre_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM genres");
            dgv_admin_users.DataSource = table.Tables[0];
            HideDynamic();
            if (dgv_admin_users.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_admin_users.ColumnCount];
                string[] info;
                info = new string[dgv_admin_users.ColumnCount];
                info[i] = dgv_admin_users.Columns[i].Name;

                tbarray[i].Clear();

                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;

                tbarray[i].Visible = true;

            }

            bt_executehelper = 4; //Segir hey þú ert í Add Genre
            bt_execute.Text = "Add Genre";
            bt_execute.Visible = true;
        }

        private void rb_add_game_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name,dev_id,path,description FROM games");
            dgv_admin_users.DataSource = table.Tables[0];
            HideDynamic();
            if (dgv_admin_users.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_admin_users.ColumnCount];
                string[] info;
                info = new string[dgv_admin_users.ColumnCount];
                info[i] = dgv_admin_users.Columns[i].Name;

                tbarray[i].Clear();

                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;

                tbarray[i].Visible = true;

            }

            bt_executehelper = 5; //Segir hey þú ert í Add Game
            bt_execute.Text = "Add Game";
            bt_execute.Visible = true;

        }

        private void rb_update_user_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT dep_id,name,email,username,pass,access_level,loggedin,title FROM USERS");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Find User";
            labelarray[0].Text = "Enter Username";

            bt_executehelper = 6; //Segir hey þú ert í Update User


        }

        private void bt_execute2_Click(object sender, EventArgs e)
        {

            if (bt_executehelper == 61) /*Update Users Continues*/
            {
                

                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
               
               
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }
                    

                  

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.UpdateUsers(Convert.ToInt32(text[0]), text[1], text[2], text[3], text[4], Convert.ToInt32(text[5]), Convert.ToInt32(text[6]), text[7], oldname);
                        MessageBox.Show("Success");
                        rb_update_user.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT dep_id,name,email,username,pass,access_level,loggedin,title FROM USERS");
                        dgv_admin_users.DataSource = table.Tables[0];
                        bt_execute2.Visible = false;
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            }/*Update Users Ends*/
            if (bt_executehelper == 71) /*Update Departments Continues*/
            {
                
                

                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };


                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }
                    
                    

                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.UpdateDepartment(text[0], oldname);
                        MessageBox.Show("Success");
                        rb_update_department.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name FROM Departments");
                        dgv_admin_users.DataSource = table.Tables[0];
                        bt_execute2.Visible = false;
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            }/*Update Departments Ends*/
            if (bt_executehelper == 81) /*Update Games Continues*/
            {
                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }



                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.UpdateGame(text[0], Convert.ToInt32(text[1]), text[2], text[3], oldname);
                        MessageBox.Show("Success");
                        rb_update_game.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name,dev_id,path,description FROM games");
                        dgv_admin_users.DataSource = table.Tables[0];
                        bt_execute2.Visible = false;
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            }/*Update Games Ends*/
            if (bt_executehelper == 91) /*Update Developer Continues*/
            {
                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }
                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.UpdateDeveloper(text[0], text[1], oldname);
                        MessageBox.Show("Success");
                        rb_update_developr.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name,dev_id,description FROM Developers");
                        dgv_admin_users.DataSource = table.Tables[0];
                        bt_execute2.Visible = false;
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            }/*Update Developer Ends*/
            if (bt_executehelper == 101) /*Update Developer Continues*/
            {
                Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
                TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
                string[] text = new string[10];
                bool error = false;
                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    tbarray[i].BackColor = Color.White;
                }

                for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
                {
                    text[i] = tbarray[i].Text;
                    if (tbarray[i].Text == "")
                    {
                        tbarray[i].BackColor = Color.Red;
                        error = true;

                    }
                }
                if (error == true)
                {
                    MessageBox.Show("Please fill inn all the boxes");
                }
                else
                {

                    try
                    {
                        callDB.UpdateGenre(text[0], oldname);
                        MessageBox.Show("Success");
                        rb_update_genre.Checked = false;
                        dgv_admin_users.DataSource = null;
                        dgv_admin_users.Rows.Clear();
                        DataSet table = callDB.InfoToDataGrid("SELECT name FROM genres");
                        dgv_admin_users.DataSource = table.Tables[0];
                        bt_execute2.Visible = false;
                    }
                    catch (MySqlException ex)
                    {
                        callDB.CloseConnection();
                        MessageBox.Show(ex.Message);
                    }
                }
            }/*Update Developer Ends*/

        }

        private void rb_update_department_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM departments");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Find Department";
            labelarray[0].Text = "Department Name";

            bt_executehelper = 7; //Segir hey þú ert í Update Department

        }

        private void rb_update_game_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name,dev_id,path,description FROM games");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Find Game";
            labelarray[0].Text = "Game Name";

            bt_executehelper = 8; //Segir hey þú ert í Update Game
        }

        private void rb_update_developr_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name,description FROM Developers");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Find Developer";
            labelarray[0].Text = "Developer Name";

            bt_executehelper = 9; //Segir hey þú ert í Update Developer
        }

        private void rb_update_genre_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM genres");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Find Genre";
            labelarray[0].Text = "Genre Name";

            bt_executehelper = 10; //Segir hey þú ert í Update Developer
        }

        private void rb_delete_genre_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM genres");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Delete Genre";
            labelarray[0].Text = "Genre Name";

            bt_executehelper = 11; //Segir hey þú ert í DELETE Genre


        }

        private void rb_delete_user_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT username FROM users");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Delete User";
            labelarray[0].Text = "Username";

            bt_executehelper = 12; //Segir hey þú ert í DELETE User
        }

        private void rb_delete_game_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM games");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Delete Game";
            labelarray[0].Text = "Game";

            bt_executehelper = 13; //Segir hey þú ert í DELETE User
        }

        private void rb_delete_developer_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name,description FROM Developers");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Delete Developer";
            labelarray[0].Text = "Developer";

            bt_executehelper = 14; //Segir hey þú ert í DELETE Developer
        }

        private void rb_delete_department_CheckedChanged(object sender, EventArgs e)
        {
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            for (int i = 0; i < labelarray.Length; i++)
            {
                labelarray[i].Text = "";
                tbarray[i].Text = "";
            }
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT name FROM Departments");
            HideDynamic();
            dgv_admin_users.DataSource = table.Tables[0];
            labelarray[0].Visible = true;
            tbarray[0].Visible = true;
            bt_execute.Visible = true;
            bt_execute.Text = "Delete Department";
            labelarray[0].Text = "Department";

            bt_executehelper = 15; //Segir hey þú ert í DELETE Department

        }

        private void bt_logout_Click(object sender, EventArgs e)
        {
           int userloggedin = callDB.IsUserLoggedIN(LoginWindow.userInfo[0]);
            if (userloggedin == 1)
            {
                try
                {
                    callDB.UserLoggedinout(0, LoginWindow.userInfo[0]);
                    MessageBox.Show(LoginWindow.userInfo[0] + " Logged Out");
                    bt_logout.Text = "Start Working";
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else if (userloggedin == 0)
            {
                try
                {
                    callDB.UserLoggedinout(1, LoginWindow.userInfo[0]);
                    MessageBox.Show(LoginWindow.userInfo[0] + " Logged In");
                    bt_logout.Text = "Quit Working";
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void rb_adddevmember_CheckedChanged(object sender, EventArgs e)
        {
            HideDynamic();
            Label[] labelarray = { label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16 };
            TextBox[] tbarray = { tb_dynamic1, tb_dynamic2, tb_dynamic3, tb_dynamic4, tb_dynamic5, tb_dynamic6, tb_dynamic7, tb_dynamic8, tb_dynamic9, tb_dynamic10, tb_dynamic11, tb_dynamic12, tb_dynamic13, tb_dynamic14, tb_dynamic15 };
            dgv_admin_users.DataSource = null;
            dgv_admin_users.Rows.Clear();
            DataSet table = callDB.InfoToDataGrid("SELECT user_id,dev_id,dev_title FROM developermembers");
            dgv_admin_users.DataSource = table.Tables[0];
            
            if (dgv_admin_users.ColumnCount > 13)
            {
                bt_execute.Location = new Point(466, 471);
            }
            else
            {
                bt_execute.Location = new Point(729, 469);
            }
            for (int i = 0; i < dgv_admin_users.ColumnCount; i++)
            {

                string[] data;
                data = new string[dgv_admin_users.ColumnCount];
                string[] info;
                info = new string[dgv_admin_users.ColumnCount];
                info[i] = dgv_admin_users.Columns[i].Name;

                tbarray[i].Clear();

                labelarray[i].Text = info[i].ToUpper();
                labelarray[i].Visible = true;
                if (labelarray[i].Text == "PASS")
                {
                    tbarray[i].PasswordChar = '*';

                }

                tbarray[i].Visible = true;

            }

            bt_executehelper = 16; //Segir hey þú ert í Add Dev.Member
            bt_execute.Text = "Add Dev.Member";
            bt_execute.Visible = true;

        }
    }
    /*Admin Panel End*/
}
