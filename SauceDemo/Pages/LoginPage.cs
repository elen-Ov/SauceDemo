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
    public readonly string DefaultPassword = "secret_sauce";

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
    
    public ProductListPage ClickLoginButton()
    {
        var loginButton = Driver.FindElement(_loginButtonField);;
        loginButton.Click();
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
    
    private ProductListPage Login(string username, string password)
    {
        SetUserName(username);
        SetPassword(password);
        return ClickLoginButton();
    }
}