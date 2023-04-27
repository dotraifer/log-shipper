using Serilog;

public static class Logger
{
    public static void Debug(string messageTemplate, params object[] propertyValues)
    {
        Log.Debug(messageTemplate, propertyValues);
    }

    public static void Information(string messageTemplate, params object[] propertyValues)
    {
        Log.Information(messageTemplate, propertyValues);
    }

    public static void Warning(string messageTemplate, params object[] propertyValues)
    {
        Log.Warning(messageTemplate, propertyValues);
    }

    public static void Error(string messageTemplate, params object[] propertyValues)
    {
        Log.Error(messageTemplate, propertyValues);
    }

    public static void Fatal(string messageTemplate, params object[] propertyValues)
    {
        Log.Fatal(messageTemplate, propertyValues);
    }
}