using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CounterInfo.Repository;
using Microsoft.Extensions.Logging;

namespace CounterInfo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiCommunityToolkit().UseMauiCommunityToolkitCore();

            builder.Services.AddSingleton<CounterInfoRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
