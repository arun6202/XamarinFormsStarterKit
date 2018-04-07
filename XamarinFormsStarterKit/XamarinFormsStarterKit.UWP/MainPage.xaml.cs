using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace XamarinFormsStarterKit.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var assembliesToInclude = new List<Assembly>()
{
    typeof(CachedImage).GetTypeInfo().Assembly,
    typeof(CachedImageRenderer).GetTypeInfo().Assembly
};
            Xamarin.Forms.Forms.Init(e, assembliesToInclude);


            LoadApplication(new XamarinFormsStarterKit.App());
        }
    }
}
