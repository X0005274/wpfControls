namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="LotSearchHostForm"/>. A single
    /// <see cref="System.Windows.Forms.Integration.ElementHost"/> fills the form and
    /// hosts the whole <c>LotSearchScreen</c> WPF UserControl, so the result looks
    /// identical to the other pure-WPF screens.
    /// </summary>
    partial class LotSearchHostForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Integration.ElementHost screenHost;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.screenHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();

            // screenHost — fills the form, hosts the WPF LotSearchScreen
            this.screenHost.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.screenHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenHost.Location = new System.Drawing.Point(0, 0);
            this.screenHost.Name = "screenHost";
            this.screenHost.Size = new System.Drawing.Size(1040, 680);
            this.screenHost.TabIndex = 0;

            // LotSearchHostForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(1040, 680);
            this.Controls.Add(this.screenHost);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(820, 520);
            this.Name = "LotSearchHostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot Search (WPF)";

            this.ResumeLayout(false);
        }
    }
}
