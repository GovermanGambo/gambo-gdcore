using Godot;
using System.Threading.Tasks;

namespace Gambo.GDCore.Transitions;

public partial class SlideTransition : ColorRect, ITransition
{
    [Export] private float duration;
    
    public SlideTransition(float duration = 0.2f)
    {
        this.duration = duration;
    }
    
    public float Duration
    {
        get => duration;
        set => duration = value;
    }

    public override void _EnterTree()
    {
        Color = Colors.Black;
        ZIndex = 127;
        SetAnchorsPreset(LayoutPreset.FullRect);
        Size = GetViewportRect().Size;
    }
    
    public async Task StartTransition()
    {
        
        float startPosition = GetViewportRect().Size.X;
        GlobalPosition = new Vector2(startPosition, 0f);

        await this.DoPosition(Vector2.Zero, Duration);
    }


    public async Task EndTransition()
    {
        GlobalPosition = Vector2.Zero;

        await this.DoPosition(new Vector2(-GetViewportRect().Size.X, 0f), Duration);
    }
}