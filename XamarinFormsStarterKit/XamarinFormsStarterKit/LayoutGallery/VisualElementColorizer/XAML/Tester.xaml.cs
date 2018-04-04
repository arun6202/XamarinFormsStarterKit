using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer.XAML
{
    public partial class Tester : ContentPage
    {
        public Tester()
        {
            InitializeComponent();
            Colorizer.LoremText((Layout)Content,true);
            Colorizer.Colorize((Layout)Content, true);

        }
    }
}
