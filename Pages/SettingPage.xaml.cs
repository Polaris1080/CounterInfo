using CounterInfo.Models;
using CounterInfo.Repository;

namespace CounterInfo.Pages;

public partial class SettingPage : ContentPage
{
    CounterSettingRepository counterSettingRepository;

	public SettingPage()
	{
		InitializeComponent();
        counterSettingRepository = new CounterSettingRepository();
        //counterSettingRepository.SaveItemAsync(new CounterSetting{ Name="窓口Ａ" });
    }

    async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}