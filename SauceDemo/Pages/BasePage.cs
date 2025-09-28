using OpenQA.Selenium;
using SauceDemo.Services;

namespace SauceDemo.Pages;

public class BasePage
{
    private readonly string _baseUrl = "https://www.saucedemo.com";
    // свойство, можно так - protected IWebDriver Driver => DriverManager.Driver;
    protected IWebDriver Driver
    {
        get { return DriverManager.Driver; }
    }
    
    // открытие сайта
    protected void OpenLoginPage()
    {
        Driver.Navigate().GoToUrl(_baseUrl);
        Driver.Manage().Window.Maximize();
    }
}