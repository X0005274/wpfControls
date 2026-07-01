namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="TrayInspectionForm"/>. A thin accent strip on top,
    /// an emphasized title, the tray grid (common <c>ModernDataGrid</c>) filling the
    /// middle, and a card-style footer (Rows count + Judgement badges) with Save / Close
    /// buttons. Columns are configured in code (see the .cs); the designer holds the
    /// controls and their placement so the form opens visually in the VS designer.
    /// </summary>
    partial class TrayInspectionForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;

        private com.example.WinForms.Controls.Data.ModernDataGrid inspectionGrid;

        private CardPanel countCard;
        private System.Windows.Forms.Label countCaptionLabel;
        private System.Windows.Forms.Label countValueLabel;

        private CardPanel judgeCard;
        private System.Windows.Forms.Label judgeCaptionLabel;
        private StatBadge succBadge;
        private StatBadge failBadge;
        private StatBadge pendingBadge;

        private CardPanel fillerCard;

        private com.example.WinForms.Controls.Input.ModernButton saveButton;
        private com.example.WinForms.Controls.Input.ModernButton closeButton;

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
            this.titleBar = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.inspectionGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.countCard = new CardPanel();
            this.countCaptionLabel = new System.Windows.Forms.Label();
            this.countValueLabel = new System.Windows.Forms.Label();
            this.judgeCard = new CardPanel();
            this.judgeCaptionLabel = new System.Windows.Forms.Label();
            this.succBadge = new StatBadge();
            this.failBadge = new StatBadge();
            this.pendingBadge = new StatBadge();
            this.fillerCard = new CardPanel();
            this.saveButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.closeButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.countCard.SuspendLayout();
            this.judgeCard.SuspendLayout();
            this.SuspendLayout();

            // ===== accentStrip (very top) =====
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.accentStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.accentStrip.Location = new System.Drawing.Point(0, 0);
            this.accentStrip.Name = "accentStrip";
            this.accentStrip.Size = new System.Drawing.Size(1180, 3);
            this.accentStrip.TabIndex = 0;

            // ===== title =====
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.titleBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.titleBar.Location = new System.Drawing.Point(16, 20);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(5, 30);
            this.titleBar.TabIndex = 1;

            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 17F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.titleLabel.Location = new System.Drawing.Point(30, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "트레이 검사·판정";
            this.titleLabel.TabIndex = 2;

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.subtitleLabel.Location = new System.Drawing.Point(32, 52);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Text = "Pick SlotNo / FingerId / FingerIdx / Result in the grid. Other columns are query results.";
            this.subtitleLabel.TabIndex = 3;

            // ===== inspectionGrid (common ModernDataGrid) =====
            this.inspectionGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.inspectionGrid.Location = new System.Drawing.Point(16, 84);
            this.inspectionGrid.Name = "inspectionGrid";
            this.inspectionGrid.Size = new System.Drawing.Size(1148, 416);
            this.inspectionGrid.TabIndex = 4;

            // ===== countCard =====
            this.countCard.BackColor = System.Drawing.Color.White;
            this.countCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countCard.Controls.Add(this.countCaptionLabel);
            this.countCard.Controls.Add(this.countValueLabel);
            this.countCard.Location = new System.Drawing.Point(16, 512);
            this.countCard.Name = "countCard";
            this.countCard.Size = new System.Drawing.Size(150, 64);
            this.countCard.TabIndex = 5;

            this.countCaptionLabel.AutoSize = true;
            this.countCaptionLabel.BackColor = System.Drawing.Color.White;
            this.countCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.countCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.countCaptionLabel.Location = new System.Drawing.Point(16, 10);
            this.countCaptionLabel.Name = "countCaptionLabel";
            this.countCaptionLabel.Text = "Rows";

            this.countValueLabel.AutoSize = true;
            this.countValueLabel.BackColor = System.Drawing.Color.White;
            this.countValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 15F);
            this.countValueLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.countValueLabel.Location = new System.Drawing.Point(16, 28);
            this.countValueLabel.Name = "countValueLabel";
            this.countValueLabel.Text = "0";

            // ===== judgeCard =====
            this.judgeCard.BackColor = System.Drawing.Color.White;
            this.judgeCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.judgeCard.Controls.Add(this.judgeCaptionLabel);
            this.judgeCard.Controls.Add(this.succBadge);
            this.judgeCard.Controls.Add(this.failBadge);
            this.judgeCard.Controls.Add(this.pendingBadge);
            this.judgeCard.Location = new System.Drawing.Point(178, 512);
            this.judgeCard.Name = "judgeCard";
            this.judgeCard.Size = new System.Drawing.Size(340, 64);
            this.judgeCard.TabIndex = 6;

            this.judgeCaptionLabel.AutoSize = true;
            this.judgeCaptionLabel.BackColor = System.Drawing.Color.White;
            this.judgeCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.judgeCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.judgeCaptionLabel.Location = new System.Drawing.Point(16, 10);
            this.judgeCaptionLabel.Name = "judgeCaptionLabel";
            this.judgeCaptionLabel.Text = "Judgement";

            this.succBadge.BackColor = System.Drawing.Color.White;
            this.succBadge.Location = new System.Drawing.Point(14, 30);
            this.succBadge.Name = "succBadge";
            this.succBadge.Size = new System.Drawing.Size(92, 26);
            this.succBadge.TabIndex = 0;
            this.succBadge.Text = "SUCC 0";

            this.failBadge.BackColor = System.Drawing.Color.White;
            this.failBadge.Location = new System.Drawing.Point(112, 30);
            this.failBadge.Name = "failBadge";
            this.failBadge.Size = new System.Drawing.Size(92, 26);
            this.failBadge.TabIndex = 1;
            this.failBadge.Text = "FAIL 0";

            this.pendingBadge.BackColor = System.Drawing.Color.White;
            this.pendingBadge.Location = new System.Drawing.Point(210, 30);
            this.pendingBadge.Name = "pendingBadge";
            this.pendingBadge.Size = new System.Drawing.Size(114, 26);
            this.pendingBadge.TabIndex = 2;
            this.pendingBadge.Text = "Pending 0";

            // ===== fillerCard (fills the gap right of Judgement, up to the buttons) =====
            this.fillerCard.BackColor = System.Drawing.Color.White;
            this.fillerCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.fillerCard.Location = new System.Drawing.Point(530, 512);
            this.fillerCard.Name = "fillerCard";
            this.fillerCard.Size = new System.Drawing.Size(362, 64);
            this.fillerCard.TabIndex = 7;

            // ===== action buttons (Save left, Close right) =====
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.IconGlyph = "\uE74E";
            this.saveButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.saveButton.Location = new System.Drawing.Point(908, 520);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 44);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.IconGlyph = "\uE711";
            this.closeButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.closeButton.Location = new System.Drawing.Point(1036, 520);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(120, 44);
            this.closeButton.TabIndex = 9;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);

            // ===== form =====
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(1180, 600);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.fillerCard);
            this.Controls.Add(this.judgeCard);
            this.Controls.Add(this.countCard);
            this.Controls.Add(this.inspectionGrid);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.accentStrip);
            this.MinimumSize = new System.Drawing.Size(900, 480);
            this.Name = "TrayInspectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "트레이 검사·판정";
            this.countCard.ResumeLayout(false);
            this.countCard.PerformLayout();
            this.judgeCard.ResumeLayout(false);
            this.judgeCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
