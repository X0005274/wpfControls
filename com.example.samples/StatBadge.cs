using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// A small rounded "pill" that mirrors the data-grid badge tones
    /// (success / neutral / danger), so a status summary reads with the same color
    /// language as the Lot State badges in the grid. The fill/text colors come from
    /// the same token values used by <c>ModernBadgeControl</c>. Reusable on any screen.
    /// </summary>
    public class StatBadge : Control
    {
        private Color fillColor = Color.FromArgb(243, 244, 246);
        private Color textColor = Color.FromArgb(55, 65, 81);
        private int cornerRadius = 11;

        public StatBadge()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            this.Size = new Size(118, 26);
        }

        /// <summary>
        /// Sets the pill color from a tone key matching the grid badge tones:
        /// "success" (green), "danger"/"error" (red), anything else → neutral (gray).
        /// </summary>
        public void SetTone(string tone)
        {
            string key = (tone ?? string.Empty).Trim().ToLowerInvariant();
            if (key == "success")
            {
                // Brush.SuccessBackground / Brush.SuccessText
                this.fillColor = Color.FromArgb(236, 253, 243);
                this.textColor = Color.FromArgb(21, 128, 61);
            }
            else if (key == "danger" || key == "error")
            {
                // Brush.ErrorBackground / Brush.ErrorText
                this.fillColor = Color.FromArgb(254, 242, 242);
                this.textColor = Color.FromArgb(185, 28, 28);
            }
            else
            {
                // Brush.NeutralBackground / Brush.NeutralText
                this.fillColor = Color.FromArgb(243, 244, 246);
                this.textColor = Color.FromArgb(55, 65, 81);
            }

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color canvas = this.Parent != null ? this.Parent.BackColor : this.BackColor;
            e.Graphics.Clear(canvas);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (GraphicsPath path = BuildPill(bounds, this.cornerRadius))
            {
                using (SolidBrush fill = new SolidBrush(this.fillColor))
                {
                    e.Graphics.FillPath(fill, path);
                }
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, bounds, this.textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
        }

        private static GraphicsPath BuildPill(Rectangle r, int radius)
        {
            int d = radius * 2;
            if (d > r.Height)
            {
                d = r.Height;
            }

            GraphicsPath path = new GraphicsPath();
            if (d <= 0)
            {
                path.AddRectangle(r);
                return path;
            }

            path.AddArc(r.X, r.Y, d, d, 90, 180);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 180);
            path.CloseFigure();
            return path;
        }
    }
}
