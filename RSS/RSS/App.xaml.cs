using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RSS.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RSS
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override async void OnStart()
        {
            string androidAppSecret = "1e6c9095-d5a1-4c3a-afd7-2e778b6e0692";
            string iOSAppSecret = "ccd9a401-d645-4ab8-b229-b426877ad132";
            AppCenter.Start($"android={androidAppSecret};ios={iOSAppSecret}", typeof(Crashes), typeof(Analytics));


        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
