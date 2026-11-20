using SQLite;
namespace CounterInfo.Server.Models;

public class CounterSetting
{
    [PrimaryKey, AutoIncrement][Column("id")] public int ID  { get; set; }
    [Column("order")]           public int    Order          { get; set; }
    [Column("name")]            public string Name           { get; set; } = "";
    [Column("issue_interval")]  public int     IssueInterval { get; set; } = 3;
    [Column("recept_interval")] public int    ReceptInterval { get; set; } = 2;
    [Column("absence_parcent")] public int    AbsenceParcent { get; set; } = 1;
    [Column("range")]           public int    Range          { get; set; }
    [Column("closed")]          public bool   Closed         { get; set; }
}