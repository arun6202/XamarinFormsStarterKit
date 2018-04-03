using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFormsStarterKit.LayoutGallery.kickstarter_androidoss.XAML
{
    public partial class DashBoardView : ContentPage
    {
        public DashBoardView()
        {
            InitializeComponent();
            VisualElementColorizer.Colorizer.Colorize((Layout)Content);
        }
    }
}
