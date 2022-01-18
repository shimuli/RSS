using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using RSS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace RSS.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public Posts Blog
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainVM()
        {
            ReadRss();
        }

        public void ReadRss()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Posts));

                using (WebClient client = new WebClient())
                {
                    string xml = Encoding.Default.GetString(client.DownloadData("https://cointelegraph.com/rss"));
                    using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                    {
                        Blog = (Posts)serializer.Deserialize(reader);
                    }
                }

                // analytics          
                var properties = new Dictionary<string, string>
                {
                    {"Initial_Page",$"{Blog}" }
                };
                Analytics.TrackEvent("Fetching_Blog_List", properties);
            }
            catch (Exception ex)
            {

                var properties = new Dictionary<string, string>
                {
                    {"Blo_Post","Error occured" }
                };
                Crashes.TrackError(ex, properties);
            }
           
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
