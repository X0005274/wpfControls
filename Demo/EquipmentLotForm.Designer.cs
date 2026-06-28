namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="EquipmentLotForm"/>. A thin accent strip on top,
    /// a query card (Equipment Id / Type / State / Search), a vertically split center
    /// (Equipment on top, Lot on the bottom) and an execution card at the bottom
    /// (selection summary + Connect Lot).
    /// </summary>
    partial class EquipmentLotForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;

        private CardPanel queryCard;
        private System.Windows.Forms.TableLayoutPanel filterTable;
        private com.example.WinForms.Controls.Input.ModernTextBox eqpIdBox;
        private com.example.WinForms.Controls.Selection.ModernComboBox typeCombo;
        private com.example.WinForms.Controls.Selection.ModernMultiSelectComboBox stateCombo;
        private com.example.WinForms.Controls.Input.ModernButton searchButton;

        private FocuslessSplitContainer equipSplit;
        private System.Windows.Forms.Panel eqpTitleBar;
        private System.Windows.Forms.Label eqpTitleLabel;
        private com.example.WinForms.Controls.Data.ModernDataGrid equipmentGrid;

        private System.Windows.Forms.Panel lotTitleBar;
        private System.Windows.Forms.Label lotTitleLabel;
        private com.example.WinForms.Controls.Data.ModernDataGrid lotGrid;

        private CardPanel execCard;
        private System.Windows.Forms.Label eqpSelLabel;
        private System.Windows.Forms.Label lotSelLabel;
        private System.Windows.Forms.Label hintLabel;
        private com.example.WinForms.Controls.Input.ModernButton connectButton;

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
            this.filterTable = new System.Windows.Forms.TableLayoutPanel();
            this.eqpIdBox = new com.example.WinForms.Controls.Input.ModernTextBox();
            this.typeCombo = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.stateCombo = new com.example.WinForms.Controls.Selection.ModernMultiSelectComboBox();
            this.searchButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.equipSplit = new FocuslessSplitContainer();
            this.eqpTitleBar = new System.Windows.Forms.Panel();
            this.eqpTitleLabel = new System.Windows.Forms.Label();
            this.equipmentGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.lotTitleBar = new System.Windows.Forms.Panel();
            this.lotTitleLabel = new System.Windows.Forms.Label();
            this.lotGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.execCard = new CardPanel();
            this.eqpSelLabel = new System.Windows.Forms.Label();
            this.lotSelLabel = new System.Windows.Forms.Label();
            this.hintLabel = new System.Windows.Forms.Label();
            this.connectButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.queryCard.SuspendLayout();
            this.filterTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.equipSplit)).BeginInit();
            this.equipSplit.Panel1.SuspendLayout();
            this.equipSplit.Panel2.SuspendLayout();
            this.equipSplit.SuspendLayout();
            this.execCard.SuspendLayout();
            this.SuspendLayout();

            // ===== accentStrip (very top) =====
            this.accentStrip.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.accentStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.accentStrip.Location = new System.Drawing.Point(0, 0);
            this.accentStrip.Name = "accentStrip";
            this.accentStrip.Size = new System.Drawing.Size(1280, 3);
            this.accentStrip.TabIndex = 0;

            // ===== queryCard (조회조건) =====
            this.queryCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.queryCard.BackColor = System.Drawing.Color.White;
            this.queryCard.Controls.Add(this.filterTable);
            this.queryCard.Location = new System.Drawing.Point(16, 16);
            this.queryCard.Name = "queryCard";
            this.queryCard.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.queryCard.Size = new System.Drawing.Size(1248, 104);
            this.queryCard.TabIndex = 1;

            // filterTable: auto-lays the filter row (no manual X coordinates).
            this.filterTable.BackColor = System.Drawing.Color.White;
            this.filterTable.ColumnCount = 5;
            this.filterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.filterTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.filterTable.Controls.Add(this.eqpIdBox, 0, 0);
            this.filterTable.Controls.Add(this.typeCombo, 1, 0);
            this.filterTable.Controls.Add(this.stateCombo, 2, 0);
            this.filterTable.Controls.Add(this.searchButton, 3, 0);
            this.filterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterTable.Name = "filterTable";
            this.filterTable.RowCount = 1;
            this.filterTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));

            // eqpIdBox
            this.eqpIdBox.BackColor = System.Drawing.Color.White;
            this.eqpIdBox.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.eqpIdBox.Name = "eqpIdBox";
            this.eqpIdBox.PlaceholderText = "Enter equipment id";
            this.eqpIdBox.Size = new System.Drawing.Size(240, 72);
            this.eqpIdBox.TabIndex = 0;
            this.eqpIdBox.Title = "Equipment Id";

            // typeCombo
            this.typeCombo.BackColor = System.Drawing.Color.White;
            this.typeCombo.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.typeCombo.Name = "typeCombo";
            this.typeCombo.Size = new System.Drawing.Size(160, 72);
            this.typeCombo.TabIndex = 1;
            this.typeCombo.Title = "Type";

            // stateCombo (multi-select)
            this.stateCombo.BackColor = System.Drawing.Color.White;
            this.stateCombo.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.stateCombo.Name = "stateCombo";
            this.stateCombo.PlaceholderText = "All states";
            this.stateCombo.Size = new System.Drawing.Size(200, 72);
            this.stateCombo.TabIndex = 2;
            this.stateCombo.Title = "State";

            // searchButton (Secondary, search icon). Top margin aligns it with the fields.
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.searchButton.BackColor = System.Drawing.Color.White;
            this.searchButton.IconGlyph = "";
            this.searchButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.searchButton.Margin = new System.Windows.Forms.Padding(0, 24, 0, 0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(104, 32);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);

            // ===== equipSplit (Equipment 위 / Lot 아래 — 가로 스플리터로 높이 조절) =====
            this.equipSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.equipSplit.Location = new System.Drawing.Point(16, 136);
            this.equipSplit.Name = "equipSplit";
            this.equipSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.equipSplit.Size = new System.Drawing.Size(1248, 512);
            this.equipSplit.SplitterWidth = 12;
            this.equipSplit.Panel1MinSize = 120;
            this.equipSplit.Panel2MinSize = 120;
            this.equipSplit.SplitterDistance = 260;
            // Equipment 패널은 고정 — 폼이 커지면 Lot 패널만 늘어납니다.
            this.equipSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.equipSplit.TabIndex = 2;

            // --- Panel1: Equipment (타이틀 + equipmentGrid) ---
            this.equipSplit.Panel1.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.equipSplit.Panel1.Controls.Add(this.equipmentGrid);
            this.equipSplit.Panel1.Controls.Add(this.eqpTitleLabel);
            this.equipSplit.Panel1.Controls.Add(this.eqpTitleBar);

            this.eqpTitleBar.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.eqpTitleBar.Location = new System.Drawing.Point(0, 3);
            this.eqpTitleBar.Name = "eqpTitleBar";
            this.eqpTitleBar.Size = new System.Drawing.Size(4, 18);
            this.eqpTitleBar.TabIndex = 0;

            this.eqpTitleLabel.AutoSize = true;
            this.eqpTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.eqpTitleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.eqpTitleLabel.Location = new System.Drawing.Point(12, 1);
            this.eqpTitleLabel.Name = "eqpTitleLabel";
            this.eqpTitleLabel.Size = new System.Drawing.Size(86, 21);
            this.eqpTitleLabel.TabIndex = 1;
            this.eqpTitleLabel.Text = "Equipment";

            this.equipmentGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.equipmentGrid.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.equipmentGrid.IsReadOnly = true;
            this.equipmentGrid.Location = new System.Drawing.Point(0, 28);
            this.equipmentGrid.Name = "equipmentGrid";
            this.equipmentGrid.Size = new System.Drawing.Size(1248, 232);
            this.equipmentGrid.TabIndex = 2;

            // --- Panel2: Lot (타이틀 + lotGrid) ---
            this.equipSplit.Panel2.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.equipSplit.Panel2.Controls.Add(this.lotGrid);
            this.equipSplit.Panel2.Controls.Add(this.lotTitleLabel);
            this.equipSplit.Panel2.Controls.Add(this.lotTitleBar);

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
            this.lotGrid.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.lotGrid.IsReadOnly = true;
            this.lotGrid.Location = new System.Drawing.Point(0, 28);
            this.lotGrid.Name = "lotGrid";
            this.lotGrid.Size = new System.Drawing.Size(1248, 212);
            this.lotGrid.TabIndex = 2;

            // ===== execCard (실행 — 선택 요약 + Connect Lot) =====
            this.execCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.execCard.BackColor = System.Drawing.Color.White;
            this.execCard.Controls.Add(this.connectButton);
            this.execCard.Controls.Add(this.hintLabel);
            this.execCard.Controls.Add(this.lotSelLabel);
            this.execCard.Controls.Add(this.eqpSelLabel);
            this.execCard.Location = new System.Drawing.Point(16, 664);
            this.execCard.Name = "execCard";
            this.execCard.Size = new System.Drawing.Size(1248, 80);
            this.execCard.TabIndex = 3;

            // eqpSelLabel — selected equipment id + state (colored by state in code)
            this.eqpSelLabel.AutoSize = true;
            this.eqpSelLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.eqpSelLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.eqpSelLabel.Location = new System.Drawing.Point(20, 14);
            this.eqpSelLabel.Name = "eqpSelLabel";
            this.eqpSelLabel.Size = new System.Drawing.Size(120, 20);
            this.eqpSelLabel.TabIndex = 0;
            this.eqpSelLabel.Text = "Equipment: —";

            // lotSelLabel — selected lot id + state (colored by state in code)
            this.lotSelLabel.AutoSize = true;
            this.lotSelLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lotSelLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lotSelLabel.Location = new System.Drawing.Point(20, 44);
            this.lotSelLabel.Name = "lotSelLabel";
            this.lotSelLabel.Size = new System.Drawing.Size(70, 20);
            this.lotSelLabel.TabIndex = 1;
            this.lotSelLabel.Text = "Lot: —";

            // hintLabel — discoverability for the right-click job menu
            this.hintLabel.AutoSize = true;
            this.hintLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.hintLabel.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
            this.hintLabel.Location = new System.Drawing.Point(460, 30);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(320, 17);
            this.hintLabel.TabIndex = 2;
            this.hintLabel.Text = "Tip: 장비를 우클릭하면 Job Prepare / Start / End 메뉴가 열립니다.";

            // connectButton — connect the selected lot to the selected equipment
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.BackColor = System.Drawing.Color.White;
            this.connectButton.IconGlyph = "";
            this.connectButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.connectButton.Location = new System.Drawing.Point(1078, 24);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(150, 32);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect Lot";
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);

            // ===== Form =====
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.equipSplit);
            this.Controls.Add(this.execCard);
            this.Controls.Add(this.queryCard);
            this.Controls.Add(this.accentStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "EquipmentLotForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Equipment / Lot List";
            this.queryCard.ResumeLayout(false);
            this.filterTable.ResumeLayout(false);
            this.equipSplit.Panel1.ResumeLayout(false);
            this.equipSplit.Panel1.PerformLayout();
            this.equipSplit.Panel2.ResumeLayout(false);
            this.equipSplit.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.equipSplit)).EndInit();
            this.equipSplit.ResumeLayout(false);
            this.execCard.ResumeLayout(false);
            this.execCard.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
