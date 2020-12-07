﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Data.SqlClient;
using ContractLayer;

namespace DAL
{
    public class UserDAL: IUserDAL, IUserCollectionDAL
    {
        string sqlConnectionString = "Server=mssql.fhict.local;Database=dbi454449;User Id=dbi454449;Password=454449ICT;";

        public bool Create(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sql = "INSERT INTO Users (Email, Username, Password) VALUES (@Email, @Username, @Password)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Name", user.Username);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Username);
                        if (command.ExecuteNonQuery() < 1)
                        {
                            return false;
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool Delete(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sql = "DELETE FROM Users WHERE Username = (@Username)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Username", user.Username);
                        if (command.ExecuteNonQuery() < 1)
                        {
                            return false;
                        }
                        //reseed();
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public List<UserDTO> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sql = "SELECT * FROM Users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                UserDTO user = new UserDTO();
                                user.Id = reader.GetInt32(0);
                                user.Email = reader.GetString(1);
                                user.Username = reader.GetString(2);           
                                user.Password = reader.GetString(3);

                                users.Add(user);
                            }

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return users;
        }

        public UserDTO Get(int Id)
        {
            UserDTO user = new UserDTO();
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sql = "SELECT * FROM Users WHERE Id = (@Id)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            user.Id = reader.GetInt32(0);
                            user.Email = reader.GetString(1);
                            user.Username = reader.GetString(2);                           
                            user.Password = reader.GetString(3);
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return user;
        }

        public bool Update(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    string sql = "UPDATE Users SET Email = (@Email), Username = (@Username), Password = (@Password) WHERE Id = (@Id)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}
