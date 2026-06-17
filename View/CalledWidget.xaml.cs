using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.MainPage;
using CounterInfo.Repository;
using System.Collections.ObjectModel;
namespace CounterInfo.View;



public partial class CalledWidget : ContentView
{
	public CalledWidget()
	{
		InitializeComponent();
	}
}



internal class CalledWidgetViewModel : ObservableObject
{
    /* Property */
    /// <summary>情報源（表示用）</summary>
    public  ObservableCollection<int>  Source {get => _source;  private set => SetProperty(ref _source, value);}
    private ObservableCollection<int> _source = [];


    /* Initialize */
    public CalledWidgetViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this))
        {
            WeakReferenceMessenger.Default.Register<InfomationUpdateMessage>(this, (_, _) => Refresh());
        }

        void Refresh() => Task.Run(() => { Source = new(new CounterInfoRepository().GetCalledAsync().Result.Select(x => x.ID)); });
    }
}