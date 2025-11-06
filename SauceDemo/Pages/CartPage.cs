using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Allure.NUnit.Attributes;
using SauceDemo.Utils;
using static System.String;

namespace SauceDemo.Pages;

public class CartPage : BasePage
{
    private readonly By _shoppingCartLabel = By.XPath("//span[text()='Your Cart']");
    private readonly By _productInCartNames = By.ClassName("inventory_item_name");
    private readonly string _productInCartName = "//div[@data-test='inventory-item-name' and text()='{0}']";
    private readonly string _productInCartPrice = "//div[@class='inventory_item_price' and string()='{0}']";
    private readonly By _removeItemButton = By.XPath("//button[@class='btn btn_secondary btn_small cart_button']");

    [AllureStep("Переход на страницу корзины")]
    public bool IsShoppingCartLabelPresentOnPage()
    {
        return LoggerUtil.LogStep<bool>("Проверка отображения лейбла корзины", () =>
        {
            try
            {
                var element = WaitForElementAndReturn(_shoppingCartLabel);  
                return element.Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        });
    }

    [AllureStep("Получение списка товаров в корзине")]
    public List<string> GetAllProductNamesInCart()
    {
        return LoggerUtil.LogStep<List<string>>("Получение списка товаров в корзине", () =>
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                var productElements = wait.Until(driver => driver.FindElements(_productInCartNames));
                return productElements.Select(e => e.Text).ToList();
            }
            catch (WebDriverTimeoutException)
            {
                return new List<string>();  // если элементы не найдены, возвращаем пустой список
            }
        });
    }

    [AllureStep("Проверка наличия товара в корзине")]
    public bool IsProductPresentInCart(string productName)
    {
        return LoggerUtil.LogStep<bool>($"Проверка наличия товара '{productName}' в корзине", () =>
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                var locator = By.XPath(Format(_productInCartName, productName));
                var productElements = wait.Until(driver => driver.FindElements(locator));
                return productElements.Any(e => e.Text == productName);  
            }
            catch (WebDriverTimeoutException)
            {
                return false;  
            }
        });
    }

    [AllureStep("Получение названия товара в корзине")]
    public string GetProductInCartName(string productName)
    {
        return LoggerUtil.LogStep<string>("Получение названия товара в корзине", () =>
        {
            try
            {
                var locator = By.XPath(Format( _productInCartName, productName));
                var element = WaitForElementAndReturn(locator);  
                return element.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;  // элемент не найден, возвращаем пустую строку
            }
        });
    }

    [AllureStep("Получение цены товара в корзине")]
    public string GetProductInCartPrice(string productPrice)
    {
        return LoggerUtil.LogStep<string>("Получение цены товара в корзине", () =>
        {
            try
            {
                var locator = By.XPath(Format( _productInCartPrice, productPrice));
                var element = WaitForElementAndReturn(locator);  
                return element.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;  
            }
        });
    }

    [AllureStep("Клик по кнопке удаления товара из корзины")]
    public void ClickRemoveItemButton()
    {
        LoggerUtil.LogStep("Клик по кнопке удаления товара из корзины", () =>
        {
            var removeButton = WaitForElementAndReturn(_removeItemButton);
            removeButton.Click();
        });
    }
}