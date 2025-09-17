using SauceDemo.Pages;
using SauceDemo.Services;

namespace SauceDemo.Tests;

public class SauceDemoTests
{
    private readonly LoginPage _loginPage = new LoginPage();
    private readonly ProductsPage _productsPage = new ProductsPage();

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        _loginPage.OpenLoginPage();
        _loginPage.Login("standard_user", "secret_sauce");

        Thread.Sleep(2000);
        //_productsPage.IsProductLabelExist();

        Assert.That(_productsPage.IsProductLabelPresentOnPage(), Is.True, "Products label is not present on page");
    }
    
    [TearDown]
    public void Teardown() 
    {
        DriverManager.Quit();
    }  
}