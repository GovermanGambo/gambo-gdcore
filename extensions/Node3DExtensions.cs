using System.Threading.Tasks;
using Gambo.GDCore;
using Godot;

namespace Gambo.Core;

public static class Node3DExtensions
{
    public static Task MoveTo(this Node3D node3D, Vector3 position, float duration, Tween.TransitionType transition = Tween.TransitionType.Linear)
    {
        var completion = new TaskCompletionSource();
        
        var tween = node3D.CreateTween();
        tween.TweenProperty(node3D, "global_position", position, duration).SetTrans(transition);
        tween.TweenCallback(Callable.From(() => completion.SetResult()));
        tween.Play();

        return completion.Task;
    }

    public static async Task DoJump(this Node3D node3D, Vector3 targetPosition, float jumpHeight, float duration = 1f)
    {
        float totalHeight = node3D.GlobalPosition.Y - targetPosition.Y + jumpHeight;
        var midJumpDelta = new Vector3(targetPosition.X - node3D.GlobalPosition.X, totalHeight,
            targetPosition.Z - node3D.GlobalPosition.Z);
        await node3D.PerformTween(tween =>
        {
            tween.TweenProperty(node3D, "global_position",
                node3D.GlobalPosition + midJumpDelta, duration / 2f)
                .SetTrans(Tween.TransitionType.Circ)
                .SetEase(Tween.EaseType.Out);
            tween.TweenProperty(node3D, "global_position", targetPosition, duration / 2f)
                .SetTrans(Tween.TransitionType.Circ)
                .SetEase(Tween.EaseType.In);
        });
    }

    public static async Task DoRotation(this Node3D node3D, Vector3 rotation, float duration)
    {
        await node3D.PerformTween(tween =>
        {
            tween.TweenProperty(node3D, "rotation", rotation, duration);
        });
    }

    public static async Task DoScale(this Node3D node3D, Vector3 scale, float duration)
    {
        await node3D.PerformTween(tween =>
        {
            tween.TweenProperty(node3D, "scale", scale, duration);
        });
    }
}