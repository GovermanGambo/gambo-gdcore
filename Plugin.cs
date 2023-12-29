#if TOOLS
using Gambo.GDCore.Diagnostics;
using Godot;
using Gambo.GDCore.Editor;

[Tool]
public partial class Plugin : EditorPlugin
{
	private InspectorButtonPlugin m_inspectorButtonPlugin;
	
	public override void _EnterTree()
	{
		m_inspectorButtonPlugin = new InspectorButtonPlugin();
		AddInspectorPlugin(m_inspectorButtonPlugin);
		
		if (!ProjectSettings.HasSetting("diagnostics/logging/log_level_development"))
		{
			ProjectSettings.SetSetting("diagnostics/logging/log_level_development", (int)LogLevel.Debug);
		}
		if (!ProjectSettings.HasSetting("diagnostics/logging/log_level_production"))
		{
			ProjectSettings.SetSetting("diagnostics/logging/log_level_production", (int)LogLevel.Info);
		}
	}

	public override void _ExitTree()
	{
		RemoveInspectorPlugin(m_inspectorButtonPlugin);
	}
}
#endif
