using CounterInfo.Server.Models;
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

                    Dictionary<int, DateTime> dict1 = [];
            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    _logger.LogInformation("定期処理を実行中: {time}", DateTimeOffset.Now);
                    // ここに定期実行したい処理を記述

                    // 60秒毎にポーリング
                    var now = DateTime.Now;
                    DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute + 1, 0, now.Kind);
                    timer.Period = dateTime - now;


                    //DB.CounterInfomations.Add(new CounterInfomation
                    //{
                    //    Counter = 1,
                    //    Called = false
                    //});
                    //await DB.SaveChangesAsync(stoppingToken);

                    // 更新
                    //var t1 = await DB.CounterSettings.Select(s => s.ID).ToArrayAsync();
                    //foreach (var item in t1)
                    //{
                    //    if (dict1.ContainsKey(item))
                    //    {
                            
                    //    }
                    //    else
                    //    {
                            
                    //    }
                    //}
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("サービスが正常に停止しました。");
            }
        }
    }
}
