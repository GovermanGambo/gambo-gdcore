using System;
using Godot;

namespace Gambo.GDCore.Editor;

[AttributeUsage(AttributeTargets.Method)]
internal class ExportButtonAttribute : Attribute
{
    public ExportButtonAttribute(string title)
        : this(title, null)
    {
        
    }

    public ExportButtonAttribute(string title, string category)
        : this(title, category, Colors.White, false, HorizontalAlignment.Center)
    {
        
    }
    
    public ExportButtonAttribute(string title, string category, Color tint, bool disabled, HorizontalAlignment alignment)
    {
        Title = title;
        Tint = tint;
        Disabled = disabled;
        Alignment = alignment;
        Category = category;
    }

    public string Title { get; }
    public Color Tint { get; }
    public bool Disabled { get; }
    public HorizontalAlignment Alignment { get; }
    public string Category { get; }
}
