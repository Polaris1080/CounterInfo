using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterInfo.Models;
using CounterInfo.Repository;
using System.Windows.Input;
namespace CounterInfo.View;



public partial class NumberSearchWidget : ContentView
{
    public NumberSearchWidget()
    {
        InitializeComponent();
    }
}



public class NumberSearchWidgetViewModel : ObservableObject
{
    /* Property */
    /// <summary>入力内容</summary>
    public  string Search {get => _search;          set => SetProperty(ref _search, value);}
    /// <summary>検索結果</summary>
    public  string Status {get => _status;  private set => SetProperty(ref _status, value);}
    private string _search = "", _status = "検索待機中…";


    /* Initialize */
    public NumberSearchWidgetViewModel()
    {
        // Command
        SearchCommand = new AsyncRelayCommand(SearchInfo);
    }


    /* Event */
    public  ICommand   SearchCommand { get; }
    private async Task SearchInfo()
    {
        if (int.TryParse(Search, out int parsed) && (1 <= parsed && parsed <= 9999))
        {
            Status = await new CounterInfoRepository().GetItemAsync(parsed) switch {
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
}