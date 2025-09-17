using OpenQA.Selenium;

namespace SauceDemo.Pages;

public class ProductsPage : BasePage
{
    private readonly By _productLabel = By.XPath("//span[text()='Products']");
    
    public bool IsProductLabelPresentOnPage()
    {
        bool state = Driver.FindElement(_productLabel).Displayed;
        return state;
    }
}