using System.Collections.Generic;

public class FieldDefinition
{
    public string Name { get; set; }
    public string AccessDataType { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string ForeignKeyTableName { get; set; }

    public override string ToString()
    {
        string fkInfo = string.IsNullOrEmpty(ForeignKeyTableName) ? "" : $" -> {ForeignKeyTableName}";
        return $"{Name} ({AccessDataType}){(IsPrimaryKey ? " [PK]" : "")}{fkInfo}";
    }
}