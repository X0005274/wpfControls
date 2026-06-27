using System;
using System.Drawing;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// A <see cref="SplitContainer"/> that carries the theme's splitter behaviour itself,
    /// so host forms don't each have to restyle it:
    /// <list type="bullet">
    /// <item>No lingering dotted focus rectangle after the splitter is clicked/dragged
    /// (focus is dropped on mouse-up).</item>
    /// <item>A flat splitter whose gutter blends with the surrounding canvas — it reads as
    /// a clean gap instead of a coloured bar, matching the WPF theme's calm, flat look.
    /// (Same <c>Parent.BackColor</c> approach used by <see cref="CardPanel"/>.)</item>
    /// </list>
    /// Mouse-based splitter resizing works exactly as usual.
    /// </summary>
    public class FocuslessSplitContainer : SplitContainer
    {
        public FocuslessSplitContainer()
        {
            this.TabStop = false;
            this.BorderStyle = BorderStyle.None;

            // Keep the gutter repainted in the canvas colour as it is dragged.
            this.SplitterMoved += (sender, e) => this.Invalidate(this.SplitterRectangle);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // After a splitter click/drag, move focus off the splitter so its focus
            // rectangle disappears (setting ActiveControl = null blurs it cleanly).
            Form form = this.FindForm();
            if (form != null)
            {
                form.ActiveControl = null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Paint the splitter strip with the host canvas colour so the gutter always
            // matches the surrounding surface, regardless of which form hosts it. This
            // removes the need for per-form splitter colour settings.
            Color canvas = this.Parent != null ? this.Parent.BackColor : this.BackColor;
            using (SolidBrush brush = new SolidBrush(canvas))
            {
                e.Graphics.FillRectangle(brush, this.SplitterRectangle);
            }
        }
    }
}
