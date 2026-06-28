using System;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// Demonstrates swapping only the buttons of an existing WinForms form to
    /// WPF <c>ModernButton</c> wrappers. The native Button and the ModernButtons
    /// share the same handler style — each fires a standard WinForms Click — so
    /// existing event-wiring code does not change when a button is replaced.
    /// </summary>
    public partial class ButtonSwapDemoForm : Form
    {
        public ButtonSwapDemoForm()
        {
            this.InitializeComponent();
        }

        private void NativeButton_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "네이티브 Button 클릭됨 (저장)";
        }

        private void ModernPrimaryButton_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "ModernButton(Primary) 클릭됨 (저장)";
        }

        private void ModernSecondaryButton_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "ModernButton(Secondary) 클릭됨 (취소)";
        }

        private void ModernDangerButton_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "ModernButton(Danger) 클릭됨 (삭제)";
        }
    }
}
