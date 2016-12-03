/*glósur fyrir að implementa SQL með c#
Gagnagrunnsklasi /tekinn úr GRU verkefni áður en maður lærði betur að búa til snyrtilegan kóða,  
maður þarf að vera með MYSQL dll skránna og using MySql.Data.MySqlClient; tilstaðar

*/

    class Gagnagrunnur //*** Gagnagrunns classinn er meira og minna bara tekin úr gömlum verkefnum sem við gerðum í FOR og ég bara bætti við klössum fyrir neðan
    {

        
        private string server;
        private string database;
        private string uid;
        private string password;
        string tengistrengur = null;
        string fyrirspurn = null;
       
        MySqlConnection sqltenging;
        MySqlCommand nySQLskipun;
        MySqlDataReader sqllesari = null;

        public Gagnagrunnur()
        {
            Initialize();
        }
        public void Initialize() /* Hérna þarf að muna ef maður ætlar að vinna með annan gagnagrunn þá þarf maður að uppfæra þessar upplýsingar */
        {
            server = "tsuts.tskoli.is";
            database = "gru_h10_gru";
            uid = "GRU_H10";
            password = "mypassword";
            tengistrengur = "Server=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            sqltenging = new MySqlConnection(tengistrengur);
        }

        private bool OpenConnection()
        {
            try
            {
                sqltenging.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid Username/password, please try again!");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                sqltenging.Close();
                return true;
            }

            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                return false;

            }
        }

        public void Nyskraning(string nafn, string simi, string email, string username, string password) //*** Klasinn sem sér um að ´stofna notanda inní gagnagrunninnn
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "INSERT INTO notandi(Nafn,Simi,Netfang,Notendanafn,Lykilorð) VALUES ('" + nafn + "','" + simi + "','" + email + "','" + username + "','" + password + "')";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
               
                    

                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
            }
            else
            {
                MessageBox.Show("Nýskráning tókst ekki");
            }
        }

        public void Skravidburd(string nafn, string salur, string time, string timeends, string date, string desc, int midar) //*** þessi græjar viðburðinn og stofnar
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "INSERT INTO vidburdir(Nafn,Salur,Time,Timeends,Date,Description, Miðar) VALUES ('" + nafn + "','" + salur + "','" + time + "','" + timeends + "','" + date + "','" + desc + "','" + midar + "')";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
               
                MessageBox.Show("Skrá viðburð tókst!");

                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
            }
            else
            {
                MessageBox.Show("Villa kom upp, viðburður ekki skráður");
            }
        }



   



        public void skra_i_vidburd(int userid, int eventid) //*** Þesi reddar því þannig að þar sem hann skráir í samtenging grunnin tekur userid og eventid og þá er notandinn kominn í gagnagrunninn, síðan er Updateskipun sem keyrist í kjölfarið sem mínusar 1 frá miðunum á viðnburðinn þannig ef það voru til 400 miðar þá þegar notandi skráir sig dettur það niður í 399
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "INSERT INTO samtenging(Id_Notandi, Id_Vidburdir) VALUES('" + userid + "','" + eventid + "');Update vidburdir SET miðar = miðar - 1 WHERE id =" + eventid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);

                
                Logingluggi login = new Logingluggi();
                login.Hide();

                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
            }
            CloseConnection();
        }

        public void skra_i_vidburd2(string username, int eventid) //Þessi er síðan notaður ef ég get ekki tekið userid úr textboxi þá finnur hann id þar sem usernameið er string, uppfærist miðafjöldin það fer 1 frá honum svo miðarnir séu réttir og ef miðarnir verða 0 þá getur hann ekkert fengið miða
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "INSERT INTO samtenging(Id_Notandi, Id_Vidburdir) VALUES((SELECT Id FROM Notandi WHERE Notendanafn='" + username + "'),'" + eventid + "');Update vidburdir SET miðar = miðar - 1 WHERE id =" + eventid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);

                
                Logingluggi login = new Logingluggi();
                login.Hide();

                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
            }
            else
            {
                MessageBox.Show("Skráning tókst ekki");
            }
            CloseConnection();
        }



              public void Innskraningadmin(string user, string password) //*** Þetta er innskráningarskipuninn fyrir admina eða síðustjórnendur þarna kannar hann hvort að userinn og passwordið passa saman við notenda og passwordið í gagnagrunninum og ef það er rétt athugar hún hvort að Isadmin sé Já eða Nei ef að Isadmin er Nei þá færðu ekki að logga þig inn, ef Isadmin er Já og lykilorðið og notendanafnið er rétt þá færðu að logga þig inn
        {
            if (OpenConnection() == true)
            {
                string admin = "Já";
                fyrirspurn = "SELECT Notendanafn,Lykilorð,Isadmin FROM notandi WHERE Notendanafn='" + user + "'AND Lykilorð='"+ password +"'AND Isadmin='"+admin+"'";              
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                int teljari = 0;
                while (sqllesari.Read())
                {
                    teljari += 1;

                }
                if (teljari == 1) //*** Teljarinn telur meðan sqllesari er að lesa og ef að notendanafnið og lykilorðið er rétt þá finnur hún notendann og hleypir þér inn
                {
                    
                    
                    MessageBox.Show("Innskráning tókst! Þú ert skráður inn sem Stjórnandi");
                    
                    


                }

                

                else if (teljari > 0)  //*** en ef það eru of margir sem á ekki að vera hægt eða ef þetta er ekki rétt þá færðu ekki að komast inn
                {
                    MessageBox.Show("Innskráning gekk ekki, ertu með stjórnandaréttindi?");
                }
                else 
                {
                    MessageBox.Show("Notendanafn eða lykilorð ekki rétt eða þú ert ekki með Stjórnandaréttindi");
                }
                
                CloseConnection();
            }

        }

              public void Innskraning(string user, string password) //*** Þetta er almenn innnskráning þarna er ekkert verið að athug hvort hann sé með admin, þarna er það bara ef notandinn og lykilorðið er rétt þá opnast bara user mode hvort sem þú ert admin eða ekki þetta
              {
                  if (OpenConnection() == true)
                  {
                      fyrirspurn = "SELECT Notendanafn,Lykilorð FROM notandi WHERE Notendanafn='" + user + "'AND Lykilorð='" + password + "'";
                      nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                      sqllesari = nySQLskipun.ExecuteReader();
                      int teljari = 0;
                      while (sqllesari.Read())
                      {
                          teljari += 1;

                      }
                      if (teljari == 1)
                      {


                          MessageBox.Show("Innskráning tókst! Þú ert skráður inn sem notandi");
                          Notendavidmot Notendavidmot = new Notendavidmot();
                          Notendavidmot.Show();
                          Logingluggi login = new Logingluggi();
                          login.Hide();


                      }



                      else if (teljari > 0)
                      {
                          MessageBox.Show("Innskráning gekk ekki, reyndu aftur");
                      }
                      else
                      {
                          MessageBox.Show("Notendanafn eða lykilorð ekki rétt");
                      }

                      CloseConnection();
                  }

              }

           

           

              

        public void Uppfaera(string nafn, string simi, string email , string isadmin ,string user) //*** Þetta keyrir update skipunina á notendann þar sem notandinn er notendanafnið og þar sem það er bara til 1stk Notandi með þetta notendanafn ætti þetta að vera bara solid 
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "UPDATE notandi SET Nafn='" + nafn + "',Simi='" + simi + "',Netfang='" + email + "',Isadmin='" + isadmin+ "' WHERE Notendanafn='" + user + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Uppfæra upplýsingar tókst!");
            }
            else
            {
                MessageBox.Show("Að uppfæra gekk ekki");
            }
        }


        public void UppfaeraEvent(string nafn, string salur, string timi, string timiendar, string date, int midar, string desc, int vidid) ///*** þetta uppfærir viðburðina í raun bara samma skipun og uppfæra notendann nema þetta velur ID á viðburðinum 
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "UPDATE vidburdir SET Nafn='" + nafn + "',Salur='" + salur + "',Time='" + timi + "',Timeends='" + timiendar + "',Date='" + date + "',Miðar='" + midar + "',Description='" + desc + "'WHERE Id=" + vidid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Uppfæra viðburð tókst!");
            }
            else
            {
                MessageBox.Show("Að uppfæra gekk ekki");
            }
        }

        

        public void Passreset(string user) //*** ef að notandi gleymir lykilorði og óskar eftir endurstillingu á lykilorði þá er hægt að enduretja lykilorðið og það verður bara mypassword 
        {
            string defaultpass = "mypassword";
            if (OpenConnection() == true)
            {
                fyrirspurn = "UPDATE notandi SET Lykilorð='" + defaultpass + "' WHERE Notendanafn='" + user + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Að breyta lykilorði Tókst");
            }
            else
            {
                MessageBox.Show("Að breyta lykilorði gekk ekki");
            }
        }

      
      

        public void Passupdate(string pass, string user)
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "UPDATE notandi SET Lykilorð='"+ pass + "'WHERE Notendanafn='" + user + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
                MessageBox.Show("Að breyta lykilorði tókst!");
            }
            else
            {
                MessageBox.Show("Að breyta lykilorði tókst ekki...");
            }
        }

       
        public void Eydauser(string user) ///*** nafnið segir ´sig sjálft en þetta eyðir notenda
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "DELETE FROM notandi WHERE Notendanafn='" + user + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
            }
            
        }

        public void EydaVidburd(int vidid) //*** segir sig sjálft, eyðir viðburð
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "DELETE FROM vidburdir WHERE Id=" + vidid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                CloseConnection();
            }

        }

        public void Eydanotendaurivdburd(int userid, int eventid) //*** Eyðar notendanum sem er í viðburðinum sem tengist Id
        {
            if (OpenConnection() == true)
            {
                fyrirspurn = "DELETE FROM samtenging WHERE id_notandi=(SELECT Notandi.Id FROM notandi WHERE Id=" + userid + ")AND id_vidburdir=(SELECT Vidburdir.Id FROM vidburdir WHERE Id=" + eventid + ") ;Update vidburdir SET miðar = miðar + 1 WHERE id =" + eventid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                nySQLskipun.ExecuteNonQuery();
                MessageBox.Show("Notandi hefur verið fjarlægður úr viðburði");
                CloseConnection();
                
            }

        }


       
       

        public string Skodavidburdiuser(int userid) //*** Þessi notar userid til að leyta eftir viðburðunum sem hann er skráður í og þarna er joinað allar töflurnar saman og þetta skilar í listbox,
        {
            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir.* FROM samtenging INNER JOIN notandi ON notandi.Id = samtenging.Id_Notandi INNER JOIN vidburdir ON vidburdir.id = samtenging.Id_Vidburdir WHERE notandi.Id ="+userid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Convert.ToString(Faerslur);
            }
            return Convert.ToString(Faerslur); 
        }

        //** sá svolítið eftir því en það var orðið of seint að breyta að þetta er meira og minna allt með listbox enda lærðum við mest að nota það þessa önn en ég hefði viljað notað eitthvað skipulagðara eins og datagridview en ég áttaði mig ekki á því fyrr en forritið var eiginlega tilbúið, en ég réttlæti þetta þannig að admininn kunni alveg að lesa hvað hann þarf úr töflunni og svo smellir hann á það sem hann vill sjá betur og þá kemur það snyrtilega til hliðar
        public List<string> Vidburdir() //*** sýnir bara alla viðburði 
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT * FROM vidburdir";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read()) //** mepan er verið að lesa töflu er sótt gildið í henni sett í string og sett íu listann sem er síðan skilað í listboxið
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Vidburdirskodauser(int userid) ///*** skoðar viðburðina sem userid er skráður í
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir.* FROM samtenging INNER JOIN notandi ON notandi.Id = samtenging.Id_Notandi INNER JOIN vidburdir ON vidburdir.id = samtenging.Id_Vidburdir WHERE notandi.Id=" + userid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Vidburdirskodauserusermode(string user) //*** þetta er notað í notendaviðmótinu
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir.* FROM samtenging INNER JOIN notandi ON notandi.Id = samtenging.Id_Notandi INNER JOIN vidburdir ON vidburdir.id = samtenging.Id_Vidburdir WHERE Notendanafn='" + user+"'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }
        public List<string> Hiddenvidburdir(int userid) //*** þetta er í listboxi sem sést í rauninni ekki en þetta er ég með svo það var ekki hægt að eyða notenda ef hann var í viðburði
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir.* FROM samtenging INNER JOIN notandi ON notandi.Id = samtenging.Id_Notandi INNER JOIN vidburdir ON vidburdir.id = samtenging.Id_Vidburdir WHERE notandi.Id=" + userid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Hiddenvidburdir2(int userid, int eventid) //*** þetta er í listboxi sem sést í rauninni ekki en þetta er ég með svo það var ekki hægt að eyða notenda ef hann var í viðburði
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir.* FROM samtenging INNER JOIN notandi ON notandi.Id = samtenging.Id_Notandi INNER JOIN vidburdir ON vidburdir.id = samtenging.Id_Vidburdir WHERE notandi.Id="+userid+" AND vidburdir.Id ="+eventid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Synatomavidburdi() //*** Sýnir hvaða viðburðir eru tómir
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir . * FROM vidburdir WHERE vidburdir.Id NOT IN (SELECT samtenging.Id_vidburdir FROM samtenging)";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Synafullavidburdi() //*** Sýnir hvað viðburðir eru búnir með miðanna síða og eru fullir
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT vidburdir . * FROM vidburdir WHERE miðar= 0";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Synanotendurividburd(int eventid) /// ///*** Sýnir notendur sem eru í viðburðnum með þetta ID
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT Notandi.Id,Notendanafn, Notandi.Nafn FROM notandi INNER JOIN samtenging ON samtenging.Id_notandi = notandi.Id INNER JOIN vidburdir ON vidburdir.Id = samtenging.Id_vidburdir WHERE notandi.Id = samtenging.id_notandi AND vidburdir.Id =" + eventid + " GROUP BY notandi.id";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        public List<string> Synanotendurividburd2(int eventid,string user)//*** í usermode máttu ekki sjá í haða viðburði aðrir eru í svo þetta leita bara eftir þínum
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT Notandi.Id,Notendanafn, Notandi.Nafn FROM notandi INNER JOIN samtenging ON samtenging.Id_notandi = notandi.Id INNER JOIN vidburdir ON vidburdir.Id = samtenging.Id_vidburdir WHERE notandi.Id = samtenging.id_notandi AND vidburdir.Id =" + eventid + " AND Notendanafn ='" + user+ "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ";";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        


      

        public List<string> Synanotendur() //*** Sýnir notendur
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT * FROM Notandi GROUP BY id";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ":";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;
        }

        

        public List<string> Synaadmin() //*** setur í listboxið alla admin, þetta er bara sýnilegt frá admin forminu
        {
            string Isadmin ="Já";
            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT * FROM Notandi WHERE isadmin ='"+Isadmin+"' GROUP BY id";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ":";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;


        }

        public List<string> Synamig(string user) /// *** Sýnir allar upplýsingar um notenda sem er loggaður inn, auðveldar fyrir breytinarnar
        {
           
            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT * FROM Notandi WHERE Notendanafn ='" + user + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ":";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;


        }
        public List<string> Synamiguser(string user) //*** sýnir notendann sem er loggaður inn í notendagluggann sýnir allt nema pass
        {

            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT Id,Nafn,Simi,Netfang,Notendanafn,Isadmin FROM Notandi WHERE Notendanafn ='" + user + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ":";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;


        }

        public string teljamida(int eventid) //*** sýnir notendann sem er loggaður inn í notendagluggann sýnir allt nema pass
        {
          List<string> Faerslur = new List<string>();
          string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT miðar FROM vidburdir WHERE id="+eventid;
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString());
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur[0];
                
            }
            else
            {
                MessageBox.Show("Villa kom upp");
                CloseConnection();
                return null;
            }
            
            


        }


        public List<string> Synanonadmin() //*** sýnir alla sem eru ekki admin
        {
            string Isadmin = "Nei";
            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT * FROM Notandi WHERE isadmin ='" + Isadmin + "'GROUP BY ID";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ":";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;


        }


        public bool IsUserTaken(string newusername) //*** kannar hvort notendanafn sé frátekið
        {
           
            List<string> Faerslur = new List<string>();
            string lina = null;
            bool ertil = false;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT Notendanafn FROM Notandi";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString());
                    }
                    Faerslur.Add(lina);
                    lina = null;
                   
                }
                CloseConnection();
                for (int i = 0; i < Faerslur.Count; i++)
                {
                    if (newusername == Faerslur[i])
                    {
                       
                        ertil = true;
                    }
                    
                    
                }
                return ertil;
                
                
            }
            return ertil;


        }

        public List<string> Synanonadminuser() //*** Sýnir alla notendur sem eru ekki admin
        {
            string Isadmin = "Nei";
            List<string> Faerslur = new List<string>();
            string lina = null;
            if (OpenConnection() == true)
            {
                fyrirspurn = "SELECT Id,Nafn,Simi,Netfang,Notendanafn,Isadmin FROM Notandi WHERE isadmin ='" + Isadmin + "'";
                nySQLskipun = new MySqlCommand(fyrirspurn, sqltenging);
                sqllesari = nySQLskipun.ExecuteReader();
                while (sqllesari.Read())
                {
                    for (int i = 0; i < sqllesari.FieldCount; i++)
                    {
                        lina += (sqllesari.GetValue(i).ToString()) + ":";
                    }
                    Faerslur.Add(lina);
                    lina = null;
                }
                CloseConnection();
                return Faerslur;
            }
            return Faerslur;


        }


       

        internal List<string> Skodavidburdiuser()
        {
            throw new NotImplementedException();
        }
    }
}