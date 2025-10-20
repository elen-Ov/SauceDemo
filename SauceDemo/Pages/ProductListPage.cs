using OpenQA.Selenium;
using static System.String;
using Allure.NUnit.Attributes;
using SauceDemo.Utils;

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
        return LoggerUtil.LogStep<bool>("Проверка отображения лейбла продукта", () =>
        {
            try
            {
                var element = WaitForElementAndReturn(_productLabel);
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
        return LoggerUtil.LogStep<bool>($"Проверка отображения товара '{itemName}'", () =>
        {
            try
            {
                var locator = By.XPath(Format(_itemLocator, itemName));  
                var element = WaitForElementAndReturn(locator);  
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
        LoggerUtil.LogStep($"Клик по кнопке 'добавить товар' для '{itemName}'", () =>
        {
            var locator = By.XPath(Format(_addToCartButtonOfProduct, itemName));  
            var button = WaitForElementAndReturn(locator);
            button.Click();
        });
    }

    [AllureStep("Количество товаров в корзине на значке корзины")]
    public int GetProductsQuantityOnShoppingCartBadge()
    {
        return LoggerUtil.LogStep<int>("Количество товаров в корзине на значке корзины", () =>
        {
            try
            {
                var cartBadge = WaitForElementAndReturn(_cartBadge);
                var quantityText = cartBadge.Text;
                if (IsNullOrEmpty(quantityText))
                {
                    return 0;  // текст пустой или null, возвращаем 0
                }
                else
                {
                    return Convert.ToInt32(quantityText);
                }  
            }
            catch (WebDriverTimeoutException)
            {
                return 0;
            }
        });
    }
    
    [AllureStep("Переход в корзину")]
    public void GoToCart()
    {
        LoggerUtil.LogStep("Переход в корзину", () =>
        {
            var cartLink = Driver.FindElement(_cartLink);
            cartLink.Click();
        });
    }
}