namespace CounterInfoServer.Endpoints;

using CounterInfoServer;
using CounterInfoServer.Models;
using Microsoft.EntityFrameworkCore;
using CounterInfo.Shared;



public static class CounterInfomationEndPoint
{
    extension(WebApplication app)
    {
        public void RegisterCounterInfomationEndPoint()
        {
            var group = app.MapGroup("/api/v1/counter_info");


            // Get counter status.
            group.MapGet("", async (TodoDb db) =>
                {
                    var t = await db.CounterInfoData.FromSqlRaw("""
        SELECT  name                                                                                                               as detail,
               (SELECT count(*) FROM CounterInfomation as CI WHERE CI.counter = CS.ID and CI.ReceptTime is null)                  as waiting,
               (SELECT CI.id    FROM CounterInfomation as CI WHERE CI.counter = CS.ID ORDER by CI.ID DESC)                         as number,
               (SELECT ReceptTime - IssueTime FROM CounterInfomation as CI WHERE CI.counter = CS.id and CI.ReceptTime is not null) as estimate
        FROM    CounterSetting as CS
     """).ToListAsync();
                    return t;
                }
            );

            // Get number state.
            group.MapGet("{id}", async (int id, TodoDb db) => await db.CounterInfomations.FirstOrDefaultAsync(f => f.ID == id) switch
            {
                null => States.UnIssue,
                CounterInfomation ci => (ci.IssueTime, ci.ReceptTime, ci.Called) switch
                {
                    (null, _, _) => States.UnIssue,
                    (_, null, _) => States.Issue,
                    (_, _, true) => States.Called,
                    (_, _, false) => States.Recept
                },
            });

            // Get "Called".
            group.MapGet("called", async (TodoDb db) => await db.CounterInfomations.Where(w => w.Called).Select(s => s.ID).ToListAsync());

            // Check connection.
            group.MapGet("ping", () => Results.Ok("pong"));
        }
    }
}