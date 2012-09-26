using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace WebSOAPService
{
    /// <summary>
    /// Descripción breve de ClientsService
    /// </summary>
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ClientsService : System.Web.Services.WebService
    {

        String str = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\Clients.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";

        [WebMethod]
        public int NewClient(String Name, String Surname, String Age)
        {
            SqlConnection con = new SqlConnection(str);

            con.Open();

            string sql = "INSERT INTO Clients (Name, Surname, Age) VALUES (@name, @surname, @age)";

            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar).Value = Name;
            cmd.Parameters.Add("@surname", System.Data.SqlDbType.VarChar).Value = Surname;
            cmd.Parameters.Add("@age", System.Data.SqlDbType.VarChar).Value = Age;

            int res = cmd.ExecuteNonQuery();

            con.Close();

            return res;
        }

        [WebMethod]
        public Client[] getClients()
        {
            SqlConnection con = new SqlConnection(str);

            con.Open();

            string sql = "SELECT _id,Name,Surname,Age FROM Clients";

            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataReader reader = cmd.ExecuteReader();

            List<Client> list = new List<Client>();

            while (reader.Read())
            {
                list.Add(
                    new Client(reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetInt32(3)));
            }

            con.Close();

            return list.ToArray();
        }

    }
}
