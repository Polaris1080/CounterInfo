using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Models
{
    public class CounterSetting
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int ID { get; set; }

        [Column("order")]
        public int Order { get; set; }

        [Column("name")]
        public string Name { get; set; } = "";
    }
}
