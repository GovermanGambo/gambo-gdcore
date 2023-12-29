using Godot;

namespace Gambo.GDCore.Diagnostics;

public static class Log
{
    private static LogLevel? s_logLevel;
    
    private const string DebugPrefix = "[DEBUG] ";
    private const string InformationPrefix = "[INFO] ";
    private const string ErrorPrefix = "[ERROR] ";
    private const string WarningPrefix = "[WARN] ";

    public static void Warning(string message)
    {
        if (GetLogLevel() > LogLevel.Warn) return;
        
        GD.PrintRich("[color=yellow]" + WarningPrefix + message + "[/color]");
    }
    
    public static void Information(string message)
    {
        if (GetLogLevel() > LogLevel.Info) return;
        
        GD.PrintRich("[color=green]" + InformationPrefix + message + "[/color]");
    }
    
    public static void Debug(string message)
    {
        if (GetLogLevel() > LogLevel.Debug) return;
        
        GD.Print(DebugPrefix + message);
    }

    public static void Error(string message)
    {
        if (GetLogLevel() > LogLevel.Error) return;
        
        GD.PrintErr(ErrorPrefix + message);
    }

    private static LogLevel GetLogLevel()
    {
        if (s_logLevel is not null) return s_logLevel.Value;
        
        if (OS.IsDebugBuild())
        {
            s_logLevel = (LogLevel)ProjectSettings.GetSetting("diagnostics/logging/log_level_development").AsInt32();
        }
        else
        {
            s_logLevel = (LogLevel)ProjectSettings.GetSetting("diagnostics/logging/log_level_production").AsInt32();
        }

        return s_logLevel.Value;
    }
}