using log4net;
using log4net.Config;

namespace SauceDemo.Utils;

public class LoggerUtil
{
    private static readonly ILog Log = LogManager.GetLogger(typeof(LoggerUtil));
    
    // инициализация логгера
    public static void Initialize(string configPath, string logFilePath)
    {
        GlobalContext.Properties["LogFileName"] = logFilePath;
        XmlConfigurator.Configure(new FileInfo(configPath));
        Log.Info("LoggerUtil инициализирован");
    }
    
    //  методы логирования
    public static void Info(string message) => Log.Info(message);
    public static void Error(string message, Exception ex = null)
    {
        Log.Error(message, ex);
    }
    
    // метод для логирования шагов с Allure
    public static void LogStep(string stepDescription, Action action)
    {
        Info($"Шаг: {stepDescription}");
        try
        {
            action(); // выполнение действия (например, клик)
            Info($"Шаг '{stepDescription}' выполнен успешно");
        }
        catch (Exception ex)
        {
            Error($"Ошибка в шаге '{stepDescription}': {ex.Message}", ex);
            throw; // для фейла теста
        }
    }
    
    // метод для действий с возвращаемым значением
    public static T LogStep<T>(string stepDescription, Func<T> func)
    {
        Info($"Шаг: {stepDescription}");
        try
        {
            T result = func(); // выполнение и получение результата
            Info($"Шаг '{stepDescription}' выполнен успешно");
            return result; // возврат результата
        }
        catch (Exception ex)
        {
            Error($"Ошибка в шаге '{stepDescription}': {ex.Message}", ex);
            throw; // для фейла теста
        }
    }
}