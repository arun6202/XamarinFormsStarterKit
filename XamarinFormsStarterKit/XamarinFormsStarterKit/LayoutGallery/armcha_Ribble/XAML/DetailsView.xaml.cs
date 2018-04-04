using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;


namespace XamarinFormsStarterKit.LayoutGallery.armcha_Ribble.XAML
{
public partial class DetailsView : ContentPage
{
    public DetailsView()
    {
        InitializeComponent();
      
        Colorizer.Colorize((Layout)Content, true);
        //Colorizer.LoremText((Layout)Content, true);
        //Colorizer.Image((Layout)Content, true);

    }
}
}
