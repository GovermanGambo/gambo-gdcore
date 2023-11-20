using System.Threading;
using System.Threading.Tasks;
using Gambo.GDCore;
using Godot;

namespace Gambo.Core;

public static class Node3DExtensions
{
    public static Task MoveTo(this Node3D node3D, Vector3 position, float duration, Tween.TransitionType transition = Tween.TransitionType.Linear, Tween.EaseType ease = Tween.EaseType.InOut)
    {
        var completion = new TaskCompletionSource();
        
        var tween = node3D.CreateTween();
        tween.TweenProperty(node3D, "global_position", position, duration).SetTrans(transition).SetEase(ease);
        tween.TweenCallback(Callable.From(() => completion.SetResult()));
        tween.Play();

        return completion.Task;
    }

    public static async Task DoJump(this Node3D node3D, Vector3 targetPosition, float height, float duration, CancellationToken cancellationToken = new())
    {
        var midJumpPosition = node3D.GlobalPosition + (targetPosition - node3D.GlobalPosition) / 2 + Vector3.Up * height;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            
            var m1 = node3D.GlobalPosition.Lerp(midJumpPosition, elapsed / duration);
            var m2 = midJumpPosition.Lerp(targetPosition, elapsed / duration );
            node3D.GlobalPosition = m1.Lerp(m2, elapsed / duration);
            await node3D.GetTree().WaitForProcessFrame();
            elapsed += (float)node3D.GetProcessDeltaTime();
        }
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