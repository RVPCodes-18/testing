using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using Project.Program.EventHandler;

namespace Project.Program
{
    public class ProgramTest
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
                
                    ExcelReader reader = new ExcelReader("Project/testdata/login.xlsx");
                    string url = reader.GetCellValue("Sheet1", 0, 0);
                    string email = reader.GetCellValue("Sheet1", 1, 0);
                    string password = reader.GetCellValue("Sheet1", 1, 1);
                
                // Assign the eventDriver to the driver variable to use its event capabilities.
                driver = eventDriver;


               
            
                    driver.Navigate().GoToUrl(url);
                    driver.FindElement(By.Name("username")).SendKeys(email);
                    driver.FindElement(By.Name("password")).SendKeys(password);
                    driver.FindElement(By.XPath("//*[@id='app']/div[1]/div/div[1]/div/div[2]/div[2]/form/div[3]/button")).Click();
            
                // Start your script from here
                // TODO: Navigate to a URL of your choice.

                // TODO: Find and interact with web elements.

                // TODO: Implement additional WebDriver actions as needed.

                // Quit the Driver
                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
