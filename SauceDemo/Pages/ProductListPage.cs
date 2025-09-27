using OpenQA.Selenium;
using static System.String;

namespace SauceDemo.Pages;

public class ProductListPage : BasePage
{
    private readonly By _productLabel = By.XPath("//span[text()='Products']");
    private readonly By _cartLink = By.XPath("//a[contains(@class,'shopping_cart_link')]");
    private readonly By _cartBadge = By.XPath("//span[@class='shopping_cart_badge']");
    private readonly string _itemLocator = "//div[text()='{0}']";
    private readonly string _addToCartButtonOfProduct = "//button[contains(@id, 'add-to-cart-{0}')]";
    
    public bool IsProductLabelPresentOnPage()
    {
        bool state = Driver.FindElement(_productLabel).Displayed;
        return state;
    }

    public bool IsItemPresentOnPage(string itemName)
    {
        bool state = Driver.FindElement(By.XPath(Format(_itemLocator, itemName))).Displayed;
        return state;
    }

    public void ChooseDefiniteProductByName(string itemName)
    {
        Driver.FindElement(By.XPath(Format(_itemLocator, itemName)));
    }

    // "Sauce Labs Bolt T-Shirt" -> "sauce-labs-bolt-t-shirt"
    private string GenerateSlug(string itemName)
    {
        return itemName.ToLower()
            .Replace(" ", "-")
            .Replace(".", "")
            .Replace("()", "")
            .Replace("(", "-")
            .Replace(")", "");
    }
    private IWebElement FindAddToCartButtonOfProduct(string itemName)
    {
        string slug = GenerateSlug(itemName);
        string elementButton = Format(_addToCartButtonOfProduct, slug);
        var element = Driver.FindElement(By.XPath(elementButton));
        return element;
    }

    public void ClickAddToCartButton(string itemName)
    {
        var button = FindAddToCartButtonOfProduct(itemName);
        button.Click();
    }

    public int GetProductsQuantityOnShoppingCartBadge()
    {
        var cartBadge = Driver.FindElement(_cartBadge);
        var quantity = cartBadge.Text;
        int productsQuantity = Convert.ToInt32(quantity);
        return productsQuantity;
    }
    
    public void GoToCart()
    {
        var cartLink = Driver.FindElement(_cartLink);
        cartLink.Click();
    }
}