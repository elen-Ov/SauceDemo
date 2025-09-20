using SauceDemo.Pages;

namespace SauceDemo.Tests;

public class CartPageTests : BaseTest
{
    private readonly LoginPage _loginPage = new LoginPage();
    private readonly ProductListPage _productListPage = new ProductListPage();
    private readonly CartPage _cartPage = new CartPage();
    
    [SetUp]
    public void Setup() {}

    [Test]
    public void CartPage_ProductAddedToCartCheckByProductNameTest()
    {
        // Arrange
        var expectedProductName = "Sauce Labs Backpack";
        _loginPage.OpenLoginPage();
        _loginPage.LoginWithStandardUser();
        // Act
        _productListPage.ChooseDefiniteProduct(1);
        _productListPage.AddProductToCart();
        _productListPage.GoToCart();
        var actualProductName = _cartPage.GetProductInCartName();
        // Assert
        Assert.That(actualProductName, 
            Is.EqualTo(expectedProductName), 
            "Product's name added to cart isn't equal to the name on the products' page.");
    }
}