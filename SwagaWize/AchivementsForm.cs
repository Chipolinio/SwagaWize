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

                    // 1. Простой подсчет всех тренировок
                    var cmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations WHERE ClientID = ?", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    object result = cmd.ExecuteScalar();
                    int registrationCount = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;

                    // 2. Получаем дату первой тренировки
                    DateTime? firstRegistrationDate = null;
                    var dateCmd = new OleDbCommand(
                        "SELECT MIN(RegistrationDateTime) FROM Registrations WHERE ClientID = ?", conn);
                    dateCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    object dateResult = dateCmd.ExecuteScalar();
                    if (dateResult != null && dateResult != DBNull.Value)
                    {
                        firstRegistrationDate = Convert.ToDateTime(dateResult);
                    }

                    // 3. Утренние тренировки (упрощенный запрос для Access)
                    int morningCount = 0;
                    try
                    {
                        var morningCmd = new OleDbCommand(
                            "SELECT COUNT(*) FROM Registrations r " +
                            "INNER JOIN WorkoutSessions ws ON r.SessionID = ws.SessionID " +
                            "WHERE r.ClientID = ? AND FORMAT(ws.SessionDateTime, 'hh') < 10", conn);
                        morningCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                        object morningResult = morningCmd.ExecuteScalar();
                        if (morningResult != null && morningResult != DBNull.Value)
                            morningCount = Convert.ToInt32(morningResult);
                    }
                    catch { } // Игнорируем ошибки в этом запросе

                    // 4. Разные типы тренировок (упрощенный запрос)
                    int typesCount = 0;
                    try
                    {
                        var typesCmd = new OleDbCommand(
                            "SELECT COUNT(DISTINCT TypeName) FROM (" +
                            "SELECT wt.TypeName FROM Registrations r " +
                            "INNER JOIN WorkoutSessions ws ON r.SessionID = ws.SessionID " +
                            "INNER JOIN WorkoutTypes wt ON ws.TypeID = wt.TypeID " +
                            "WHERE r.ClientID = ?)", conn);
                        typesCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                        object typesResult = typesCmd.ExecuteScalar();
                        if (typesResult != null && typesResult != DBNull.Value)
                            typesCount = Convert.ToInt32(typesResult);
                    }
                    catch { } // Игнорируем ошибки

                    // 5. Неделя тренировок
                    int weekCount = 0;
                    try
                    {
                        var weekCmd = new OleDbCommand(
                            "SELECT COUNT(*) FROM Registrations " +
                            "WHERE ClientID = ? AND RegistrationDateTime >= DATE()-7", conn);
                        weekCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                        object weekResult = weekCmd.ExecuteScalar();
                        if (weekResult != null && weekResult != DBNull.Value)
                            weekCount = Convert.ToInt32(weekResult);
                    }
                    catch { } // Игнорируем ошибки

                    // Создаем достижения

                    // 1. Новичок
                    achievements.Add(new Achievement
                    {
                        Title = "🏆 Новичок",
                        Description = registrationCount >= 1 ?
                            "Первая тренировка ✓" :
                            "Посетить первую тренировку",
                        IsUnlocked = registrationCount >= 1,
                        UnlockedDate = registrationCount >= 1 ? firstRegistrationDate : null,
                        Points = 10
                    });

                    // 2. Завсегдатай
                    achievements.Add(new Achievement
                    {
                        Title = "🔥 Завсегдатай",
                        Description = registrationCount >= 5 ?
                            "5 тренировок ✓" :
                            $"Посетить 5 тренировок ({registrationCount}/5)",
                        IsUnlocked = registrationCount >= 5,
                        Points = 50
                    });

                    // 3. Ранняя пташка
                    achievements.Add(new Achievement
                    {
                        Title = "🌅 Ранняя пташка",
                        Description = morningCount >= 3 ?
                            "3 утренние тренировки ✓" :
                            $"3 утренние тренировки ({morningCount}/3)",
                        IsUnlocked = morningCount >= 3,
                        Points = 30
                    });

                    // 4. Универсал
                    achievements.Add(new Achievement
                    {
                        Title = "🎯 Универсал",
                        Description = typesCount >= 3 ?
                            "3 типа тренировок ✓" :
                            $"Попробовать 3 типа ({typesCount}/3)",
                        IsUnlocked = typesCount >= 3,
                        Points = 40
                    });

                    // 5. Железная воля
                    achievements.Add(new Achievement
                    {
                        Title = "💪 Железная воля",
                        Description = weekCount >= 3 ?
                            "3 тренировки за неделю ✓" :
                            $"3 тренировки за неделю ({weekCount}/3)",
                        IsUnlocked = weekCount >= 3,
                        Points = 35
                    });

                    // 6. Первый вход (всегда разблокировано)
                    achievements.Add(new Achievement
                    {
                        Title = "🌟 Первый вход",
                        Description = "Регистрация в системе ✓",
                        IsUnlocked = true,
                        Points = 5
                    });

                    // 7. Мастер (дополнительное)
                    achievements.Add(new Achievement
                    {
                        Title = "👑 Мастер",
                        Description = registrationCount >= 10 ?
                            "10 тренировок ✓" :
                            $"Посетить 10 тренировок ({registrationCount}/10)",
                        IsUnlocked = registrationCount >= 10,
                        Points = 100
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

                achievements.Add(new Achievement
                {
                    Title = "🏆 Первая тренировка",
                    Description = "Запишитесь на первую тренировку",
                    IsUnlocked = false,
                    Points = 10
                });

                achievements.Add(new Achievement
                {
                    Title = "🎯 Разнообразие",
                    Description = "Попробуйте разные типы тренировок",
                    IsUnlocked = false,
                    Points = 30
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