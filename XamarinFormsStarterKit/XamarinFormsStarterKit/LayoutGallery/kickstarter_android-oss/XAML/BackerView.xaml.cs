using System;
using System.Collections.Generic;
using XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer;

using Xamarin.Forms;

namespace XamarinFormsStarterKit.LayoutGallery.kickstarter_androidoss.XAML
{
    public partial class BackerView : ContentPage
    {
        public BackerView()
        {
            InitializeComponent();
            Colorizer.Colorize((Layout)Content);
        }
    }
}
