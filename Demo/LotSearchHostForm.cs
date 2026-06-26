using System.Windows.Forms;
using com.example.Controls.Wpf.Screens;

namespace com.example.Demo
{
    /// <summary>
    /// WinForms host that shows the WPF <see cref="LotSearchScreen"/> through a
    /// single ElementHost. The child is assigned in code (ElementHost.Child cannot
    /// be set in the WinForms designer for a cross-assembly WPF control).
    /// </summary>
    public partial class LotSearchHostForm : Form
    {
        public LotSearchHostForm()
        {
            this.InitializeComponent();
            this.screenHost.Child = new LotSearchScreen();
        }
    }
}
