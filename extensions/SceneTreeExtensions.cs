using System;
using System.Threading.Tasks;
using Gambo.GDCore.Transitions;
using Godot;

namespace Gambo.GDCore;

public static class SceneTreeExtensions
{
    public static async Task WaitForProcessFrame(this SceneTree sceneTree)
    {
        await sceneTree.ToSignal(sceneTree, "process_frame");
    }

    public static async Task WaitForSeconds(this SceneTree sceneTree, float duration)
    {
        var timer = sceneTree.CreateTimer(duration);
        await sceneTree.ToSignal(timer, "timeout");
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