using CommunityToolkit.Mvvm.Messaging;
using CounterInfo.Messages.MainPage;
namespace CounterInfo.Pages;



public partial class MainPage : ContentPage
{
    /* Property */
    /// <summary>更新時間</summary>
    public  DateTime  UpdatedTime {get => _updatedTime;  private set {_updatedTime = value;  UpdateTime.Text = UpdatedTime.ToString("MM/dd HH:mm");}}
    private DateTime _updatedTime = DateTime.MinValue;


    /* Value */
#if DEBUG_STOPREALITYMOCK
#else
    private readonly RealityMock realityMock = RealityMock.GetInstance();
#endif



    /* Initialize */
    public MainPage()
    {
        InitializeComponent();
        UpdatedTime = DateTime.Now;
    }

    /* Event */
    async void OnSettingButtonClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new SettingMenuPage());
    }
    async void OnReloadButtonClicked (object? sender, EventArgs e)
    {
        // 「分」が進んでいなければ更新しない
        var now = DateTime.Now;
        if (now.Hour > UpdatedTime.Hour || now.Minute > UpdatedTime.Minute)
        {
            UpdatedTime = now;
            WeakReferenceMessenger.Default.Send(new InfomationUpdateMessage());
        }
    }
}