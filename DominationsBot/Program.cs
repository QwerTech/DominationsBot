using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using DominationsBot.Tools;
using OpenQA.Selenium.Appium.Service;
using System;

namespace DominationsBot
{
    class Program
    {
        private static AppiumLocalService LocalService;

        public static Uri sauceURI = new Uri("http://ondemand.saucelabs.com:80/wd/hub");

        public static Uri LocalServiceURIAndroid
        {
            get
            {
                if (LocalService == null)
                {
                    AppiumServiceBuilder builder = new AppiumServiceBuilder().WithLogFile(new System.IO.FileInfo("Log"));
                    LocalService = builder.Build();
                }

                if (!LocalService.IsRunning)
                {
                    LocalService.Start();
                }

                return LocalService.ServiceUrl;
            }
        }
        static void Main(string[] args)
        {

            var blueStackWindowHandle = BlueStackHelper.GetBlueStackWindowHandle();

            IoC.Container.GetInstance<CollectGold>().DoWork();



            //DesiredCapabilities capabilities = new DesiredCapabilities();
            //capabilities.SetCapability(MobileCapabilityType.DeviceName, "emulator-5554");
            //capabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
            ////capabilities.SetCapability(MobileCapabilityType.AppPackage, "com.nexonm.dominations.adk");
            ////capabilities.SetCapability(MobileCapabilityType.AppActivity, "com.nexon.dominations.DomLauncher");
            ////capabilities.SetCapability(MobileCapabilityType.App,"c:/test.apk");
            //var driver = new AndroidDriver<AppiumWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"),capabilities);
            //var elements = driver.FindElements(By.ClassName("android.view.View"));
            //driver.GetScreenshot().SaveAsFile("123.png",ImageFormat.Png);
            //var view = elements.First();
            //new TouchAction(driver).Tap(view, 100, 10, 1);
            //view.Tap(1, 300);
            //view.Click();
            //view.GetScreenshot();
            //var readOnlyCollection = view.FindElements(By.XPath(".//*"));
        }
    }
}
