using System;
using System.Reflection;
using Godot;

namespace Gambo.GDCore.Editor;

internal partial class InspectorButton : HBoxContainer
{
    private Button m_button = new();
    private GodotObject m_obj;
    private MethodInfo m_methodInfo;
    
    public InspectorButton(GodotObject obj, MethodInfo fieldInfo)
    {
        m_obj = obj;
        m_methodInfo = fieldInfo;
        
        var attribute = fieldInfo.GetCustomAttribute<ExportButtonAttribute>();

        if (attribute is null)
        {
            GD.PrintErr("No InspectorButton attribute found on field!");
            return;
        }
        
        Alignment = AlignmentMode.Center;
        SizeFlagsHorizontal = SizeFlags.ExpandFill;
        
        AddChild(m_button);
        m_button.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        m_button.Text = attribute.Title;
        m_button.Modulate = attribute.Tint;
        m_button.Disabled = attribute.Disabled;
        m_button.Pressed += Button_OnPressed;
        m_button.Alignment = attribute.Alignment;
    }

    private void Button_OnPressed()
    {
        m_methodInfo.Invoke(m_obj, Array.Empty<object>());
    }
}