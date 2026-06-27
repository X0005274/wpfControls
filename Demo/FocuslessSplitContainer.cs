using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// A <see cref="SplitContainer"/> that does not keep a dotted focus rectangle after
    /// the splitter is clicked or dragged: it drops focus on mouse-up so the focus cue
    /// clears immediately. Mouse-based splitter resizing works exactly as usual.
    /// </summary>
    public class FocuslessSplitContainer : SplitContainer
    {
        public FocuslessSplitContainer()
        {
            this.TabStop = false;
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
    }
}
