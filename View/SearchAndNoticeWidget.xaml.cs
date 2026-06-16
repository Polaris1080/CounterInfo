
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using CounterInfo.Repository;
using System.Diagnostics;
using System.Windows.Input;

namespace CounterInfo.View;



public partial class SearchAndNoticeWidget : ContentView
{
    public SearchAndNoticeWidget()
    {
        InitializeComponent();
    }
}



public class SearchAndNoticeWidgetViewModel : ObservableObject
{
    /* Value */
    CounterInfoRepository counterInfoRepository = new();


    public SearchAndNoticeWidgetViewModel()
    {
        // Command
        SearchCommand = new AsyncRelayCommand(SearchInfo);
        NoticeCommand = new AsyncRelayCommand(AppendNotice);

        // AsyncTask
        //Task.Run(async () =>
        //{

        //});
    }



    public string Search
    {
        get => _search;
        set => SetProperty(ref _search, value);
    }
    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }
    private string _search = "", _status = "状態表示";


    public ICommand SearchCommand { get; }
    public ICommand NoticeCommand { get; }

    async Task SearchInfo()
    {
        if (int.TryParse(Search, out int re))
        {
            var item = await counterInfoRepository.GetItemAsync(re);
            Status = item switch {
                null => "発券しておりません。",
                CounterInfomation ci => (ci.IssueTime, ci.ReceptTime, ci.Called) switch {
                    (null,    _,     _) => "不具合が発生しています！",
                    (   _, null,     _) => "順番待ちです。",
                    (   _,    _,  true) => "呼び出し時に不在でした…",
                    (   _,    _, false) => "既に対応しています。"
                },
            };
        }
    }
    async Task AppendNotice() { }
}