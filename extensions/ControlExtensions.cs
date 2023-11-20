using System.Threading.Tasks;
using Godot;

namespace Gambo.GDCore;

public static class ControlExtensions
{
    public static async Task DoPosition(this Control control, Vector2 targetPosition, float duration, Tween.TransitionType transitionType = Tween.TransitionType.Linear)
    {
        await control.PerformTween(tween =>
        {
            tween.TweenProperty(control, "global_position", targetPosition, duration).SetTrans(transitionType);
        });
    }

    public static async Task DoScale(this Control control, Vector2 targetScale, float duration, Tween.TransitionType transitionType = Tween.TransitionType.Linear)
    {
        await control.PerformTween(tween =>
        {
            tween.TweenProperty(control, "scale", targetScale, duration).SetTrans(transitionType);
        });
    }

    public static async Task DoColor(this Control control, Color targetColor, float duration,
        Tween.TransitionType transitionType = Tween.TransitionType.Linear)
    {
        await control.PerformTween(tween =>
        {
            tween.TweenProperty(control, "modulate", targetColor, duration).SetTrans(transitionType);
        });
    }

    public static async Task DoSelfModulate(this Control control, Color targetColor, float duration,
        Tween.TransitionType transitionType = Tween.TransitionType.Linear)
    {
        await control.PerformTween(tween =>
        {
            tween.TweenProperty(control, "self_modulate", targetColor, duration).SetTrans(transitionType);
        });
    }
}