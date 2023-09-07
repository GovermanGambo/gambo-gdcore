using Godot;

namespace Gambo.GDCore.Diagnostics;

public static class Log
{
    public static void Debug(string message)
    {
        GD.Print(message);
    }

    public static void Error(string message)
    {
        GD.PrintErr(message);
    }
}