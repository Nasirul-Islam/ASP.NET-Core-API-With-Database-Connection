using API_DB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace API_DB.Services
{
    public class UsersService
    {
        private readonly string _connectionString;

        public UsersService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all users  ID  Username  Password  ApplicationUsers
        public List<Users> GetAllUsers()
        {
            var users = new List<Users>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Username, Password FROM ApplicationUsers", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new Users
                    {
                        ID = (int)reader["ID"],
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
            }

            return users;
        }

        // Get a user by ID  [  ID  Username  Password  ApplicationUsers  ]
        public Users GetUserById(int id)
        {
            Users user = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Username, Password FROM ApplicationUsers WHERE ID = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new Users
                    {
                        ID = (int)reader["ID"],
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
            }

            return user;
        }

        // Create a new user  [  ID  Username  Password  ApplicationUsers  ]
        public void CreateUser(Users user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ApplicationUsers (Username, Password) VALUES (@UserName, @Password)", conn);
                cmd.Parameters.AddWithValue("@UserName", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
            }
        }

        // Update an existing user  [  ID  Username  Password  ApplicationUsers  ]
        public void UpdateUser(Users user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ApplicationUsers SET Username = @UserName, Password = @Password WHERE ID = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", user.ID);
                cmd.Parameters.AddWithValue("@UserName", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a user by ID  [  ID  Username  Password  ApplicationUsers  ]
        public void DeleteUser(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ApplicationUsers WHERE ID = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

