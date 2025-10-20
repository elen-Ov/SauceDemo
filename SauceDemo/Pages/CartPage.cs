using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Allure.NUnit.Attributes;
using SauceDemo.Utils;

namespace SauceDemo.Pages;

public class CartPage : BasePage
{
    private readonly By _shoppingCartLabel = By.XPath("//span[text()='Your Cart']");
    private readonly By _productInCartName = By.ClassName("inventory_item_name");
    private readonly By _productInCartPrice = By.ClassName("inventory_item_price");
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
                var productElements = wait.Until(driver => driver.FindElements(_productInCartName));
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
                var productElements = wait.Until(driver => driver.FindElements(_productInCartName));
                return productElements.Any(e => e.Text == productName);  
            }
            catch (WebDriverTimeoutException)
            {
                return false;  
            }
        });
    }

    [AllureStep("Получение названия товара в корзине")]
    public string GetProductInCartName()
    {
        return LoggerUtil.LogStep<string>("Получение названия товара в корзине", () =>
        {
            try
            {
                var element = WaitForElementAndReturn(_productInCartName);  
                return element.Text;
            }
            catch (WebDriverTimeoutException)
            {
                return string.Empty;  // элемент не найден, возвращаем пустую строку
            }
        });
    }

    [AllureStep("Получение цены товара в корзине")]
    public string GetProductInCartPrice()
    {
        return LoggerUtil.LogStep<string>("Получение цены товара в корзине", () =>
        {
            try
            {
                var element = WaitForElementAndReturn(_productInCartPrice);  
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