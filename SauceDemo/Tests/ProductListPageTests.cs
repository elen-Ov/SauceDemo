using SauceDemo.Pages;

namespace SauceDemo.Tests;

public class ProductListPageTests : BaseTest
{
    private readonly LoginPage _loginPage = new LoginPage();
    private readonly ProductListPage _productListPage = new ProductListPage();
    private readonly CartPage _cartPage = new CartPage();
    
    [Test]
    public void ProductListPage_GoToCartPageTest()
    {
        // Arrange
        _loginPage.LoginWithStandardUser();
        // Act
        _productListPage.GoToCart();
        // Assert
        Assert.That(_cartPage.IsShoppingCartLabelPresentOnPage(), 
            Is.True, "Cart label is not present on page");
    }
    
    [Test]
    public void ProductListPage_QuantityOfProductsOnShoppingCartBadgeTest()
    {
        // Arrange
        _loginPage.LoginWithStandardUser();
        // Act
        var productsToAdd = new List<string>
        {
            "Sauce Labs Bolt T-Shirt",
            "Sauce Labs Bike Light"
        };
        foreach (var product in productsToAdd)
        {
            _productListPage.ClickAddToCartButton(product);
        }
        
        // Assert
        Assert.That(_productListPage.GetProductsQuantityOnShoppingCartBadge(), 
            Is.EqualTo(productsToAdd.Count), "Quantity of products should be equal to the number added");
    }
    
    // classwork
    [Test]
    public void ProductListPage_CheckItemTest()
    {
        // Arrange
        _loginPage.LoginWithStandardUser();

        Assert.Multiple(() =>
        {
            // для проверки работы теста Assert.IsTrue(!_productListPage.IsItemPresentOnPage("Sauce Labs Bike Light"), "2");
            Assert.IsTrue(_productListPage.IsItemPresentOnPage("Sauce Labs Backpack"), "1");
            Assert.IsTrue(_productListPage.IsItemPresentOnPage("Sauce Labs Bike Light"), "2");
            Assert.IsTrue(_productListPage.IsItemPresentOnPage("Sauce Labs Bolt T-Shirt"), "3");
            Assert.IsTrue(_productListPage.IsItemPresentOnPage("Sauce Labs Fleece Jacket"), "4");
        });
    }
}