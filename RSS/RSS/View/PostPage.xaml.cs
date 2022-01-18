using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RSS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RSS.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        public PostPage()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
        }

        public PostPage(Item item)
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);

            try
            {
                //throw(new Exception("Cannot load blog"));
                webView.Source = item.ItemLink;

                // analytics          
                var properties = new Dictionary<string, string>
                {
                    {"Blo_Post",$"{item.Title}" }
                };
                TrackEvent(properties);
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string>
                {
                    {"Blo_Post",$"{item.Title}" }
                };
                TrackError(ex,properties);
            }
            
        }
        private async void TrackEvent(Dictionary<string, string> properties)
        {
            if(await Analytics.IsEnabledAsync())
            {
                Analytics.TrackEvent("Blog_Post_Opened", properties);
            }
           
        }

        private async void TrackError(Exception ex, Dictionary<string, string> properties)
        {
            if (await Crashes.IsEnabledAsync())
            {
                Crashes.TrackError(ex, properties);

                bool appCrashed = await Crashes.HasCrashedInLastSessionAsync();
                if (appCrashed)
                {
                    var crashReport = await Crashes.GetLastSessionCrashReportAsync();
                }
            }


        }
    }
}