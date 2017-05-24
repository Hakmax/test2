using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConnectionChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "";
            Console.WriteLine(ConfigurationManager.AppSettings["connectionString"]);
            Console.WriteLine("Использовать соединение из app.config? (Введите 1)");
            if (Console.ReadLine() != "1")
            {
                Console.WriteLine("Server address:\n");
                string dbAddress = Console.ReadLine();

                Console.WriteLine("Db name:\n");
                string dbName = Console.ReadLine();

                Console.WriteLine("Username:\n");
                string usrName = Console.ReadLine();

                Console.WriteLine("UserPassword:\n");
                string usrPassword = Console.ReadLine();


                connectionString = string.Format(@"data source='{0}'; initial catalog='{1}'; user id='{2}'; password='{3}'", dbAddress, dbName, usrName, usrPassword);
                if (string.IsNullOrEmpty(usrName) && string.IsNullOrEmpty(usrPassword))
                {
                    connectionString = string.Format(@"data source='{0}'; initial catalog='{1}'; integrated security=true", dbAddress, dbName);
                }
            }
            else
                connectionString = ConfigurationManager.AppSettings["connectionString"];

            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection succeeded");
            }
            catch (Exception exc)
            {
                Console.WriteLine(String.Format("Error: \'{0}\'", exc.Message));
            }
            Console.ReadLine();
        }
    }
}
