namespace CounterInfoServer.Endpoints;

using CounterInfo.Server;
using Microsoft.EntityFrameworkCore;



public static class CounterInfomationEndPoint
{
    extension(WebApplication app)
    {
        public void RegisterCounterInfomationEndPoint()
        {
            var counterinfo = app.MapGroup("/api/v1/counter_info");
            counterinfo.MapGet("", async (TodoDb db) =>
                {
                    var t = await db.CounterInfomations.ToListAsync();
                    return t;
                }
            );
            counterinfo.MapGet("called", async (TodoDb db) => await db.CounterInfomations.Where(w => w.Called).Select(s => s.ID).ToListAsync());
        }
    }
}
