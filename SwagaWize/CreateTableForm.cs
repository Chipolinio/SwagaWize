using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using FitnessCenterApp.DataAccess;

namespace FitnessCenterApp.Forms
{
    public partial class CreateTableForm : Form
    {
        private List<TableField> _fields = new List<TableField>();
        private string[] _dataTypes = { "TEXT", "INTEGER", "DATE", "CURRENCY", "BOOLEAN", "MEMO" };
        private List<string> _existingTables = new List<string>();

        public CreateTableForm()
        {
            InitializeComponent();
            cmbType.DataSource = _dataTypes;
            LoadExistingTables();
            UpdateFieldList();
        }

        private void LoadExistingTables()
        {
            _existingTables.Clear();
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    var schema = conn.GetSchema("Tables");
                    foreach (System.Data.DataRow row in schema.Rows)
                    {
                        string name = row["TABLE_NAME"].ToString();
                        if (!name.StartsWith("MSys") && !name.StartsWith("~"))
                        {
                            _existingTables.Add(name);
                        }
                    }
                }
                cmbLinkTable.DataSource = _existingTables;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить список таблиц:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            string name = txtFieldName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите имя поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_fields.Exists(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Поле с таким именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string type = cmbType.SelectedItem.ToString();
            _fields.Add(new TableField { Name = name, Type = type });
            UpdateFieldList();
            txtFieldName.Clear();
            txtFieldName.Focus();
        }

        private void btnRemoveField_Click(object sender, EventArgs e)
        {
            if (lstFields.SelectedIndex >= 0)
            {
                _fields.RemoveAt(lstFields.SelectedIndex);
                UpdateFieldList();
            }
        }

        private void UpdateFieldList()
        {
            lstFields.Items.Clear();
            foreach (var f in _fields)
            {
                lstFields.Items.Add($"{f.Name} ({f.Type})");
            }
            btnCreateTable.Enabled = _fields.Count > 0;
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            string tableName = txtTableName.Text.Trim();
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Введите имя таблицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_fields.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одно поле.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Добавляем поле-ссылку, если выбрано
            if (chkLink.Checked && cmbLinkTable.SelectedItem != null)
            {
                string linkedTable = cmbLinkTable.SelectedItem.ToString();
                string fkName = $"{linkedTable}ID";
                if (!_fields.Exists(f => f.Name.Equals(fkName, StringComparison.OrdinalIgnoreCase)))
                {
                    _fields.Add(new TableField { Name = fkName, Type = "INTEGER" });
                }
            }

            // Формируем SQL
            var cols = new List<string>();
            foreach (var f in _fields)
            {
                string def = $"[{f.Name}]";
                switch (f.Type)
                {
                    case "TEXT": def += " TEXT(255)"; break;
                    case "MEMO": def += " MEMO"; break;
                    case "INTEGER": def += " INTEGER"; break;
                    case "DATE": def += " DATETIME"; break;
                    case "CURRENCY": def += " CURRENCY"; break;
                    case "BOOLEAN": def += " YESNO"; break;
                    default: def += " TEXT(255)"; break;
                }
                cols.Add(def);
            }

            string sql = $"CREATE TABLE [{tableName}] ({string.Join(", ", cols)})";

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new OleDbCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Таблица [{tableName}] успешно создана!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _fields.Clear();
                txtTableName.Clear();
                chkLink.Checked = false;
                UpdateFieldList();
                LoadExistingTables(); // обновляем список для вкладки удаления
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания таблицы:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string tableName = txtDeleteTable.Text.Trim();
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("Введите имя таблицы для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var res = MessageBox.Show(
                $"Удалить таблицу [{tableName}]?\nВсе данные будут утеряны!",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (res != DialogResult.Yes) return;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new OleDbCommand($"DROP TABLE [{tableName}]", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"Таблица [{tableName}] удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDeleteTable.Clear();
                LoadExistingTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления таблицы:\n{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class TableField
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}