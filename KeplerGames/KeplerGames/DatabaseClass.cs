using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using MySql.Data.MySqlClient; /*Þetta verður að vera tilstaðar til að fá hluti eins og MySQL innbygðu föllinn*/


namespace KeplerGames /*HÖF: Ólafur Jón Valgeirsson*/
{
    class DatabaseClass /*Tekið úr gömlum verkefnum og útfært á ensku*/
    {
        public static int clearance;
        public static string userloggedin;
        private string server; 
        private string database;
        private string user;
        private string password;
        string connection = null;
        string query = null;

        MySqlConnection sqlconnection;
        MySqlCommand sqlcommand;
        MySqlDataReader sqlreader;
        MySqlDataAdapter Datagetter = new MySqlDataAdapter();
        public DatabaseClass()
        {
            Initialize();
        }

        public void Initialize()
        {
            server = "tsuts.tskoli.is";
            database = "3010943379_kepler_games";
            user = "3010943379";
            password = "mypassword";
            connection = "Server=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";
            
            sqlconnection = new MySqlConnection(connection);
   
        }

       

        private bool OpenConnection()
        {
            try
            {
                sqlconnection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                { 
                    case 0:
                        MessageBox.Show("Cannot Contact the Server. Contact Administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid Username/Password, please try again!");
                        break;
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                sqlconnection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;


            }
        }

        public void LogUserIn(string user)
            {
                if (OpenConnection() == true)
                {
                    
                    query = "UPDATE Users SET LoggedIn = 1 WHERE username = user";
                    sqlcommand = new MySqlCommand(query, sqlconnection);
                    sqlcommand.ExecuteNonQuery();
                    CloseConnection();
                }
                
            } /*Ekki í noktun, notaðist sem smá upprifjun*/

        public int GetUserAccessLevel(string username)
            {
                if (OpenConnection() == true)
                {
                    query = "Select access_level FROM users WHERE username ='@username'";
                    sqlcommand = new MySqlCommand(query, sqlconnection);
                    sqlcommand.Parameters.AddWithValue("username", username);
                    sqlreader = sqlcommand.ExecuteReader();
                    while (sqlreader.Read())
                    {
                        clearance = Convert.ToInt32(sqlreader.GetValue(0));
                    }
                    MessageBox.Show("" + clearance);
                    CloseConnection();
                    return clearance;
                    
                    
                }
                else 
                {
                    MessageBox.Show("Error Occurred Minimum Acces level granted");
                    CloseConnection();
                    return 1;
                    
                }


        }/*Ekki í noktun, notaðist sem smá upprifjun*/

        public string[] Userloggingin(string username, string password)
            {
                string[] userInfo;
                 userInfo = new string[3];
                
                if (OpenConnection() == true)
                {
                    
                   
                    
                    query = "SELECT username,pass,access_level FROM users WHERE username =@username AND pass=@password";
                    sqlcommand = new MySqlCommand(query, sqlconnection);
                    sqlcommand.Parameters.AddWithValue("username", username);
                    sqlcommand.Parameters.AddWithValue("password", password);
                    sqlreader = sqlcommand.ExecuteReader();
                    int teljari = 0;
                    while (sqlreader.Read())
                    {
                        userInfo[0] = Convert.ToString(sqlreader.GetValue(0));
                        userInfo[1] = Convert.ToString(sqlreader.GetValue(1));
                        userInfo[2] = Convert.ToString(sqlreader.GetValue(2));
                        clearance = Convert.ToInt32(sqlreader.GetValue(2));
                        teljari += 1;
                    }
                    userloggedin = username;
                    
                    if (teljari == 1 && clearance > 0)
                    {
                        
                        DialogResult result1 = MessageBox.Show("Welcome "+username +" Do you want to start working?","Welcome Box",MessageBoxButtons.YesNo);
                        
                        
                        if (result1 == DialogResult.Yes)
                        {
                            CloseConnection();
                            OpenConnection();

                            query = "UPDATE Users SET LoggedIn = 1 WHERE username =@username";
                            sqlcommand = new MySqlCommand(query, sqlconnection);
                            sqlcommand.Parameters.AddWithValue("username", username);
                            sqlcommand.ExecuteNonQuery();
                        }
                        return userInfo;
                    
                       
                    }
                    else if(teljari == 1)
                    {
                        
                      MessageBox.Show("Welcome " + username,"Welcome Box");
                      return userInfo;
                        
                    }
                    else if (teljari > 0)
                    {
                        MessageBox.Show("Logon Failed try again");
                        userInfo[1] = "False";
                        return userInfo;
                    }
                    else { MessageBox.Show("Username or Password incorrect");
                    userInfo[1] = "False";
                    CloseConnection();
                    return userInfo;
                    }
                    
                    
                }
                userInfo[1] = "False";
                return userInfo;
        } /*Var með þetta öðruvísi en þótti þetta síðan bara þægileg leið að vera með þetta í Array og vera með public breytu svo ég gæti náð í Login upplýsingarnar annarstaðar*/

        public DataSet InfoToDataGrid(string query) /*Þægilegt query getur verið hvaða SQL query sem er, Update, insert, select þá þarf ekki að eiga fullt af functionum*/
        {
         
                MySqlDataAdapter infoToDataGrid = new MySqlDataAdapter();
                infoToDataGrid = new MySqlDataAdapter(query, sqlconnection);
                DataSet DS = new DataSet();
                infoToDataGrid.Fill(DS);
                CloseConnection();
                return DS;
                
        }

        public void CreateUser(int dep_id, string name, string email, string username, string pass, int access_level, string title)
        {
            DateTime date = DateTime.Now;
            date.ToUniversalTime();
            if (OpenConnection() == true)
            {
                query = "Call UsersCreate(@dep_id, @name, @email, @username, @pass, @access_level, @title)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("dep_id", dep_id);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("email", email);
                sqlcommand.Parameters.AddWithValue("username", username);
                sqlcommand.Parameters.AddWithValue("pass", pass);
                sqlcommand.Parameters.AddWithValue("access_level", access_level);
                sqlcommand.Parameters.AddWithValue("title", title);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*Callar í create user aðferðina */

        public void CreateDepartments(string name) /*Útskýrir sig sjálft...ADMINPANEL*/
        {
            if (OpenConnection() == true)
            {
                query = "INSERT INTO departments(name) VALUES (@name)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }

        public void CreateDeveloper(string name, string desc)
        {
            
            if (OpenConnection() == true)
            {
                query = "INSERT INTO developers(name, description) VALUES(@name, @desc)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("desc", desc);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Create Developer ADMINPANEL*/

        public void CreateGenre(string name)
        {
            if (OpenConnection() == true)
            {
                query = "INSERT INTO Genres(name) VALUES (@name)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*Býr til Genre ef svo þess, ADMINPANEL*/

        public void AddGame(string name, int dev_id, string path, string description)
        {
            
            DateTime date = DateTime.Now;
            if (OpenConnection() == true)
            {
                query = "CALL GamesCreate(@name, @dev_id, @path, @description)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("dev_id", dev_id);
                sqlcommand.Parameters.AddWithValue("path", path);
                sqlcommand.Parameters.AddWithValue("description", description);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*Býr til leiki og hérna manually seturmaður dev_id, ADMINPANEL*/

        public void UpdateUsers(int depid,string name, string email, string username, string pass, int access_level, int logged, string title, string oldusername)
        {
            if (OpenConnection() == true)
            {
                try
                {

              
                query = "CALL UsersUpdate(@dep_id, @name, @email, @username, @pass, @access_level, @title, @oldusername)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("dep_id", depid);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("email", email);
                sqlcommand.Parameters.AddWithValue("username", username);
                sqlcommand.Parameters.AddWithValue("pass", pass);
                sqlcommand.Parameters.AddWithValue("access_level", access_level);
                sqlcommand.Parameters.AddWithValue("title", title);
                sqlcommand.Parameters.AddWithValue("oldusername", oldusername);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                    CloseConnection();
                }
            }
            CloseConnection();
        } /*Uppfærir users, ADMINPANEL*/

        public void UpdateDepartment(string name, string oldname)
        {
            if (OpenConnection() == true)
            {
                query = "UPDATE Departments SET name=@name WHERE name =@oldname";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("oldname", oldname);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Uppfærir Department, ADMINPANEL*/

        public void UpdateGame(string name, int dev_id, string path, string desc, string oldname)
        {
            if (OpenConnection() == true)
            {
                query = "CALL GamesUpdate(@dev_id, @name, @path, @desc, @oldname)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("dev_id", dev_id);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("path", path);
                sqlcommand.Parameters.AddWithValue("desc", desc);
                sqlcommand.Parameters.AddWithValue("oldname", oldname);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*Útskýrir sig sjálft, ADMINPANEL*/

        public void UpdateDeveloper(string name,string desc, string oldname)
        {
            if (OpenConnection() == true)
            {
                query = "CALL DevelopersUpdate(@name, @desc, @oldname)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("desc", desc);
                sqlcommand.Parameters.AddWithValue("oldname", oldname);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*Útskýrir sig sjálft, ADMINPANEL*/

        public void UpdateGenre(string name, string oldname)
        {
            if (OpenConnection() == true)
            {
                query = "UPDATE Genres SET name=@name WHERE name =@oldname";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("oldname", oldname);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Útskýrir sig sjálft, ADMINPANEL*/

        public void DeleteGenre(string name)
        {
            if (OpenConnection() == true)
            {
                query = "DELETE FROM `genres` WHERE name =@name";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Útskýrir sig sjálft, ADMINPANEL*/

        public void DeleteUser(string username)
        {
            if (OpenConnection() == true)
            {
                query = "CALL UsersDelete(@username)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("username", username);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Útskýrir sig sjálft, ADMINPANEL*/

        public void DeleteGame(string name)
        {
            if (OpenConnection() == true)
            {
                query = "CALL GamesDelete(@name)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Útskýrir sig sjálft, ADMINPANEL*/

        public void DeleteDeveloper(string name)
        {
            if (OpenConnection() == true)
            {
                query = "DELETE FROM `Developers` WHERE name =@name";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Útskýrir sig sjálft, ADMINPANEL*/

        public void DeleteDepartments(string name)
        {
            if (OpenConnection() == true)
            {
                query = "DELETE FROM `Departments` WHERE name =@name";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }/*Útskýrir sig sjálft, ADMINPANEL*/

        public void PostReview(string gamename ,string user, string comment)
        {
            if (OpenConnection() == true)
            {

                query = "CALL PostReview(@username, @gamename, @comment)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("username", user);
                sqlcommand.Parameters.AddWithValue("gamename", gamename);
                sqlcommand.Parameters.AddWithValue("comment", comment);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
                

        }

        public void AddGameProgrammer(string name, string path, string description, string user)
        {

            DateTime date = DateTime.Now;
            int devid = 0;
            if (OpenConnection() == true)
            {
                /*SELECT developers.dev_id FROM Developers
                    JOIN Developermembers ON developermembers.dev_id = developers.dev_id
                    JOIN users ON Developermembers.user_id = users.user_id
                    WHERE Username = 'OlaVal'*/
                query = "SELECT developers.dev_id FROM Developers JOIN Developermembers ON developermembers.dev_id = developers.dev_id  JOIN users ON Developermembers.user_id = users.user_id WHERE username =@user";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("user", user);
                sqlreader = sqlcommand.ExecuteReader();
                while (sqlreader.Read())
                {
                    devid = Convert.ToInt32(sqlreader.GetValue(0));

                }
                CloseConnection();
                OpenConnection();
                query = "INSERT INTO `games`( `name`, `dev_id`, `path`, `dateadded`, `description`) VALUES(@name, @dev_id, @path, '" + date.ToString(string.Format("yy/MM/dd hh:mm:ss")) + "',@description)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("name", name);
                sqlcommand.Parameters.AddWithValue("dev_id", devid);
                sqlcommand.Parameters.AddWithValue("path", path);
                sqlcommand.Parameters.AddWithValue("description", description);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*Finnur ID á forritaranum svo hann þarf bara að setja pathið, Programmer Panel*/

        public void UserLoggedinout(int x, string username)
        {
            if (OpenConnection() == true)
            {
                query = "UPDATE Users SET LoggedIn = @x WHERE username = @username";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("x", x);
                sqlcommand.Parameters.AddWithValue("username", username);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }

        public int IsUserLoggedIN(string username)
        {
            int status = 3;
            if (OpenConnection() == true)
            {
                query = "SELECT LoggedIn FROM users WHERE username =@username";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("username", username);
                sqlreader = sqlcommand.ExecuteReader();
                while (sqlreader.Read())
                {
                    status = Convert.ToInt32(sqlreader.GetValue(0));
                   
                }
                CloseConnection();
                return status;
            }
            CloseConnection();
            return status;
        }

        public void AddDevMember(int user_id, int dev_id, string title)
        {
           
            if (OpenConnection() == true)
            {
                query = "INSERT INTO `developermembers`(`user_id`, `dev_id`, `dev_title`) VALUES (@user_id,@dev_id,@title)";
                sqlcommand = new MySqlCommand(query, sqlconnection);
                sqlcommand.Parameters.AddWithValue("user_id", user_id);
                sqlcommand.Parameters.AddWithValue("dev_id", dev_id);
                sqlcommand.Parameters.AddWithValue("dev_title", title);
                sqlcommand.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        } /*INSERT DEV*/

    }
}
