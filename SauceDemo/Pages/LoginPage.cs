using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class LoginPage : BasePage
{
    private readonly By _userNameField = By.Id("user-name");
    private readonly By _passwordField = By.Id("password");
    private readonly By _loginButtonField = By.Id("login-button");
    private readonly By _errorMessageBy = By.CssSelector(".error-message-container.error");
    public readonly string StandardUsername = "standard_user";
    public readonly string LockedOutUsername = "locked_out_user";
    //public readonly string ProblemUsername = "problem_user";
    //public readonly string PerformanceGlitchUsername = "performance_glitch_user";
    //public readonly string ErrorUsername = "error_user";
    //public readonly string VisualUsername = "visual_user";
    public readonly string DefaultPassword = "secret_sauce";
    
    public void OpenLoginPage()
    {
        Driver.Navigate().GoToUrl("https://www.saucedemo.com");
    }

    public LoginPage SetUserName(string userName)
    {
        Driver.FindElement(_userNameField).SendKeys(userName);
        return this;
    }

    public LoginPage SetPassword(string password)
    {
        Driver.FindElement(_passwordField).SendKeys(password);
        return this;
    }

    private IWebElement GetLoginButton()
    {
        return Driver.FindElement(_loginButtonField);
    }
    
    public ProductListPage ClickLogin()
    {
        GetLoginButton().Click();
        return new ProductListPage();
    }

    public string GetErrorMessage()
    {
        return Driver.FindElement(_errorMessageBy).Text;
    }

    public ProductListPage LoginWithStandardUser()
    {
        return Login(StandardUsername, DefaultPassword);
    }
    
    public ProductListPage Login(string username, string password)
    {
        SetUserName(username);
        SetPassword(password);
        return ClickLogin();
    }
}