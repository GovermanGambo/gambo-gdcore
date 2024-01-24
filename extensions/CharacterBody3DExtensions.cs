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
        characterBody.Velocity = characterBody.Velocity with { X = 0f, Z = 0f };
    }

    public static Task Jump(this CharacterBody3D characterBody, float force)
    {
        characterBody.Velocity = characterBody.Velocity with { Y = -force };
        
        return Task.CompletedTask;
    }
}