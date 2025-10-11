namespace SauceDemo.Utils;

public class TestDataSource
{
    public static IEnumerable<TestCaseData> GetTestCasesForLogin()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDir, "Resources", "testdataForLogin.csv");
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            string userName = parts[0];
            string userPassword = parts[1];
            bool isLoggedIn = bool.Parse(parts[2]);
            yield return new TestCaseData(userName, userPassword, isLoggedIn)
                .SetName($"Login under user with name {userName} and {userPassword} equals to {isLoggedIn}");
        }
    }
    
    public static IEnumerable<TestCaseData> GetTestCasesForCart()
    {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(baseDir, "Resources", "testdataForCart.csv");
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            string productName = parts[0];
            string productPriceCurrencyAndSum = parts[1];
            yield return new TestCaseData(productName, productPriceCurrencyAndSum)
                .SetName($"The product's name is {productName} and its price is {productPriceCurrencyAndSum}");
        }
    }
}