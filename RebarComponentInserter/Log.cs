using NLog;

namespace RebarComponentInserter;

internal static class Log
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public static void Debug(string message) => Logger.Debug(message);
    public static void Info(string message) => Logger.Info(message);
    public static void Warn(string message) => Logger.Warn(message);
    public static void Error(string message) => Logger.Error(message);
    public static void Error(System.Exception ex, string message) => Logger.Error(ex, message);

    public static Logger For<T>() => LogManager.GetLogger(typeof(T).FullName);
}
