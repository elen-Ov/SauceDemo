using OpenQA.Selenium;
using Allure.NUnit.Attributes;

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
        // делегат Action (лямбда-выражение () => { ... })  
        // лямбда не выполняется сразу — она передаётся как параметр и вызывается внутри метода  PerformLoggedAction()
        return PerformLoggedAction($"Ввод имени пользователя: {userName}", () =>
            {
                Driver.FindElement(_userNameField).SendKeys(userName);
                return this;
            });
    }

    [AllureStep("Ввод пароля: {password}")]
    public LoginPage SetPassword(string password)
    {
        return PerformLoggedAction($"Ввод пароля: {password}", () =>
            {
                Driver.FindElement(_passwordField).SendKeys(password);
                return this;
            });
    }
    
    [AllureStep("Клик по кнопке логина")]
    public ProductListPage ClickLoginButton()
    {
        return PerformLoggedAction($"Клик по кнопке логина", () =>
        {
            var loginButton = Driver.FindElement(_loginButtonField);
            loginButton.Click();
            return new ProductListPage();
        });
    }

    public string GetErrorMessage()
    {
        return Driver.FindElement(_errorMessageBy).Text;
    }

    [AllureStep("Логин под стандартным пользователем")]
    public ProductListPage LoginWithStandardUser()
    {
        return PerformLoggedAction($"Логин под стандартным пользователем", () =>
            {
                Login(StandardUsername, DefaultPassword);
                return new ProductListPage();
            });
    }
    
    [AllureStep("Логин с именем и паролем: {username}, {password}")]
    private ProductListPage Login(string username, string password)
    {
        return PerformLoggedAction($"Логин с именем и паролем: {username}, {password}", () =>
        {
            SetUserName(username);
            SetPassword(password);
            ClickLoginButton();
            return new ProductListPage();
        });
    }
}