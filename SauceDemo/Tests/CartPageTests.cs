using SauceDemo.Pages;
using Allure.NUnit.Attributes;
using MyAllure = Allure.NUnit;
using Allure.Net.Commons;
using SauceDemo.Utils;

namespace SauceDemo.Tests;

[MyAllure.AllureNUnit]
public class CartPageTests : BaseTest
{
    private readonly LoginPage _loginPage = new LoginPage();
    private readonly ProductListPage _productListPage = new ProductListPage();
    private readonly CartPage _cartPage = new CartPage();

    [Test]
    [Category("Cart tests")]
    [Category("QA")]
    [AllureTag("smoke")]
    [AllureSeverity(SeverityLevel.blocker)]
    [AllureOwner("Elena Ov")]
    [AllureSuite("Cart flow check by product name")]
    public void CartPage_ProductsAddedToCartCheckByProductNamesTest()
    {
        // Arrange
        var expectedProductNames = new List<string>
        {
            "Sauce Labs Backpack",
            "Sauce Labs Fleece Jacket"
        };
        _loginPage.LoginWithStandardUser();
        // Act
        foreach (var product in expectedProductNames)
        {
            _productListPage.ClickAddToCartButton(product);
        }
        _productListPage.GoToCart();
        var actualProductNames = _cartPage.GetAllProductNamesInCart();
        // Assert
        Assert.That(expectedProductNames, 
            Is.EqualTo(actualProductNames), 
            "Product's name(s) added to cart is/are not equal to the name(s) on the products' page.");
    }

    [Test]
    [Category("Cart tests")]
    [Category("QA")]
    [AllureTag("regression")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureOwner("Elena Ov")]
    [AllureSuite("Cart flow check by product name and price")]
    [TestCaseSource(typeof(TestDataSource), nameof(TestDataSource.GetTestCasesForCart))]
    public void CartPage_CartCheckByProductNameAndPriceTest(string productName, string price)
    {
        // логин и начальная проверка
        _loginPage.LoginWithStandardUser();
        Assert.That(_productListPage.IsProductLabelPresentOnPage(),
            Is.True, "Product label isn't present on page.");
        // добавить продукт в корзину
        _productListPage.ClickAddToCartButton(productName);
        Assert.That(_productListPage.GetProductsQuantityOnShoppingCartBadge(),
            Is.EqualTo(1), "Product quantity isn't equal to 1.");
        // перейти в корзину и проверить
        _productListPage.GoToCart();
        var itemInCartName = _cartPage.GetProductInCartName();
        var itemInCartPrice = _cartPage.GetProductInCartPrice();
        // soft asserts
        Assert.Multiple(() =>
        {
            Assert.That(_cartPage.IsShoppingCartLabelPresentOnPage(),
                Is.True, "Shopping cart label isn't present on page.");
            Assert.That(itemInCartName, Is.EqualTo(productName), 
                $"Product name {itemInCartName} isn't equal to the expected {productName}.");
            Assert.That(itemInCartPrice, Is.EqualTo(price), 
                $"Product price in cart {itemInCartPrice} isn't equal to expected {price}.");
        });
        // удалить продукт и проверить
        _cartPage.ClickRemoveItemButton();
        Assert.That(_cartPage.IsProductPresentInCart(productName),
            Is.False, $"Product {productName} is present in the cart.");
    }
}