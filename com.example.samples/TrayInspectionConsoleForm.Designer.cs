namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="TrayInspectionConsoleForm"/> — a master/detail
    /// inspection console. Top: title + progress bar + "pending only" filter. Left: a
    /// master grid (common ModernDataGrid) of trays. Right: a detail card with the
    /// selected tray's read-only fields, a preview placeholder, the judge input combos
    /// (common ModernComboBox), big SUCC/FAIL buttons and Prev / Next-pending navigation.
    /// All controls are common wrapper controls placed in the designer.
    /// </summary>
    partial class TrayInspectionConsoleForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;
        private com.example.WinForms.Controls.Display.ModernProgressBar progressBar;

        private com.example.WinForms.Controls.Input.ModernButton filterButton;
        private System.Windows.Forms.Label countLabel;
        private com.example.WinForms.Controls.Data.ModernDataGrid masterGrid;

        private CardPanel detailCard;
        private System.Windows.Forms.Label detailHeaderLabel;
        private StatBadge detailStatusBadge;
        private System.Windows.Forms.Label detailInfoLabel;
        private CardPanel previewBox;
        private System.Windows.Forms.Label previewLabel;
        private com.example.WinForms.Controls.Selection.ModernComboBox slotCombo;
        private com.example.WinForms.Controls.Selection.ModernComboBox fingerCombo;
        private com.example.WinForms.Controls.Selection.ModernComboBox idxCombo;
        private com.example.WinForms.Controls.Input.ModernButton succButton;
        private com.example.WinForms.Controls.Input.ModernButton failButton;
        private com.example.WinForms.Controls.Input.ModernButton prevButton;
        private com.example.WinForms.Controls.Input.ModernButton nextButton;

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
            this.progressBar = new com.example.WinForms.Controls.Display.ModernProgressBar();
            this.filterButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.countLabel = new System.Windows.Forms.Label();
            this.masterGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.detailCard = new CardPanel();
            this.detailHeaderLabel = new System.Windows.Forms.Label();
            this.detailStatusBadge = new StatBadge();
            this.detailInfoLabel = new System.Windows.Forms.Label();
            this.previewBox = new CardPanel();
            this.previewLabel = new System.Windows.Forms.Label();
            this.slotCombo = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.fingerCombo = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.idxCombo = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.succButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.failButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.prevButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.nextButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.saveButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.closeButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.detailCard.SuspendLayout();
            this.previewBox.SuspendLayout();
            this.SuspendLayout();

            // accentStrip
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.accentStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.accentStrip.Location = new System.Drawing.Point(0, 0);
            this.accentStrip.Name = "accentStrip";
            this.accentStrip.Size = new System.Drawing.Size(1200, 3);
            this.accentStrip.TabIndex = 0;

            // title
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.titleBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.titleBar.Location = new System.Drawing.Point(16, 18);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(5, 30);
            this.titleBar.TabIndex = 1;

            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 17F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.titleLabel.Location = new System.Drawing.Point(30, 14);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Text = "트레이 검사 콘솔";
            this.titleLabel.TabIndex = 2;

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.subtitleLabel.Location = new System.Drawing.Point(32, 48);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Text = "행을 선택해 판정하세요. S=SUCC · F=FAIL · [다음 미판정]으로 이동";
            this.subtitleLabel.TabIndex = 3;

            // progressBar
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(700, 12);
            this.progressBar.Maximum = 100D;
            this.progressBar.Minimum = 0D;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(484, 54);
            this.progressBar.TabIndex = 4;
            this.progressBar.Title = "진행 0 / 0";
            this.progressBar.Value = 0D;

            // filterButton
            this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.filterButton.IconGlyph = "\uE71C";
            this.filterButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.filterButton.Location = new System.Drawing.Point(16, 84);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(160, 34);
            this.filterButton.TabIndex = 5;
            this.filterButton.Text = "미판정만 보기";
            this.filterButton.Click += new System.EventHandler(this.FilterButton_Click);

            // countLabel
            this.countLabel.AutoSize = true;
            this.countLabel.BackColor = System.Drawing.Color.Transparent;
            this.countLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.countLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.countLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.countLabel.Location = new System.Drawing.Point(188, 92);
            this.countLabel.Name = "countLabel";
            this.countLabel.Text = "총 0건 · 미판정 0";
            this.countLabel.TabIndex = 6;

            // masterGrid
            this.masterGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left))));
            this.masterGrid.Location = new System.Drawing.Point(16, 126);
            this.masterGrid.Name = "masterGrid";
            this.masterGrid.Size = new System.Drawing.Size(560, 510);
            this.masterGrid.TabIndex = 7;

            // detailCard
            this.detailCard.BackColor = System.Drawing.Color.White;
            this.detailCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.detailCard.Controls.Add(this.detailHeaderLabel);
            this.detailCard.Controls.Add(this.detailStatusBadge);
            this.detailCard.Controls.Add(this.detailInfoLabel);
            this.detailCard.Controls.Add(this.previewBox);
            this.detailCard.Controls.Add(this.slotCombo);
            this.detailCard.Controls.Add(this.fingerCombo);
            this.detailCard.Controls.Add(this.idxCombo);
            this.detailCard.Controls.Add(this.succButton);
            this.detailCard.Controls.Add(this.failButton);
            this.detailCard.Controls.Add(this.prevButton);
            this.detailCard.Controls.Add(this.nextButton);
            this.detailCard.Location = new System.Drawing.Point(592, 126);
            this.detailCard.Name = "detailCard";
            this.detailCard.Padding = new System.Windows.Forms.Padding(20);
            this.detailCard.Size = new System.Drawing.Size(592, 510);
            this.detailCard.TabIndex = 8;

            this.detailHeaderLabel.AutoSize = true;
            this.detailHeaderLabel.BackColor = System.Drawing.Color.White;
            this.detailHeaderLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.detailHeaderLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.detailHeaderLabel.Location = new System.Drawing.Point(20, 18);
            this.detailHeaderLabel.Name = "detailHeaderLabel";
            this.detailHeaderLabel.Text = "—";

            this.detailStatusBadge.BackColor = System.Drawing.Color.White;
            this.detailStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detailStatusBadge.Location = new System.Drawing.Point(452, 20);
            this.detailStatusBadge.Name = "detailStatusBadge";
            this.detailStatusBadge.Size = new System.Drawing.Size(120, 26);
            this.detailStatusBadge.TabIndex = 0;
            this.detailStatusBadge.Text = "미판정";

            this.detailInfoLabel.BackColor = System.Drawing.Color.White;
            this.detailInfoLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.detailInfoLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.detailInfoLabel.Location = new System.Drawing.Point(20, 58);
            this.detailInfoLabel.Name = "detailInfoLabel";
            this.detailInfoLabel.Size = new System.Drawing.Size(250, 190);
            this.detailInfoLabel.TabIndex = 1;
            this.detailInfoLabel.Text = "";

            // previewBox (placeholder card)
            this.previewBox.BackColor = System.Drawing.Color.White;
            this.previewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBox.CardColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.previewBox.Controls.Add(this.previewLabel);
            this.previewBox.Location = new System.Drawing.Point(300, 58);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(272, 190);
            this.previewBox.TabIndex = 2;

            this.previewLabel.BackColor = System.Drawing.Color.Transparent;
            this.previewLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.previewLabel.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.previewLabel.Name = "previewLabel";
            this.previewLabel.Text = "미리보기 없음";
            this.previewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // input combos
            this.slotCombo.Location = new System.Drawing.Point(20, 262);
            this.slotCombo.Name = "slotCombo";
            this.slotCombo.Size = new System.Drawing.Size(170, 72);
            this.slotCombo.TabIndex = 3;
            this.slotCombo.Title = "SlotNo *";

            this.fingerCombo.Location = new System.Drawing.Point(200, 262);
            this.fingerCombo.Name = "fingerCombo";
            this.fingerCombo.Size = new System.Drawing.Size(170, 72);
            this.fingerCombo.TabIndex = 4;
            this.fingerCombo.Title = "FingerId";

            this.idxCombo.Location = new System.Drawing.Point(380, 262);
            this.idxCombo.Name = "idxCombo";
            this.idxCombo.Size = new System.Drawing.Size(170, 72);
            this.idxCombo.TabIndex = 5;
            this.idxCombo.Title = "FingerIdx";

            // judge buttons
            this.succButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.succButton.IconGlyph = "\uE73E";
            this.succButton.Location = new System.Drawing.Point(20, 350);
            this.succButton.Name = "succButton";
            this.succButton.Size = new System.Drawing.Size(265, 56);
            this.succButton.TabIndex = 6;
            this.succButton.Text = "SUCC  (S)";
            this.succButton.Click += new System.EventHandler(this.SuccButton_Click);

            this.failButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Danger;
            this.failButton.IconGlyph = "\uE711";
            this.failButton.Location = new System.Drawing.Point(295, 350);
            this.failButton.Name = "failButton";
            this.failButton.Size = new System.Drawing.Size(255, 56);
            this.failButton.TabIndex = 7;
            this.failButton.Text = "FAIL  (F)";
            this.failButton.Click += new System.EventHandler(this.FailButton_Click);

            // navigation
            this.prevButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.prevButton.Location = new System.Drawing.Point(20, 420);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(150, 40);
            this.prevButton.TabIndex = 8;
            this.prevButton.Text = "◀ 이전";
            this.prevButton.Click += new System.EventHandler(this.PrevButton_Click);

            this.nextButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.nextButton.Location = new System.Drawing.Point(180, 420);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(200, 40);
            this.nextButton.TabIndex = 9;
            this.nextButton.Text = "다음 미판정 ▶";
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);

            // Save / Close
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.IconGlyph = "\uE74E";
            this.saveButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.saveButton.Location = new System.Drawing.Point(928, 648);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(120, 40);
            this.saveButton.TabIndex = 10;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);

            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.IconGlyph = "\uE8BB";
            this.closeButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.closeButton.Location = new System.Drawing.Point(1056, 648);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(120, 40);
            this.closeButton.TabIndex = 11;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);

            // form
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.detailCard);
            this.Controls.Add(this.masterGrid);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.accentStrip);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 560);
            this.Name = "TrayInspectionConsoleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "트레이 검사 콘솔";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.detailCard.ResumeLayout(false);
            this.detailCard.PerformLayout();
            this.previewBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
