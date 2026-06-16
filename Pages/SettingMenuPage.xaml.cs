using CounterInfo.Repository;
namespace CounterInfo.Pages;

public partial class SettingMenuPage : ContentPage
{
    CounterSettingRepository counterSettingRepository;

	public SettingMenuPage()
	{
		InitializeComponent();
        counterSettingRepository = new CounterSettingRepository();
        //counterSettingRepository.SaveItemAsync(new CounterSetting{ Name="窓口Ａ" });
    }


    /* Event */
    async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    async void OnAppendButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingPage());
    }
}