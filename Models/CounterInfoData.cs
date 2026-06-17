using SQLite;
namespace CounterInfo.Models;

public sealed record CounterInfoData
{
    /// <summary>受付内容(Details of registration)</summary>
    [Column("detail")]
    public string Detail   { get; set; } = "";

    /// <summary>待ち人数(Number of people waiting)</summary>
    [Column("waiting")]
    public int    Waiting  { get; set; }

    /// <summary>受付番号(Registration number)</summary>
    [Column("number")]
    public int    Number   { get; set; }

    /// <summary>想定待ち時間(Estimated waiting time)単位：分</summary>
    [Column("estimate")]
    public DateTime    Estimate { get; set; }
}