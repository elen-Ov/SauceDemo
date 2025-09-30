using OpenQA.Selenium;
namespace SauceDemo.Pages;

public class CartPage : BasePage
{
    private readonly By _shoppingCartLabel = By.XPath("//span[text()='Your Cart']");
    
    public bool IsShoppingCartLabelPresentOnPage()
    {
        bool state = Driver.FindElement(_shoppingCartLabel).Displayed;
        return state;
    }
    
    public List<string> GetAllProductNamesInCart()
    {
        var productElements = Driver.FindElements(By.ClassName("inventory_item_name"));
        return productElements.Select(e => e.Text).ToList();
    }
}
