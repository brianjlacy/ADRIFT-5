using Microsoft.Maui.Graphics;
using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Controls;

/// <summary>
/// Custom drawable for rendering the adventure map
/// </summary>
public class MapDrawable : IDrawable
{
    public MapViewModel? ViewModel { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (ViewModel == null) return;

        canvas.SaveState();

        // Apply zoom and pan transformations
        canvas.Translate((float)ViewModel.PanX, (float)ViewModel.PanY);
        canvas.Scale((float)ViewModel.ZoomLevel, (float)ViewModel.ZoomLevel);

        // Draw connection lines first (underneath location boxes)
        DrawConnections(canvas);

        // Draw location boxes on top
        DrawLocations(canvas);

        canvas.RestoreState();
    }

    private void DrawConnections(ICanvas canvas)
    {
        if (ViewModel == null) return;

        foreach (var line in ViewModel.ConnectionLines)
        {
            // Set line color based on restriction
            canvas.StrokeColor = line.HasRestriction ? Colors.Red : Colors.Gray;
            canvas.StrokeSize = line.HasRestriction ? 2 : 1;

            // Draw arrow line from source to target
            var sx = (float)line.SourceX;
            var sy = (float)line.SourceY;
            var tx = (float)line.TargetX;
            var ty = (float)line.TargetY;

            canvas.DrawLine(sx, sy, tx, ty);

            // Draw arrowhead at target
            DrawArrowhead(canvas, sx, sy, tx, ty);

            // Draw direction label midway
            var midX = (sx + tx) / 2;
            var midY = (sy + ty) / 2;
            canvas.FontColor = Colors.DarkGray;
            canvas.FontSize = 10;
            canvas.DrawString(line.Direction, midX, midY, HorizontalAlignment.Center);
        }
    }

    private void DrawArrowhead(ICanvas canvas, float sx, float sy, float tx, float ty)
    {
        // Calculate angle of line
        var angle = Math.Atan2(ty - sy, tx - sx);
        var arrowSize = 8f;

        // Calculate arrowhead points
        var angle1 = angle + Math.PI * 0.85;
        var angle2 = angle - Math.PI * 0.85;

        var ax1 = tx + arrowSize * (float)Math.Cos(angle1);
        var ay1 = ty + arrowSize * (float)Math.Sin(angle1);
        var ax2 = tx + arrowSize * (float)Math.Cos(angle2);
        var ay2 = ty + arrowSize * (float)Math.Sin(angle2);

        // Draw arrowhead
        var path = new PathF();
        path.MoveTo(tx, ty);
        path.LineTo(ax1, ay1);
        path.MoveTo(tx, ty);
        path.LineTo(ax2, ay2);
        canvas.DrawPath(path);
    }

    private void DrawLocations(ICanvas canvas)
    {
        if (ViewModel == null) return;

        const float boxWidth = 80f;
        const float boxHeight = 40f;

        foreach (var node in ViewModel.LocationNodes)
        {
            var x = (float)node.X;
            var y = (float)node.Y;

            // Determine colors based on state
            Color fillColor = node.IsSelected ? Colors.Gold :
                             node.IsHidden ? Colors.LightGray :
                             Colors.LightBlue;

            Color strokeColor = node.IsSelected ? Colors.DarkOrange : Colors.Black;

            // Draw location box
            canvas.FillColor = fillColor;
            canvas.StrokeColor = strokeColor;
            canvas.StrokeSize = node.IsSelected ? 3 : 1;

            canvas.FillRoundedRectangle(x, y, boxWidth, boxHeight, 5);
            canvas.DrawRoundedRectangle(x, y, boxWidth, boxHeight, 5);

            // Draw location name
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 12;
            var textX = x + boxWidth / 2;
            var textY = y + boxHeight / 2;
            canvas.DrawString(node.Name, textX, textY, HorizontalAlignment.Center);

            // Draw exit count badge if > 0
            if (node.ExitCount > 0)
            {
                var badgeX = x + boxWidth - 15;
                var badgeY = y - 5;
                canvas.FillColor = Colors.DarkBlue;
                canvas.FillCircle(badgeX, badgeY, 10);
                canvas.FontColor = Colors.White;
                canvas.FontSize = 10;
                canvas.DrawString(node.ExitCount.ToString(), badgeX, badgeY, HorizontalAlignment.Center);
            }
        }
    }
}
