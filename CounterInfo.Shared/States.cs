using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Shared
{
    public enum States
    {
        /// <summary>待機中</summary>
        Waiting,
        /// <summary>未発券</summary>
        UnIssue,
        /// <summary>発券済</summary>
        Issue,
        /// <summary>呼出済</summary>
        Recept,
        /// <summary>対応済</summary>
        Called,
        /// <summary>エラー</summary>
        Error
    }
}
