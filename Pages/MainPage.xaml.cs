
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages;
using CounterInfo.Models;
using CounterInfo.Repository;
using System.Diagnostics;

namespace CounterInfo.Pages
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        CounterInfoRepository counterInfoRepository;

        public MainPage()
        {
            InitializeComponent();
            counterInfoRepository = new CounterInfoRepository();
            counterInfoRepository.SaveItemAsync(new CounterInfomation { ID = 1, Counter = 0 });
            counterInfoRepository.SaveItemAsync(new CounterInfomation { ID = 2, Counter = 0, Called = true });
            Debug.WriteLine(counterInfoRepository.GetItemsAsync());
            Debug.WriteLine(Constants.DatabasePath);
        }

        async void OnSettingButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingPage());
        }
        async void OnReloadButtonClicked(object sender, EventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new InfomationUpdateMessage());
        }
    }
}
