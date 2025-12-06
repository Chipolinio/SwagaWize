using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;

namespace FitnessCenterApp.Forms
{
    public partial class UserDashboardForm : Form
    {
        public UserDashboardForm()
        {
            InitializeComponent();
            DateTime today = DateTime.Today;
            dtpDateFrom.Value = today;
            dtpDateTo.Value = today.AddDays(7);
            chkFilterByDate.Checked = false;
        }

        private void UserDashboardForm_Load(object sender, EventArgs e)
        {
            LoadWorkoutTypes();
            LoadAllSessions();

            // Показать достижения при загрузке
            ShowDailyAchievements();
        }

        private void LoadWorkoutTypes()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT TypeID, TypeName FROM WorkoutTypes ORDER BY TypeName", conn);
                var adapter = new OleDbDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);

                // Добавляем "Все типы" в начало
                var allRow = table.NewRow();
                allRow["TypeID"] = DBNull.Value;
                allRow["TypeName"] = "— Все типы —";
                table.Rows.InsertAt(allRow, 0);

                cmbWorkoutType.DataSource = table;
                cmbWorkoutType.DisplayMember = "TypeName";
                cmbWorkoutType.ValueMember = "TypeID";
                cmbWorkoutType.SelectedIndex = 0;
            }
        }

        private void LoadAllSessions()
        {
            string sql = @"
                SELECT 
                    ws.SessionID,
                    wt.TypeName AS [Тип],
                    t.FirstName & ' ' & t.LastName AS [Тренер],
                    ws.SessionDateTime AS [Дата и время],
                    CASE 
                        WHEN DATEPART('h', ws.SessionDateTime) < 10 THEN '😴 Утро' 
                        WHEN DATEPART('h', ws.SessionDateTime) < 18 THEN '😎 День'
                        ELSE '🌙 Вечер'
                    END AS [Время суток]
                FROM (WorkoutSessions ws
                INNER JOIN WorkoutTypes wt ON ws.TypeID = wt.TypeID)
                INNER JOIN Trainers t ON ws.TrainerID = t.TrainerID
                ORDER BY ws.SessionDateTime";

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var adapter = new OleDbDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);

                // Добавляем смайлик к типу тренировки
                foreach (DataRow row in table.Rows)
                {
                    string type = row["Тип"].ToString();
                    row["Тип"] = GetWorkoutEmoji(type) + " " + type;
                }

                dgvSessions.DataSource = table;
            }
        }

        private string GetWorkoutEmoji(string type)
        {
            if (string.IsNullOrEmpty(type)) return "🏋️";

            if (type.Contains("Йога") || type.Contains("Yoga")) return "🧘";
            if (type.Contains("Бокс") || type.Contains("Box")) return "🥊";
            if (type.Contains("Плавание") || type.Contains("Swim")) return "🏊";
            if (type.Contains("Бег") || type.Contains("Run") || type.Contains("Cardio")) return "🏃";
            if (type.Contains("Силов") || type.Contains("Power") || type.Contains("Strength")) return "💪";
            if (type.Contains("Танц") || type.Contains("Dance")) return "💃";
            if (type.Contains("Кардио") || type.Contains("Cardio")) return "❤️";
            if (type.Contains("Стретчинг") || type.Contains("Stretch")) return "🤸";

            return "🏋️";
        }

        private void LoadFilteredSessions()
        {
            string baseSql = @"
                SELECT 
                    ws.SessionID,
                    wt.TypeName AS [Тип],
                    t.FirstName & ' ' & t.LastName AS [Тренер],
                    ws.SessionDateTime AS [Дата и время],
                    CASE 
                        WHEN DATEPART('h', ws.SessionDateTime) < 10 THEN '😴 Утро' 
                        WHEN DATEPART('h', ws.SessionDateTime) < 18 THEN '😎 День'
                        ELSE '🌙 Вечер'
                    END AS [Время суток]
                FROM (WorkoutSessions ws
                INNER JOIN WorkoutTypes wt ON ws.TypeID = wt.TypeID)
                INNER JOIN Trainers t ON ws.TrainerID = t.TrainerID";

            var conditions = new List<string>();
            var parameters = new List<OleDbType>();
            var values = new List<object>();

            // Фильтр по типу
            if (cmbWorkoutType.SelectedIndex > 0)
            {
                conditions.Add("wt.TypeName = ?");
                parameters.Add(OleDbType.VarChar);
                values.Add(cmbWorkoutType.Text);
            }

            // Фильтр по диапазону дат
            if (chkFilterByDate.Checked)
            {
                conditions.Add("ws.SessionDateTime >= ?");
                conditions.Add("ws.SessionDateTime <= ?");
                parameters.Add(OleDbType.Date);
                parameters.Add(OleDbType.Date);
                values.Add(dtpDateFrom.Value.Date);
                values.Add(dtpDateTo.Value.Date.AddDays(1).AddTicks(-1)); // включая весь конечный день
            }

            string whereClause = "";
            if (conditions.Count > 0)
            {
                whereClause = " WHERE " + string.Join(" AND ", conditions);
            }

            string orderBy = " ORDER BY ws.SessionDateTime";
            string finalSql = baseSql + whereClause + orderBy;

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                using (var cmd = new OleDbCommand(finalSql, conn))
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        cmd.Parameters.Add("", parameters[i]).Value = values[i];
                    }

                    var adapter = new OleDbDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    // Добавляем смайлики
                    foreach (DataRow row in table.Rows)
                    {
                        string type = row["Тип"].ToString();
                        row["Тип"] = GetWorkoutEmoji(type) + " " + type;
                    }

                    dgvSessions.DataSource = table;
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadFilteredSessions();
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            LoadAllSessions();
            cmbWorkoutType.SelectedIndex = 0;
            DateTime today = DateTime.Today;
            dtpDateFrom.Value = today;
            dtpDateTo.Value = today.AddDays(7);
            chkFilterByDate.Checked = false;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (dgvSessions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите тренировку для регистрации.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CurrentUser.ClientID.HasValue)
            {
                MessageBox.Show("Ошибка: у вас нет профиля клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int sessionID = Convert.ToInt32(dgvSessions.SelectedRows[0].Cells["SessionID"].Value);

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Проверка: уже зарегистрирован?
                    var checkCmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations WHERE ClientID = ? AND SessionID = ?", conn);
                    checkCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    checkCmd.Parameters.Add("", OleDbType.Integer).Value = sessionID;
                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("Вы уже зарегистрированы на эту тренировку.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Регистрация
                    var cmd = new OleDbCommand(
                        "INSERT INTO Registrations (ClientID, SessionID, RegistrationDateTime) VALUES (?, ?, ?)", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    cmd.Parameters.Add("", OleDbType.Integer).Value = sessionID;
                    cmd.Parameters.Add("", OleDbType.Date).Value = DateTime.Now;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Вы успешно зарегистрированы на тренировку!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Проверить достижения после регистрации
                    CheckAndShowAchievements();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDailyAchievements()
        {
            // Простая проверка достижений
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Проверка первой тренировки
                    var cmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations WHERE ClientID = ?", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    int registrationCount = (int)cmd.ExecuteScalar();

                    if (registrationCount == 1)
                    {
                        ShowAchievementPopup("🏆 Новичок", "Поздравляем с первой регистрацией на тренировку!");
                    }
                    else if (registrationCount >= 5)
                    {
                        ShowAchievementPopup("🔥 Завсегдатай", "Вы уже посетили 5 тренировок! Так держать!");
                    }

                    // Проверка утренних тренировок
                    var morningCmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations r " +
                        "INNER JOIN WorkoutSessions ws ON r.SessionID = ws.SessionID " +
                        "WHERE r.ClientID = ? AND DATEPART('h', ws.SessionDateTime) < 10", conn);
                    morningCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    int morningCount = (int)morningCmd.ExecuteScalar();

                    if (morningCount >= 3)
                    {
                        ShowAchievementPopup("🌅 Ранняя пташка", "3 утренние тренировки - вы настоящий ранняя пташка!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки в достижениях
                Console.WriteLine("Ошибка проверки достижений: " + ex.Message);
            }
        }

        private void CheckAndShowAchievements()
        {
            // Перепроверяем достижения после регистрации
            ShowDailyAchievements();
        }

        private void ShowAchievementPopup(string title, string message)
        {
            var form = new Form
            {
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Text = title,
                BackColor = Color.Gold
            };

            var label = new Label
            {
                Text = message,
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            var button = new Button
            {
                Text = "🎉 Ура!",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };

            button.Click += (s, e) => form.Close();

            form.Controls.Add(label);
            form.Controls.Add(button);

            // Показываем ненадолго
            form.Show();

            var timer = new Timer { Interval = 3000 };
            timer.Tick += (s, e) => { form.Close(); timer.Stop(); };
            timer.Start();
        }

        private void btnShowAchievements_Click(object sender, EventArgs e)
        {
            new AchievementsForm().ShowDialog();
        }

        private void btnRandomWorkout_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new OleDbCommand(
                        "SELECT TOP 1 ws.SessionID, wt.TypeName, t.FirstName, t.LastName, ws.SessionDateTime " +
                        "FROM ((WorkoutSessions ws " +
                        "INNER JOIN WorkoutTypes wt ON ws.TypeID = wt.TypeID) " +
                        "INNER JOIN Trainers t ON ws.TrainerID = t.TrainerID) " +
                        "WHERE ws.SessionDateTime > NOW() " +
                        "ORDER BY RND(-Timer()*[SessionID])", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string type = reader["TypeName"].ToString();
                            string trainer = $"{reader["FirstName"]} {reader["LastName"]}";
                            DateTime time = Convert.ToDateTime(reader["SessionDateTime"]);
                            int sessionId = Convert.ToInt32(reader["SessionID"]);

                            string emoji = GetWorkoutEmoji(type);
                            string message = $"{emoji} {type}\n" +
                                           $"👤 Тренер: {trainer}\n" +
                                           $"🕐 {time:dd.MM HH:mm}\n\n" +
                                           $"Попробуй что-то новое сегодня!";

                            if (MessageBox.Show(message, "🎲 Случайная тренировка",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                // Автоматически записываем на эту тренировку
                                RegisterForSpecificSession(sessionId);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Нет доступных тренировок", "Информация",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void RegisterForSpecificSession(int sessionId)
        {
            if (!CurrentUser.ClientID.HasValue) return;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Проверка: уже зарегистрирован?
                    var checkCmd = new OleDbCommand(
                        "SELECT COUNT(*) FROM Registrations WHERE ClientID = ? AND SessionID = ?", conn);
                    checkCmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    checkCmd.Parameters.Add("", OleDbType.Integer).Value = sessionId;
                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        MessageBox.Show("Вы уже зарегистрированы на эту тренировку.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Регистрация
                    var cmd = new OleDbCommand(
                        "INSERT INTO Registrations (ClientID, SessionID, RegistrationDateTime) VALUES (?, ?, ?)", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = CurrentUser.ClientID.Value;
                    cmd.Parameters.Add("", OleDbType.Integer).Value = sessionId;
                    cmd.Parameters.Add("", OleDbType.Date).Value = DateTime.Now;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Вы успешно зарегистрированы на случайную тренировку! 🎲", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllSessions(); // Обновляем список
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkFilterByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpDateFrom.Enabled = chkFilterByDate.Checked;
            dtpDateTo.Enabled = chkFilterByDate.Checked;
        }
    }
}