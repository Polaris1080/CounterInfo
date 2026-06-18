using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.MainPage;
using CounterInfo.Messages.SettingMenuPage;
using CounterInfo.Models;
using CounterInfo.Repository;
namespace CounterInfo.View;



public partial class CounterInfomationWidget : ContentView
{
    public CounterInfomationWidget() => InitializeComponent();
}



internal partial class CounterInfomationWidgetViewModel : ObservableObject
{
    private readonly CounterInfoDataContext repository = new();

    [ObservableProperty] public partial List<CounterInfoData>? Source { get; set; }

    public CounterInfomationWidgetViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>        (this)) {
             WeakReferenceMessenger.Default.    Register<InfomationUpdateMessage>        (this, (_, _) => Refresh());
        }
        if (!WeakReferenceMessenger.Default.IsRegistered<SettingCounterListUpdateMessage>(this)) {
             WeakReferenceMessenger.Default.    Register<SettingCounterListUpdateMessage>(this, (_, _) => Refresh());
        }
        void Refresh() => Task.Run(async () => {
            Source = await repository.GetItemsAsync();
        });
    }
}