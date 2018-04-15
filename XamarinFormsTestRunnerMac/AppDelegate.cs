using AppKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;
using XamarinFormsTestRunnerMac;

namespace XamarinFormsTestRunnerMac
{


    public class App : Application
    {
        public App()
        {
            MainPage = new ContentPage
            {
                Content = new Label
                {
                    Text = "Hello, Forms !",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                }
            };
        }
    }


	[Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        NSWindow _window;

        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;

            var rect = new CoreGraphics.CGRect(500, 1000, 500, 500);
            _window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            _window.Title = "Xamarin.Forms on Mac!";
            _window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow
        {
            get { return _window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            Forms.Init();
            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }

    

}
