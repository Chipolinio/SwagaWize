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

                    // 1. Проверка первой тренировки
                    var firstCmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations WHERE ClientID = ?", conn);
                    firstCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    int registrationCount = (int)firstCmd.ExecuteScalar();

                    if (registrationCount >= 1)
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🏆 Новичок",
                            Description = "Посетить первую тренировку",
                            IsUnlocked = true,
                            UnlockedDate = GetFirstRegistrationDate(conn),
                            Points = 10
                        });
                    }
                    else
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🏆 Новичок",
                            Description = "Посетить первую тренировку",
                            IsUnlocked = false,
                            Points = 10
                        });
                    }

                    // 2. Проверка 5 тренировок
                    if (registrationCount >= 5)
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🔥 Завсегдатай",
                            Description = "Посетить 5 тренировок",
                            IsUnlocked = true,
                            UnlockedDate = DateTime.Now.AddDays(-1), // Примерная дата
                            Points = 50
                        });
                    }
                    else
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🔥 Завсегдатай",
                            Description = $"Посетить 5 тренировок ({registrationCount}/5)",
                            IsUnlocked = false,
                            Points = 50
                        });
                    }

                    // 3. Утренние тренировки
                    var morningCmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations r " +
                        "INNER JOIN WorkoutSessions ws ON r.SessionID = ws.SessionID " +
                        "WHERE r.ClientID = ? AND DATEPART('h', ws.SessionDateTime) < 10", conn);
                    morningCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    int morningCount = (int)morningCmd.ExecuteScalar();

                    if (morningCount >= 3)
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🌅 Ранняя пташка",
                            Description = "Посетить 3 утренние тренировки (до 10:00)",
                            IsUnlocked = true,
                            Points = 30
                        });
                    }
                    else
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🌅 Ранняя пташка",
                            Description = $"Посетить 3 утренние тренировки ({morningCount}/3)",
                            IsUnlocked = false,
                            Points = 30
                        });
                    }

                    // 4. Разные типы тренировок
                    var typesCmd = new OleDbCommand(
                        "SELECT COUNT(DISTINCT wt.TypeName) FROM Registrations r " +
                        "INNER JOIN WorkoutSessions ws ON r.SessionID = ws.SessionID " +
                        "INNER JOIN WorkoutTypes wt ON ws.TypeID = wt.TypeID " +
                        "WHERE r.ClientID = ?", conn);
                    typesCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    int typesCount = (int)typesCmd.ExecuteScalar();

                    if (typesCount >= 3)
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🎯 Универсал",
                            Description = "Попробовать 3 разных типа тренировок",
                            IsUnlocked = true,
                            Points = 40
                        });
                    }
                    else
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "🎯 Универсал",
                            Description = $"Попробовать 3 разных типа тренировок ({typesCount}/3)",
                            IsUnlocked = false,
                            Points = 40
                        });
                    }

                    // 5. Неделя тренировок
                    var weekCmd = new OleDbCommand(
                        "SELECT COUNT(DISTINCT FORMAT(r.RegistrationDateTime, 'yyyy-mm-dd')) " +
                        "FROM Registrations r WHERE r.ClientID = ? " +
                        "AND r.RegistrationDateTime >= DATEADD('d', -7, NOW())", conn);
                    weekCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    int weekCount = (int)weekCmd.ExecuteScalar();

                    if (weekCount >= 3)
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "💪 Железная воля",
                            Description = "3 тренировки за последнюю неделю",
                            IsUnlocked = true,
                            Points = 35
                        });
                    }
                    else
                    {
                        achievements.Add(new Achievement
                        {
                            Title = "💪 Железная воля",
                            Description = $"3 тренировки за неделю ({weekCount}/3)",
                            IsUnlocked = false,
                            Points = 35
                        });
                    }

                    // 6. Виртуальный бейдж за регистрацию в системе
                    achievements.Add(new Achievement
                    {
                        Title = "🌟 Первый вход",
                        Description = "Зарегистрироваться в системе",
                        IsUnlocked = true,
                        UnlockedDate = DateTime.Now.AddDays(-registrationCount), // Примерная дата
                        Points = 5
                    });
                }

                // Отображаем достижения
                DisplayAchievements(achievements);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки достижений: " + ex.Message);
            }
        }

        private DateTime? GetFirstRegistrationDate(OleDbConnection conn)
        {
            var cmd = new OleDbCommand(
                "SELECT MIN(RegistrationDateTime) FROM Registrations WHERE ClientID = ?", conn);
            cmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
            var result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
                return Convert.ToDateTime(result);

            return null;
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

                if (achievement.IsUnlocked && achievement.UnlockedDate.HasValue)
                {
                    var dateLabel = new Label
                    {
                        Text = $"Получено: {achievement.UnlockedDate.Value:dd.MM.yyyy}",
                        Font = new Font("Arial", 8),
                        ForeColor = Color.DarkBlue,
                        Location = new Point(120, 75),
                        AutoSize = true
                    };
                    panel.Controls.Add(dateLabel);
                }

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
            int progress = (int)((double)unlockedCount / achievements.Count * 100);
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