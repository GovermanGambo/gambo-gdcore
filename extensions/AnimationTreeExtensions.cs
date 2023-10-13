using Godot;

namespace Gambo.GDCore;

public static class AnimationTreeExtensions
{
    public static AnimationNodeStateMachinePlayback GetStateMachine(this AnimationTree animationTree)
    {
        return (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
    }
}