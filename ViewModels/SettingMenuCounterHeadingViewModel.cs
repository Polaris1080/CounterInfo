using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.SettingMenuPage;



namespace CounterInfo.ViewModels
{
    internal partial class SettingMenuCounterHeadingViewModel(Models.CounterSetting cs, int orderMax) : ObservableObject
    {
        private const    int OrderMin = 0;
        private readonly int order    = cs.Order;
        private readonly int orderMax = orderMax;
        private readonly Repository.CounterSettingRepository repository = new();

        [ObservableProperty] public partial int     ID   { get; private set; } = cs.ID;
        [ObservableProperty] public partial string? Name { get; private set; } = cs.Name;

        public bool   UpButtonEnable => order > OrderMin;
        public bool DownButtonEnable => order < orderMax;

        [RelayCommand] private async Task OnUp()
        {
            if (UpButtonEnable)
            {
                await repository.ChangeOrderAsync(ID);                                      // 順番を変更
                WeakReferenceMessenger.Default.Send(new SettingCounterListUpdateMessage()); // リスト更新
            }
        }
        [RelayCommand] private async Task OnDown()
        {
            if (DownButtonEnable)
            {
                await repository.ChangeOrderAsync(ID,true);                                 // 順番を変更
                WeakReferenceMessenger.Default.Send(new SettingCounterListUpdateMessage()); // リスト更新
            }
        }
        [RelayCommand] private async Task Setting() => WeakReferenceMessenger.Default.Send(new OpenSettingPageMessage(ID));
    }
}
