using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages;
using CounterInfo.Repository;
using System.Collections.ObjectModel;

namespace CounterInfo.ViewModels
{
    internal class SettingPageViewModel : ObservableObject
    {
        CounterSettingRepository counterSettingRepository;

        private ObservableCollection<CounterSettingViewModel>  _counterSetting = [];
        public ObservableCollection<CounterSettingViewModel> CounterSetting
        {
            get => _counterSetting;
            set => SetProperty(ref _counterSetting, value);
        }

        public SettingPageViewModel()
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
        }
    }
}
