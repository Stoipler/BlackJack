using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.DAL.Entities;
using BlackJack.DAL.Inerfaces;
using System.Data.SqlClient;

namespace BlackJack.DAL
{
    public class UserRepository:IUserRepository
    {
        private readonly string _connectionString = @"Data Source=DESKTOP-C2DTPRP\SQLEXPRESS;Initial Catalog=UsersDB;Integrated Security=True";

        public bool CreateNewUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlExpression = "SELECT * FROM Users WHERE Username='" + username + "'";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    return false;
                }
                reader.Close();

                sqlExpression = String.Format("INSERT INTO Users (Username,Password) VALUES ('{0}','{1}')", username, password);
                command = new SqlCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
                return true;
            }
        }

        public User GetUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlExpression = "SELECT * FROM Users WHERE Username='" + username + "' AND Password='" + password + "'";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return null;
                }
                reader.Read();
                return new User() { Userame = reader.GetString(1) };
            }
        }
    }
}
