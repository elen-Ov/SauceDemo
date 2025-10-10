using OpenQA.Selenium;
using Allure.NUnit.Attributes;
using log4net;

namespace SauceDemo.Pages;

public class LoginPage : BasePage
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoginPage));
    private readonly By _userNameField = By.Id("user-name");
    private readonly By _passwordField = By.Id("password");
    private readonly By _loginButtonField = By.Id("login-button");
    private readonly By _errorMessageBy = By.CssSelector(".error-message-container.error");
    public readonly string StandardUsername = "standard_user";
    public readonly string LockedOutUsername = "locked_out_user";
    public readonly string DefaultPassword = "secret_sauce";

    [AllureStep("Ввод имени пользователя: {userName}")]
    public LoginPage SetUserName(string userName)
    {
        Log.Info($"Ввод имени пользователя: {userName}");
        try
        {
            Driver.FindElement(_userNameField).SendKeys(userName);
        }
        catch (Exception ex)
        {
            Log.Error("Ошибка при вводе имени пользователя", ex);
        }
        return this;
    }

    [AllureStep("Ввод пароля: {password}")]
    public LoginPage SetPassword(string password)
    {
        Driver.FindElement(_passwordField).SendKeys(password);
        return this;
    }
    
    [AllureStep("Клик по кнопке логина")]
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

    [AllureStep("Логин под стандартным пользователем")]
    public ProductListPage LoginWithStandardUser()
    {
        return Login(StandardUsername, DefaultPassword);
    }
    
    [AllureStep("Логин с именем и паролем: {username}, {password}")]
    private ProductListPage Login(string username, string password)
    {
        SetUserName(username);
        SetPassword(password);
        return ClickLoginButton();
    }
}