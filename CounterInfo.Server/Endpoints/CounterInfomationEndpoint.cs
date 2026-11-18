namespace CounterInfoServer.Endpoints;

using CounterInfoServer;
using CounterInfoServer.Models;
using Microsoft.EntityFrameworkCore;



public static class CounterInfomationEndPoint
{
    extension(WebApplication app)
    {
        public void RegisterCounterInfomationEndPoint()
        {
            var group = app.MapGroup("/api/v1/counter_info");
            group.MapGet("", async (TodoDb db) =>
                {
                    var t = await db.CounterInfomations.ToListAsync();
                    return t;
                }
            );
            group.MapGet("{id}", async (int id, TodoDb db) => await db.CounterInfomations.FirstOrDefaultAsync(w => w.ID == id) switch
            {
                null => States.UnIssue,
                CounterInfomation ci => (ci.IssueTime, ci.ReceptTime, ci.Called) switch
                {
                    (null, _, _) => States.UnIssue,
                    (_, null, _) => States.Issue,
                    (_, _,  true) => States.Called,
                    (_, _, false) => States.Recept
                },
            });
            group.MapGet("called", async (TodoDb db) => await db.CounterInfomations.Where(w => w.Called).Select(s => s.ID).ToListAsync());
        }
    }
}

internal enum States
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