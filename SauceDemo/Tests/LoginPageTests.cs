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
    public void Login_ValidDataValueInUpperCaseTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName("STANDARD_USER").SetPassword("SECRET_SAUCE").ClickLogin();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Username and password do not match any user in this service"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_InvalidDataValueTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.Login("blfbla123@#$%^&()", "qwerty12<>*&^%$#@!_+");
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Username and password do not match any user in this service"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_EmptyUsernameTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName("").SetPassword(_loginPage.DefaultPassword).ClickLogin();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Username is required"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_EmptyPasswordTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName(_loginPage.StandardUsername).SetPassword("").ClickLogin();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Password is required"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_WhiteSpaceValueTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName(" ").SetPassword(" ").ClickLogin();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Username and password do not match any user in this service"), 
            "Error message doesn't match the expected one.");
    }
    
    [Test]
    public void Login_LockedOutUserTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        // Act
        _loginPage.SetUserName(_loginPage.LockedOutUsername).SetPassword(_loginPage.DefaultPassword).ClickLogin();
        // Assert
        Assert.That(_loginPage.GetErrorMessage(), 
            Is.EqualTo
                ("Epic sadface: Sorry, this user has been locked out."), 
            "Error message doesn't match the expected one.");
    }
}