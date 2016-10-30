using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceTheatre
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        static List<AccountClient> accountList = new List<AccountClient>();
        List<Spectacle> spectacleList = new List<Spectacle>();
        List<Actor> actorsList = new List<Actor>();
        List<Place> placeList = new List<Place>();
        List<Price> priceList = new List<Price>();

       [WebMethod]
        public AccountClient AddClient(string name, string surname, string e_mail, string pin, string address, bool type)
        {
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            AccountClient AClient = new AccountClient(name, surname, address, pin, e_mail, type);

            accountList.Add(AClient);
            command.CommandText = String.Format("INSERT Users VALUES ('{0}','{1}','{2}','{3}','{4}',{5});", name, surname, address, pin, e_mail, Convert.ToByte(type));
            command.ExecuteNonQuery();
            return AClient;
        }
       [WebMethod]
       public AccountClient Login(string e_mail, string pin)
        {
            AccountClient SendClient = new AccountClient("", "", "", "", "", false);

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "select User_id,Name,Surname,Address,Pin,E_mail,Type from Users";
            SqlDataReader reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                AccountClient client = new AccountClient(Convert.ToInt16(reader1["User_id"].ToString()), reader1["Name"].ToString(), reader1["Surname"].ToString(), reader1["Address"].ToString(), reader1["Pin"].ToString(), reader1["E_mail"].ToString(), Convert.ToBoolean(reader1["Type"]));
                accountList.Add(client);
            }

            foreach (var account in accountList)
            {
                if (String.Equals(account.e_mail, e_mail) && String.Equals(account.pin, pin))
                {
                    SendClient = account;
                }

            }
            return SendClient;
        }
       [WebMethod]
       public string AddSpectacle(string name, string exposition, DateTime dateSpect, List<Actor> listActors)
        {
            string respond = "not added";
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            List<Actor> SpectacleActors = listActors;
            command.CommandText = String.Format("insert Spectacles values('{0}','{1},'{2}/{13}/{14}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", name, exposition, dateSpect.Date.Day, (SpectacleActors[0] != null) ? SpectacleActors[0].actor_id : 0, (SpectacleActors[1] != null) ? SpectacleActors[1].actor_id : 0, (SpectacleActors[2] != null) ? SpectacleActors[2].actor_id : 0, (SpectacleActors[3] != null) ? SpectacleActors[3].actor_id : 0, (SpectacleActors[4] != null) ? SpectacleActors[4].actor_id : 0, (SpectacleActors[5] != null) ? SpectacleActors[5].actor_id : 0, (SpectacleActors[6] != null) ? SpectacleActors[6].actor_id : 0, (SpectacleActors[7] != null) ? SpectacleActors[7].actor_id : 0, (SpectacleActors[8] != null) ? SpectacleActors[8].actor_id : 0, (SpectacleActors[9] != null) ? SpectacleActors[9].actor_id : 0, dateSpect.Date.Month, dateSpect.Date.Year);

            if (command.ExecuteNonQuery() > 0)
            {
                respond = "spectacle added";
            }
            return respond;
        }
       [WebMethod]
       public List<Spectacle> GetCurrentSpectacle(DateTime date_sp)
        {
            Spectacle spectacle = new Spectacle(0, "", "", DateTime.Today, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            Spectacle SendSpectacle;


            //AccountClient SendClient = new AccountClient("", "", "", "", "",false);

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "select Spectacle_id,Spectacle_name,Exposition_sp,Date_sp,Actor1,Actor2,Actor3,Actor4,Actor5,Actor6,Actor7,Actor8,Actor9,Actor10 from Spectacles";
            SqlDataReader reader2 = command.ExecuteReader();

            List<Spectacle> CurrentSpectacles = new List<Spectacle>();
            while (reader2.Read())
            {
                SendSpectacle = new Spectacle(Convert.ToInt16(reader2["Spectacle_id"].ToString()), reader2["Spectacle_name"].ToString(), reader2["Exposition_sp"].ToString(), Convert.ToDateTime(reader2["Date_sp"].ToString()), Convert.ToInt16(reader2["Actor1"].ToString()), Convert.ToInt16(reader2["Actor2"].ToString()), Convert.ToInt16(reader2["Actor3"].ToString()), Convert.ToInt16(reader2["Actor4"].ToString()), Convert.ToInt16(reader2["Actor5"].ToString()), Convert.ToInt16(reader2["Actor6"].ToString()), Convert.ToInt16(reader2["Actor7"].ToString()), Convert.ToInt16(reader2["Actor8"].ToString()), Convert.ToInt16(reader2["Actor9"].ToString()), Convert.ToInt16(reader2["Actor10"].ToString()));
                spectacleList.Add(SendSpectacle);
            }

            foreach (var spec in spectacleList)
            {
                CurrentSpectacles.Clear();
                if (String.Equals(spec.date_sp, date_sp.Date))
                {
                    CurrentSpectacles.Add(spec);
                }


            }
            return CurrentSpectacles;
        }
       [WebMethod]
       public List<Spectacle> GetSpectacles()
        {
            spectacleList.Clear();
            Spectacle spectacle = new Spectacle(0, "", "", DateTime.Today, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            Spectacle SendSpectacle;


            //AccountClient SendClient = new AccountClient("", "", "", "", "",false);

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "select Spectacle_id,Spectacle_name,Exposition_sp,Date_sp,Actor1,Actor2,Actor3,Actor4,Actor5,Actor6,Actor7,Actor8,Actor9,Actor10 from Spectacles";
            SqlDataReader reader2 = command.ExecuteReader();

            List<Spectacle> CurrentSpectacles = new List<Spectacle>();
            while (reader2.Read())
            {
                SendSpectacle = new Spectacle(Convert.ToInt16(reader2["Spectacle_id"].ToString()), reader2["Spectacle_name"].ToString(), reader2["Exposition_sp"].ToString(), Convert.ToDateTime(reader2["Date_sp"].ToString()), Convert.ToInt16(reader2["Actor1"].ToString()), Convert.ToInt16(reader2["Actor2"].ToString()), Convert.ToInt16(reader2["Actor3"].ToString()), Convert.ToInt16(reader2["Actor4"].ToString()), Convert.ToInt16(reader2["Actor5"].ToString()), Convert.ToInt16(reader2["Actor6"].ToString()), Convert.ToInt16(reader2["Actor7"].ToString()), Convert.ToInt16(reader2["Actor8"].ToString()), Convert.ToInt16(reader2["Actor9"].ToString()), Convert.ToInt16(reader2["Actor10"].ToString()));
                spectacleList.Add(SendSpectacle);
            }
            reader2.Close();
            return spectacleList;
        }
       [WebMethod]
       public List<Actor> GetActorsBySpectacle(int spectacle_id)
        {

            actorsList.Clear();

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            for (int i = 1; i < 11; ++i)
            {

                string NumberActor = "Actor" + i;
                command.CommandText = String.Format("select Actor_id,Name_actor,Surname_actor,Exposition from Actors WHERE Actor_id=(SELECT {0} from Spectacles WHERE Spectacle_id='{1}')", NumberActor, spectacle_id);
                SqlDataReader reader3 = command.ExecuteReader();
                while (reader3.Read())
                {
                    Actor actor = new Actor(Convert.ToInt16(reader3["Actor_id"].ToString()), reader3["Name_actor"].ToString(), reader3["Surname_actor"].ToString(), reader3["Exposition"].ToString());
                    actorsList.Add(actor);
                }
                reader3.Close();
            }

            return actorsList;
        }
       [WebMethod]
       public List<Actor> GetActors()
        {

            actorsList.Clear();

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = String.Format("select Actor_id,Name_actor,Surname_actor,Exposition from Actors");
            SqlDataReader reader5 = command.ExecuteReader();
            while (reader5.Read())
            {
                Actor actor = new Actor(Convert.ToInt16(reader5["Actor_id"].ToString()), reader5["Name_actor"].ToString(), reader5["Surname_actor"].ToString(), reader5["Exposition"].ToString());
                actorsList.Add(actor);
            }
            reader5.Close();


            return actorsList;
        }
       [WebMethod]
       public List<Place> GetFreePlace(int spectacle_id)
        {
            Place place = new Place(0, 0, 0, 0, 0);

            Place SendPlace;


            //AccountClient SendClient = new AccountClient("", "", "", "", "",false);

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = String.Format("select Place_id,Type_t,Line,Number,Scenne from Places WHERE Place_id=(SELECT Place_id from Tickets WHERE Spectacle_id!='{0}')", spectacle_id);
            SqlDataReader reader2 = command.ExecuteReader();

            List<Spectacle> CurrentSpectacles = new List<Spectacle>();
            while (reader2.Read())
            {
                SendPlace = new Place(Convert.ToInt16(reader2["Place_id"].ToString()), Convert.ToInt16(reader2["Type_t"].ToString()), Convert.ToInt16(reader2["Line"].ToString()), Convert.ToInt16(reader2["Number"].ToString()), Convert.ToInt16(reader2["Scenne"].ToString()));
                placeList.Add(SendPlace);
            }
            return placeList;
        }
       [WebMethod]
       public List<Place> GetPlacelist()
        {

            Place SendPlace;


            //AccountClient SendClient = new AccountClient("", "", "", "", "",false);

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = String.Format("select Place_id,Type_t,Line,Number,Scenne from Places");
            SqlDataReader reader2 = command.ExecuteReader();

            while (reader2.Read())
            {
                SendPlace = new Place(Convert.ToInt16(reader2["Place_id"].ToString()), Convert.ToInt16(reader2["Type_t"].ToString()), Convert.ToInt16(reader2["Line"].ToString()), Convert.ToInt16(reader2["Number"].ToString()), Convert.ToInt16(reader2["Scenne"].ToString()));
                placeList.Add(SendPlace);
            }
            return placeList;
        }

       [WebMethod]
       public int GetPriceCurrent(int spectacle_id, int place_id)
        {
            int SendPrice = 0;


            //AccountClient SendClient = new AccountClient("", "", "", "", "",false);

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = String.Format("select Spectacle_id,Type_Price,Price from Prices WHERE Type_Price=(select Type_t from Places where Place_id={1})", spectacle_id, place_id);
            SqlDataReader reader2 = command.ExecuteReader();

            List<Spectacle> CurrentSpectacles = new List<Spectacle>();
            while (reader2.Read())
            {
                SendPrice = Convert.ToInt16(reader2["Price"].ToString());
            }
            return SendPrice;
        }

       [WebMethod]
       public string AddPlace(int type_t, int line, int number, int scenne)
        {

            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();

            string respond = "not added";
            for (int i = 1; i <= number; ++i)
            {
                command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = String.Format("INSERT Places VALUES ('{0}','{1}','{2}','{3}');", type_t, line, i, scenne);
                command.ExecuteNonQuery();
                if (i == number)
                    respond = "place added";
            }
            return respond;
        }

       [WebMethod]
       public string PriceSetting(int spectacle_id, int type_t, int price)
        {
            string respond = String.Format("not setted prices for type: {0}", type_t);
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();

            command = new SqlCommand();
            command.Connection = connection;
            if (command.Connection == connection)
            {
                command.CommandText = String.Format("INSERT Prices VALUES ('{0}','{1}','{2}');", spectacle_id, type_t, price);
                command.ExecuteNonQuery();
                respond = String.Format("not setted prices for type: {0}", type_t);
            }


            return respond;
        }

       [WebMethod]
       public string CheckTicket(int user_id, int spectacle_id, int place_id, bool state)
        {
            int CPrice = 0; int BoolState = 0;
            if (state == false)
            {
                BoolState = 0;
            }
            else
                BoolState = 1;
            CPrice = GetPriceCurrent(spectacle_id, place_id);
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();

            string respond = "not added";

            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = String.Format("INSERT Tickets VALUES ('{0}','{1}','{2}','{3}','{4}');", user_id, spectacle_id, place_id, BoolState, CPrice);
            command.ExecuteNonQuery();
            respond = String.Format("Ціна квитка: {0}", CPrice);

            return respond;
        }

       [WebMethod]
       public List<string> GetTicket(int spectacle_id)
        {
            List<string> SendList = new List<string>();
            SqlConnection connection = new SqlConnection();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"SY\SQLEXPRESS";
            builder.IntegratedSecurity = true;
            builder.InitialCatalog = "Theatre";

            connection.ConnectionString = builder.ConnectionString;

            connection.Open();

            SqlCommand command = new SqlCommand();


            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = String.Format("select Place_id from Tickets where Spectacle_id='{0}';", spectacle_id);
            SqlDataReader reader3 = command.ExecuteReader();

            List<Spectacle> CurrentSpectacles = new List<Spectacle>();
            while (reader3.Read())
            {
                SendList.Add(reader3["Place_id"].ToString());
            }

            return SendList;
        }

        
        public class AccountClient
        {
            
            public int user_id;
            public string name;
            public string surname;
            public string e_mail;
            public string pin;
            public string address;
            public bool type;
            public AccountClient()
            {
                this.user_id = 0;
                this.name = "";
                this.surname = "";
                this.e_mail = "";
                this.pin = "";
                this.address = "";
                this.type = false;
            }
            public AccountClient(int user_id, string name, string surname, string address, string pin, string e_mail, bool type)
            {
                this.user_id = user_id;
                this.name = name;
                this.surname = surname;
                this.e_mail = e_mail;
                this.pin = pin;
                this.address = address;
                this.type = type;
            }
            public AccountClient(string name, string surname, string address, string pin, string e_mail, bool type)
            {
                this.name = name;
                this.surname = surname;
                this.e_mail = e_mail;
                this.pin = pin;
                this.address = address;
                this.type = type;
            }


        }
        
        public class Actor
        {
            
            public int actor_id;

            
            public string name;

            
            public string surname;

            
            public string exposition;

            public Actor()
            {
                this.actor_id = 0;
                this.name = "";
                this.surname = "";
                this.exposition = "";
            }
            public Actor(int actor_id, string name, string surname, string exposition)
            {
                this.actor_id = actor_id;
                this.name = name;
                this.surname = surname;
                this.exposition = exposition;
            }


        }
        
        public class Spectacle
        {
            
            public int spectacle_id;
            
            public string spectacle_name;
            
            public string exposition;
            
            public DateTime date_sp;
            
            public int actor1;
            
            public int actor2;
            
            public int actor3;
            
            public int actor4;
            
            public int actor5;
            
            public int actor6;
            
            public int actor7;
            
            public int actor8;
            
            public int actor9;
            
            public int actor10;
            public Spectacle()
            {
                this.spectacle_id = 0;
                this.spectacle_name = "";
                this.exposition = "";
                this.date_sp = DateTime.Now;
                this.actor1 = 0;
                this.actor2 = 0;
                this.actor3 = 0;
                this.actor4 = 0;
                this.actor5 = 0;
                this.actor6 = 0;
                this.actor7 = 0;
                this.actor8 = 0;
                this.actor9 = 0;
                this.actor10 =0;
            }
            public Spectacle(int spectacle_id, string spectacle_name, string exposition, DateTime date_sp, int actor1, int actor2, int actor3, int actor4, int actor5, int actor6, int actor7, int actor8, int actor9, int actor10)
            {
                this.spectacle_id = spectacle_id;
                this.spectacle_name = spectacle_name;
                this.exposition = exposition;
                this.date_sp = date_sp;
                this.actor1 = actor1;
                this.actor2 = actor2;
                this.actor3 = actor3;
                this.actor4 = actor4;
                this.actor5 = actor5;
                this.actor6 = actor6;
                this.actor7 = actor7;
                this.actor8 = actor8;
                this.actor9 = actor9;
                this.actor10 = actor10;
            }
        }
        
        public class Ticket
        {
            
            public int ticket_id;
            
            public int user_id;
            
            public int spectacle_id;
            
            public int price;
            
            public int place_id;
            
            public bool state;
            public Ticket()
            {
                this.ticket_id = 0;
                this.user_id = 0;
                this.spectacle_id = 0;
                this.price = 0;
                this.place_id = 0;
                this.state = false;

            }
            public Ticket(int ticket_id, int user_id, int spectacle_id, int price, int place_id, bool state)
            {
                this.ticket_id = ticket_id;
                this.user_id = user_id;
                this.spectacle_id = spectacle_id;
                this.price = price;
                this.place_id = place_id;
                this.state = state;

            }
        }
        
        public class Place
        {
            
            public int place_id;
            
            public int type_t;
            
            public int line;
            
            public int number;
            
            public int scenne;
            public Place()
            {
                this.place_id = 0;
                this.type_t = 0;
                this.line = 0;
                this.number = 0;
                this.scenne = 0;

            }
            public Place(int place_id, int type_t, int line, int number, int scenne)
            {
                this.place_id = place_id;
                this.type_t = type_t;
                this.line = line;
                this.number = number;
                this.scenne = scenne;

            }
        }
        
        public class Price
        {
            
            public int spectacle_id;
            
            public int type;
            
            public int price;
            public Price()
            {
                this.spectacle_id = 0;
                this.type = 0;
                this.price = 0;
            }
            public Price(int spectacle_id, int type, int price)
            {
                this.spectacle_id = spectacle_id;
                this.type = type;
                this.price = price;
            }
        }
    }
}