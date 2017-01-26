using System.Drawing;
using Rhino;
using Rhino.Commands;
using Rhino.Display;

namespace CompositionRulers
{
	public class CompositionGuidesCenter : Command
	{
		public CompositionGuidesCenter()
		{
			// Rhino only creates one instance of each command class defined in a
			// plug-in, so it is safe to store a refence in a static property.
			Instance = this;
		}

		///<summary>The only instance of this command.</summary>
		public static CompositionGuidesCenter Instance
		{
			get; private set;
		}

		///<returns>The command name as it appears on the Rhino command line.</returns>
		public override string EnglishName => "CompositionGuidesCenter";

		protected override Result RunCommand(RhinoDoc doc, RunMode mode)
		{
			CompositionRulersPlugIn.center.Enabled = !CompositionRulersPlugIn.center.Enabled;
			RhinoApp.WriteLine($"center: {CompositionRulersPlugIn.center.Enabled}");
			doc.Views.Redraw();
			return Result.Success;
		}
	}

	public class Center : DisplayConduit
	{
		protected override void DrawOverlay(DrawEventArgs ev)
		{
			if (ev.Viewport.Id != RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewportID) return;
			var rect = ev.Viewport.Bounds;
			var hor3rd = rect.Width / 2;
			var ver3rd = rect.Height / 2;
			int[] dirs = { hor3rd, ver3rd };
			for (int j = 0; j < 2; j++)
			{
				var hor = j == 0;
				var aa = dirs[j];
				var a = hor ? rect.Top : rect.Left;
				var b = hor ? rect.Bottom : rect.Right;
				var ptStart = new Point(hor ? aa : a, hor ? a : aa);
				var ptEnd = new Point(hor ? aa : b, hor ? b : aa);
				ev.Display.Draw2dLine(hor ? ptStart : ptEnd, hor ? ptEnd : ptStart, Color.MediumAquamarine, 0.5f);
			}
		}
	}
}
