using System;
using System.Collections.Generic;
using XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer;
using Xamarin.Forms;

namespace XamarinFormsStarterKit.DesignKitTemplates.Books.XAML
{
    public partial class Sign_Up_1_1 : ContentPage
    {
        public Sign_Up_1_1()
        {
            InitializeComponent();
			Colorizer.RandomImage((Layout)Content);
        }
    }
}
