using CounterInfo.Server.Models;
using CounterInfo.Shared;
using Microsoft.EntityFrameworkCore;

namespace CounterInfo.Server
{
    public class RealityMockService(IServiceScopeFactory contextFactory, ILogger<RealityMockService> logger) : BackgroundService
    {
        private readonly ILogger<RealityMockService> _logger = logger;
        private TodoDb DB => contextFactory.CreateScope().ServiceProvider.GetRequiredService<TodoDb>();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 5秒間隔で実行するタイマーを作成
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(5));

            Dictionary<int, DateTime> dictI = [];
            Dictionary<int, DateTime> dictR = [];
            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    _logger.LogInformation("定期処理を実行中: {time}", DateTimeOffset.Now);
                    // ここに定期実行したい処理を記述

                    // 60秒毎にポーリング
                    var now = DateTime.Now;
                    DateTime dateTime = new DateTime(
                        now.Year,
                        now.Month,
                        now.Day,
                        now.Hour,
                        (now.Minute + 1) % 60,
                        0,
                        now.Kind
                    );
                    timer.Period = dateTime - now;


                    //DB.CounterInfomations.Add(new CounterInfomation
                    //{
                    //    Counter = 1,
                    //    Called = false
                    //});


                    await refresh();

                    var context = DB;
                    foreach (var item in dictI) 
                    {
                        // 更新フラグ
                        var t3 = (DateTime.Now - item.Value).Minutes;

                        var openCD = await DB.CounterSettings.Where(w =>w.ID == item.Key).Select(s => s.IssueInterval).FirstAsync(stoppingToken);
                        if (t3 >=openCD)
                        {
                            dictI[item.Key] = DateTime.Now;

                            context.CounterInfomations.Add(new CounterInfomation
                            {
                                Counter = item.Key,
                                Called = false
                            });
                            _logger.LogInformation("called I {key}: {time}",[item.Key, DateTimeOffset.Now]);
                        }
                        // 変更
                    }
                    foreach (var item in dictR)
                    {
                        // 更新フラグ
                        var t3 = (DateTime.Now - item.Value).Minutes;

                        var openCD = await DB.CounterSettings.Where(w => w.ID == item.Key).Select(s => s.ReceptInterval).FirstAsync(stoppingToken);
                        if (t3 >= openCD)
                        {
                            dictR[item.Key] = DateTime.Now;
                            CounterInfomation? t4 = await context.CounterInfomations.Where(w => w.Counter == item.Key).Where(w => w.ReceptTime == null).FirstOrDefaultAsync(stoppingToken);
                            if (t4 is not null)
                            {
                                t4.ReceptTime = DateTime.Now;
                            }

                            _logger.LogInformation("called R {key}: {time}", [item.Key, DateTimeOffset.Now]);
                            //await DB.SaveChangesAsync(stoppingToken);
                        }
                        // 変更
                    }
                    await context.SaveChangesAsync(stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("サービスが正常に停止しました。");
            }

            async Task refresh()  {
                var openCounterId = await DB.CounterSettings.Where(w => w.Closed == false).Select(s => s.ID).ToArrayAsync(stoppingToken);
                var nowTime = DateTime.Now;

                // Add unregistered counter.
                openCounterId.Except(dictI.Keys).ForEach(item =>
                {
                    dictI.Add(item, nowTime);
                    dictR.Add(item, nowTime);
                });

                // Delete closed counter.
                dictI.Keys.Except(openCounterId).ForEach(item =>
                {
                    dictI.Remove(item);
                    dictR.Remove(item);
                });
            }
        }
    }
}
