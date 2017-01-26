using System.Drawing;
using Rhino;
using Rhino.Commands;
using Rhino.Display;

namespace CompositionRulers
{
	public class CompositionGuidesCenterDiagonal : Command
	{
		public CompositionGuidesCenterDiagonal()
		{
			// Rhino only creates one instance of each command class defined in a
			// plug-in, so it is safe to store a refence in a static property.
			Instance = this;
		}

		///<summary>The only instance of this command.</summary>
		public static CompositionGuidesCenterDiagonal Instance
		{
			get; private set;
		}

		///<returns>The command name as it appears on the Rhino command line.</returns>
		public override string EnglishName => "CompositionGuidesCenterDiagonal";

		protected override Result RunCommand(RhinoDoc doc, RunMode mode)
		{
			CompositionRulersPlugIn.centerdiagonal.Enabled = !CompositionRulersPlugIn.centerdiagonal.Enabled;
			RhinoApp.WriteLine($"centerdiagonal: {CompositionRulersPlugIn.centerdiagonal.Enabled}");
			doc.Views.Redraw();
			return Result.Success;
		}
	}

	public class CenterDiagonal : DisplayConduit
	{
		protected override void DrawOverlay(DrawEventArgs ev)
		{
			if (ev.Viewport.Id != RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewportID) return;
			var rect = ev.Viewport.Bounds;
			ev.Display.Draw2dLine(new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Bottom), Color.MediumAquamarine, 0.5f);
			ev.Display.Draw2dLine(new Point(rect.Left, rect.Bottom), new Point(rect.Right, rect.Top), Color.MediumAquamarine, 0.5f);
		}
	}
}
