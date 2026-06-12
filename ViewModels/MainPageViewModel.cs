using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CounterInfo.Repository;
using System.Diagnostics;
using System.Windows.Input;

namespace CounterInfo.ViewModels
{
    internal class MainPageViewModel : ObservableObject
    {
        CounterInfoRepository counterInfoRepository;

        public int number = 2;
        public DateTime _updateTime = DateTime.Now;
        public DateTime UpdateTime
        {
            get => _updateTime;
            set => SetProperty(ref _updateTime, value);
        }

        public string _status = "状態表示";
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public string _search = "";
        public string Search
        {
            get => _search;
            set => SetProperty(ref _search, value);
        }

        public int[] _called = [];
        public int[] Called
        {
            get => _called;
            set => SetProperty(ref _called, value);
        }

        public ICommand SettingCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand NoticeCommand { get; }

        public MainPageViewModel()
        {
            // Model
            counterInfoRepository = new CounterInfoRepository();

            // Command
            SettingCommand = new AsyncRelayCommand(OpenSetting);
            ReloadCommand = new AsyncRelayCommand(ReloadInfo);
            SearchCommand = new AsyncRelayCommand(SearchInfo);
            NoticeCommand = new AsyncRelayCommand(AppendNotice);

            // AsyncTask
            Task.Run(async () =>
            {
                Called = counterInfoRepository.GetItemsAsync().Result.Where(x => x.Called).Select(x => x.ID).ToArray();
            });
        }

        async Task OpenSetting() { }
        async Task ReloadInfo()
        {

            UpdateTime = DateTime.Now;
        }
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
}
