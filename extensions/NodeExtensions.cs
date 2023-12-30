using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gambo.GDCore.Diagnostics;
using Godot;

namespace Gambo.GDCore;

public static class NodeExtensions
{
    public static TNode CreateNode<TNode>(this Node owner, PackedScene packedScene, Action<TNode> beforeEnterTree = null)
        where TNode : Node
    {
        var node = packedScene.Instantiate<TNode>();
        beforeEnterTree?.Invoke(node);
        owner.AddChild(node);
        return node;
    }
    
    public static void ReplaceWith(this Node node, Node otherNode)
    {
        var parent = node.GetParent();
        parent.RemoveChild(node);
        parent.AddChild(otherNode);
    }

    public static TNode GetChildOfType<TNode>(this Node owner) where TNode : class
    {
        foreach (var child in owner.GetChildren())
        {
            if (child is TNode node)
            {
                return node;
            }
        }

        return null;
    }
    
    public static TNode[] GetChildrenOfType<TNode>(this Node owner) where TNode : Node
    {
        var list = new List<TNode>();
        
        foreach (var child in owner.GetChildren())
        {
            if (child is TNode node)
            {
                list.Add(node);
            }
        }

        return list.ToArray();
    }

    public static TNode TryGetNode<TNode>(this Node node, NodePath nodePath, string message = null) where TNode : class
    {
        var foundNode = node.GetNode<TNode>(nodePath);
        Assert.IsNotNull(foundNode, message);

        return foundNode;
    }
    
    public static Signal TaskToSignal(this Node node, Task task, string signalName)
    {
        PerformTask(node, task, signalName);

        return new Signal(node, signalName);
    }

    private static async void PerformTask(GodotObject node, Task task, string signalName)
    {
        await task;

        node.EmitSignal(signalName);
    }
    
    public static Task PerformTween(this Node node, Action<Tween> tweenAction)
    {
        var tree = node.GetTree();
        var completion = new TaskCompletionSource();
        var tween = node.CreateTween();
        tweenAction(tween);
        tween.TweenCallback(Callable.From(() => completion.SetResult()));
        tween.Play();

        return completion.Task;
    }
}