namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="SampleShellForm"/>: a thin accent strip on top,
    /// a left navigation sidebar (header + generated nav buttons), and a content host
    /// that embeds the selected sample screen.
    /// </summary>
    partial class SampleShellForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel sidebarDivider;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.Label headerSubLabel;
        private System.Windows.Forms.FlowLayoutPanel navPanel;
        private System.Windows.Forms.Panel contentPanel;

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
            this.accentStrip = new System.Windows.Forms.Panel();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.navPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.headerSubLabel = new System.Windows.Forms.Label();
            this.headerLabel = new System.Windows.Forms.Label();
            this.sidebarDivider = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();

            // ===== contentPanel (가운데 — 선택된 샘플 화면이 임베드됨) =====
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.TabIndex = 2;

            // ===== sidebarPanel (좌측 내비게이션) =====
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.BackColor = System.Drawing.Color.White;
            this.sidebarPanel.Controls.Add(this.navPanel);
            this.sidebarPanel.Controls.Add(this.headerSubLabel);
            this.sidebarPanel.Controls.Add(this.headerLabel);
            this.sidebarPanel.Controls.Add(this.sidebarDivider);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(240, 897);
            this.sidebarPanel.TabIndex = 1;

            // navPanel — generated nav buttons stack here (top-down)
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.navPanel.WrapContents = false;
            this.navPanel.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.navPanel.Name = "navPanel";
            this.navPanel.TabIndex = 2;

            // headerSubLabel — caption under the title
            this.headerSubLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerSubLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.headerSubLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.headerSubLabel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 12);
            this.headerSubLabel.Name = "headerSubLabel";
            this.headerSubLabel.Size = new System.Drawing.Size(240, 30);
            this.headerSubLabel.TabIndex = 1;
            this.headerSubLabel.Text = "Pick a screen";

            // headerLabel — sidebar title
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.headerLabel.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(240, 52);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Sample Gallery";

            // sidebarDivider — 1px separator on the sidebar's right edge
            this.sidebarDivider.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidebarDivider.BackColor = System.Drawing.Color.FromArgb(209, 213, 219);
            this.sidebarDivider.Name = "sidebarDivider";
            this.sidebarDivider.Size = new System.Drawing.Size(1, 897);
            this.sidebarDivider.TabIndex = 3;

            // ===== accentStrip (very top) =====
            this.accentStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.accentStrip.Name = "accentStrip";
            this.accentStrip.Size = new System.Drawing.Size(1600, 3);
            this.accentStrip.TabIndex = 0;

            // ===== SampleShellForm =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Controls.Add(this.accentStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 640);
            this.Name = "SampleShellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "com.example — Sample Gallery";
            this.sidebarPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
