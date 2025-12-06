using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;
using FitnessCenterApp.Forms;

namespace FitnessCenterApp.Forms
{
    public partial class ManageTrainersForm : Form
    {
        private DataTable trainersTable;

        public ManageTrainersForm()
        {
            InitializeComponent();
        }

        private void ManageTrainersForm_Load(object sender, EventArgs e)
        {
            LoadTrainers();
        }

        private void LoadTrainers()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new OleDbCommand(
                        "SELECT t.*, " +
                        "(SELECT COUNT(*) FROM WorkoutSessions ws " +
                        " WHERE ws.TrainerID = t.TrainerID " +
                        " AND ws.SessionDateTime > DATE()) AS [Будущих тренировок] " +
                        "FROM Trainers t ORDER BY LastName, FirstName", conn);

                    var adapter = new OleDbDataAdapter(cmd);
                    trainersTable = new DataTable();
                    adapter.Fill(trainersTable);
                    dataGridView1.DataSource = trainersTable;

                    // Раскрашиваем строки по загруженности
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Будущих тренировок"].Value != null)
                        {
                            int count = 0;
                            if (int.TryParse(row.Cells["Будущих тренировок"].Value.ToString(), out count))
                            {
                                Color rowColor = GetTrainerColor(count);
                                row.DefaultCellStyle.BackColor = rowColor;

                                // Добавляем иконку загруженности
                                if (count > 10)
                                    row.Cells["FirstName"].Value = "🔴 " + row.Cells["FirstName"].Value;
                                else if (count > 5)
                                    row.Cells["FirstName"].Value = "🟡 " + row.Cells["FirstName"].Value;
                                else if (count == 0)
                                    row.Cells["FirstName"].Value = "🟢 " + row.Cells["FirstName"].Value;
                            }
                        }
                    }

                    // Добавляем подсказку
                    AddColorLegend();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тренеров: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Color GetTrainerColor(int sessionCount)
        {
            if (sessionCount > 10)
                return Color.LightPink;     // Очень занят
            else if (sessionCount > 5)
                return Color.LightYellow;   // Занят
            else if (sessionCount > 0)
                return Color.LightBlue;     // Умеренная загрузка
            else
                return Color.LightGreen;    // Свободен
        }

        private void AddColorLegend()
        {
            var legendPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle
            };

            var legendLabel = new Label
            {
                Text = "Легенда: 🔴 Очень занят (>10) | 🟡 Занят (6-10) | 🟢 Свободен (0) | 🔵 Умеренная загрузка (1-5)",
                Font = new Font("Arial", 9),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            legendPanel.Controls.Add(legendLabel);
            this.Controls.Add(legendPanel);

            // Перемещаем кнопки выше
            btnAdd.Top -= 40;
            btnEdit.Top -= 40;
            btnDelete.Top -= 40;
            btnClose.Top -= 40;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var input1 = new InputDialog("Добавление тренера", "Имя:");
            if (input1.ShowDialog() != DialogResult.OK) return;
            string firstName = input1.InputText.Trim();
            var input2 = new InputDialog("Добавление тренера", "Фамилия:");
            if (input2.ShowDialog() != DialogResult.OK) return;
            string lastName = input2.InputText.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Имя и фамилия не могут быть пустыми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    var cmd = new OleDbCommand(
                        "INSERT INTO Trainers (FirstName, LastName) VALUES (?, ?)", conn);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Тренер успешно добавлен! ✅", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTrainers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении тренера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите тренера для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int trainerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TrainerID"].Value);
            string currentFirst = dataGridView1.SelectedRows[0].Cells["FirstName"].Value?.ToString() ?? "";
            string currentLast = dataGridView1.SelectedRows[0].Cells["LastName"].Value?.ToString() ?? "";

            // Убираем смайлик если есть
            if (currentFirst.StartsWith("🔴 ") || currentFirst.StartsWith("🟡 ") ||
                currentFirst.StartsWith("🟢 ") || currentFirst.StartsWith("🔵 "))
            {
                currentFirst = currentFirst.Substring(2);
            }

            using (var editForm = new EditTrainerForm(currentFirst, currentLast))
            {
                if (editForm.ShowDialog() != DialogResult.OK) return;

                string newFirst = editForm.FirstName?.Trim() ?? "";
                string newLast = editForm.LastName?.Trim() ?? "";

                if (string.IsNullOrEmpty(newFirst) || string.IsNullOrEmpty(newLast))
                {
                    MessageBox.Show("Имя и фамилия не могут быть пустыми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (var conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        var cmd = new OleDbCommand(
                            "UPDATE Trainers SET FirstName = ?, LastName = ? WHERE TrainerID = ?", conn);
                        cmd.Parameters.Add("@FirstName", OleDbType.VarChar).Value = newFirst;
                        cmd.Parameters.Add("@LastName", OleDbType.VarChar).Value = newLast;
                        cmd.Parameters.Add("@TrainerID", OleDbType.Integer).Value = trainerID;

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Данные тренера обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTrainers(); // Обновляем список на форме
                        }
                        else
                        {
                            MessageBox.Show("Тренер не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении тренера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите тренера для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Вы уверены, что хотите удалить выбранного тренера?\nЭто действие нельзя отменить.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            int trainerID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TrainerID"].Value);

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Проверка наличия связанных сессий
                    var cmdCheck = new OleDbCommand("SELECT COUNT(*) FROM WorkoutSessions WHERE TrainerID = ?", conn);
                    cmdCheck.Parameters.AddWithValue("@TrainerID", trainerID);
                    int sessionCount = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (sessionCount > 0)
                    {
                        MessageBox.Show(
                            "Нельзя удалить тренера: существуют тренировки, связанные с ним.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var cmd = new OleDbCommand("DELETE FROM Trainers WHERE TrainerID = ?", conn);
                    cmd.Parameters.AddWithValue("@TrainerID", trainerID);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Тренер успешно удалён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTrainers(); // Обновляем список на форме
                    }
                    else
                    {
                        MessageBox.Show("Тренер не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении тренера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}