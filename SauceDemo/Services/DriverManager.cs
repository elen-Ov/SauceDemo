using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemo.Services;

public class DriverManager
{
    private static IWebDriver driver;

    public static IWebDriver Driver
    {
        get
        {
            if (driver == null)
            {
                driver = Init();
            }
            return driver;
        }
    }

    private static IWebDriver Init()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        return new ChromeDriver(options);
    }

    public static void Quit()
    {
        driver?.Quit();
        driver = null;
    } 
}