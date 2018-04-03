using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsStarterKit.LayoutGallery.airbnb_lottie_android.XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashBoardView : ContentPage
	{
		public DashBoardView ()
		{
			InitializeComponent ();
            VisualElementColorizer.Colorizer.Colorize((Layout)Content);
        }
	}
}