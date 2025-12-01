using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
                    ws.SessionDateTime AS [Дата и время]
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
                dgvSessions.DataSource = table;
            }
        }

        private void LoadFilteredSessions()
        {
            string baseSql = @"
        SELECT 
            ws.SessionID,
            wt.TypeName AS [Тип],
            t.FirstName & ' ' & t.LastName AS [Тренер],
            ws.SessionDateTime AS [Дата и время]
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
            dtpDateTo.Value = today.AddDays(7); // или today, как тебе нужно
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}