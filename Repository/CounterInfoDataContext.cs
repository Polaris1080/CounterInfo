using CounterInfo.Models;
using SQLite;
namespace CounterInfo.Repository;

class CounterInfoDataContext
{
    SQLiteAsyncConnection? database;

    public async Task<List<CounterInfoData>> GetItemsAsync() => await (
        database ??= new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags)).QueryAsync<CounterInfoData>("""
        SELECT  name                                                                                        as detail,
               (SELECT count(*) FROM CounterInfo as CI WHERE CI.counter = CS.id and CI.recept_time is null) as waiting,
               (SELECT CI.id    FROM CounterInfo as CI WHERE CI.counter = CS.id ORDER by CI.id DESC)        as number,
               (SELECT recept_time - issue_time FROM CounterInfo as CI
                       WHERE CI.counter = CS.id      and
                             recept_time is not null and
                             recept_time > strftime('%s', 'now', '-1 hour'))                                as estimate
        FROM   CounterSetting as CS
     """);
}