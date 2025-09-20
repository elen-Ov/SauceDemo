using SauceDemo.Pages;

namespace SauceDemo.Tests;

public class ProductListPageTests : BaseTest
{
    private readonly LoginPage _loginPage = new LoginPage();
    private readonly ProductListPage _productListPage = new ProductListPage();
    private readonly CartPage _cartPage = new CartPage();
    
    [SetUp]
    public void Setup() {}

    [Test]
    public void ProductListPage_GoToCartTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        _loginPage.LoginWithStandardUser();
        // Act
        _productListPage.GoToCart();
        // Assert
        Assert.That(_cartPage.IsShoppingCartLabelPresentOnPage(), 
            Is.True, "Cart label is not present on page");
    }
    
    [Test]
    public void ProductListPage_QuantityOfProductsTest()
    {
        // Arrange
        _loginPage.OpenLoginPage();
        _loginPage.LoginWithStandardUser();
        // Act
        for (int i = 1; i <= 2; i++)
        {
            _productListPage.ChooseDefiniteProduct(i);
            _productListPage.AddProductToCart();
        }
        // Assert
        Assert.That(_productListPage.GetProductsQuantity(), 
            Is.EqualTo(2), "Quantity of products should be 2");
    }
}