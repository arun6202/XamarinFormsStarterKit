using System;
using System.Collections.Generic;
using XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer;
using Splat;
using Xamarin.Forms;

namespace XamarinFormsStarterKit.LayoutGallery.kickstarter_androidoss.XAML
{
    public partial class BackerView : ContentPage
    {
         void Handle_Appearing(object sender, System.EventArgs e)
        {
           
        }

        public BackerView()
        {
            InitializeComponent();
          
            Colorizer.Colorize((Layout)Content);

            var er = new Label().Text;

            //back.Source = "https://lorempixel.com/100/100";

           // back.Source = new UriImageSource
           //{
           //     Uri = new Uri("https://lorempixel.com/400/200/"),
           //    CachingEnabled = true,
           //    CacheValidity = new TimeSpan(5, 0, 0, 0)
           //};



        }
    }
}
