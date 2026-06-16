using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.View;

namespace CounterInfo.Pages;

public partial class SettingPage : ContentPage
{
	public SettingPage()
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
        // TODO : 保存処理
        WeakReferenceMessenger.Default.Send(new CounterListUpdateMessage());
        await Navigation.PopAsync();
    }
}