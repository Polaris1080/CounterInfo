namespace CounterInfo;

public partial class SettingPage : ContentPage
{
	public SettingPage()
	{
		InitializeComponent();
	}

    async void OnReturnButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}