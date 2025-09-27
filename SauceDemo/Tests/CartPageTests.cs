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
    public void CartPage_ProductsAddedToCartCheckByProductNamesTest()
    {
        // Arrange
        var expectedProductNames = new List<string>
        {
            "Sauce Labs Backpack",
            "Sauce Labs Fleece Jacket"
        };
        _loginPage.OpenLoginPage();
        _loginPage.LoginWithStandardUser();
        // Act
        foreach (var product in expectedProductNames)
        {
            _productListPage.ChooseDefiniteProductByName(product);
            _productListPage.ClickAddToCartButton(product);
        }
        _productListPage.GoToCart();
        var actualProductNames = _cartPage.GetAllProductNamesInCart();
        // Assert
        Assert.That(expectedProductNames, 
            Is.EqualTo(actualProductNames), 
            "Product's name(s) added to cart is/are not equal to the name(s) on the products' page.");
    }
}