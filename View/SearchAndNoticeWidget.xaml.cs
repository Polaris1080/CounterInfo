using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            var t1 = await counterInfoRepository.GetItemAsync(re);
            Status = t1.Called ? "呼ばれました" : "まだです";
        }
        Debug.WriteLine("done");
    }
    async Task AppendNotice() { }
}