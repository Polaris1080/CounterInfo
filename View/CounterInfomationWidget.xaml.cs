using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.MainPage;
using CounterInfo.Models;
using CounterInfo.Repository;
using System.Collections.ObjectModel;
namespace CounterInfo.View;



public partial class CounterInfomationWidget : ContentView
{
	public CounterInfomationWidget()
	{
		InitializeComponent();
	}
}



internal class CounterInfomationWidgetViewModel : ObservableObject
{
    /* Property */
    /// <summary>情報源（表示用）</summary>
    public  ObservableCollection<CounterInfoData>  Source { get => _source;  private set => SetProperty(ref _source, value); }
    private ObservableCollection<CounterInfoData> _source = [];


    /* Initialize */
    public CounterInfomationWidgetViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this))
        {
            WeakReferenceMessenger.Default.Register<InfomationUpdateMessage>(this, (_, _) => Refresh());
        }

        void Refresh() => Task.Run(() => { Source = new(new CounterInfoDataContext().GetItemsAsync().Result); });
    }
}