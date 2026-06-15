using CommunityToolkit.Mvvm.ComponentModel;
using CounterInfo.Repository;

namespace CounterInfo.ViewModels
{
    internal class CalledWidgetViewModel : ObservableObject
    {
        public CalledWidgetViewModel()
        {
            Task.Run(async () => { Source = [.. new CounterInfoRepository().GetCalledAsync().Result.Select(x => x.ID)]; });
        }

        public  int[]  Source { get => _source;  set => SetProperty(ref _source, value); }
        private int[] _source = [];
    }
}
