namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="LotReceiveForm"/>. Three regions separated by
    /// color bands and dividers (no region titles): a white query toolbar on top, a
    /// white grid card floating on a gray canvas in the center, and a white action
    /// footer at the bottom. A thin accent strip runs along the very top. Search is a
    /// Secondary button; Receive Lot is a Primary button.
    /// </summary>
    partial class LotReceiveForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;

        private System.Windows.Forms.Panel queryPanel;
        private com.example.WinForms.Controls.Input.ModernTextBox lotIdBox;
        private com.example.WinForms.Controls.Input.ModernButton searchButton;
        private com.example.WinForms.Controls.Selection.ModernComboBox stateCombo;
        private System.Windows.Forms.Panel querySeparator;

        private System.Windows.Forms.Panel gridPanel;
        private com.example.WinForms.Controls.Data.ModernDataGrid lotGrid;

        private System.Windows.Forms.Panel execPanel;
        private System.Windows.Forms.Panel execSeparator;
        private System.Windows.Forms.Label statusLabel;
        private com.example.WinForms.Controls.Input.ModernButton receiveButton;

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
            this.queryPanel = new System.Windows.Forms.Panel();
            this.lotIdBox = new com.example.WinForms.Controls.Input.ModernTextBox();
            this.searchButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.stateCombo = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.querySeparator = new System.Windows.Forms.Panel();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.lotGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.execPanel = new System.Windows.Forms.Panel();
            this.execSeparator = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.receiveButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.queryPanel.SuspendLayout();
            this.gridPanel.SuspendLayout();
            this.execPanel.SuspendLayout();
            this.SuspendLayout();

            // ===== queryPanel (top toolbar) =====
            this.queryPanel.BackColor = System.Drawing.Color.White;
            this.queryPanel.Controls.Add(this.searchButton);
            this.queryPanel.Controls.Add(this.stateCombo);
            this.queryPanel.Controls.Add(this.lotIdBox);
            this.queryPanel.Controls.Add(this.querySeparator);
            this.queryPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.queryPanel.Location = new System.Drawing.Point(0, 3);
            this.queryPanel.Name = "queryPanel";
            this.queryPanel.Size = new System.Drawing.Size(840, 104);
            this.queryPanel.TabIndex = 1;

            // lotIdBox (ModernTextBox) — "Lot ID" is the field label, not a region title
            this.lotIdBox.BackColor = System.Drawing.Color.White;
            this.lotIdBox.Location = new System.Drawing.Point(20, 16);
            this.lotIdBox.Name = "lotIdBox";
            this.lotIdBox.PlaceholderText = "Enter Lot ID";
            this.lotIdBox.Size = new System.Drawing.Size(280, 72);
            this.lotIdBox.TabIndex = 0;
            this.lotIdBox.Title = "Lot ID";

            // stateCombo (Lot State filter)
            this.stateCombo.BackColor = System.Drawing.Color.White;
            this.stateCombo.Location = new System.Drawing.Point(316, 16);
            this.stateCombo.Name = "stateCombo";
            this.stateCombo.Size = new System.Drawing.Size(200, 72);
            this.stateCombo.TabIndex = 1;
            this.stateCombo.Title = "Lot State";

            // searchButton (Secondary)
            this.searchButton.BackColor = System.Drawing.Color.White;
            this.searchButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.searchButton.Location = new System.Drawing.Point(532, 44);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(96, 40);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);

            // querySeparator
            this.querySeparator.BackColor = System.Drawing.Color.FromArgb(226, 229, 233);
            this.querySeparator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.querySeparator.Location = new System.Drawing.Point(0, 103);
            this.querySeparator.Name = "querySeparator";
            this.querySeparator.Size = new System.Drawing.Size(840, 1);
            this.querySeparator.TabIndex = 2;

            // ===== gridPanel (center, gray canvas with floating grid card) =====
            this.gridPanel.BackColor = System.Drawing.Color.FromArgb(236, 238, 241);
            this.gridPanel.Controls.Add(this.lotGrid);
            this.gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPanel.Location = new System.Drawing.Point(0, 107);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Padding = new System.Windows.Forms.Padding(20);
            this.gridPanel.Size = new System.Drawing.Size(840, 418);
            this.gridPanel.TabIndex = 2;

            // lotGrid (ModernDataGrid) — renders as a white rounded card
            this.lotGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lotGrid.IsReadOnly = true;
            this.lotGrid.Location = new System.Drawing.Point(20, 20);
            this.lotGrid.Name = "lotGrid";
            this.lotGrid.Size = new System.Drawing.Size(800, 378);
            this.lotGrid.TabIndex = 0;

            // ===== execPanel (bottom action footer) =====
            this.execPanel.BackColor = System.Drawing.Color.White;
            this.execPanel.Controls.Add(this.receiveButton);
            this.execPanel.Controls.Add(this.statusLabel);
            this.execPanel.Controls.Add(this.execSeparator);
            this.execPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.execPanel.Location = new System.Drawing.Point(0, 525);
            this.execPanel.Name = "execPanel";
            this.execPanel.Size = new System.Drawing.Size(840, 72);
            this.execPanel.TabIndex = 3;

            // execSeparator
            this.execSeparator.BackColor = System.Drawing.Color.FromArgb(226, 229, 233);
            this.execSeparator.Dock = System.Windows.Forms.DockStyle.Top;
            this.execSeparator.Location = new System.Drawing.Point(0, 0);
            this.execSeparator.Name = "execSeparator";
            this.execSeparator.Size = new System.Drawing.Size(840, 1);
            this.execSeparator.TabIndex = 0;

            // statusLabel (row count) — secondary meta text shown in italic
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.statusLabel.Location = new System.Drawing.Point(20, 28);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 15);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "0 lots";

            // receiveButton (Primary, right-aligned)
            this.receiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.receiveButton.BackColor = System.Drawing.Color.White;
            this.receiveButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.receiveButton.Location = new System.Drawing.Point(670, 14);
            this.receiveButton.Name = "receiveButton";
            this.receiveButton.Size = new System.Drawing.Size(150, 44);
            this.receiveButton.TabIndex = 2;
            this.receiveButton.Text = "Receive Lot";
            this.receiveButton.Click += new System.EventHandler(this.ReceiveButton_Click);

            // ===== accentStrip (very top) =====
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.accentStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.accentStrip.Location = new System.Drawing.Point(0, 0);
            this.accentStrip.Name = "accentStrip";
            this.accentStrip.Size = new System.Drawing.Size(840, 3);
            this.accentStrip.TabIndex = 0;

            // ===== LotReceiveForm =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(236, 238, 241);
            this.ClientSize = new System.Drawing.Size(840, 597);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.execPanel);
            this.Controls.Add(this.queryPanel);
            this.Controls.Add(this.accentStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(720, 520);
            this.Name = "LotReceiveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot Receive";
            this.queryPanel.ResumeLayout(false);
            this.gridPanel.ResumeLayout(false);
            this.execPanel.ResumeLayout(false);
            this.execPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
