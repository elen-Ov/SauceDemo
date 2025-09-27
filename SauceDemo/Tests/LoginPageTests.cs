using SauceDemo.Pages;

namespace SauceDemo.Tests;

public class LoginPageTests : BaseTest
{
    private readonly LoginPage _loginPage = new LoginPage();
    private readonly ProductListPage _productListPage = new ProductListPage();

    [SetUp]
    public void Setup() {}

    [Test]
    public void Login_ValidDataValueTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.LoginWithStandardUser();
        // Assert
        Assert.That(_productListPage.IsProductLabelPresentOnPage(), 
            Is.True, "Products label is not present on page");
    }
    
    [Test]
    [TestCase("STANDARD_USER", "SECRET_SAUCE")]
    [TestCase("~!@#$%^&*()_+={}[]|<>?//", "~!@#$%^&*()_+={}[]|<>?//")]
    [TestCase(" ", " ")]
    public void Login_InvalidDataValueTest(string username, string password)
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName(username).SetPassword(password).ClickLoginButton();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Username and password do not match any user in this service"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_EmptyUsernameWithValidPasswordTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName("").SetPassword(_loginPage.DefaultPassword).ClickLoginButton();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Username is required"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_EmptyPasswordWithValidUserNameTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName(_loginPage.StandardUsername).SetPassword("").ClickLoginButton();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Password is required"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_LockedOutUserTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName(_loginPage.LockedOutUsername).SetPassword(_loginPage.DefaultPassword).ClickLoginButton();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Sorry, this user has been locked out."), 
            "Error message doesn't match the expected one.");
    }
}