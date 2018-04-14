using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace XamarinFormsTestRunner
{
    class Program
    {
		private static AppiumDriver<IOSElement>  driver;

		public static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180);
        public static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(5);
      
		
        static void Main(string[] args)
        {
           
 
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("platformName", "11.3");
			capabilities.SetCapability("deviceName", "iPhone 6");
			capabilities.SetCapability("platformName", "iOS");
			capabilities.SetCapability("automationName", "XCUITest");
			capabilities.SetCapability("app", "/Users/arun/Desktop/XamarinFormsStarterKit.iOS.app");
         
			Uri serverUri = new Uri("http://0.0.0.0:4723/wd/hub");
			driver = new IOSDriver<IOSElement>(serverUri, capabilities, INIT_TIMEOUT_SEC);
			driver.Manage().Timeouts().ImplicitWait = INIT_TIMEOUT_SEC;


			var fileName = String.Format("{0}{1}{2}", "ScreenShot", DateTime.Now.ToString("HHmmss"), ".png");
			var screenShot = driver.GetScreenshot();
            screenShot.SaveAsFile(fileName);
 
            
			driver.Quit();
           

			Console.Write("Launched");
                
 		}
    }
}
