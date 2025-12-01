using System;
using System.Data.OleDb;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;
using FitnessCenterApp.Utils;

namespace FitnessCenterApp.Forms
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string firstName = txtFirstName.Text.Trim();
            string lastName = txtLastName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Все поля обязательны для заполнения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 3)
            {
                MessageBox.Show("Пароль должен содержать минимум 3 символа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    OleDbTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Проверка уникальности логина
                        using (var cmd = new OleDbCommand("SELECT COUNT(*) FROM Users WHERE Username = ?", conn, transaction))
                        {
                            cmd.Parameters.Add("", OleDbType.VarChar).Value = username;
                            int count = (int)cmd.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show("Логин уже занят. Выберите другой.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // Добавление клиента
                        using (var cmd = new OleDbCommand(
                            "INSERT INTO Clients (FirstName, LastName, Email) VALUES (?, ?, ?)", conn, transaction))
                        {
                            cmd.Parameters.Add("", OleDbType.VarChar).Value = firstName;
                            cmd.Parameters.Add("", OleDbType.VarChar).Value = lastName;
                            cmd.Parameters.Add("", OleDbType.VarChar).Value = email;
                            cmd.ExecuteNonQuery();
                        }

                        // Получение последнего ClientID
                        int clientID;
                        using (var cmd = new OleDbCommand("SELECT MAX(ClientID) FROM Clients", conn, transaction))
                        {
                            clientID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Добавление пользователя
                        string passwordHash = HashHelper.ComputeSha256Hash(password);
                        using (var cmd = new OleDbCommand(
                            "INSERT INTO Users (Username, PasswordHash, Role, ClientID) VALUES (?, ?, 'User', ?)", conn, transaction))
                        {
                            cmd.Parameters.Add("", OleDbType.VarChar).Value = username;
                            cmd.Parameters.Add("", OleDbType.VarChar).Value = passwordHash;
                            cmd.Parameters.Add("", OleDbType.Integer).Value = clientID;
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Регистрация прошла успешно! Теперь вы можете войти.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось подключиться к БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}