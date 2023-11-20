using Godot;

namespace Gambo.GDCore.Diagnostics;

public static class Log
{
    private const string DebugPrefix = "[DEBUG] ";
    private const string InformationPrefix = "[INFO] ";
    private const string ErrorPrefix = "[ERROR] ";
    private const string WarningPrefix = "[WARN] ";


    public static void Warning(string message)
    {
        GD.PrintRich("[color=yellow]" + WarningPrefix + message + "[/color]");
    }
    
    public static void Information(string message)
    {
        GD.PrintRich("[color=green]" + InformationPrefix + message + "[/color]");
    }
    
    public static void Debug(string message)
    {
        GD.Print(DebugPrefix + message);
    }

    public static void Error(string message)
    {
        GD.PrintErr(ErrorPrefix + message);
    }
}