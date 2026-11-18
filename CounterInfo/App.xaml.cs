using CounterInfo.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace CounterInfo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new MainPage()));
        }
    }
}