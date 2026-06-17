using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Models
{
    [Table("CounterInfo")]
    public class CounterInfomation
    {
        /// <summary>番号</summary>
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int ID { get; set; }
        /// <summary>窓口</summary>
        [Column("counter")]	
        public int Counter { get; set; }
        /// <summary>発行時刻</summary>
        [Column("issue_time")]
        public DateTime? IssueTime { get; set; } = DateTime.Now;
        /// <summary>受付時刻</summary>
        [Column("recept_time")]
        public DateTime? ReceptTime { get; set; }
        /// <summary>呼ばれたか</summary>
        [Column("called")]
        public bool Called { get; set; } = false;
    }
}
