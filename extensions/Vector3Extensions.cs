using Godot;

namespace Gambo.GDCore;

public static class Vector3Extensions
{
    public static Vector3 SmoothStep(this Vector3 startValue, Vector3 endValue, float weight)
    {
        float x = Mathf.SmoothStep(startValue.X, endValue.X, weight);
        float y = Mathf.SmoothStep(startValue.Y, endValue.Y, weight);
        float z = Mathf.SmoothStep(startValue.Z, endValue.Z, weight);

        return new Vector3(x, y, z);
    }
}