using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages;
using CounterInfo.Repository;
using CounterInfo.ViewModels;
using System.Collections.ObjectModel;
namespace CounterInfo.Pages;



public partial class SettingMenuPage : ContentPage
{
    /* Initialize */
    public SettingMenuPage()
	{
		InitializeComponent();

        if (!WeakReferenceMessenger.Default.IsRegistered<OpenSettingPageMessage>(this))
        {
            WeakReferenceMessenger.Default.Register<OpenSettingPageMessage>(this, (_, _) => OpenSettingPage());
        }

        void OpenSettingPage(){
            Navigation.PushAsync(new SettingPage());
        }
    }


    /* Event */
    async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    async void OnAppendButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingAppendPage());
    }
}



internal class SettingMenuPageViewModel : ObservableObject
{
    /* Property */
    /// <summary>情報源（表示用）</summary>
    public  ObservableCollection<SettingCounterHeadingViewModel>  Source {get => _source;  private set => SetProperty(ref _source, value);}
    private ObservableCollection<SettingCounterHeadingViewModel> _source = [];


    /* Initialize */
    public SettingMenuPageViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this))
        {
             WeakReferenceMessenger.Default.Register    <InfomationUpdateMessage>(this, (_, _) => Refresh());
        }

        void Refresh() => Task.Run(() => {
            Source = new(new CounterSettingRepository()
                .GetItemsAsync().Result
                .OrderBy(x => x.Order)
                .Select (x => new SettingCounterHeadingViewModel(x))
            );
        });
    }
}