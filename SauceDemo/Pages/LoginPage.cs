using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class LoginPage : BasePage
{
    private readonly By _userNameField = By.Id("user-name");
    private readonly By _passwordField = By.Id("password");
    private readonly By _loginButtonField = By.Id("login-button");
    
    public void OpenLoginPage()
    {
        Driver.Navigate().GoToUrl("https://www.saucedemo.com");
    }
    
    public void Login(string username, string password)
    {
        //Driver.FindElement(_userNameField).Click();
        Driver.FindElement(_userNameField).SendKeys(username);
        //Driver.FindElement(_passwordField).Click();
        Driver.FindElement(_passwordField).SendKeys(password);
        Driver.FindElement(_loginButtonField).Click();
    }
}