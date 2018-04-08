using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer;

namespace XamarinFormsStarterKit.LayoutGallery.k0shk0sh_FastHub.XAML
{
    public partial class DashBoardView : ContentPage
    {
        public DashBoardView()
        {
            InitializeComponent();
 
            Colorizer.Colorize((Layout)Content,true);
            Colorizer.LoremText((Layout)Content,false);
            Colorizer.RandomImage((Layout)Content,false);

        }
    }
}
