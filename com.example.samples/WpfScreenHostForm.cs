using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace com.example.Demo
{
    /// <summary>
    /// Generic WinForms host that shows a WPF screen (a WPF <see cref="System.Windows.UIElement"/>)
    /// full-bleed through a single <see cref="ElementHost"/>, so the menu shell can present the
    /// WPF example screens (Payroll, Employee Management, …) alongside the WinForms sample forms.
    /// </summary>
    public class WpfScreenHostForm : Form
    {
        public WpfScreenHostForm(System.Windows.UIElement screen)
        {
            this.BackColor = Color.FromArgb(247, 248, 250);

            ElementHost host = new ElementHost();
            host.Dock = DockStyle.Fill;
            host.Child = screen;
            this.Controls.Add(host);
        }
    }
}
