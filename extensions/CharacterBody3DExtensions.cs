using System.Threading.Tasks;
using Godot;

namespace Gambo.GDCore;

public static class CharacterBody3DExtensions
{
    public static async Task MoveTo(this CharacterBody3D characterBody, Vector3 targetPosition, float speed = 1f)
    {
        var delta = (targetPosition - characterBody.GlobalPosition);

        characterBody.Velocity = delta.Normalized() * speed;

        await characterBody.PerformTween(tween =>
        {
            tween.TweenProperty(characterBody, "global_position", targetPosition, delta.Length() / speed);
        });
        
        characterBody.GlobalPosition = targetPosition;
        characterBody.Velocity = Vector3.Zero;
    }
}