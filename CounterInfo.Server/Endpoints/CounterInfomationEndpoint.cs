namespace CounterInfo.Server.Endpoints;

using CounterInfo.Server;
using CounterInfo.Server.Models;
using CounterInfo.Server.Extensions;
using CounterInfo.Shared;
using Microsoft.EntityFrameworkCore;

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

            // Get registration state.
            group.MapGet("{id}", async (int id, TodoDb db) => await db.CounterInfomations.FirstOrDefaultAsync(f => f.ID == id) switch
            {
                CounterInfomation ci => ci.ToRegistrationState(),
                null => RegistrationState.UnIssue,
            });

            // Get "Called".
            group.MapGet("called", async (TodoDb db) => await db.CounterInfomations.Where(w => w.Called).Select(s => s.ID).ToListAsync());

            // Check connection.
            group.MapGet("ping", () => Results.Ok("pong"));
        }
    }
}