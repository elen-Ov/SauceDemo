using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class ProductListPage : BasePage
{
    private readonly By _productLabel = By.XPath("//span[text()='Products']");
    private readonly By _cartLink = By.XPath("//a[contains(@class,'shopping_cart_link')]");
    private readonly By _cartBadge = By.XPath("//span[@class='shopping_cart_badge']");
    private readonly By _productElement = By.XPath("//div[@class='inventory_list']");
    private readonly By _productsList = By.TagName("div");
    private readonly By _addToCartButton = By.XPath("//button[@class='btn btn_primary btn_small btn_inventory ']");
    
    public bool IsProductLabelPresentOnPage()
    {
        bool state = Driver.FindElement(_productLabel).Displayed;
        return state;
    }
    
    public CartPage GoToCart()
    {
        var cartLink = Driver.FindElement(_cartLink);
        cartLink.Click();
        return new CartPage();
    }

    public IWebElement ChooseDefiniteProduct(int index)
    {
        var productElement = Driver.FindElement(_productElement);
        var productsList = productElement.FindElements(_productsList);
        var chosenProduct = productsList[index];
        return chosenProduct;
    }

    public void AddProductToCart()
    {
        var addToCartButton = Driver.FindElement(_addToCartButton);
        addToCartButton.Click();
    }

    public int GetProductsQuantity()
    {
        var cartBadge = Driver.FindElement(_cartBadge);
        var quantity = cartBadge.Text;
        int productsQuantity = Convert.ToInt32(quantity);
        return productsQuantity;
    }
}