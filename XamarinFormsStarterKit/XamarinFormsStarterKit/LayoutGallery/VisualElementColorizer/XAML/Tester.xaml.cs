using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinFormsStarterKit.Xamarin.FormsBook.Toolkit;

namespace XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer.XAML
{
    public partial class Tester : ContentPage
    {
        public Tester()
        {
            InitializeComponent();
            Colorizer.LoremText((Layout)Content,true);
            Colorizer.Colorize((Layout)Content, true);
            Colorizer.Image((Layout)Content, true);


           // image.Source = ImageSource.FromResource("XamarinFormsStarterKit.LayoutGallery.SampleImages.basicbitmaps-large.png");

            //int rows = 128;
            //int cols = 64;
            //BmpMaker bmpMaker = new BmpMaker(cols, rows);

            ////bmpMaker.SetPixel(50,50,Color.BlueViolet);

            //for (int row = 0; row < rows; row++)
            //    for (int col = 0; col < cols; col++)
            //    {
            //        bmpMaker.SetPixel(row, col, 2 * row, 0, 2 * (128 - row));
               
            //    }

            //ImageSource imageSource = bmpMaker.Generate();
            //image.Source = imageSource;

        }
    }
}
