using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FitnessCenterApp.DataAccess;

namespace FitnessCenterApp.Forms
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
            LoadReportTypes();
        }

        private void LoadReportTypes()
        {
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("📊 Расписание тренировок");
            cmbReportType.SelectedIndex = 0;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cmbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип отчета", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable reportData = GenerateScheduleReport();

                if (reportData != null && reportData.Rows.Count > 0)
                {
                    dgvReport.DataSource = reportData;
                    lblStatus.Text = $"Сгенерировано записей: {reportData.Rows.Count}";
                    lblStatus.ForeColor = Color.Green;

                    // === Построение диаграммы ===
                    BuildChart(reportData);
                }
                else
                {
                    dgvReport.DataSource = null;
                    chartReport.Series.Clear();
                    chartReport.Titles.Clear();
                    lblStatus.Text = "Нет данных для отображения";
                    lblStatus.ForeColor = Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Ошибка генерации";
                lblStatus.ForeColor = Color.Red;
                chartReport.Series.Clear();
                chartReport.Titles.Clear();
                MessageBox.Show($"Ошибка генерации отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildChart(DataTable data)
        {
            chartReport.Series.Clear();
            chartReport.Titles.Clear();
            chartReport.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chartReport.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8);
            chartReport.Titles.Add("Количество тренировок по типам");

            var series = new Series("Типы")
            {
                ChartType = SeriesChartType.Column
            };

            var grouped = data.AsEnumerable()
                .GroupBy(row => row.Field<string>("Тип тренировки"))
                .Select(g => new { Type = g.Key, Count = g.Count() });

            foreach (var item in grouped)
            {
                series.Points.AddXY(item.Type, item.Count);
            }

            chartReport.Series.Add(series);
        }

        private DataTable GenerateScheduleReport()
        {
            string sql = @"
                SELECT 
                    wt.TypeName AS [Тип тренировки],
                    t.FirstName & ' ' & t.LastName AS [Тренер],
                    ws.SessionDateTime AS [Дата и время],
                    (SELECT COUNT(*) FROM Registrations r WHERE r.SessionID = ws.SessionID) AS [Зарегистрировано]
                FROM (WorkoutSessions ws
                INNER JOIN WorkoutTypes wt ON ws.TypeID = wt.TypeID)
                INNER JOIN Trainers t ON ws.TrainerID = t.TrainerID
                WHERE ws.SessionDateTime > NOW()
                ORDER BY ws.SessionDateTime";

            return ExecuteQuery(sql);
        }

        private DataTable ExecuteQuery(string sql)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var adapter = new OleDbDataAdapter(sql, conn);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv|Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FileName = $"Расписание_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                Title = "Экспорт расписания"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToCsv(saveDialog.FileName);
                    MessageBox.Show($"Расписание сохранено в файл:\n{saveDialog.FileName}", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine($"Отчет: {cmbReportType.SelectedItem}");
                writer.WriteLine($"Дата генерации: {DateTime.Now:dd.MM.yyyy HH:mm}");
                writer.WriteLine();

                var dataTable = (DataTable)dgvReport.DataSource;
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    writer.Write(dataTable.Columns[i].ColumnName);
                    if (i < dataTable.Columns.Count - 1)
                        writer.Write(";");
                }
                writer.WriteLine();

                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        string value = row[i].ToString().Replace(";", ",");
                        writer.Write(value);
                        if (i < dataTable.Columns.Count - 1)
                            writer.Write(";");
                    }
                    writer.WriteLine();
                }

                writer.WriteLine();
                writer.WriteLine($"Итого записей: {dataTable.Rows.Count}");
                writer.WriteLine($"Сгенерировано: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null || dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                FileName = $"Расписание_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                Title = "Экспорт в текстовый формат"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportToText(saveDialog.FileName);
                    MessageBox.Show($"Текстовый отчет сохранен:\n{saveDialog.FileName}", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка экспорта: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportToText(string filePath)
        {
            using (var writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("=".PadRight(60, '='));
                writer.WriteLine($"РАСПИСАНИЕ ТРЕНИРОВОК".PadLeft(40));
                writer.WriteLine($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}".PadLeft(40));
                writer.WriteLine("=".PadRight(60, '='));
                writer.WriteLine();

                var dataTable = (DataTable)dgvReport.DataSource;

                writer.WriteLine("┌" + new string('─', 58) + "┐");
                foreach (DataColumn column in dataTable.Columns)
                {
                    writer.WriteLine($"│ {column.ColumnName.PadRight(56)} │");
                }
                writer.WriteLine("├" + new string('─', 58) + "┤");

                int rowsToShow = Math.Min(dataTable.Rows.Count, 50);
                for (int i = 0; i < rowsToShow; i++)
                {
                    var row = dataTable.Rows[i];
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        string value = row[j].ToString();
                        writer.WriteLine($"│ {value.PadRight(56)} │");
                    }
                    if (i < rowsToShow - 1)
                        writer.WriteLine("├" + new string('─', 58) + "┤");
                }

                writer.WriteLine("└" + new string('─', 58) + "┘");
                writer.WriteLine();
                writer.WriteLine($"Всего тренировок в расписании: {dataTable.Rows.Count}");
                writer.WriteLine($"Показано: {rowsToShow}");
                writer.WriteLine($"Сгенерировано системой FitnessCenterApp");
                writer.WriteLine(new string('-', 60));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}