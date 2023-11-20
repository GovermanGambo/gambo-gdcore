using System.Threading.Tasks;
using Gambo.GDCore.Diagnostics;
using Godot;

namespace Gambo.GDCore;

public static class AudioStreamPlayerExtensions
{
    public static async Task FadeIn(this AudioStreamPlayer player, float targetVolumeDb, float duration)
    {
        if (player.Stream is null)
        {
            Log.Error("FadeIn() requires that a stream has been assigned to the AudioStreamPlayer!");
            return;
        }
        
        if (duration == 0f)
        {
            player.VolumeDb = targetVolumeDb;
            player.Play();
        }
        else
        {
            player.VolumeDb = -20;
            player.Play();
            await player.DoVolume(targetVolumeDb, duration);
        }
    }

    public static async Task FadeOut(this AudioStreamPlayer player, float duration)
    {
        if (duration > 0f)
        {
            await player.DoVolume(-20, duration);
        }
        
        player.Stop();
    }

    public static Task DoVolume(this AudioStreamPlayer player, float targetVolumeDb, float duration)
    {
        return player.PerformTween(tween =>
        {
            tween.TweenProperty(player, "volume_db", targetVolumeDb, duration);
        });
    }
}