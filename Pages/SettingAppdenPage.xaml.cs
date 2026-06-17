using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using CounterInfo.Repository;
using CounterInfo.View;
using CounterInfo.ViewModels;

namespace CounterInfo.Pages;

public partial class SettingAppendPage : ContentPage
{
    CounterSettingRepository repository = new();

	public SettingAppendPage()
	{
		InitializeComponent();
	}


    /* Event */
    async void BackButtonClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    async void SaveButtonClicked(object? sender, EventArgs e)
    {
        if (BindingContext is SettingPageViewModel vm)
        {
            await repository.SaveItemAsync(
                item : new CounterSetting() {
                    Name  = vm.Name,
                    Order = await repository.GetCountAsync()
                }
            );
            WeakReferenceMessenger.Default.Send(new Messages.SettingCounterListUpdateMessage());
        }
        await Navigation.PopAsync();
    }
}