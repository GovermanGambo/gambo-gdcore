using System.Threading.Tasks;
using Godot;

namespace Gambo.GDCore.Transitions;

public partial class BlackBarsTransition : Control, ITransition
{
    private readonly ColorRect m_topBar = new() { Color = Colors.Black };
    private readonly ColorRect m_bottomBar = new() { Color = Colors.Black };

    private readonly float m_barsHeight;

    public BlackBarsTransition(float barsHeight, float duration = 0.2f)
    {
        Duration = duration;
        m_barsHeight = barsHeight;
    }

    public override void _Ready()
    {
        SetAnchorsPreset(LayoutPreset.FullRect);
        
        AddChild(m_topBar);
        AddChild(m_bottomBar);

        m_topBar.Size = new Vector2(GetViewportRect().Size.X, m_barsHeight);
        m_topBar.Position = new Vector2(0f, -m_barsHeight);
        
        m_bottomBar.Size = new Vector2(GetViewportRect().Size.X, m_barsHeight);
        m_bottomBar.Position = new Vector2(0f, GetViewportRect().Size.Y);
    }

    public float Duration { get; set; }
    
    public async Task StartTransition()
    {
        await Task.WhenAll(
            m_topBar.DoPosition(new Vector2(0f, 0f), Duration),
            m_bottomBar.DoPosition(new Vector2(0f, GetViewportRect().Size.Y - m_barsHeight), Duration));

    }

    public async Task EndTransition()
    {
        await Task.WhenAll(
            m_topBar.DoPosition(new Vector2(0f, -m_barsHeight), Duration),
            m_bottomBar.DoPosition(new Vector2(0f, GetViewportRect().Size.Y), Duration));
    }
}