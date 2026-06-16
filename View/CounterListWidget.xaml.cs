using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages;
using CounterInfo.Repository;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CounterInfo.View;



public partial class CounterListWidget : ContentView
{
	public CounterListWidget()
	{
		InitializeComponent();
	}
}



internal class CounterListWidgetViewModel : ObservableObject
{
    CounterSettingRepository counterSettingRepository = new();

    /* Property */
    /// <summary>情報源（表示用）</summary>
    public  ObservableCollection<CounterListWidgetData>  Source {get => _source;  private set => SetProperty(ref _source, value);}
    private ObservableCollection<CounterListWidgetData> _source = [];


    /* Initialize */
    public CounterListWidgetViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this))
        {
            WeakReferenceMessenger.Default.Register<InfomationUpdateMessage>(this, (_, _) => Refresh());
        }

        void Refresh() => Task.Run(() => { Source = new(counterSettingRepository.GetItemsAsync().Result.OrderBy(x => x.Order).Select(x => new CounterListWidgetData() {ID = x.ID, Name = x.Name })); });
    }
}



internal class CounterListWidgetData : ObservableObject
{

    CounterSettingRepository counterSettingRepository = new();

    public int    ID   { get; set; }
    public string Name { get; set; } = "";

    public CounterListWidgetData() {
            // Command
        UpCommand     = new AsyncRelayCommand(OrderUp);
        DownCommand   = new AsyncRelayCommand(OrderDown);
        DeleteCommand = new AsyncRelayCommand(Delete);
    }

    public ICommand UpCommand     { get; }
    public ICommand DownCommand   { get; }
    public ICommand DeleteCommand { get; }

    async Task OrderUp() { await counterSettingRepository.ChangeOrderAsync(1); }
    async Task OrderDown() { }
    async Task Delete() { }
}



internal sealed record CounterListUpdateMessage;