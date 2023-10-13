using System.Threading.Tasks;
using Godot;

namespace Gambo.GDCore;

public static class CanvasItemExtensions
{
    public static async Task FadeIn(this CanvasItem canvasItem, float duration = 0.2f)
    {
        var modulate = canvasItem.Modulate;
        canvasItem.Modulate = new Color(modulate.R, modulate.G, modulate.B, 0f);
        canvasItem.Visible = true;
        await canvasItem.PerformTween(tween =>
        {
            tween.TweenProperty(canvasItem, "modulate", modulate with { A = 1.0f }, duration);
        });
    }

    public static async Task FadeOut(this CanvasItem canvasItem, float duration = 0.2f)
    {
        var modulate = canvasItem.Modulate;
        canvasItem.Modulate = new Color(modulate.R, modulate.G, modulate.B);
        await canvasItem.PerformTween(tween =>
        {
            tween.TweenProperty(canvasItem, "modulate", modulate with { A = 0.0f }, duration);
        });

        canvasItem.Visible = false;
    }
}