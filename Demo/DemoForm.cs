using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace com.example.Demo
{
    /// <summary>
    /// WinForms host form. A native WinForms header and status bar surround a
    /// single ElementHost (Dock = Fill) that hosts a WPF gallery of every
    /// modern control. The WPF search box raises a routed event that is handled
    /// here to update the native WinForms status bar — demonstrating
    /// cross-boundary interaction.
    /// </summary>
    public class DemoForm : Form
    {
        private ElementHost wpfHost;
        private Panel headerPanel;
        private Label headerLabel;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        public DemoForm()
        {
            this.InitializeLayout();
        }

        private void InitializeLayout()
        {
            this.Text = "com.example - WPF Modern Controls hosted in WinForms (ElementHost)";
            this.Size = new Size(1120, 840);
            this.MinimumSize = new Size(720, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F);

            // --- Native WinForms header ---
            this.headerPanel = new Panel();
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 52;
            this.headerPanel.BackColor = Color.FromArgb(37, 99, 235);

            this.headerLabel = new Label();
            this.headerLabel.Text = "WinForms Host   -   WPF Modern Controls via ElementHost";
            this.headerLabel.ForeColor = Color.White;
            this.headerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.headerLabel.Dock = DockStyle.Fill;
            this.headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            this.headerLabel.Padding = new Padding(16, 0, 0, 0);
            this.headerPanel.Controls.Add(this.headerLabel);

            // --- Native WinForms status bar ---
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();
            this.statusLabel.Text = "Ready";
            this.statusStrip.Items.Add(this.statusLabel);

            // --- WPF content hosted through ElementHost ---
            this.wpfHost = new ElementHost();
            this.wpfHost.Dock = DockStyle.Fill;
            this.wpfHost.Child = GalleryBuilder.Build(this.OnWpfSearchRequested);

            // Add the Fill control first, then the docked header and status bar.
            this.Controls.Add(this.wpfHost);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.statusStrip);
        }

        /// <summary>
        /// Handles the WPF ModernSearchBoxControl.SearchRequested event on the
        /// shared UI thread and updates the native WinForms status bar.
        /// </summary>
        private void OnWpfSearchRequested(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                this.statusLabel.Text = "Search cleared.";
            }
            else
            {
                this.statusLabel.Text = "Search requested from WPF control: \"" + term + "\"";
            }
        }
    }
}
