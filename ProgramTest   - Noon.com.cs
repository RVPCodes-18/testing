using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using Project.Program.EventHandler;
using OpenQA.Selenium.Interactions;
using System.IO;
using NUnit.Framework;

namespace Project.Program
{
    public class ProgramGoogle
    {
        public static void Main(string[] args)
        {
            IWebDriver driver = null;
            try
            {
                // Set up the Selenium Hub URL.
                Uri seleniumHubUrl = new Uri("http://localhost:4444/");

                // Define the Chrome options.
                ChromeOptions options = new ChromeOptions();

                // Initialize the RemoteWebDriver with the specified hub URL and Chrome options.
                driver = new RemoteWebDriver(seleniumHubUrl, options.ToCapabilities(), TimeSpan.FromSeconds(30));
                driver.Manage().Window.Maximize();

                // Wrap the WebDriver with EventFiringWebDriver and attach event handlers.
                EventFiringWebDriver eventDriver = new EventFiringWebDriver(driver);
                WebDriverEventHandler eventHandler = new WebDriverEventHandler(eventDriver);

                // Assign the eventDriver to the driver variable to use its event capabilities.
                driver = eventDriver;

                driver.Navigate().GoToUrl("https://www.noon.com/");
                IWebElement menu = driver.FindElement(By.XPath("//*[@id='default-header-desktop']/div[2]/div/div/div[2]/ul/li[2]/a"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(menu).Perform();
                IWebElement ielement = driver.FindElement(By.XPath("//*[@id='default-header-desktop']/div[2]/div/div/div[2]/ul/li[2]/div/div/div[1]/ul/li[1]/ul/li[1]/a"));
                ielement.Click();

                //


                string fileName = "sstest";
            
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                // /home/coder/project/workspace/Project/screenshots
                // /home/coder/project/workspace/Project/Program
                string folderPath = Path.Combine("/home/coder/project/workspace/Project/screenshots", "screenshots");
                Directory.CreateDirectory(folderPath);
    
                string filePath = Path.Combine(folderPath, $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
    
                TestContext.WriteLine($"Screenshot saved at: {filePath}");
        

                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
} 