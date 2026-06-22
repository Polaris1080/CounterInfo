using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Models;
using CounterInfo.Repository;
using CounterInfo.View;
using CounterInfo.ViewModels;

namespace CounterInfo.Pages;

public partial class SettingPage : ContentPage
{
    private readonly CounterSettingRepository repository = new();

    private readonly int? id = null;

	public SettingPage(int? id)
	{
        this.id = id;
        InitializeComponent();
        if (id is int _id)
        {            
            Task.Run(async () => {
                var vm = new SettingPageViewModel(await repository.GetItemAsync(_id));
                MainThread.BeginInvokeOnMainThread(() => { BindingContext = vm; });
            });        
        }
    }

    /* Event */
    async void BackButtonClicked(object? sender, EventArgs e)
    {
        if (await DisplayAlertAsync("戻りますか？", "保存されません", "はい", "いいえ"))
        {
            await Navigation.PopAsync();
        }
    }
    private async void ReturnButtonClicked(object? sender, EventArgs e)
    {
        if (BindingContext is SettingPageViewModel vm && await DisplayAlertAsync("変更しますか？", "", "はい", "いいえ"))
        {
            await repository.SaveItemAsync(vm.ToCounterSetting());
            WeakReferenceMessenger.Default.Send(new Messages.SettingMenuPage.SettingCounterListUpdateMessage());
        }
        await Navigation.PopAsync();
    }
    async void SaveButtonClicked(object? sender, EventArgs e)
    {
        if (BindingContext is SettingPageViewModel vm && await DisplayAlertAsync("保存しますか？", "", "保存", "キャンセル"))
        {
            await repository.SaveItemAsync(vm.ToCounterSetting());
            WeakReferenceMessenger.Default.Send(new Messages.SettingMenuPage.SettingCounterListUpdateMessage());
            await Navigation.PopAsync();
        }
    }
    async void DeleteButtonClicked(object? sender, EventArgs e)
    {
        if (this.id is int id && await DisplayAlertAsync("削除しますか？", "元には戻せません", "削除", "キャンセル"))
        {
            await repository.DeleteItemAsync(id);
            WeakReferenceMessenger.Default.Send(new Messages.SettingMenuPage.SettingCounterListUpdateMessage());
            await Navigation.PopAsync();
        }
    }

}