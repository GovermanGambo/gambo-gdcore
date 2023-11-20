#if TOOLS
using Godot;
using System;
using Gambo.GDCore.Editor;

[Tool]
public partial class Plugin : EditorPlugin
{
	private InspectorButtonPlugin m_inspectorButtonPlugin;
	
	public override void _EnterTree()
	{
		m_inspectorButtonPlugin = new InspectorButtonPlugin();
		AddInspectorPlugin(m_inspectorButtonPlugin);
	}

	public override void _ExitTree()
	{
		RemoveInspectorPlugin(m_inspectorButtonPlugin);
	}
}
#endif
