namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="LotReceiveForm"/>. Three floating cards on a
    /// gray canvas: a query card (Fab Id / Lot ID / Lot State / Search), the data
    /// grid card in the middle, and an execution card (status + Receive Lot). A thin
    /// accent strip runs along the very top.
    /// </summary>
    partial class LotReceiveForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;

        private CardPanel queryCard;
        private com.example.WinForms.Controls.Selection.ModernComboBox fabCombo;
        private com.example.WinForms.Controls.Input.ModernTextBox lotIdBox;
        private com.example.WinForms.Controls.Selection.ModernMultiSelectComboBox stateCombo;
        private com.example.WinForms.Controls.Input.ModernButton searchButton;

        private com.example.WinForms.Controls.Data.ModernDataGrid lotGrid;

        private CardPanel execCard;
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
            this.queryCard = new CardPanel();
            this.fabCombo = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.lotIdBox = new com.example.WinForms.Controls.Input.ModernTextBox();
            this.stateCombo = new com.example.WinForms.Controls.Selection.ModernMultiSelectComboBox();
            this.searchButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.lotGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.execCard = new CardPanel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.receiveButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.queryCard.SuspendLayout();
            this.execCard.SuspendLayout();
            this.SuspendLayout();

            // ===== accentStrip (very top) =====
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.accentStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.accentStrip.Location = new System.Drawing.Point(0, 0);
            this.accentStrip.Name = "accentStrip";
            this.accentStrip.Size = new System.Drawing.Size(1920, 3);
            this.accentStrip.TabIndex = 0;

            // ===== queryCard (조회) =====
            this.queryCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.queryCard.BackColor = System.Drawing.Color.White;
            this.queryCard.Controls.Add(this.searchButton);
            this.queryCard.Controls.Add(this.stateCombo);
            this.queryCard.Controls.Add(this.lotIdBox);
            this.queryCard.Controls.Add(this.fabCombo);
            this.queryCard.Location = new System.Drawing.Point(16, 16);
            this.queryCard.Name = "queryCard";
            this.queryCard.Size = new System.Drawing.Size(1888, 104);
            this.queryCard.TabIndex = 1;

            // fabCombo (Fab Id, required)
            this.fabCombo.BackColor = System.Drawing.Color.White;
            this.fabCombo.IsRequired = true;
            this.fabCombo.Location = new System.Drawing.Point(20, 16);
            this.fabCombo.Name = "fabCombo";
            this.fabCombo.Size = new System.Drawing.Size(140, 72);
            this.fabCombo.TabIndex = 0;
            this.fabCombo.Title = "Fab Id";

            // lotIdBox
            this.lotIdBox.BackColor = System.Drawing.Color.White;
            this.lotIdBox.Location = new System.Drawing.Point(176, 16);
            this.lotIdBox.Name = "lotIdBox";
            this.lotIdBox.PlaceholderText = "Enter Lot ID";
            this.lotIdBox.Size = new System.Drawing.Size(280, 72);
            this.lotIdBox.TabIndex = 1;
            this.lotIdBox.Title = "Lot ID";

            // stateCombo (Lot State multi-select filter)
            this.stateCombo.BackColor = System.Drawing.Color.White;
            this.stateCombo.Location = new System.Drawing.Point(472, 16);
            this.stateCombo.Name = "stateCombo";
            this.stateCombo.PlaceholderText = "All states";
            this.stateCombo.Size = new System.Drawing.Size(200, 72);
            this.stateCombo.TabIndex = 2;
            this.stateCombo.Title = "Lot State";

            // searchButton (Secondary, search icon)
            this.searchButton.BackColor = System.Drawing.Color.White;
            this.searchButton.IconGlyph = "\uE721";
            this.searchButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.searchButton.Location = new System.Drawing.Point(688, 44);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(104, 40);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);

            // ===== lotGrid (그리드 카드) =====
            this.lotGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lotGrid.BackColor = System.Drawing.Color.FromArgb(225, 229, 235);
            this.lotGrid.IsReadOnly = true;
            this.lotGrid.Location = new System.Drawing.Point(16, 136);
            this.lotGrid.Name = "lotGrid";
            this.lotGrid.Size = new System.Drawing.Size(1888, 520);
            this.lotGrid.TabIndex = 2;

            // ===== execCard (실행) =====
            this.execCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.execCard.BackColor = System.Drawing.Color.White;
            this.execCard.Controls.Add(this.receiveButton);
            this.execCard.Controls.Add(this.statusLabel);
            this.execCard.Location = new System.Drawing.Point(16, 672);
            this.execCard.Name = "execCard";
            this.execCard.Size = new System.Drawing.Size(1888, 80);
            this.execCard.TabIndex = 3;

            // statusLabel (row count) — secondary meta text in italic
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.statusLabel.Location = new System.Drawing.Point(20, 32);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 15);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "0 lots";

            // receiveButton (Primary, right-aligned, receive icon)
            this.receiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.receiveButton.BackColor = System.Drawing.Color.White;
            this.receiveButton.IconGlyph = "\uE896";
            this.receiveButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.receiveButton.Location = new System.Drawing.Point(1718, 18);
            this.receiveButton.Name = "receiveButton";
            this.receiveButton.Size = new System.Drawing.Size(150, 44);
            this.receiveButton.TabIndex = 1;
            this.receiveButton.Text = "Receive Lot";
            this.receiveButton.Click += new System.EventHandler(this.ReceiveButton_Click);

            // ===== LotReceiveForm =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(225, 229, 235);
            this.ClientSize = new System.Drawing.Size(1920, 768);
            this.Controls.Add(this.lotGrid);
            this.Controls.Add(this.execCard);
            this.Controls.Add(this.queryCard);
            this.Controls.Add(this.accentStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "LotReceiveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot Receive";
            this.queryCard.ResumeLayout(false);
            this.execCard.ResumeLayout(false);
            this.execCard.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
