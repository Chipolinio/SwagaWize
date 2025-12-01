using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;
using FitnessCenterApp.Forms; // чтобы использовать InputDialog

namespace FitnessCenterApp.Forms
{
    public partial class ManageTrainersForm : Form
    {
        private DataTable trainersTable;

        public ManageTrainersForm()
        {
            InitializeComponent();
            LoadTrainers();
        }

        private void LoadTrainers()
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new OleDbCommand("SELECT * FROM Trainers", conn);
                var adapter = new OleDbDataAdapter(cmd);
                trainersTable = new DataTable();
                adapter.Fill(trainersTable);
                dataGridView1.DataSource = trainersTable;
            }
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
                MessageBox.Show("Тренер успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Создаем новое окно для редактирования
            using (var editForm = new EditTrainerForm(currentFirst, currentLast))
            {
                if (editForm.ShowDialog() != DialogResult.OK) return;

                string newFirst = editForm.FirstName.Trim();
                string newLast = editForm.LastName.Trim();

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
                        cmd.Parameters.Add("", OleDbType.VarChar).Value = newFirst;
                        cmd.Parameters.Add("", OleDbType.VarChar).Value = newLast;
                        cmd.Parameters.Add("", OleDbType.Integer).Value = trainerID;
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Данные тренера обновлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTrainers();
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
                    int sessionCount = (int)cmdCheck.ExecuteScalar();

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
                        LoadTrainers();
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