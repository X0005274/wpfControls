namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="WpfOnFormDemo"/>. Each WPF control lives in
    /// its own <see cref="System.Windows.Forms.Integration.ElementHost"/> docked
    /// like a native control. The hosted controls carry the same design tokens and
    /// behavior as EmployeeBrowserScreen.
    /// </summary>
    partial class WpfOnFormDemo
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Integration.ElementHost comboHost;
        private System.Windows.Forms.Integration.ElementHost searchButtonHost;
        private System.Windows.Forms.SplitContainer split;
        private System.Windows.Forms.Integration.ElementHost treeHost;
        private System.Windows.Forms.Integration.ElementHost gridHost;

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
            this.topPanel = new System.Windows.Forms.Panel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.comboHost = new System.Windows.Forms.Integration.ElementHost();
            this.searchButtonHost = new System.Windows.Forms.Integration.ElementHost();
            this.split = new System.Windows.Forms.SplitContainer();
            this.treeHost = new System.Windows.Forms.Integration.ElementHost();
            this.gridHost = new System.Windows.Forms.Integration.ElementHost();

            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();

            // topPanel — white filter bar: native Label + WPF ComboBox + WPF Button
            this.topPanel.BackColor = System.Drawing.Color.White;
            this.topPanel.Controls.Add(this.searchButtonHost);
            this.topPanel.Controls.Add(this.comboHost);
            this.topPanel.Controls.Add(this.nameLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(900, 64);
            this.topPanel.TabIndex = 0;

            // nameLabel (native, secondary-gray)
            this.nameLabel.AutoSize = true;
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.nameLabel.Location = new System.Drawing.Point(16, 24);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(31, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "이름";

            // comboHost — hosts a styled WPF ComboBox
            this.comboHost.BackColor = System.Drawing.Color.White;
            this.comboHost.Location = new System.Drawing.Point(56, 14);
            this.comboHost.Name = "comboHost";
            this.comboHost.Size = new System.Drawing.Size(260, 36);
            this.comboHost.TabIndex = 1;

            // searchButtonHost — hosts a styled WPF primary Button (조회)
            this.searchButtonHost.BackColor = System.Drawing.Color.White;
            this.searchButtonHost.Location = new System.Drawing.Point(328, 14);
            this.searchButtonHost.Name = "searchButtonHost";
            this.searchButtonHost.Size = new System.Drawing.Size(84, 36);
            this.searchButtonHost.TabIndex = 2;

            // split: left = WPF TreeView card, right = WPF DataGrid card
            this.split.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 64);
            this.split.Name = "split";
            this.split.Panel1.Controls.Add(this.treeHost);
            this.split.Panel1.Padding = new System.Windows.Forms.Padding(16, 16, 8, 16);
            this.split.Panel1MinSize = 180;
            this.split.Panel2.Controls.Add(this.gridHost);
            this.split.Panel2.Padding = new System.Windows.Forms.Padding(8, 16, 16, 16);
            this.split.Panel2MinSize = 320;
            this.split.Size = new System.Drawing.Size(900, 497);
            this.split.SplitterDistance = 248;
            this.split.SplitterWidth = 2;
            this.split.TabIndex = 1;

            // treeHost — hosts a WPF card (Border > DockPanel > TreeView)
            this.treeHost.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.treeHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeHost.Location = new System.Drawing.Point(16, 16);
            this.treeHost.Name = "treeHost";
            this.treeHost.Size = new System.Drawing.Size(224, 465);
            this.treeHost.TabIndex = 0;

            // gridHost — hosts a WPF card (Border > DockPanel > DataGrid)
            this.gridHost.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.gridHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHost.Location = new System.Drawing.Point(8, 16);
            this.gridHost.Name = "gridHost";
            this.gridHost.Size = new System.Drawing.Size(626, 465);
            this.gridHost.TabIndex = 0;

            // WpfOnFormDemo
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(900, 561);
            this.Controls.Add(this.split);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(720, 460);
            this.Name = "WpfOnFormDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WPF 컨트롤을 Form에 개별 배치 (ElementHost x N)";

            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
