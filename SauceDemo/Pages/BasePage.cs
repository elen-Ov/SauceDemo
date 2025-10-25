using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Utils;

namespace SauceDemo.Pages;

public class BasePage
{
    private readonly string _baseUrl = "https://www.saucedemo.com";
    
    // свойство
    protected IWebDriver Driver => DriverManager.Driver;
    
    // открытие сайта
    protected void OpenLoginPage()
    {
        Driver.Navigate().GoToUrl(_baseUrl);
    }
    
    // ожидание с возвратом + логирование
    protected IWebElement WaitForElementAndReturn(By locator, int timeoutSeconds = 10)
    {
        return LoggerUtil.LogStep($"Ожидание элемента {locator}", () =>
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(d =>
            {
                var element = d.FindElement(locator);
                if (element.Displayed)
                {
                    return element;
                }
                else
                {
                    return null;
                }
            });
        });
    }
}