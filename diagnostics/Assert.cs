using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Humbug;

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

    public static bool AreEqual<T>(T a, T b, string message = null)
    {
        bool result = a.Equals(b);

        if (!result)
        {
            Log.Error(message ?? "Variables were not equal!");
        }

        return result;
    }

    public static bool IsOfType<T>(object obj, string message = null) where T : class
    {
        bool result = obj is T;

        if (!result)
        {
            Log.Error(message ?? $"Object was not of type {typeof(T).Name}!");
        }

        return result;
    }

    public static bool Single<T>(IEnumerable<T> enumerable, string message = null)
    {
        bool result = enumerable.Count() == 1;

        if (!result)
        {
            Log.Error(message ?? "Collection must contain exactly one element!");
        }

        return result;
    }
}