using CounterInfo.Models;
using CounterInfo.Repository;
using System.Diagnostics;

namespace CounterInfo
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

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
