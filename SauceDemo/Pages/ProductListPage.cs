using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static System.String;
using Allure.NUnit.Attributes;

namespace SauceDemo.Pages;

public class ProductListPage : BasePage
{
    private readonly By _productLabel = By.XPath("//span[text()='Products']");
    private readonly By _cartLink = By.XPath("//a[contains(@class,'shopping_cart_link')]");
    private readonly By _cartBadge = By.XPath("//span[@class='shopping_cart_badge']");
    private readonly string _itemLocator = "//div[text()='{0}']";
    private readonly string _addToCartButtonOfProduct = "//div[text()='{0}']//following::button[text() ='Add to cart']";
    
    [AllureStep("Переход на страницу с товарами / витрину")]
    public bool IsProductLabelPresentOnPage()
    {
        return PerformLoggedAction("Переход на страницу с товарами / витрину", () =>
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            try
            {
                var element = wait.Until(driver => driver.FindElement(_productLabel));
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        });
    }

    [AllureStep("Проверка наличия товара на витрине")]
    public bool IsItemPresentOnPage(string itemName)
    {
        return PerformLoggedAction($"Проверка наличия товара '{itemName}' на витрине", () =>
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            try
            {
                var element = wait.Until(driver => driver.FindElement(By.XPath(Format(_itemLocator, itemName))));
                return element.Displayed; 
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        });
    }

    [AllureStep("Клик по кнопке 'добавить товар'")]
    public void ClickAddToCartButton(string itemName)
    {
        PerformLoggedAction($"Клик по кнопке 'добавить товар'", () =>
        {
            var button = Driver.FindElement(By.XPath(Format(_addToCartButtonOfProduct, itemName)));
            button.Click();
        });
    }

    [AllureStep("Количество товаров в корзине на значке корзины")]
    public int GetProductsQuantityOnShoppingCartBadge()
    {
        return PerformLoggedAction($"Количество товаров в корзине на значке корзины", () =>
        {
            var cartBadge = Driver.FindElement(_cartBadge);
            var quantity = cartBadge.Text;
            int productsQuantity = Convert.ToInt32(quantity);
            return productsQuantity;
        });
    }
    
    [AllureStep("Переход в корзину")]
    public void GoToCart()
    {
        PerformLoggedAction($"", () =>
        {
            var cartLink = Driver.FindElement(_cartLink);
            cartLink.Click();
        });
    }
}