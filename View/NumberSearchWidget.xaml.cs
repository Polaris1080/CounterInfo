using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterInfo.Models;
using CounterInfo.Repository;
namespace CounterInfo.View;



public partial class NumberSearchWidget : ContentView
{
    public NumberSearchWidget()
    {
        InitializeComponent();
    }
}



public partial class NumberSearchWidgetViewModel : ObservableObject
{
    /// <summary>検索範囲（最小値）</summary>
    private const int NumberMin = 1;
    /// <summary>検索範囲（最大値）</summary>
    private const int NumberMax = 9999;

    [ObservableProperty][NotifyPropertyChangedFor(nameof(SearchButtonEnable))] public  partial string? Query { get; set; }
    private int? Parsed             => int.TryParse(Query, out int parsed) ? parsed : null;
    public  bool SearchButtonEnable => Parsed is not null && NumberMin <= Parsed && Parsed <= NumberMax;

    [ObservableProperty][NotifyPropertyChangedFor(nameof(Result))]             private partial States  State { get; set; }
    public string Result => State switch {
        States.Waiting => "検索待機中…",
        States.UnIssue => "発券しておりません。",
        States.Issue   => "順番待ちです。",
        States.Recept  => "呼び出し時に不在でした…",
        States.Called  => "既に対応しています。",
        States.Error   => "不具合が発生しています！",
        _ => throw new NotImplementedException(),
    };

    [RelayCommand] private async Task Search()
    {
        if (SearchButtonEnable)
        {
            State = await new CounterInfoRepository().GetItemAsync((int)Parsed!) switch {
                null => States.UnIssue,
                CounterInfomation ci => (ci.IssueTime, ci.ReceptTime, ci.Called) switch {
                    (null,    _,     _) => States.UnIssue,
                    (   _, null,     _) => States.Issue,
                    (   _,    _,  true) => States.Called,
                    (   _,    _, false) => States.Recept
                },
            };
        }
    }
    private enum States
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