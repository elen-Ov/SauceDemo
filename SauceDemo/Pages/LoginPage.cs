using OpenQA.Selenium;
using Allure.NUnit.Attributes;
using SauceDemo.Utils;

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

    [AllureStep("Ввод имени пользователя: {userName}")]
    public LoginPage SetUserName(string userName)
    {
        return LoggerUtil.LogStep($"Ввод имени пользователя: '{userName}'", () =>
        {
            var element = WaitForElementAndReturn(_userNameField);
            element.SendKeys(userName);
            return this;
        });
    }

    [AllureStep("Ввод пароля: {password}")]
    public LoginPage SetPassword(string password)
    {
        return LoggerUtil.LogStep($"Ввод пароля: '{password}'", () =>
        {
            var element = WaitForElementAndReturn(_passwordField);  
            element.SendKeys(password);
            return this;
        });
    }
    
    [AllureStep("Клик по кнопке логина")]
    public ProductListPage ClickLoginButton()
    {
        return LoggerUtil.LogStep($"Клик по кнопке логина", () =>
        {
            var loginButton = WaitForElementAndReturn(_loginButtonField);
            loginButton.Click();
            return new ProductListPage();
        });
    }

    public string GetErrorMessage()
    {
        var errorElement = WaitForElementAndReturn(_errorMessageBy);
        return errorElement.Text;
    }

    [AllureStep("Логин под стандартным пользователем")]
    public ProductListPage LoginWithStandardUser()
    {
        return LoggerUtil.LogStep($"Логин под стандартным пользователем", () =>
        {
            return Login(StandardUsername, DefaultPassword);
        });
    }
    
    [AllureStep("Логин с именем и паролем: {username}, {password}")]
    private ProductListPage Login(string username, string password)
    {
        return LoggerUtil.LogStep($"Логин с именем и паролем: '{username}', '{password}'", () =>
        {
            SetUserName(username);
            SetPassword(password);
            return ClickLoginButton();
        });
    }
}