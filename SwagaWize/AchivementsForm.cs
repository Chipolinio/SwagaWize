using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;

namespace FitnessCenterApp.Forms
{
    public partial class AchievementsForm : Form
    {
        public AchievementsForm()
        {
            InitializeComponent();
            LoadAchievements();
        }

        private void LoadAchievements()
        {
            if (!CurrentUser.ClientID.HasValue) return;

            List<Achievement> achievements = new List<Achievement>();

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Подсчет тренировок
                    var cmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations WHERE ClientID = ?", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    object result = cmd.ExecuteScalar();
                    int registrationCount = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;

                    // Создаем достижения

                    // 1. Новичок
                    achievements.Add(new Achievement
                    {
                        Title = registrationCount >= 1 ? "🏆 Новичок" : "❓ Новичок",
                        Description = registrationCount >= 1 ?
                            "Первая тренировка ✓" :
                            "Посетить первую тренировку",
                        IsUnlocked = registrationCount >= 1,
                        Points = 10
                    });

                    // 2. Завсегдатай
                    achievements.Add(new Achievement
                    {
                        Title = registrationCount >= 5 ? "🔥 Завсегдатай" : "❓ Завсегдатай",
                        Description = registrationCount >= 5 ?
                            "5 тренировок ✓" :
                            $"Посетить 5 тренировок ({registrationCount}/5)",
                        IsUnlocked = registrationCount >= 5,
                        Points = 50
                    });

                    // 3. Мастер
                    achievements.Add(new Achievement
                    {
                        Title = registrationCount >= 10 ? "👑 Мастер" : "❓ Мастер",
                        Description = registrationCount >= 10 ?
                            "10 тренировок ✓" :
                            $"Посетить 10 тренировок ({registrationCount}/10)",
                        IsUnlocked = registrationCount >= 10,
                        Points = 100
                    });

                    // 4. Первый вход (всегда разблокировано)
                    achievements.Add(new Achievement
                    {
                        Title = "🌟 Активный пользователь",
                        Description = "Зарегистрироваться в системе ✓",
                        IsUnlocked = true,
                        Points = 5
                    });
                }
            }
            catch (Exception ex)
            {
                // Простые достижения в случае ошибки
                achievements.Add(new Achievement
                {
                    Title = "🌟 Добро пожаловать",
                    Description = "Вы в системе фитнес-центра",
                    IsUnlocked = true,
                    Points = 5
                });
            }

            // Отображаем достижения
            DisplayAchievements(achievements);
        }

        private void DisplayAchievements(List<Achievement> achievements)
        {
            flpAchievements.Controls.Clear();

            foreach (var achievement in achievements)
            {
                var panel = new Panel
                {
                    Width = 250,
                    Height = 100,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5),
                    BackColor = achievement.IsUnlocked ? Color.LightGreen : Color.LightGray
                };

                var titleLabel = new Label
                {
                    Text = achievement.Title,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    ForeColor = achievement.IsUnlocked ? Color.DarkGreen : Color.DarkGray,
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                var descLabel = new Label
                {
                    Text = achievement.Description,
                    Font = new Font("Arial", 9),
                    ForeColor = Color.Black,
                    Location = new Point(10, 35),
                    Size = new Size(220, 40)
                };

                var pointsLabel = new Label
                {
                    Text = $"{achievement.Points} очков",
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    ForeColor = Color.Blue,
                    Location = new Point(10, 75),
                    AutoSize = true
                };

                panel.Controls.Add(titleLabel);
                panel.Controls.Add(descLabel);
                panel.Controls.Add(pointsLabel);

                flpAchievements.Controls.Add(panel);
            }

            // Подсчет очков
            int totalPoints = 0;
            int unlockedCount = 0;
            foreach (var ach in achievements)
            {
                if (ach.IsUnlocked)
                {
                    totalPoints += ach.Points;
                    unlockedCount++;
                }
            }

            lblTotalPoints.Text = $"Всего очков: {totalPoints}";
            lblUnlockedCount.Text = $"Достижений: {unlockedCount}/{achievements.Count}";

            // Прогресс бар
            int progress = achievements.Count > 0 ? (int)((double)unlockedCount / achievements.Count * 100) : 0;
            pbProgress.Value = progress;
            lblProgress.Text = $"{progress}% выполнено";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class Achievement
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedDate { get; set; }
        public int Points { get; set; }
    }
}