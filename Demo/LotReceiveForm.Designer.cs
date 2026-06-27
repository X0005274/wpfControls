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

        private FocuslessSplitContainer gridSplit;
        private System.Windows.Forms.Panel lotTitleBar;
        private System.Windows.Forms.Label lotTitleLabel;
        private com.example.WinForms.Controls.Data.ModernDataGrid lotGrid;

        private System.Windows.Forms.Panel waferTitleBar;
        private System.Windows.Forms.Label waferTitleLabel;
        private com.example.WinForms.Controls.Data.ModernDataGrid waferGrid;

        private CardPanel countCard;
        private System.Windows.Forms.Label countValueLabel;
        private System.Windows.Forms.Label countCaptionLabel;

        private CardPanel stateCard;
        private StatBadge createdBadge;
        private StatBadge releasedBadge;
        private StatBadge scrappedBadge;
        private System.Windows.Forms.Label selectedLabel;

        private CardPanel execCard;
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
            this.gridSplit = new FocuslessSplitContainer();
            this.lotTitleBar = new System.Windows.Forms.Panel();
            this.lotTitleLabel = new System.Windows.Forms.Label();
            this.lotGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.waferTitleBar = new System.Windows.Forms.Panel();
            this.waferTitleLabel = new System.Windows.Forms.Label();
            this.waferGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.countCard = new CardPanel();
            this.countValueLabel = new System.Windows.Forms.Label();
            this.countCaptionLabel = new System.Windows.Forms.Label();
            this.stateCard = new CardPanel();
            this.createdBadge = new StatBadge();
            this.releasedBadge = new StatBadge();
            this.scrappedBadge = new StatBadge();
            this.selectedLabel = new System.Windows.Forms.Label();
            this.execCard = new CardPanel();
            this.receiveButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.queryCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplit)).BeginInit();
            this.gridSplit.Panel1.SuspendLayout();
            this.gridSplit.Panel2.SuspendLayout();
            this.gridSplit.SuspendLayout();
            this.countCard.SuspendLayout();
            this.stateCard.SuspendLayout();
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
            // Y=40 lines up with the input field boxes (label 24 + control top 16);
            // height 32 matches the field height (Size.ControlHeight) so the row aligns.
            this.searchButton.Location = new System.Drawing.Point(688, 40);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(104, 32);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);

            // ===== gridSplit (Lot | Wafer — 가운데 스플리터로 넓이 조절) =====
            this.gridSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            // Splitter gutter colour is owned by FocuslessSplitContainer (blends with canvas).
            this.gridSplit.Location = new System.Drawing.Point(16, 136);
            this.gridSplit.Name = "gridSplit";
            this.gridSplit.Size = new System.Drawing.Size(1888, 520);
            this.gridSplit.SplitterWidth = 12;
            this.gridSplit.Panel1MinSize = 360;
            this.gridSplit.Panel2MinSize = 320;
            this.gridSplit.SplitterDistance = 1404;
            // Wafer 패널은 고정 — 폼이 넓어지면 Lot 패널만 늘어나고 Wafer는 컬럼 폭 유지.
            this.gridSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.gridSplit.TabIndex = 2;

            // --- Panel1: Lot (타이틀 + lotGrid) ---
            this.gridSplit.Panel1.BackColor = System.Drawing.Color.FromArgb(225, 229, 235);
            this.gridSplit.Panel1.Controls.Add(this.lotGrid);
            this.gridSplit.Panel1.Controls.Add(this.lotTitleLabel);
            this.gridSplit.Panel1.Controls.Add(this.lotTitleBar);

            this.lotTitleBar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.lotTitleBar.Location = new System.Drawing.Point(0, 3);
            this.lotTitleBar.Name = "lotTitleBar";
            this.lotTitleBar.Size = new System.Drawing.Size(4, 18);
            this.lotTitleBar.TabIndex = 0;

            this.lotTitleLabel.AutoSize = true;
            this.lotTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lotTitleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lotTitleLabel.Location = new System.Drawing.Point(12, 1);
            this.lotTitleLabel.Name = "lotTitleLabel";
            this.lotTitleLabel.Size = new System.Drawing.Size(34, 21);
            this.lotTitleLabel.TabIndex = 1;
            this.lotTitleLabel.Text = "Lot";

            this.lotGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lotGrid.BackColor = System.Drawing.Color.FromArgb(225, 229, 235);
            this.lotGrid.IsReadOnly = true;
            this.lotGrid.Location = new System.Drawing.Point(0, 28);
            this.lotGrid.Name = "lotGrid";
            this.lotGrid.Size = new System.Drawing.Size(1404, 492);
            this.lotGrid.TabIndex = 2;

            // --- Panel2: Wafer (타이틀 + waferGrid) ---
            this.gridSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(225, 229, 235);
            this.gridSplit.Panel2.Controls.Add(this.waferGrid);
            this.gridSplit.Panel2.Controls.Add(this.waferTitleLabel);
            this.gridSplit.Panel2.Controls.Add(this.waferTitleBar);

            this.waferTitleBar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.waferTitleBar.Location = new System.Drawing.Point(0, 3);
            this.waferTitleBar.Name = "waferTitleBar";
            this.waferTitleBar.Size = new System.Drawing.Size(4, 18);
            this.waferTitleBar.TabIndex = 0;

            this.waferTitleLabel.AutoSize = true;
            this.waferTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.waferTitleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.waferTitleLabel.Location = new System.Drawing.Point(12, 1);
            this.waferTitleLabel.Name = "waferTitleLabel";
            this.waferTitleLabel.Size = new System.Drawing.Size(52, 21);
            this.waferTitleLabel.TabIndex = 1;
            this.waferTitleLabel.Text = "Wafer";

            this.waferGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.waferGrid.BackColor = System.Drawing.Color.FromArgb(225, 229, 235);
            this.waferGrid.IsReadOnly = true;
            this.waferGrid.Location = new System.Drawing.Point(0, 28);
            this.waferGrid.Name = "waferGrid";
            this.waferGrid.Size = new System.Drawing.Size(472, 492);
            this.waferGrid.TabIndex = 2;

            // ===== countCard (조회 건수, 좌측 하단 / 좁게) =====
            this.countCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.countCard.BackColor = System.Drawing.Color.White;
            this.countCard.Controls.Add(this.countCaptionLabel);
            this.countCard.Controls.Add(this.countValueLabel);
            this.countCard.Location = new System.Drawing.Point(16, 672);
            this.countCard.Name = "countCard";
            this.countCard.Size = new System.Drawing.Size(120, 80);
            this.countCard.TabIndex = 3;

            // countValueLabel — queried row count (primary, SemiBold)
            this.countValueLabel.AutoSize = true;
            this.countValueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.countValueLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.countValueLabel.Location = new System.Drawing.Point(20, 13);
            this.countValueLabel.Name = "countValueLabel";
            this.countValueLabel.Size = new System.Drawing.Size(60, 25);
            this.countValueLabel.TabIndex = 0;
            this.countValueLabel.Text = "0";

            // countCaptionLabel — generic, screen-agnostic caption (not "lots")
            this.countCaptionLabel.AutoSize = true;
            this.countCaptionLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.countCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.countCaptionLabel.Location = new System.Drawing.Point(22, 47);
            this.countCaptionLabel.Name = "countCaptionLabel";
            this.countCaptionLabel.Size = new System.Drawing.Size(40, 17);
            this.countCaptionLabel.TabIndex = 1;
            this.countCaptionLabel.Text = "rows";

            // ===== stateCard (선택 Lot + 상태 분포, 중앙 하단 / 배지 색 통일) =====
            this.stateCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stateCard.BackColor = System.Drawing.Color.White;
            this.stateCard.Controls.Add(this.selectedLabel);
            this.stateCard.Controls.Add(this.scrappedBadge);
            this.stateCard.Controls.Add(this.releasedBadge);
            this.stateCard.Controls.Add(this.createdBadge);
            this.stateCard.Location = new System.Drawing.Point(152, 672);
            this.stateCard.Name = "stateCard";
            this.stateCard.Size = new System.Drawing.Size(476, 80);
            this.stateCard.TabIndex = 4;

            // selectedLabel — selected Lot Id + state, on the state card's top row.
            // Italic always; colored with the selected state's badge tone and made
            // bold when a row is selected (font + tone applied in UpdateExecutionState).
            this.selectedLabel.AutoSize = true;
            this.selectedLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.selectedLabel.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.selectedLabel.Location = new System.Drawing.Point(20, 14);
            this.selectedLabel.Name = "selectedLabel";
            this.selectedLabel.Size = new System.Drawing.Size(90, 20);
            this.selectedLabel.TabIndex = 0;
            this.selectedLabel.Text = "No selection";

            // createdBadge (success tone — matches grid badge)
            this.createdBadge.Location = new System.Drawing.Point(20, 40);
            this.createdBadge.Name = "createdBadge";
            this.createdBadge.Size = new System.Drawing.Size(104, 26);
            this.createdBadge.TabIndex = 1;
            this.createdBadge.Text = "Created 0";

            // releasedBadge (neutral tone — matches grid badge)
            this.releasedBadge.Location = new System.Drawing.Point(128, 40);
            this.releasedBadge.Name = "releasedBadge";
            this.releasedBadge.Size = new System.Drawing.Size(104, 26);
            this.releasedBadge.TabIndex = 2;
            this.releasedBadge.Text = "Released 0";

            // scrappedBadge (danger tone — matches grid badge)
            this.scrappedBadge.Location = new System.Drawing.Point(236, 40);
            this.scrappedBadge.Name = "scrappedBadge";
            this.scrappedBadge.Size = new System.Drawing.Size(104, 26);
            this.scrappedBadge.TabIndex = 3;
            this.scrappedBadge.Text = "Scrapped 0";

            // ===== execCard (실행, 우측 하단 / 넓게 — 현재 넓이 유지) =====
            this.execCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.execCard.BackColor = System.Drawing.Color.White;
            this.execCard.Controls.Add(this.receiveButton);
            this.execCard.Location = new System.Drawing.Point(644, 672);
            this.execCard.Name = "execCard";
            this.execCard.Size = new System.Drawing.Size(1260, 80);
            this.execCard.TabIndex = 5;

            // receiveButton (Primary, right-aligned, receive icon)
            this.receiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.receiveButton.BackColor = System.Drawing.Color.White;
            this.receiveButton.IconGlyph = "\uE896";
            this.receiveButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.receiveButton.Location = new System.Drawing.Point(1090, 18);
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
            this.Controls.Add(this.gridSplit);
            this.Controls.Add(this.execCard);
            this.Controls.Add(this.stateCard);
            this.Controls.Add(this.countCard);
            this.Controls.Add(this.queryCard);
            this.Controls.Add(this.accentStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "LotReceiveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lot Receive";
            this.queryCard.ResumeLayout(false);
            this.gridSplit.Panel1.ResumeLayout(false);
            this.gridSplit.Panel1.PerformLayout();
            this.gridSplit.Panel2.ResumeLayout(false);
            this.gridSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplit)).EndInit();
            this.gridSplit.ResumeLayout(false);
            this.countCard.ResumeLayout(false);
            this.countCard.PerformLayout();
            this.stateCard.ResumeLayout(false);
            this.stateCard.PerformLayout();
            this.execCard.ResumeLayout(false);
            this.execCard.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
