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
        
        driver.FindElement(By.ClassName("header_label"));
        driver.FindElement(By.TagName("span"));
        driver.FindElement(By.LinkText("Sauce Labs Backpack"));
        driver.FindElement(By.PartialLinkText("Backpack"));
        
        // seach by Xpath
        driver.FindElement(By.XPath("//div[@class='primary_header']"));
        driver.FindElement(By.XPath("//div[text()='Swag Labs']"));
        driver.FindElement(By.XPath("//a[contains(@data-test,'shopping-cart-link')]"));
        driver.FindElement(By.XPath("//div[contains(text(),'carry.allTheThings')]"));
        driver.FindElement(By.XPath("//*[text()='29.99']/ancestor::div[@class='inventory_item']"));
        driver.FindElement(By.XPath("//div[@id='menu_button_container']/descendant::button[@id='react-burger-cross-btn']"));
        driver.FindElement(By.XPath("//ul[@class='social']/following::div[@class='footer_copy']"));
        driver.FindElement(By.XPath("//div[@class='footer_copy']/parent::footer[@data-test='footer']"));
        driver.FindElement(By.XPath("//span[@class='title']/preceding::div[@id='menu_button_container']"));
        driver.FindElement(By.XPath("//a[@id='inventory_sidebar_link']/following-sibling::a[@id='reset_sidebar_link']"));
        driver.FindElement(By.XPath("//span[@class='active_option' and @data-test='active-option']"));
        
        // search by CSS
        driver.FindElement(By.CssSelector(".select_container"));
        driver.FindElement(By.XPath("//div[@id='shopping_cart_container']")).Click();
        driver.FindElement(By.CssSelector(".btn.btn_secondary.back.btn_medium"));
        driver.FindElement(By.CssSelector("#checkout"));
        driver.FindElement(By.CssSelector("footer.footer"));
        driver.FindElement(By.CssSelector("[name='continue-shopping']"));
        driver.FindElement(By.CssSelector("[class~='back']"));
        driver.FindElement(By.CssSelector("[name|='continue']"));
        driver.FindElement(By.CssSelector("[href^='https://twitter']"));
        driver.FindElement(By.CssSelector("[data-test$='list']"));
        driver.FindElement(By.CssSelector("[class*='btn_action']"));
        
        // for check
        //Thread.Sleep(2000);
        driver.Quit();
    }
}