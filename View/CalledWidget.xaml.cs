using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.MainPage;
using CounterInfo.Repository;
namespace CounterInfo.View;



public partial class CalledWidget : ContentView
{
    public CalledWidget() => InitializeComponent();
}



internal partial class CalledWidgetViewModel : ObservableObject
{
    private readonly CounterInfoRepository repository = new();

    [ObservableProperty] public partial List<int>? Source { get; set; }

    public CalledWidgetViewModel()
    {
        Refresh();
        if (!WeakReferenceMessenger.Default.IsRegistered<InfomationUpdateMessage>(this)) {
             WeakReferenceMessenger.Default.    Register<InfomationUpdateMessage>(this, (_, _) => Refresh());
        }
        void Refresh() => Task.Run(async () => {
            Source = [.. (await repository.GetCalledAsync()).Select(s => s.ID)];
        });
    }
}