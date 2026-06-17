using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using CounterInfo.Repository;
using CounterInfo.View;
using CounterInfo.ViewModels;

namespace CounterInfo.Pages;

public partial class SettingPage : ContentPage
{
	public SettingPage(int? id)
	{

        
		InitializeComponent();
        if (id is int _id) 
        {
            Task.Run(() => {
                var t1 = new SettingPageViewModel();

                var source = (new CounterSettingRepository()
                    .GetItemAsync(_id).Result
                );

                t1.ID = source.ID;
                t1.ChangeMode = true;
                t1.Name = source.Name;
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Code to run on the main thread
                BindingContext = t1;
                });
            });
        
        }

    }


    /* Event */
    async void BackButtonClicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void ReturnButtonClicked(object sender, EventArgs e)
    {
        if (await DisplayAlertAsync("保存","しますか","はい","いいえ"))
        {
            
        }
        await Navigation.PopAsync();
    }
    async void SaveButtonClicked(object? sender, EventArgs e)
    {
        // TODO : 保存処理
        WeakReferenceMessenger.Default.Send(new Messages.SettingCounterListUpdateMessage());
        await Navigation.PopAsync();
    }


}