using SauceDemo.Pages;
using SauceDemo.Services;

namespace SauceDemo.Tests;

public class BaseTest : BasePage
{
    [SetUp]
    public void Setup()
    {
        OpenLoginPage();
    }
    
    [TearDown]
    public void TearDown() 
    {
        DriverManager.CloseBrowser();
    }
}