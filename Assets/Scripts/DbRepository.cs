using UnityEngine;
using MySqlConnector;
// using MySql.Data.MySqlClient;
using System;
public static class DbRepository 
{
    // All test accounts will have password "1234"
    // Admin account has password "admin"
    private static readonly String connectionString = "server=localhost;port=3306;database=roby_db;uid=root;pwd=mysqlroot1234";

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
    
    private static float GetUserLevelGenerationSeed()
    {
        string query = "SELECT `value` FROM game_config WHERE `key` = 'seed'";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    return Convert.ToSingle(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }

        return 0.0f;
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
    
    internal static void CreateSession(string username)
    {
        UserSession.username = username;
        UserSession.totalScore = GetTotalScore(username);
        UserSession.levelsCompleted = GetLevelsCompleted(username);
        GetUserAudioSettings(username);
        GetBestTimesAndStarsGained(username);
        UserSession.levelGenerationSeed = GetUserLevelGenerationSeed();
    }

    private static float GetTotalScore(string username)
    {
        string query = "SELECT total_score FROM users WHERE username = @username";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToSingle(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }

        return 0.0f;
    }
    
    private static int GetLevelsCompleted(string username)
    {
        string query = "SELECT levels_completed FROM users WHERE username = @username";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }

        return 0;
    }
    
    internal static void GetUserAudioSettings(string username)
    {
        string query = "SELECT bgm_enabled, sfx_enabled, bgm_volume, sfx_volume FROM users WHERE username = @username";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserSession.isBgmEnabled = reader.GetBoolean(0);
                            UserSession.isSfxEnabled = reader.GetBoolean(1);
                            UserSession.bgmVolume = reader.GetFloat(2);
                            UserSession.sfxVolume = reader.GetFloat(3);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }
    }

    private static void GetBestTimesAndStarsGained(string username)
    {
        string query = "SELECT best_time, stars_gained FROM high_score WHERE username = @username";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserSession.highScores.Add(reader.GetFloat(0));
                            UserSession.maxStars.Add(reader.GetInt32(1));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }
    }

    internal static void UpdateUserAudioSettings()
    {
        string updateQuery = "UPDATE users SET bgm_enabled = @bgmEnabled, sfx_enabled = @sfxEnabled, bgm_volume = @bgmVolume, sfx_volume = @sfxVolume WHERE username = @username";
        
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@bgmEnabled", UserSession.isBgmEnabled);
                    cmd.Parameters.AddWithValue("@sfxEnabled", UserSession.isSfxEnabled);
                    cmd.Parameters.AddWithValue("@bgmVolume", UserSession.bgmVolume);
                    cmd.Parameters.AddWithValue("@sfxVolume", UserSession.sfxVolume);
                    cmd.Parameters.AddWithValue("@username", UserSession.username);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }
    }

    internal static void UpdateUserStats()
    {
        string updateUsersTable = "UPDATE users SET total_score = @totalScore, levels_completed = @levelsCompleted WHERE username = @username";
        string updateHighScoreTable = "REPLACE INTO high_score (best_time, stars_gained, level, username) VALUES (@bestTime, @starsGained, @level, @username)";
        string deleteHighScoreTable = "DELETE FROM high_score WHERE username = @username";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(updateUsersTable, conn))
                {
                    cmd.Parameters.AddWithValue("@totalScore", UserSession.totalScore);
                    cmd.Parameters.AddWithValue("@levelsCompleted", UserSession.levelsCompleted);
                    cmd.Parameters.AddWithValue("@username", UserSession.username);
                    cmd.ExecuteNonQuery();
                }
                if(UserSession.highScores.Count > 0 || UserSession.maxStars.Count > 0)
                    using (MySqlCommand cmd = new MySqlCommand(updateHighScoreTable, conn))
                    {
                            for (int i = 0; i < UserSession.highScores.Count; i++)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@bestTime", UserSession.highScores[i]);
                                cmd.Parameters.AddWithValue("@starsGained", UserSession.maxStars[i]);
                                cmd.Parameters.AddWithValue("@username", UserSession.username);
                                cmd.Parameters.AddWithValue("@level", i + 1);
                                cmd.ExecuteNonQuery();
                            }
                    }
                else
                {
                    using (MySqlCommand cmd = new MySqlCommand(deleteHighScoreTable, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", UserSession.username);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("❌ Database error: " + ex.Message);
            }
        }
    }
}
