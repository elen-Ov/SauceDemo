using OpenQA.Selenium;

namespace SauceDemo.Utils;

public class ScreenshotUtils
{
    private static IWebDriver Driver => DriverManager.Driver;

    public string SaveScreenshotAndReturnFileName()
    {
        var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
        var fileName = $"screenshot_{Guid.NewGuid()}.png";
        var filePath = Path.Combine("allure-results", fileName);
        screenshot.SaveAsFile(filePath);
        return fileName;
    }
}