using SauceDemo.Services;

namespace SauceDemo.Tests;

public class BaseTest
{
    [OneTimeTearDown]
    public void OneTimeTeardown() 
    {
        DriverManager.CloseBrowser();
    }
}