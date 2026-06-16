using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages;
using CounterInfo.Repository;
using CounterInfo.View;
using System.Collections.ObjectModel;

namespace CounterInfo.ViewModels
{
    internal class SettingMenuPageViewModel : ObservableObject
    {
        CounterSettingRepository counterSettingRepository;

        private ObservableCollection<CounterSettingViewModel>  _counterSetting = [];
        public ObservableCollection<CounterSettingViewModel> CounterSetting
        {
            get => _counterSetting;
            set => SetProperty(ref _counterSetting, value);
        }
        /* Property */
        /// <summary>情報源（表示用）</summary>
        public ObservableCollection<CounterListWidgetData> Source { get => _source; private set => SetProperty(ref _source, value); }
        private ObservableCollection<CounterListWidgetData> _source = [];


        /* Initialize */

        public SettingMenuPageViewModel()
        {
            counterSettingRepository = new CounterSettingRepository();


            if (!WeakReferenceMessenger.Default.IsRegistered<CounterSettingDeleteMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<CounterSettingDeleteMessage>(
                this,
                 (recipient, message) =>
                {
                    var target = CounterSetting.First(x => x.ID == message.Value);
                    if (target is not null) {

                        CounterSetting.Remove(CounterSetting.First(x => x.ID == message.Value));
                        //OnPropertyChanged(nameof(CounterSetting));
                    }

                });                
            }

            // AsyncTask
            Task.Run(async () =>
            {
                var t1 = counterSettingRepository.GetItemsAsync().Result;
                foreach (var item in t1)
                {
                    CounterSetting.Add(new CounterSettingViewModel(item));
                }
            });

            Refresh();
            if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this))
            {
                WeakReferenceMessenger.Default.Register<InfomationUpdateMessage>(this, (_, _) => Refresh());
            }

            void Refresh() => Task.Run(() => { Source = new(counterSettingRepository.GetItemsAsync().Result.OrderBy(x => x.Order).Select(x => new CounterListWidgetData() { ID = x.ID, Name = x.Name })); });
        }
    }
}


internal sealed record CounterListUpdateMessage;
