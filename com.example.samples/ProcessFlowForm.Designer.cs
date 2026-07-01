namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="ProcessFlowForm"/> — the work-flow console.
    /// Top: step indicator (의뢰서 → LOT → FOUP → SAW → FIB → TEM) + LOT bar.
    /// Start phase shows the request list grid (의뢰서 목록, with LOT info); after LOT
    /// creation it is swapped for the per-stage action card (준비/시작/종료 + inputs) with
    /// Chip / Lamella result cards. An event-log grid runs along the bottom.
    /// All common wrapper controls.
    /// </summary>
    partial class ProcessFlowForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel accentStrip;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitleLabel;

        private com.example.WinForms.Controls.Layout.ModernStepIndicator stepIndicator;

        private com.example.WinForms.Controls.Input.ModernButton createLotButton;
        private System.Windows.Forms.Label lotLabel;

        private com.example.WinForms.Controls.Data.ModernDataGrid requestGrid;

        private CardPanel stageCard;
        private System.Windows.Forms.Label stageTitleLabel;
        private StatBadge stageStatusBadge;
        private com.example.WinForms.Controls.Selection.ModernComboBox comboA;
        private com.example.WinForms.Controls.Selection.ModernComboBox comboB;
        private com.example.WinForms.Controls.Selection.ModernComboBox comboC;
        private com.example.WinForms.Controls.Input.ModernButton prepButton;
        private com.example.WinForms.Controls.Input.ModernButton startButton;
        private com.example.WinForms.Controls.Input.ModernButton endButton;
        private System.Windows.Forms.Label hintLabel;

        private CardPanel chipCard;
        private System.Windows.Forms.Label chipCaption;
        private System.Windows.Forms.Label chipValue;
        private CardPanel lamellaCard;
        private System.Windows.Forms.Label lamellaCaption;
        private System.Windows.Forms.Label lamellaValue;

        private com.example.WinForms.Controls.Data.ModernDataGrid logGrid;

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
            this.stepIndicator = new com.example.WinForms.Controls.Layout.ModernStepIndicator();
            this.createLotButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.lotLabel = new System.Windows.Forms.Label();
            this.requestGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.stageCard = new CardPanel();
            this.stageTitleLabel = new System.Windows.Forms.Label();
            this.stageStatusBadge = new StatBadge();
            this.comboA = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.comboB = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.comboC = new com.example.WinForms.Controls.Selection.ModernComboBox();
            this.prepButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.startButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.endButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.hintLabel = new System.Windows.Forms.Label();
            this.chipCard = new CardPanel();
            this.chipCaption = new System.Windows.Forms.Label();
            this.chipValue = new System.Windows.Forms.Label();
            this.lamellaCard = new CardPanel();
            this.lamellaCaption = new System.Windows.Forms.Label();
            this.lamellaValue = new System.Windows.Forms.Label();
            this.logGrid = new com.example.WinForms.Controls.Data.ModernDataGrid();
            this.stageCard.SuspendLayout();
            this.chipCard.SuspendLayout();
            this.lamellaCard.SuspendLayout();
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
            this.titleLabel.Text = "작업 흐름 콘솔";
            this.titleLabel.TabIndex = 2;

            this.subtitleLabel.AutoSize = true;
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.subtitleLabel.Location = new System.Drawing.Point(32, 48);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Text = "의뢰서를 선택해 LOT을 생성하면 FOUP EXCHANGE → LASER SAW → 4\"FIB → TEM 순으로 진행합니다.";
            this.subtitleLabel.TabIndex = 3;

            // stepIndicator
            this.stepIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.stepIndicator.CurrentStep = 0;
            this.stepIndicator.Location = new System.Drawing.Point(16, 84);
            this.stepIndicator.Name = "stepIndicator";
            this.stepIndicator.Size = new System.Drawing.Size(1168, 40);
            this.stepIndicator.TabIndex = 4;

            // LOT bar
            this.createLotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.createLotButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.createLotButton.Location = new System.Drawing.Point(16, 138);
            this.createLotButton.Name = "createLotButton";
            this.createLotButton.Size = new System.Drawing.Size(160, 44);
            this.createLotButton.TabIndex = 5;
            this.createLotButton.Text = "LOT 생성";
            this.createLotButton.Click += new System.EventHandler(this.CreateLotButton_Click);

            this.lotLabel.AutoSize = true;
            this.lotLabel.BackColor = System.Drawing.Color.Transparent;
            this.lotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.lotLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lotLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lotLabel.Location = new System.Drawing.Point(190, 150);
            this.lotLabel.Name = "lotLabel";
            this.lotLabel.Text = "LOT: -   (의뢰서를 선택하세요)";
            this.lotLabel.TabIndex = 6;

            // requestGrid (start phase)
            this.requestGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.requestGrid.Location = new System.Drawing.Point(16, 196);
            this.requestGrid.Name = "requestGrid";
            this.requestGrid.Size = new System.Drawing.Size(1168, 320);
            this.requestGrid.TabIndex = 7;

            // stageCard (work phase, same region as requestGrid)
            this.stageCard.BackColor = System.Drawing.Color.White;
            this.stageCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.stageCard.Controls.Add(this.stageTitleLabel);
            this.stageCard.Controls.Add(this.stageStatusBadge);
            this.stageCard.Controls.Add(this.comboA);
            this.stageCard.Controls.Add(this.comboB);
            this.stageCard.Controls.Add(this.comboC);
            this.stageCard.Controls.Add(this.prepButton);
            this.stageCard.Controls.Add(this.startButton);
            this.stageCard.Controls.Add(this.endButton);
            this.stageCard.Controls.Add(this.hintLabel);
            this.stageCard.Location = new System.Drawing.Point(16, 196);
            this.stageCard.Name = "stageCard";
            this.stageCard.Size = new System.Drawing.Size(770, 320);
            this.stageCard.TabIndex = 8;
            this.stageCard.Visible = false;

            this.stageTitleLabel.AutoSize = true;
            this.stageTitleLabel.BackColor = System.Drawing.Color.White;
            this.stageTitleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.stageTitleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.stageTitleLabel.Location = new System.Drawing.Point(20, 18);
            this.stageTitleLabel.Name = "stageTitleLabel";
            this.stageTitleLabel.Text = "단계";

            this.stageStatusBadge.BackColor = System.Drawing.Color.White;
            this.stageStatusBadge.Location = new System.Drawing.Point(620, 20);
            this.stageStatusBadge.Name = "stageStatusBadge";
            this.stageStatusBadge.Size = new System.Drawing.Size(130, 26);
            this.stageStatusBadge.TabIndex = 0;
            this.stageStatusBadge.Text = "대기";

            this.comboA.Location = new System.Drawing.Point(20, 58);
            this.comboA.Name = "comboA";
            this.comboA.Size = new System.Drawing.Size(230, 72);
            this.comboA.TabIndex = 1;
            this.comboA.Title = "Input Port";

            this.comboB.Location = new System.Drawing.Point(266, 58);
            this.comboB.Name = "comboB";
            this.comboB.Size = new System.Drawing.Size(230, 72);
            this.comboB.TabIndex = 2;
            this.comboB.Title = "Output Port";

            this.comboC.Location = new System.Drawing.Point(512, 58);
            this.comboC.Name = "comboC";
            this.comboC.Size = new System.Drawing.Size(230, 72);
            this.comboC.TabIndex = 3;
            this.comboC.Title = "Material";

            this.prepButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.prepButton.Location = new System.Drawing.Point(20, 152);
            this.prepButton.Name = "prepButton";
            this.prepButton.Size = new System.Drawing.Size(150, 44);
            this.prepButton.TabIndex = 4;
            this.prepButton.Text = "작업준비";
            this.prepButton.Click += new System.EventHandler(this.PrepButton_Click);

            this.startButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.startButton.Location = new System.Drawing.Point(180, 152);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(150, 44);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "작업시작";
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);

            this.endButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.endButton.Location = new System.Drawing.Point(340, 152);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(150, 44);
            this.endButton.TabIndex = 6;
            this.endButton.Text = "작업종료";
            this.endButton.Click += new System.EventHandler(this.EndButton_Click);

            this.hintLabel.BackColor = System.Drawing.Color.White;
            this.hintLabel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.hintLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.hintLabel.Location = new System.Drawing.Point(20, 214);
            this.hintLabel.Name = "hintLabel";
            this.hintLabel.Size = new System.Drawing.Size(722, 86);
            this.hintLabel.TabIndex = 7;
            this.hintLabel.Text = "";

            // chipCard (work phase)
            this.chipCard.BackColor = System.Drawing.Color.White;
            this.chipCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chipCard.Controls.Add(this.chipCaption);
            this.chipCard.Controls.Add(this.chipValue);
            this.chipCard.Location = new System.Drawing.Point(802, 196);
            this.chipCard.Name = "chipCard";
            this.chipCard.Size = new System.Drawing.Size(182, 150);
            this.chipCard.TabIndex = 9;
            this.chipCard.Visible = false;

            this.chipCaption.AutoSize = true;
            this.chipCaption.BackColor = System.Drawing.Color.White;
            this.chipCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chipCaption.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.chipCaption.Location = new System.Drawing.Point(16, 16);
            this.chipCaption.Name = "chipCaption";
            this.chipCaption.Text = "Chip 생성 (LASER SAW)";

            this.chipValue.AutoSize = true;
            this.chipValue.BackColor = System.Drawing.Color.White;
            this.chipValue.Font = new System.Drawing.Font("Segoe UI Semibold", 22F);
            this.chipValue.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.chipValue.Location = new System.Drawing.Point(16, 44);
            this.chipValue.Name = "chipValue";
            this.chipValue.Text = "0";

            // lamellaCard (work phase)
            this.lamellaCard.BackColor = System.Drawing.Color.White;
            this.lamellaCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lamellaCard.Controls.Add(this.lamellaCaption);
            this.lamellaCard.Controls.Add(this.lamellaValue);
            this.lamellaCard.Location = new System.Drawing.Point(1002, 196);
            this.lamellaCard.Name = "lamellaCard";
            this.lamellaCard.Size = new System.Drawing.Size(182, 150);
            this.lamellaCard.TabIndex = 10;
            this.lamellaCard.Visible = false;

            this.lamellaCaption.AutoSize = true;
            this.lamellaCaption.BackColor = System.Drawing.Color.White;
            this.lamellaCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lamellaCaption.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.lamellaCaption.Location = new System.Drawing.Point(16, 16);
            this.lamellaCaption.Name = "lamellaCaption";
            this.lamellaCaption.Text = "Lamella 생성 (4\"FIB)";

            this.lamellaValue.AutoSize = true;
            this.lamellaValue.BackColor = System.Drawing.Color.White;
            this.lamellaValue.Font = new System.Drawing.Font("Segoe UI Semibold", 22F);
            this.lamellaValue.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lamellaValue.Location = new System.Drawing.Point(16, 44);
            this.lamellaValue.Name = "lamellaValue";
            this.lamellaValue.Text = "0";

            // logGrid (always)
            this.logGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right))));
            this.logGrid.Location = new System.Drawing.Point(16, 532);
            this.logGrid.Name = "logGrid";
            this.logGrid.Size = new System.Drawing.Size(1168, 178);
            this.logGrid.TabIndex = 11;

            // form
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(247, 248, 250);
            this.ClientSize = new System.Drawing.Size(1200, 728);
            this.Controls.Add(this.logGrid);
            this.Controls.Add(this.lamellaCard);
            this.Controls.Add(this.chipCard);
            this.Controls.Add(this.stageCard);
            this.Controls.Add(this.requestGrid);
            this.Controls.Add(this.lotLabel);
            this.Controls.Add(this.createLotButton);
            this.Controls.Add(this.stepIndicator);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.accentStrip);
            this.MinimumSize = new System.Drawing.Size(1040, 640);
            this.Name = "ProcessFlowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "작업 흐름 콘솔";
            this.stageCard.ResumeLayout(false);
            this.stageCard.PerformLayout();
            this.chipCard.ResumeLayout(false);
            this.chipCard.PerformLayout();
            this.lamellaCard.ResumeLayout(false);
            this.lamellaCard.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
