using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages;
using CounterInfo.Repository;
using System.Collections.ObjectModel;

namespace CounterInfo.ViewModels
{
    internal class CalledWidgetViewModel : ObservableObject
    {
        public CalledWidgetViewModel()
        {
            Refresh();
            if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<InfomationUpdateMessage>(this, (_, _) => Refresh());
            }

            void Refresh() => Task.Run(() => { Source = [.. new CounterInfoRepository().GetCalledAsync().Result.Select(x => x.ID)]; });
        }

        public  ObservableCollection<int>  Source { get => _source;  private set => SetProperty(ref _source, value); }
        private ObservableCollection<int> _source = [];
    }
}
