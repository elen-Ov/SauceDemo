using log4net;
using OpenQA.Selenium;
using SauceDemo.Utils;

namespace SauceDemo.Pages;

public class BasePage
{
    private readonly string _baseUrl = "https://www.saucedemo.com";
    private static readonly ILog Log = LogManager.GetLogger(typeof(BasePage));
    
    // свойство, можно так - protected IWebDriver Driver => DriverManager.Driver;
    protected IWebDriver Driver
    {
        get { return DriverManager.Driver; }
    }
    
    // открытие сайта
    protected void OpenLoginPage()
    {
        Driver.Navigate().GoToUrl(_baseUrl);
    }
    
    // логирование
    protected void PerformLoggedAction(string logMessage, Action action)
    {
        Log.Info(logMessage); // логируем сообщение
        try
        {
            action(); // выполняем лямбду — находим элемент и вводим текст
        }
        catch (Exception ex)
        {
            Log.Error($"Ошибка: {logMessage}", ex); // логируем ошибку
            throw;
        }
    }
    
    // для методов с возвращаемым значением 
    protected T PerformLoggedAction<T>(string logMessage, Func<T> action)
    {
        Log.Info(logMessage);
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            Log.Error($"Ошибка: {logMessage}", ex);
            throw;
        }
    }
}