using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;



namespace KeplerGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseClass callDB = new DatabaseClass();
        UserInterface userInterface = new UserInterface();

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                callDB.Initialize();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public static string uniuser;
        public string;

        private void bt_login_login_Click(object sender, RoutedEventArgs e)
        {
            string user = tb_login_username.Text;
            string pass = pwb_login_password.Password;
            MessageBox.Show(pass);

            if (user == "" || pass == "")
            {
                MessageBox.Show("You need to fill in the blanks");

            }
            else
            {
                try
                {
                    callDB.Userloggingin(user, pass);
                    uniuser = user;
                    unipass = pass;
                    userInterface.Show();
                    base.Hide();
                    
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
            

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            Application.Current.Shutdown();
        }


        










    }
}
