namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="WpfOnFormDemo"/>. Demonstrates placing WPF
    /// controls individually on a WinForms Form: each WPF control lives in its own
    /// <see cref="System.Windows.Forms.Integration.ElementHost"/> docked like any
    /// native control, mixed with native WinForms Label/Button.
    /// </summary>
    partial class WpfOnFormDemo
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Integration.ElementHost comboHost;
        private System.Windows.Forms.Button searchButton;
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
            this.searchButton = new System.Windows.Forms.Button();
            this.split = new System.Windows.Forms.SplitContainer();
            this.treeHost = new System.Windows.Forms.Integration.ElementHost();
            this.gridHost = new System.Windows.Forms.Integration.ElementHost();

            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();

            // topPanel (native) holds: native Label + WPF ComboBox (ElementHost) + native Button
            this.topPanel.Controls.Add(this.searchButton);
            this.topPanel.Controls.Add(this.comboHost);
            this.topPanel.Controls.Add(this.nameLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(8);
            this.topPanel.Size = new System.Drawing.Size(900, 56);
            this.topPanel.TabIndex = 0;

            // nameLabel (native WinForms)
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 20);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(31, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "이름";

            // comboHost — hosts a WPF ComboBox
            this.comboHost.Location = new System.Drawing.Point(52, 14);
            this.comboHost.Name = "comboHost";
            this.comboHost.Size = new System.Drawing.Size(240, 28);
            this.comboHost.TabIndex = 1;

            // searchButton (native WinForms)
            this.searchButton.Location = new System.Drawing.Point(304, 13);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(80, 28);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "조회";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);

            // split: left = WPF TreeView host, right = WPF DataGrid host
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 56);
            this.split.Name = "split";
            this.split.Panel1.Controls.Add(this.treeHost);
            this.split.Panel1MinSize = 160;
            this.split.Panel2.Controls.Add(this.gridHost);
            this.split.Panel2MinSize = 320;
            this.split.Size = new System.Drawing.Size(900, 505);
            this.split.SplitterDistance = 240;
            this.split.TabIndex = 1;

            // treeHost — hosts a WPF TreeView
            this.treeHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeHost.Location = new System.Drawing.Point(0, 0);
            this.treeHost.Name = "treeHost";
            this.treeHost.Size = new System.Drawing.Size(240, 505);
            this.treeHost.TabIndex = 0;

            // gridHost — hosts a WPF DataGrid
            this.gridHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHost.Location = new System.Drawing.Point(0, 0);
            this.gridHost.Name = "gridHost";
            this.gridHost.Size = new System.Drawing.Size(656, 505);
            this.gridHost.TabIndex = 0;

            // WpfOnFormDemo
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 561);
            this.Controls.Add(this.split);
            this.Controls.Add(this.topPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(640, 400);
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
