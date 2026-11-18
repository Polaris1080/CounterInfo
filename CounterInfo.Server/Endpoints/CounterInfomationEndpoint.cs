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
            group.MapGet("", async (TodoDb db) =>
                {
                    var t = await db.CounterInfomations.ToListAsync();
                    return t;
                }
            );
            //group.MapGet("{id}", async (int id, TodoDb db) => await db.CounterInfomations.FirstOrDefaultAsync(w => w.ID == id) switch
            //{
            //    null => States.UnIssue,
            //    CounterInfomation ci => (ci.IssueTime, ci.ReceptTime, ci.Called) switch
            //    {
            //        (null, _, _) => States.UnIssue,
            //        (_, null, _) => States.Issue,
            //        (_, _,  true) => States.Called,
            //        (_, _, false) => States.Recept
            //    },
            //});
            group.MapGet("{id}", async (int id, TodoDb db) => {
                var t1 = await db.CounterInfomations.FirstOrDefaultAsync(w => w.ID == id);
                return t1 switch
                {
                    null => States.UnIssue,
                    CounterInfomation ci => (ci.IssueTime, ci.ReceptTime, ci.Called) switch
                    {
                        (null, _, _) => States.UnIssue,
                        (_, null, _) => States.Issue,
                        (_, _, true) => States.Recept,
                        (_, _, false) => States.Called
                    },
                };
            }  );
            group.MapGet("called", async (TodoDb db) => await db.CounterInfomations.Where(w => w.Called).Select(s => s.ID).ToListAsync());
        }
    }
}