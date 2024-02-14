using System.Collections.Generic;
using Godot;

namespace Gambo.GDCore;

[GlobalClass]
public partial class SerializedMethod : Resource
{
    [Export] private NodePath node;
    [Export] private StringName methodName;

    public NodePath Node => node;
    public StringName Method => methodName;
    public virtual IEnumerable<Variant> GetParameters() => new List<Variant>();
}

public static class SerializedMethodExtensions
{
    public static void Invoke(this IEnumerable<SerializedMethod> methods, Node sender)
    {
        foreach (var serializedMethod in methods)
        {
            var node = sender.GetNode(serializedMethod.Node);
            node.Call(serializedMethod.Method);
        }
    }
}