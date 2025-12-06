using FitnessCenterApp.BusinessLogic;
using FitnessCenterApp.Models;
using System;
using System.Windows.Forms;

namespace FitnessCenterApp.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Показываем случайную цитату при загрузке
            ShowMotivationalQuote();
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

                // Показываем приветственную цитату
                ShowWelcomeQuote();

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

        private void ShowMotivationalQuote()
        {
            string[] quotes =
            {
                "💪 Сила не в мышцах, а в силе воли!",
                "🔥 Каждая тренировка - шаг к лучшей версии себя!",
                "🚀 Начни сегодня - гордись завтра!",
                "🌟 Твое тело способно на большее, чем ты думаешь!",
                "🎯 Цель - не идеал, а прогресс!",
                "⚡ Энергия приходит через действие!",
                "🏆 Победа начинается с первого шага в зал!"
            };

            Random rnd = new Random();
            string quote = quotes[rnd.Next(quotes.Length)];

            lblQuote.Text = quote;
            lblQuote.Visible = true;
        }

        private void ShowWelcomeQuote()
        {
            string[] welcomeQuotes =
            {
                "💪 Отличная работа! Твой путь к фитнесу начинается!",
                "🔥 Добро пожаловать! Готовься стать сильнее!",
                "🚀 Приветствуем! Новые достижения ждут тебя!",
                "🌟 Рады видеть! Твоя энергия вдохновляет!",
                "🎯 С возвращением! Цели становятся ближе!"
            };

            Random rnd = new Random();
            string quote = welcomeQuotes[rnd.Next(welcomeQuotes.Length)];

            MessageBox.Show(quote, "Добро пожаловать!",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("✅ Подключение к БД успешно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private void btnRefreshQuote_Click(object sender, EventArgs e)
        {
            ShowMotivationalQuote();
        }
    }
}