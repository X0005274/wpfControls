using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// A white card panel with rounded corners and a 1px border, drawn on top of
    /// the parent's (gray) canvas so the corners show through. Used to give the
    /// query / execution regions the same floating-card look as the WPF data grid.
    /// </summary>
    public class CardPanel : Panel
    {
        private int cornerRadius = 8;
        private Color borderColor = Color.FromArgb(209, 213, 219);
        private Color cardColor = Color.White;

        public CardPanel()
        {
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.BackColor = Color.White;
        }

        public int CornerRadius
        {
            get { return this.cornerRadius; }
            set { this.cornerRadius = value; this.Invalidate(); }
        }

        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; this.Invalidate(); }
        }

        public Color CardColor
        {
            get { return this.cardColor; }
            set { this.cardColor = value; this.Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color canvas = this.Parent != null ? this.Parent.BackColor : this.BackColor;
            e.Graphics.Clear(canvas);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (GraphicsPath path = BuildRoundedRect(bounds, this.cornerRadius))
            {
                using (SolidBrush fill = new SolidBrush(this.cardColor))
                {
                    e.Graphics.FillPath(fill, path);
                }

                using (Pen pen = new Pen(this.borderColor, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private static GraphicsPath BuildRoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            if (d <= 0)
            {
                path.AddRectangle(r);
                return path;
            }

            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
