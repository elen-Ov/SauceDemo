using Allure.Net.Commons;
using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;
using SauceDemo.Pages;
using SauceDemo.Utils;

namespace SauceDemo.Tests;

public class BaseTest : BasePage
{
    private readonly ScreenshotUtils _screenshotUtils = new ScreenshotUtils();
    
    // настройка логирования
    [OneTimeSetUp]
    public void SetupLogging()
    {
        string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        string logDir = Path.Combine(projectRoot, "Logs");
        Directory.CreateDirectory(logDir);
        string logPath = Path.Combine(logDir, "test.log");
        GlobalContext.Properties["LogFileName"] = logPath;
        var configPath = Path.Combine(projectRoot, "log4net.config");
        XmlConfigurator.Configure(new FileInfo(configPath));
        LoggerUtil.Initialize(configPath, logPath); 
        LoggerUtil.Info("Логгер готов");
    }

    [SetUp]
    public void Setup()
    {
        LoggerUtil.Info("Открываю браузер");
        OpenLoginPage();
    }
    
    [TearDown]
    public void TearDown() 
    {
        // скриншот падания, до закрытия браузера
        var context = TestContext.CurrentContext;
        if (context.Result.Outcome.Status == TestStatus.Failed)
        {
            AllureLifecycle.Instance.UpdateTestCase(x =>
            {
                x.attachments.Add(new Attachment
                {
                    name = "Failure Screenshot",
                    type = "image/png",
                    source = _screenshotUtils.SaveScreenshotAndReturnFileName()
                }) ;
            });
        }
        LoggerUtil.Info("Закрываю браузер");
        DriverManager.CloseBrowser();
    }
}