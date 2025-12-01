using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using FitnessCenterApp.DataAccess;

public static class AdminTableManager
{
    public static void CreateTableWithFields(string newTableName, List<FieldDefinition> fields)
    {
        if (fields == null || fields.Count == 0)
        {
            throw new ArgumentException("Список полей не может быть пустым.");
        }

        StringBuilder createTableSql = new StringBuilder($"CREATE TABLE [{newTableName}] (");

        List<string> columnDefinitions = new List<string>();
        string primaryKeyField = null;

        foreach (var field in fields)
        {
            columnDefinitions.Add($"[{field.Name}] {field.AccessDataType}");

            if (field.IsPrimaryKey)
            {
                primaryKeyField = field.Name;
            }
        }

        createTableSql.Append(string.Join(", ", columnDefinitions));

        if (primaryKeyField != null)
        {
            createTableSql.Append($", CONSTRAINT PK_{newTableName} PRIMARY KEY ([{primaryKeyField}])");
        }

        createTableSql.Append(")");

        using (OleDbConnection conn = DatabaseConnection.GetConnection())
        {
            conn.Open();
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(createTableSql.ToString(), conn))
                {
                    cmd.ExecuteNonQuery();
                }

                foreach (var field in fields)
                {
                    if (!string.IsNullOrEmpty(field.ForeignKeyTableName))
                    {
                        string createIndexSql = $"CREATE INDEX FK_{newTableName}_{field.Name} ON [{newTableName}] ([{field.Name}])";
                        using (OleDbCommand cmdIndex = new OleDbCommand(createIndexSql, conn))
                        {
                            cmdIndex.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при создании таблицы: {ex.Message}", ex);
            }
        }
    }
}