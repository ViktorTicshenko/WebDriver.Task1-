using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace NetAutomation_WebDriverTask1;

public class BrowserFactory 
{
    public enum BrowserType
    {
        FIREFOX,
        CHROME,
        EDGE
    };

    public static BrowserFactory Instance => new();
    private IWebDriver? driver;
    private  BrowserFactory(){}

    public IWebDriver GetDriver(BrowserType browserType)
    {
        if (driver == null)
        {
            switch (browserType)
            {
                case BrowserType.CHROME: 
                {
                    var options = new ChromeOptions();
                    options.AddArgument("--start-maximized");

                    driver = new ChromeDriver(options);

                    break;
                }
                case BrowserType.EDGE: 
                {
                    var options = new EdgeOptions();
                    options.AddArgument("--start-maximized");

                    driver = new EdgeDriver(options);

                    break;
                }
                case BrowserType.FIREFOX: 
                {
                    driver = new FirefoxDriver();
                    driver.Manage().Window.Maximize();

                    break;
                }
                default: 
                {
                    throw new ArgumentOutOfRangeException(nameof(browserType), browserType, null);
                }
            }
        }

        return driver;
    }

    public void CloseDriver()
    {
        driver?.Dispose();
    }
}