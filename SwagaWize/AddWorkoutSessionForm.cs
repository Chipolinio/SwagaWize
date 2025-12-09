using FitnessCenterApp.DataAccess;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace FitnessCenterApp.Forms
{
    public partial class AddWorkoutSessionForm : Form
    {
        private int? _selectedSessionID = null; // ID выбранной в таблице сессии

        public AddWorkoutSessionForm()
        {
            InitializeComponent();
            LoadWorkoutTypes();
            LoadTrainers();
            LoadAllSessions(); // Загрузка всех сессий в таблицу
        }

        private void LoadWorkoutTypes()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT TypeID, TypeName FROM WorkoutTypes", conn);
                var adapter = new OleDbDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                cmbType.DataSource = table;
                cmbType.DisplayMember = "TypeName";
                cmbType.ValueMember = "TypeID";
            }
        }

        private void LoadTrainers()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT TrainerID, FirstName & ' ' & LastName AS Name FROM Trainers", conn);
                var adapter = new OleDbDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                cmbTrainer.DataSource = table;
                cmbTrainer.DisplayMember = "Name";
                cmbTrainer.ValueMember = "TrainerID";
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
                    ws.Duration AS [Длительность (мин)],
                    ws.Price AS [Цена (руб)]
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

        private void dgvSessions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSessions.SelectedRows.Count > 0)
            {
                _selectedSessionID = Convert.ToInt32(dgvSessions.SelectedRows[0].Cells["SessionID"].Value);
            }
            else
            {
                _selectedSessionID = null;
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            // Очищаем поля для добавления новой сессии
            cmbType.SelectedIndex = -1;
            cmbTrainer.SelectedIndex = -1;
            dtpDateTime.Value = DateTime.Now;
            // --- СБРАСЫВАЕМ Duration и Price ---
            nudDuration.Value = nudDuration.Minimum; // Или установите значение по умолчанию
            nudPrice.Value = nudPrice.Minimum;       // Или установите значение по умолчанию
            // --- КОНЕЦ СБРОСА ---
            _selectedSessionID = null;
            this.Text = "Добавить тренировку";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedSessionID.HasValue)
            {
                LoadSessionData(_selectedSessionID.Value);
                this.Text = "Редактировать тренировку";
            }
            else
            {
                MessageBox.Show("Выберите тренировку для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSessionData(int sessionID)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new OleDbCommand(
                        "SELECT TypeID, TrainerID, SessionDateTime, Duration, Price FROM WorkoutSessions WHERE SessionID = ?", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = sessionID;
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        cmbType.SelectedValue = reader["TypeID"];
                        cmbTrainer.SelectedValue = reader["TrainerID"];
                        dtpDateTime.Value = Convert.ToDateTime(reader["SessionDateTime"]);
                        // --- ЗАГРУЖАЕМ Duration и Price ---
                        nudDuration.Value = Convert.ToDecimal(reader["Duration"]);
                        nudPrice.Value = Convert.ToDecimal(reader["Price"]);
                        // --- КОНЕЦ ЗАГРУЗКИ ---
                    }
                    else
                    {
                        MessageBox.Show("Тренировка не найдена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _selectedSessionID = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _selectedSessionID = null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbType.SelectedValue == null || cmbTrainer.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип и тренера.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int typeID = Convert.ToInt32(cmbType.SelectedValue);
            int trainerID = Convert.ToInt32(cmbTrainer.SelectedValue);
            DateTime dateTime = dtpDateTime.Value;

            // --- НОВЫЕ ПЕРЕМЕННЫЕ ---
            decimal price;
            int duration;

            if (!int.TryParse(nudDuration.Value.ToString(), out duration) || duration <= 0)
            {
                MessageBox.Show("Введите корректную длительность тренировки (целое число > 0).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(nudPrice.Value.ToString(), out price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену тренировки (число >= 0).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // --- КОНЕЦ НОВЫХ ПЕРЕМЕННЫХ ---

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    if (_selectedSessionID.HasValue)
                    {
                        // Редактирование существующей сессии
                        var cmd = new OleDbCommand(
                            "UPDATE WorkoutSessions SET TypeID = ?, TrainerID = ?, SessionDateTime = ?, Duration = ?, Price = ? WHERE SessionID = ?", conn);
                        cmd.Parameters.Add("", OleDbType.Integer).Value = typeID;
                        cmd.Parameters.Add("", OleDbType.Integer).Value = trainerID;
                        cmd.Parameters.Add("", OleDbType.Date).Value = dateTime;
                        // --- ДОБАВЛЯЕМ ПАРАМЕТРЫ ДЛЯ Duration и Price ---
                        cmd.Parameters.Add("", OleDbType.Integer).Value = duration;
                        cmd.Parameters.Add("", OleDbType.Currency).Value = price; // Используем Currency для цены
                        // --- КОНЕЦ ДОБАВЛЕНИЯ ПАРАМЕТРОВ ---
                        cmd.Parameters.Add("", OleDbType.Integer).Value = _selectedSessionID.Value;
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Тренировка успешно обновлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllSessions(); // Обновляем таблицу
                            _selectedSessionID = null;
                            this.Text = "Добавить тренировку";
                        }
                        else
                        {
                            MessageBox.Show("Не удалось обновить тренировку.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Добавление новой сессии
                        var cmd = new OleDbCommand(
                            "INSERT INTO WorkoutSessions (TypeID, TrainerID, SessionDateTime, Duration, Price) VALUES (?, ?, ?, ?, ?)", conn);
                        cmd.Parameters.Add("", OleDbType.Integer).Value = typeID;
                        cmd.Parameters.Add("", OleDbType.Integer).Value = trainerID;
                        cmd.Parameters.Add("", OleDbType.Date).Value = dateTime;
                        // --- ДОБАВЛЯЕМ ПАРАМЕТРЫ ДЛЯ Duration и Price ---
                        cmd.Parameters.Add("", OleDbType.Integer).Value = duration;
                        cmd.Parameters.Add("", OleDbType.Currency).Value = price; // Используем Currency для цены
                        // --- КОНЕЦ ДОБАВЛЕНИЯ ПАРАМЕТРОВ ---
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Тренировка успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllSessions(); // Обновляем таблицу
                        _selectedSessionID = null;
                        this.Text = "Добавить тренировку";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedSessionID.HasValue)
            {
                MessageBox.Show("Выберите тренировку для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить эту тренировку?\nЭто действие нельзя отменить.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new OleDbCommand("DELETE FROM WorkoutSessions WHERE SessionID = ?", conn);
                    cmd.Parameters.Add("", OleDbType.Integer).Value = _selectedSessionID.Value;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Тренировка успешно удалена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAllSessions(); // Обновляем таблицу
                        _selectedSessionID = null;
                        this.Text = "Добавить тренировку";
                    }
                    else
                    {
                        MessageBox.Show("Тренировка не найдена или уже удалена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}