using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Common.Models;
using BlackJack.Common.Enums;
using BlackJack.BLL;
using BlackJack.BLL.Services;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string _connectionString = @"Data Source=DESKTOP-C2DTPRP\SQLEXPRESS;Initial Catalog=UsersDB;Integrated Security=True";
            string username = "NewUser";
            string password = "NewUser1234";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlExpression = "SELECT * FROM Users WHERE Username='" + username + "'";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return;
                }
                reader.Close();

                //command = new SqlCommand(connection);
                //command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                //command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
                //command.Prepare();
                //command.ExecuteNonQuery();
                
                sqlExpression = String.Format("INSERT INTO Users (Username,Password) VALUES ('{0}','{1}')", username, password);
                command = new SqlCommand(sqlExpression, connection);
                int number=command.ExecuteNonQuery();
                Console.WriteLine(number);
            }
            
            Console.ReadKey();
        }
    }
}
