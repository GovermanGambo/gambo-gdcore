namespace Gambo.GDCore.Diagnostics;

public static class Assert
{
    public static bool IsNotNull<T>(T obj, string message = null) where T : class
    {
        bool result = obj is not null;

        if (!result)
        {
            Log.Error(message ?? $"Object of type {typeof(T).Name} was null!");
        }

        return result;
    }
}