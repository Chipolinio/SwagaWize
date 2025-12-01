using FitnessCenterApp.BusinessLogic;
using FitnessCenterApp.Models;
using SwagaWize;
using System;
using System.Windows.Forms;

namespace FitnessCenterApp.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            User user = AuthService.AuthenticateUser(username, password);

            if (user != null)
            {
                CurrentUser.Role = user.Role;
                CurrentUser.Username = user.Username;
                CurrentUser.ClientID = user.ClientID;

                this.Hide();

                if (user.Role == "Admin")
                {
                    new AdminForm().Show();
                }
                else
                {
                    new UserDashboardForm().Show();
                }
            }
            else
            {
                lblError.Text = "Неверный логин или пароль";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            new RegistrationForm().ShowDialog();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DataAccess.DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    MessageBox.Show("Подключение к БД успешно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }
    }
}