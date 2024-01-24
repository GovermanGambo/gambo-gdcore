using System;
using System.Threading.Tasks;
using Gambo.GDCore.Diagnostics;
using Gambo.GDCore.Transitions;
using Godot;

namespace Gambo.GDCore;

public static class SceneTreeExtensions
{
    public static TNode GetNodeInGroup<TNode>(this SceneTree sceneTree, string nodeName, string groupName)
        where TNode : Node
    {
        var nodesInGroup = sceneTree.GetNodesInGroup(groupName);
        foreach (var node in nodesInGroup)
        {
            if (node.Name != nodeName) continue;

            if (node is not TNode result)
            {
                Log.Error($"Node {nodeName} in group {groupName} was not of expected type!");
                return null;
            }

            return result;
        }
        
        Log.Error($"Node {nodeName} was not found in group {groupName}!");
        return null;
    }
    
    public static async Task WaitForProcessFrame(this SceneTree sceneTree)
    {
        await sceneTree.ToSignal(sceneTree, "process_frame");
    }

    public static async Task WaitForSeconds(this SceneTree sceneTree, float duration)
    {
        var timer = sceneTree.CreateTimer(duration);
        await sceneTree.ToSignal(timer, "timeout");
    }

    public static async Task InvokeDelayed(this SceneTree sceneTree, float delaySeconds, Action action)
    {
        await sceneTree.WaitForSeconds(delaySeconds);
        action();
    }

    public static async Task ChangeSceneTo<TNode>(this SceneTree sceneTree, PackedScene packedScene, Action<TNode> beforeChange = null, PackedScene transition = null) where TNode : Node
    {
        ITransition transitionNode = null;
        if (transition is not null)
        {
            transitionNode = transition.Instantiate<ITransition>();
            sceneTree.Root.AddChild((Node)transitionNode);
            await transitionNode.StartTransition();
        }
        
        var scene = packedScene.Instantiate<TNode>();
        beforeChange?.Invoke(scene);
        var previousScene = sceneTree.CurrentScene;
        sceneTree.Root.RemoveChild(previousScene);
        previousScene.QueueFree();
        sceneTree.Root.AddChild(scene);
        sceneTree.CurrentScene = scene;

        if (transitionNode is not null)
        {
            await transitionNode.EndTransition();
            ((Node)transitionNode).QueueFree();
        }
    }
}