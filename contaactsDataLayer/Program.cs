using System;
using System.Data;
using System.Data.SqlClient;

namespace contaactsDataLayer
{
    internal class Program
    {
        #region variable
        static string severName = ".";
        static string DataBase = "ContactsDB";
        static string connectionString = $"Server=${severName};Database={DataBase};Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        #endregion


        static void Main(string[] args)
        {
            Console.WriteLine(connectionString);
            Console.ReadKey();

        }
    }
}
