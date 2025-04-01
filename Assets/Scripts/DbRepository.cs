using UnityEngine;
using MySql.Data.MySqlClient;
using System;
public static class DbRepository 
{
    // All test accounts will have password "1234"
    // Admin account has password "admin"
    private static readonly String connectionString = "server=localhost;port=3306;database=roby_db;uid=root;pwd=mysqlroot1234;";

    internal static void TestConnection()
    {
        using MySqlConnection conn = new MySqlConnection(connectionString);
        try
        {
            conn.Open();
            Debug.Log("Connected to MySQL database!");
        }
        catch (Exception ex)
        {
            Debug.LogError("Database connection failed: " + ex.Message);
        }
    }

    internal static void AddUserToDatabase(String  username, String hashedPassword)
    {
        String addToUsersQuery = "INSERT INTO users (username, password) VALUES (@username, @hashedPassword)";
        String addToHighScoresQuery = "INSERT INTO high_scores (username) VALUES (@username)";
        
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(addToUsersQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@hashedPassword", hashedPassword); 
                    cmd.ExecuteNonQuery();
                }

                using (MySqlCommand cmd = new MySqlCommand(addToHighScoresQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
                Debug.Log("User registered successfully!");
            }
            catch (Exception ex)
            {
                Debug.LogError("User registration failed: " + ex.Message);
            }
        }
    }

    internal static bool UsernameExists(String username)
    {
        String usernameIsUsedQuery = "SELECT username FROM users WHERE username = @username";
        
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(usernameIsUsedQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Database error: " + ex.Message);
            }
        }
        return false;
    }

    internal static string HashPassword(String password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    private static bool IsPaswordCorrect(String hashedPassword, String password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    internal static bool AreCredentialsValid(String enteredUsername, String enteredPassword)
    {
        string storedHash = GetStoredPasswordHash(enteredUsername);

        if (storedHash == null)
        {
            return false;
        }
        
        return IsPaswordCorrect(storedHash, enteredPassword);
    }
    internal static string GetStoredPasswordHash(string username)
    {
        string hashedPassword = null;

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT password FROM users WHERE username = @username LIMIT 1";
                
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hashedPassword = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }
        return hashedPassword;
    }
    
    internal static void UpdatePasswordInDatabase(string username, string newPasswordHashed)
    {
        string updateQuery = "UPDATE users SET password = @hashedPassword WHERE username = @username";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@hashedPassword", newPasswordHashed);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }
    }
}
