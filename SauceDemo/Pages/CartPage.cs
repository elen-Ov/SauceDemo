using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class CartPage : BasePage
{
    private readonly By _shoppingCartLink = By.XPath("//a[contains(@data-test,'shopping-cart-link')]");
    private readonly By _shoppingCartLabel = By.XPath("//span[text()='Your Cart']");
        
    public void GoToCartPage()
    {
        Driver.FindElement(_shoppingCartLink).Click();
    }
    
    public bool IsShoppingCartLabelPresentOnPage()
    {
        bool state = Driver.FindElement(_shoppingCartLabel).Displayed;
        return state;
    }
}