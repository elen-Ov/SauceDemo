using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        IWebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        driver.Manage().Window.Maximize();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        // seach by id, name, classname, tagname, linktext, partiallinktext
        driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
        driver.FindElement(By.Name("password")).SendKeys("secret_sauce");
        driver.FindElement(By.Id("login-button")).Click();
        // seach by Xpath
        driver.FindElement(By.ClassName("header_label"));
        driver.FindElement(By.TagName("span"));
        driver.FindElement(By.LinkText("Sauce Labs Backpack"));
        driver.FindElement(By.PartialLinkText("Backpack"));
        driver.FindElement(By.XPath("//div[@style='z-index: 1000;']"));
        driver.FindElement(By.XPath("//div[text()='Swag Labs']"));
        driver.FindElement(By.XPath("//a[contains(@data-test,'shopping-cart-link')]"));
        driver.FindElement(By.XPath("//div[contains(text(),'carry.allTheThings')]"));
        driver.FindElement(By.XPath("//*[text()='29.99']//ancestor::div"));
        driver.FindElement(By.XPath("//div[@id='menu_button_container']/descendant::*"));
        driver.FindElement(By.XPath("//a[@target='_blank']/following::*"));
        driver.FindElement(By.XPath("//li[@class='social_twitter']/parent::*"));
        driver.FindElement(By.XPath("//a[@id='item_1_title_link']/preceding::*"));
        driver.FindElement(By.XPath("//a[@id='inventory_sidebar_link']/following-sibling::*"));
        driver.FindElement(By.XPath("//a[@id='inventory_sidebar_link']/following-sibling::*"));
        driver.FindElement(By.XPath("//span[@class='active_option' and @data-test='active-option']"));
        // seach by CSS - to be done
        // for check
        //Thread.Sleep(2000);
        driver.Quit();
    }
}