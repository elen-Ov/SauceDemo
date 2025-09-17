using OpenQA.Selenium;
using SauceDemo.Services;

namespace SauceDemo.Pages;

public class BasePage
{
    protected readonly IWebDriver Driver = DriverManager.Driver;
}