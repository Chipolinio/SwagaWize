using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;

namespace FitnessCenterApp.Forms
{
    public partial class CreateTableForm : Form
    {
        private List<TableColumn> _columns = new List<TableColumn>();

        public CreateTableForm()
        {
            InitializeComponent();
            LoadExistingTables();
            SetupDataTypes();
        }

        private void SetupDataTypes()
        {
            cmbColType.Items.Clear(); // Очищаем перед заполнением
            cmbColType.Items.AddRange(new string[]
            {
                "TEXT", "INTEGER", "DATE", "BOOLEAN", "CURRENCY", "MEMO"
            });
            cmbColType.SelectedIndex = 0; // Выбираем первый элемент по умолчанию
        }

        private void LoadExistingTables()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    DataTable schema = conn.GetSchema("Tables");
                    var table = new DataTable();
                    table.Columns.Add("TableName", typeof(string));

                    foreach (DataRow row in schema.Rows)
                    {
                        string name = row["TABLE_NAME"].ToString();
                        if (row["TABLE_TYPE"].ToString() == "TABLE" && !name.StartsWith("MSys"))
                        {
                            table.Rows.Add(name);
                        }
                    }
                    dgvExistingTables.DataSource = table;

                    // Обновляем список для cmbRefTable
                    cmbRefTable.Items.Clear();
                    foreach (DataRow row in table.Rows)
                    {
                        cmbRefTable.Items.Add(row["TableName"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки таблиц: " + ex.Message);
            }
        }

        private void btnViewStructure_Click(object sender, EventArgs e)
        {
            if (dgvExistingTables.CurrentRow == null)
            {
                MessageBox.Show("Выберите таблицу.");
                return;
            }
            string tableName = dgvExistingTables.CurrentRow.Cells[0].Value.ToString();
            lstStructure.Items.Clear();
            lstStructure.Items.Add($"Структура: {tableName}");
            lstStructure.Items.Add("---");

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    DataTable schema = conn.GetSchema("Columns", new[] { null, null, tableName, null });
                    foreach (DataRow r in schema.Rows)
                    {
                        string nullable = r["IS_NULLABLE"].ToString() == "YES" ? "NULL" : "NOT NULL";
                        lstStructure.Items.Add($"{r["COLUMN_NAME"]} ({r["DATA_TYPE"]}) {nullable}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void chkForeignKey_CheckedChanged(object sender, EventArgs e)
        {
            cmbRefTable.Enabled = chkForeignKey.Checked;
            cmbRefColumn.Enabled = chkForeignKey.Checked && cmbRefTable.SelectedItem != null;
            if (!chkForeignKey.Checked)
            {
                cmbRefTable.SelectedIndex = -1;
                cmbRefColumn.Items.Clear(); // Очищаем список колонок
                cmbRefColumn.SelectedIndex = -1;
            }
        }

        private void cmbRefTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRefColumn.Items.Clear(); // Сначала очищаем
            cmbRefColumn.Enabled = cmbRefTable.SelectedItem != null && chkForeignKey.Checked;

            if (cmbRefTable.SelectedItem == null) return;

            string tableName = cmbRefTable.SelectedItem.ToString();
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    DataTable schema = conn.GetSchema("Columns", new[] { null, null, tableName, null });
                    foreach (DataRow r in schema.Rows)
                    {
                        cmbRefColumn.Items.Add(r["COLUMN_NAME"].ToString());
                    }
                    if (cmbRefColumn.Items.Count > 0)
                    {
                        cmbRefColumn.SelectedIndex = 0; // Выбираем первую колонку по умолчанию
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки колонок: " + ex.Message);
                cmbRefColumn.Enabled = false;
            }
        }

        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtColName.Text))
            {
                MessageBox.Show("Введите имя колонки.");
                return;
            }
            if (cmbColType.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип данных.");
                return;
            }

            var col = new TableColumn
            {
                Name = txtColName.Text.Trim(),
                Type = cmbColType.SelectedItem.ToString(),
                IsForeignKey = chkForeignKey.Checked
            };

            if (chkForeignKey.Checked)
            {
                if (cmbRefTable.SelectedItem == null || cmbRefColumn.SelectedItem == null)
                {
                    MessageBox.Show("Выберите таблицу и колонку для связи.");
                    return;
                }
                col.ReferencedTable = cmbRefTable.SelectedItem.ToString();
                col.ReferencedColumn = cmbRefColumn.SelectedItem.ToString();
            }

            // Проверим, не дублируется ли имя колонки
            if (_columns.Exists(c => c.Name.Equals(col.Name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show($"Колонка с именем '{col.Name}' уже добавлена.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _columns.Add(col);
            UpdateColumnList();
            txtColName.Clear(); // Очищаем поле имени колонки
            chkForeignKey.Checked = false; // Сбрасываем чекбокс FK
            // Остальные поля (cmbRefTable, cmbRefColumn) сбросятся автоматически через обработчики
        }

        private void btnRemoveColumn_Click(object sender, EventArgs e)
        {
            if (lstColumns.SelectedIndex >= 0 && lstColumns.SelectedIndex < _columns.Count)
            {
                _columns.RemoveAt(lstColumns.SelectedIndex);
                UpdateColumnList();
            }
            else
            {
                MessageBox.Show("Выберите колонку для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateColumnList()
        {
            lstColumns.Items.Clear();
            foreach (var c in _columns)
            {
                string fk = c.IsForeignKey ? $" 🔗→ {c.ReferencedTable}.{c.ReferencedColumn}" : "";
                lstColumns.Items.Add($"{c.Name} ({c.Type}){fk}");
            }
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewTableName.Text))
            {
                MessageBox.Show("Введите имя таблицы.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_columns.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну колонку.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newTableName = txtNewTableName.Text.Trim();
            if (TableExists(newTableName))
            {
                MessageBox.Show("Таблица с таким именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверим, нет ли колонок без имени или с дубликатами
            var names = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var c in _columns)
            {
                if (string.IsNullOrWhiteSpace(c.Name))
                {
                    MessageBox.Show("Все колонки должны иметь имя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (names.Contains(c.Name))
                {
                    MessageBox.Show($"Имя колонки '{c.Name}' дублируется.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                names.Add(c.Name);
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Создаём новую таблицу
                    var defs = new List<string>();
                    foreach (var c in _columns)
                    {
                        // Маппинг типов на типы MS Access
                        string mappedType;
                        switch (c.Type)
                        {
                            case "TEXT": mappedType = "TEXT(255)"; break;
                            case "INTEGER": mappedType = "INTEGER"; break;
                            case "DATE": mappedType = "DATETIME"; break; // DATETIME в Access
                            case "BOOLEAN": mappedType = "YESNO"; break; // YESNO в Access
                            case "CURRENCY": mappedType = "CURRENCY"; break;
                            case "MEMO": mappedType = "MEMO"; break; // MEMO в Access
                            default: mappedType = "TEXT(255)"; break; // fallback
                        }
                        defs.Add($"[{c.Name}] {mappedType}");
                    }
                    string createSql = $"CREATE TABLE [{newTableName}] ({string.Join(", ", defs)})";

                    using (var cmd = new OleDbCommand(createSql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    System.Diagnostics.Debug.WriteLine($"Таблица [{newTableName}] создана.");

                    // 2. Для каждой колонки с FK — добавляем колонку в СУЩЕСТВУЮЩУЮ таблицу и создаём связь
                    foreach (var col in _columns.FindAll(c => c.IsForeignKey))
                    {
                        string ownerTable = col.ReferencedTable;      // в какую существующую таблицу добавим FK
                        string fkColName = col.Name;                  // имя новой колонки (напр. ClientID)
                        string refTable = newTableName;               // наша новая таблица
                        string refCol = col.ReferencedColumn;         // колонка в новой таблице (обычно ID)

                        // Проверка: есть ли уже такая колонка в внешней таблице?
                        bool hasCol = false;
                        DataTable colsSchema = conn.GetSchema("Columns", new[] { null, null, ownerTable, null });
                        foreach (DataRow r in colsSchema.Rows)
                        {
                            if (r["COLUMN_NAME"].ToString().Equals(fkColName, StringComparison.OrdinalIgnoreCase))
                            {
                                hasCol = true;
                                break;
                            }
                        }

                        // Добавляем колонку внешнего ключа во внешнюю таблицу, если её нет
                        if (!hasCol)
                        {
                            using (var cmdAddCol = new OleDbCommand($"ALTER TABLE [{ownerTable}] ADD COLUMN [{fkColName}] INTEGER", conn)) // Используем INTEGER для связи
                            {
                                cmdAddCol.ExecuteNonQuery();
                            }
                            System.Diagnostics.Debug.WriteLine($"Колонка [{fkColName}] добавлена в [{ownerTable}].");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Колонка [{fkColName}] уже существует в [{ownerTable}].");
                        }

                        // Создаём имя для ограничения внешнего ключа
                        string constraintName = $"FK_{ownerTable}_{fkColName}_To_{refTable}_{refCol}";

                        // Проверим, не существует ли уже такое ограничение (на всякий случай)
                        bool constraintExists = false;
                        try
                        {
                            // Простой запрос для проверки - может не сработать во всех случаях, но как базовая защита подойдёт
                            using (var cmdCheck = new OleDbCommand(
                                "SELECT COUNT(*) FROM MSysRelationships WHERE Name = ?", conn))
                            {
                                cmdCheck.Parameters.Add("@name", OleDbType.VarChar).Value = constraintName;
                                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                                constraintExists = count > 0;
                            }
                        }
                        catch
                        {
                            // Если проверка не прошла, просто продолжим и попытаемся создать.
                            // Ошибка будет поймана в блоке catch ниже.
                        }

                        if (constraintExists)
                        {
                            System.Diagnostics.Debug.WriteLine($"Ограничение {constraintName} уже существует.");
                            continue; // Пропускаем создание дубликата
                        }

                        // Создаём само ограничение внешнего ключа
                        using (var cmdFk = new OleDbCommand(
                            $"ALTER TABLE [{ownerTable}] ADD CONSTRAINT [{constraintName}] " +
                            $"FOREIGN KEY ([{fkColName}]) REFERENCES [{refTable}]([{refCol}])", conn))
                        {
                            cmdFk.ExecuteNonQuery();
                        }
                        System.Diagnostics.Debug.WriteLine($"Создана связь: [{ownerTable}].[{fkColName}] -> [{refTable}].[{refCol}]");

                        // Сохраняем информацию о связи в служебную таблицу AppRelations
                        using (var cmdLog = new OleDbCommand(
                            "INSERT INTO AppRelations (ParentTable, ParentColumn, ChildTable, ChildColumn, ConstraintName) " +
                            "VALUES (?, ?, ?, ?, ?)", conn))
                        {
                            cmdLog.Parameters.Add("@pTab", OleDbType.VarChar).Value = refTable;
                            cmdLog.Parameters.Add("@pCol", OleDbType.VarChar).Value = refCol;
                            cmdLog.Parameters.Add("@cTab", OleDbType.VarChar).Value = ownerTable;
                            cmdLog.Parameters.Add("@cCol", OleDbType.VarChar).Value = fkColName;
                            cmdLog.Parameters.Add("@cName", OleDbType.VarChar).Value = constraintName;
                            cmdLog.ExecuteNonQuery();
                        }
                        System.Diagnostics.Debug.WriteLine($"Запись о связи добавлена в AppRelations.");
                    }
                }

                MessageBox.Show("Таблица и связи успешно созданы!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Сброс формы после успешного создания
                _columns.Clear();
                UpdateColumnList();
                txtNewTableName.Clear();
                txtColName.Clear();
                LoadExistingTables(); // <-- ЭТО КЛЮЧЕВОЕ ИЗМЕНЕНИЕ! Обновляем datagridview
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка создания: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (dgvExistingTables.CurrentRow == null)
            {
                MessageBox.Show("Выберите таблицу для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tableName = dgvExistingTables.CurrentRow.Cells[0].Value.ToString();
            var result = MessageBox.Show(
                $"Удалить таблицу '{tableName}' и ВСЕ связанные с ней ограничения? Это действие необратимо.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2 // Кнопка "Нет" по умолчанию
            );

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Найдём все связи, где эта таблица участвует (как Parent или Child)
                    var relations = new List<(string ChildTable, string ConstraintName, string ChildColumn)>();
                    using (var cmd = new OleDbCommand(
                        "SELECT ChildTable, ConstraintName, ChildColumn FROM AppRelations " +
                        "WHERE ParentTable = ? OR ChildTable = ?", conn))
                    {
                        cmd.Parameters.Add("@tableName1", OleDbType.VarChar).Value = tableName;
                        cmd.Parameters.Add("@tableName2", OleDbType.VarChar).Value = tableName;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                relations.Add((
                                    reader["ChildTable"].ToString(),
                                    reader["ConstraintName"].ToString(),
                                    reader["ChildColumn"].ToString()
                                ));
                            }
                        }
                    }

                    // 2. Удалим все найденные FOREIGN KEY ограничения из базы данных
                    foreach (var (childTable, constraintName, childColumn) in relations)
                    {
                        try
                        {
                            // Попытка удалить ограничение
                            using (var cmdDrop = new OleDbCommand($"ALTER TABLE [{childTable}] DROP CONSTRAINT [{constraintName}]", conn))
                            {
                                cmdDrop.ExecuteNonQuery();
                            }
                            System.Diagnostics.Debug.WriteLine($"Ограничение {constraintName} удалено из [{childTable}].");
                        }
                        catch (OleDbException ex)
                        {
                            // В Access удаление FK через SQL может быть ограничено.
                            // В таких случаях можно попробовать использовать ADOX, но это усложнит код.
                            // Для простоты логируем ошибку.
                            System.Diagnostics.Debug.WriteLine($"Не удалось удалить FK {constraintName} из [{childTable}]: {ex.Message}");
                            // Важно: не прерываем процесс, просто логируем.
                        }
                    }

                    // 3. Удалим колонки, которые были созданы во *внешних* таблицах как часть связей с удаляемой таблицей
                    // Это колонки, где ChildTable - это НЕ удаляемая таблица
                    foreach (var (childTable, constraintName, childColumn) in relations)
                    {
                        // Проверяем, является ли ChildTable внешней (не той, что удаляем)
                        if (childTable != tableName)
                        {
                            try
                            {
                                // Проверим, существует ли колонка перед удалением
                                bool columnExists = false;
                                DataTable colsSchema = conn.GetSchema("Columns", new[] { null, null, childTable, null });
                                foreach (DataRow r in colsSchema.Rows)
                                {
                                    if (r["COLUMN_NAME"].ToString().Equals(childColumn, StringComparison.OrdinalIgnoreCase))
                                    {
                                        columnExists = true;
                                        break;
                                    }
                                }

                                if (columnExists)
                                {
                                    using (var cmdDropCol = new OleDbCommand($"ALTER TABLE [{childTable}] DROP COLUMN [{childColumn}]", conn))
                                    {
                                        cmdDropCol.ExecuteNonQuery();
                                    }
                                    System.Diagnostics.Debug.WriteLine($"Колонка [{childColumn}] удалена из [{childTable}].");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"Колонка [{childColumn}] не найдена в [{childTable}] при попытке удаления.");
                                }
                            }
                            catch (Exception ex)
                            {
                                // Логируем ошибку удаления колонки, но не останавливаем процесс
                                System.Diagnostics.Debug.WriteLine($"Не удалось удалить колонку [{childColumn}] из [{childTable}]: {ex.Message}");
                            }
                        }
                    }

                    // 4. Удалим записи из AppRelations
                    using (var cmdDelLog = new OleDbCommand(
                        "DELETE FROM AppRelations WHERE ParentTable = ? OR ChildTable = ?", conn))
                    {
                        cmdDelLog.Parameters.Add("@tableName1", OleDbType.VarChar).Value = tableName;
                        cmdDelLog.Parameters.Add("@tableName2", OleDbType.VarChar).Value = tableName;
                        cmdDelLog.ExecuteNonQuery();
                    }
                    System.Diagnostics.Debug.WriteLine($"Записи о связях для [{tableName}] удалены из AppRelations.");

                    // 5. Удалим саму таблицу
                    using (var cmdDropTable = new OleDbCommand($"DROP TABLE [{tableName}]", conn))
                    {
                        cmdDropTable.ExecuteNonQuery();
                    }
                    System.Diagnostics.Debug.WriteLine($"Таблица [{tableName}] удалена.");
                }

                MessageBox.Show("Таблица и связанные данные удалены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadExistingTables(); // <-- ЭТО КЛЮЧЕВОЕ ИЗМЕНЕНИЕ! Обновляем datagridview
                lstStructure.Items.Clear(); // Очистим структуру
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TableExists(string name)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    foreach (DataRow row in conn.GetSchema("Tables").Rows)
                    {
                        if (row["TABLE_NAME"].ToString().Equals(name, StringComparison.OrdinalIgnoreCase))
                            return true;
                    }
                }
            }
            catch { }
            return false;
        }
    }

    // Вспомогательный класс для хранения информации о колонке
    public class TableColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsForeignKey { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }
    }
}