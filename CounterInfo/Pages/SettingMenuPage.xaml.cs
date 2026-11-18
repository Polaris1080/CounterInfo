using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.SettingMenuPage;
using CounterInfo.Repository;
using CounterInfo.ViewModels;
namespace CounterInfo.Pages;



public partial class SettingMenuPage : ContentPage
{
    public SettingMenuPage()
	{
		InitializeComponent();
        if (!WeakReferenceMessenger.Default.IsRegistered<OpenSettingPageMessage>(this)) {
             WeakReferenceMessenger.Default.    Register<OpenSettingPageMessage>(this, (_, message) => OpenSettingChangePage(message.Value));
        }
        void OpenSettingChangePage(int? CounterId){
            Navigation.PushAsync(new SettingPage(CounterId));
        }
    }

    async void OnReturnButtonClicked(object? sender, EventArgs e) => await Navigation.PopAsync();
    async void OnAppendButtonClicked(object? sender, EventArgs e) => WeakReferenceMessenger.Default.Send(new OpenSettingPageMessage(null));
}



internal partial class SettingMenuPageViewModel : ObservableObject
{
    private readonly CounterSettingRepository repository = new();

    [ObservableProperty] public partial List<SettingMenuCounterHeadingViewModel>? Source {get; private set;}

    public SettingMenuPageViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<SettingCounterListUpdateMessage>(this)) {
             WeakReferenceMessenger.Default.Register    <SettingCounterListUpdateMessage>(this, (_, _) => Refresh());
        }
        void Refresh() => Task.Run(async () => {
            var setting = (await repository.GetItemsAsync()).OrderBy(o => o.Order);
            Source = [..setting.Select(s => new SettingMenuCounterHeadingViewModel(s, orderMax: setting.Count() - 1))];
        });
    }
}