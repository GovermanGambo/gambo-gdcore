using Gambo.GDCore.Diagnostics;
using Godot;

namespace Gambo.GDCore;

public static class NodeExtensions
{
    public static TNode TryGetNode<TNode>(this Node node, NodePath nodePath, string message = null) where TNode : class
    {
        var foundNode = node.GetNode<TNode>(nodePath);
        Assert.IsNotNull(foundNode, message);

        return foundNode;
    }
}