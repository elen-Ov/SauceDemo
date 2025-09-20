using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class CartPage : BasePage
{
    private readonly By _shoppingCartLabel = By.XPath("//span[text()='Your Cart']");
    private readonly By _productNameElement = By.XPath("//div[@class='inventory_item_name']");
    
    public bool IsShoppingCartLabelPresentOnPage()
    {
        bool state = Driver.FindElement(_shoppingCartLabel).Displayed;
        return state;
    }

    public string GetProductInCartName()
    {
        var productNameElement = Driver.FindElement(_productNameElement);
        var productName = productNameElement.Text;
        return productName;
    }
}