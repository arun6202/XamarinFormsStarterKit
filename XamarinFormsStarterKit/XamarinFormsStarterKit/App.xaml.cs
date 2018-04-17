using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
//using XamarinFormsStarterKit.LayoutGallery.k0shk0sh_FastHub.XAML;
//using XamarinFormsStarterKit.LayoutGallery.wwayne_react_native_nba_app.XAML;
//using XamarinFormsStarterKit.LayoutGallery.airbnb_lottie_android.XAML;
//using XamarinFormsStarterKit.LayoutGallery.armcha_Ribble.XAML;
//using XamarinFormsStarterKit.LayoutGallery.kickstarter_androidoss.XAML;
//using XamarinFormsStarterKit.LayoutGallery.VisualElementColorizer.XAML;

using XamarinFormsStarterKit.DesignKitTemplates.Books.XAML;

namespace XamarinFormsStarterKit
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new Sign_Up_1_1();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
