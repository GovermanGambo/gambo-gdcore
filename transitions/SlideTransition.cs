using Godot;
using System.Threading.Tasks;

namespace Gambo.GDCore.Transitions;

public partial class SlideTransition : Control, ITransition
{
    [Export] private float duration;

    private readonly ColorRect m_box = new();

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
        AddChild(m_box);
        m_box.Color = Colors.Black;
        m_box.SetAnchorsPreset(LayoutPreset.FullRect);
    }
    
    public async Task StartTransition()
    {
        float startPosition = GetViewportRect().Size.X;
        m_box.GlobalPosition = new Vector2(startPosition, 0f);

        await m_box.DoPosition(Vector2.Zero, Duration);
    }


    public async Task EndTransition()
    {
        m_box.GlobalPosition = Vector2.Zero;

        await m_box.DoPosition(new Vector2(-GetViewportRect().Size.X, 0f), Duration);
    }
}