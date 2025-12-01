using FitnessCenterApp.DataAccess;
using FitnessCenterApp.Models;
using FitnessCenterApp.Utils;
using System.Data.OleDb;

namespace FitnessCenterApp.BusinessLogic
{
    public static class AuthService
    {
        public static User AuthenticateUser(string username, string password)
        {
            string passwordHash = HashHelper.ComputeSha256Hash(password);

            OleDbConnection conn = DatabaseConnection.GetConnection();
            try
            {
                conn.Open();

                string sql = "SELECT * FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString(),
                            ClientID = reader["ClientID"] as int?
                        };
                    }
                }
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return null; // если пользователь не найден
        }
    }
}