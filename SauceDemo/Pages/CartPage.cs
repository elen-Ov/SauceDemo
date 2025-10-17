using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Allure.NUnit.Attributes;

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
        return PerformLoggedAction($"Переход на страницу корзины", () =>
        {
            bool state = Driver.FindElement(_shoppingCartLabel).Displayed;
            return state;
        });
    }

    [AllureStep("Получение списка товаров в корзине")]
    public List<string> GetAllProductNamesInCart()
    {
        return PerformLoggedAction($"Получение списка товаров в корзине", () =>
        {
            var productElements = Driver.FindElements(_productInCartName);
            return productElements.Select(e => e.Text).ToList();
        });
    }

    [AllureStep("Проверка наличия товара в корзине")]
    public bool IsProductPresentInCart(string productName)
    {
        return PerformLoggedAction($"Проверка наличия товара в корзине", () =>
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            try
            {
                var element = wait.Until(driver => driver.FindElement(_productInCartName));
                var itemName = element.Text;
                if (itemName == productName)
                {
                    return true;
                }

                return false;
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
        return PerformLoggedAction($"Получение названия товара в корзине", () =>
        {
            var itemName = Driver.FindElement(_productInCartName).Text;
            return itemName;
        });
    }

    [AllureStep("Получение цены товара в корзине")]
    public string GetProductInCartPrice()
    {
        return PerformLoggedAction($"", () =>
        {
            var itemPrice = Driver.FindElement(_productInCartPrice).Text;
            return itemPrice;
        });
    }

    [AllureStep("Клик по кнопке удаления товара из корзины")]
    public void ClickRemoveItemButton()
    {
        PerformLoggedAction($"Клик по кнопке удаления товара из корзины", () =>
        {
            var removeButton = Driver.FindElement(_removeItemButton);
            removeButton.Click();
        });
    }
}