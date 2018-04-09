using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFormsStarterKit.LayoutGallery.k0shk0sh_FastHub.XAML
{
    public partial class home : ContentPage
    {
        public home()
        {
            InitializeComponent();
            VisualElementColorizer.Colorizer.Colorize((Layout)Content);
            VisualElementColorizer.Colorizer.RandomImage((Layout)Content);
            VisualElementColorizer.Colorizer.LoremText((Layout)Content);


        }
    }
}
