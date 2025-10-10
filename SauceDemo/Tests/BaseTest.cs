using Allure.Net.Commons;
using log4net;
using log4net.Config;
using NUnit.Framework.Interfaces;
using SauceDemo.Pages;
using SauceDemo.Utils;

namespace SauceDemo.Tests;

public class BaseTest : BasePage
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(BaseTest));
    private readonly ScreenshotUtils _screenshotUtils = new ScreenshotUtils();
    
    // настройка логирования
    [OneTimeSetUp]
    public void SetupLogging()
    {
        // путь к папке, где исполняется тест (обычно bin/Debug/netX.X)
        // @"..\..\.." — поднимаемся на три уровня вверх, чтобы попасть в корень проекта
        string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
        // создаёт путь к папке Logs внутри корня проекта, здесь будут храниться лог-файлы
        // '/Users/eovcharova/RiderProjects/SauceDemo/SauceDemo/bin/Debug/net8.0/..\..\../Logs'
        string logDir = Path.Combine(projectRoot, "Logs");
        // создаёт папку Logs, если она ещё не существует
        Directory.CreateDirectory(logDir);
        // формирует полный путь к лог-файлу test.log внутри папки Logs
        string logPath = Path.Combine(logDir, "test.log");
        // позволяет динамически подставлять путь к лог-файлу в log4net.config через ${logfile} или %property{LogFileName}
        GlobalContext.Properties["LogFileName"] = logPath;
        // формирует путь к конфигурационному файлу log4net (log4net.config) в корне проекта
        var configPath = Path.Combine(projectRoot, "log4net.config");
        // загружает и применяет настройки логирования из log4net.config, без этой строки log4net не будет знать, как и куда писать логи
        XmlConfigurator.Configure(new FileInfo(configPath));
        // использовать для записи логов с привязкой к конкретному классу
        //var log = LogManager.GetLogger(typeof(BaseTest));
        // записывает информационное сообщение в лог, подтверждающее, что логгер настроен
        Log.Info("Логгер инициализирован");
    }

    [SetUp]
    public void Setup()
    {
        Log.Info("Открываю браузер");
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
        Log.Info("Закрываю браузер");
        DriverManager.CloseBrowser();
    }
}