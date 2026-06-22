using CounterInfo.Models;
using CounterInfo.Repository;

namespace CounterInfo;

internal sealed class RealityMock
{
    /* Singleton */
    public static RealityMock GetInstance() => _singleInstance;
    private static readonly RealityMock _singleInstance = new();

    /// <summary>発行（自動化）</summary>
    private List<Timer>  issueTimers = [];
    /// <summary>受付（自動化）</summary>
    private List<Timer> receptTimers = [];

    private readonly CounterSettingRepository counterSettingRepository = new();
    private readonly    CounterInfoRepository    counterInfoRepository = new();

    private RealityMock()
    {
        Task.Run(async () =>
        {
            var _cs = await counterSettingRepository.GetItemsAsync();
            issueTimers = [.. _cs.Select(x => new Timer(new(
                async _ => await counterInfoRepository.InsertItemAsync(new CounterInfomation() { ID = x.Range, Counter = x.ID })), null,
                dueTime: TimeSpan.FromSeconds(x.IssueInterval * 60),
                 period: TimeSpan.FromSeconds(x.IssueInterval * 60)
            ))];

            _cs.ToList().ForEach(async cs => {
               receptTimers.Add(new(new(
                   async _ => await counterInfoRepository.ReceptAsync(cs.ID, cs.AbsenceParcent)), null,
                   dueTime: TimeSpan.FromSeconds(cs.ReceptInterval * 60),
                    period: TimeSpan.FromSeconds(cs.ReceptInterval * 60)
               ));
               await Task.Delay(1000).ConfigureAwait(false);
            });
        });
    }

    ~RealityMock()
    {
        issueTimers.ForEach(static timer => timer.Dispose());
    }

}