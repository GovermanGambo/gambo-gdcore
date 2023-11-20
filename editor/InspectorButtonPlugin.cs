using System.Linq;
using System.Reflection;
using Godot;

namespace Gambo.GDCore.Editor;

internal partial class InspectorButtonPlugin : EditorInspectorPlugin
{
    public override bool _CanHandle(GodotObject @object)
    {
        return true;
    }

    public override void _ParseCategory(GodotObject @object, string category)
    {
        var methods = @object
            .GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.GetCustomAttribute<ExportButtonAttribute>() is not null);
        foreach (var methodInfo in methods)
        {
            var attribute = methodInfo.GetCustomAttribute<ExportButtonAttribute>();
            if (attribute?.Category is null)
            {
                if (category != @object.GetType().Name)
                {
                    continue;
                }
            }
            else
            {
                if (category != attribute.Category)
                {
                    continue;
                }
            }

            AddCustomControl(new InspectorButton(@object, methodInfo));
        }
    }
}