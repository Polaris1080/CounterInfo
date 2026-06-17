using SQLite;
namespace CounterInfo.Models;

public sealed record CounterSetting
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int ID { get; set; }

    [Column("order")]
    public int Order { get; set; }

    [Column("name")]
    public string Name { get; set; } = "";
}