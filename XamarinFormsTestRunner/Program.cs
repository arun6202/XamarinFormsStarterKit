using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Collections.Generic;

namespace XamarinFormsTestRunner
{
	class Program
	{
		private static AppiumDriver<IOSElement> driver;

		public static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180);
		public static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(5);




		static void Main(string[] args)
		{
			Console.WriteLine("Starting Program...");
            Console.WriteLine(DateTime.Now);

			CreateSimulatorAndScreenshots();


			Console.WriteLine(DateTime.Now);
            
			Console.WriteLine("Completed All");

			Console.Read();
		}

		private static void CreateSimulatorAndScreenshots()
		{
			foreach (var device in DeviceList)
			{
				Console.WriteLine("Opening... " + device );

				DesiredCapabilities capabilities = new DesiredCapabilities();
				capabilities.SetCapability("platformName", "11.3");
				capabilities.SetCapability("deviceName", device);
				capabilities.SetCapability("platformName", "iOS");
				capabilities.SetCapability("automationName", "XCUITest");
				capabilities.SetCapability("app", "/Users/arun/Desktop/XamarinFormsStarterKit.iOS.app");

				Uri serverUri = new Uri("http://0.0.0.0:4723/wd/hub");
				driver = new IOSDriver<IOSElement>(serverUri, capabilities, INIT_TIMEOUT_SEC);
				driver.Manage().Timeouts().ImplicitWait = INIT_TIMEOUT_SEC;

				Console.WriteLine("taking screenshot... " + device);
                 
				var fileName = String.Format("{0}{1}{2}{3}", "Screenshots/",device + " ", DateTime.Now.ToString("dd HH mm ss"), ".png");
				var screenShot = driver.GetScreenshot();
				screenShot.SaveAsFile(fileName);

				Console.WriteLine("Shutting down... " + device);

				driver.Quit();


			}
		}

		public static string iPad5 = "iPad (5th generation)";
		public static string iPadair = "iPad Air";
		public static string iPadair2 = "iPad Air 2";
		public static string iPadpro10 = "iPad Pro (10.5-inch)";
		public static string iPadpro12 = "iPad Pro (12.9-inch)";
		public static string iPadpro9 = "iPad Pro (9.7-inch)";
		public static string iPhone5s = "iPhone 5s";
		public static string iPhone6 = "iPhone 6";
		public static string iPhone6s = "iPhone 6s";
		public static string iPhone6sp = "iPhone 6s Plus";
		public static string iPhone7 = "iPhone 7";
		public static string iPhone7P = "iPhone 7 Plus";
		public static string iPhone8 = "iPhone 8";
		public static string iPhone8p = "iPhone 8 Plus";
		public static string iPhoneSE = "iPhone SE";
		public static string iPhoneX = "iPhone X";


		public static List<string> DeviceList = new List<string>
		{

		iPhone5s ,
		iPhone6 ,
		iPhone6s ,
		iPhone6sp ,
		iPhone7 ,
		iPhone7P ,
		iPhone8 ,
		iPhone8p ,
		iPhoneSE ,
		iPhoneX ,
		iPad5 ,
        iPadair ,
        iPadair2 ,
        iPadpro10 ,
        iPadpro12 ,
        iPadpro9 ,
		};





	}
}
