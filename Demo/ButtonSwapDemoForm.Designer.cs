namespace com.example.Demo
{
    /// <summary>
    /// Designer layout for <see cref="ButtonSwapDemoForm"/>. A normal WinForms form
    /// whose action buttons and input are replaced by WPF wrappers (ModernButton /
    /// ModernTextBox, each a WPF control hosted in ElementHost). A native Button is
    /// kept for contrast. Every wrapper's standard WinForms events keep working, so
    /// existing event code does not change after the swap.
    /// </summary>
    partial class ButtonSwapDemoForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label descLabel;

        private System.Windows.Forms.Label nativeRowLabel;
        private System.Windows.Forms.Button nativeButton;

        private System.Windows.Forms.Label modernRowLabel;
        private com.example.WinForms.Controls.Input.ModernButton modernPrimaryButton;
        private com.example.WinForms.Controls.Input.ModernButton modernSecondaryButton;
        private com.example.WinForms.Controls.Input.ModernButton modernDangerButton;

        private System.Windows.Forms.Label textRowLabel;
        private com.example.WinForms.Controls.Input.ModernTextBox nameTextBox;

        private System.Windows.Forms.Label statusCaptionLabel;
        private System.Windows.Forms.Label statusLabel;

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
            this.titleLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.nativeRowLabel = new System.Windows.Forms.Label();
            this.nativeButton = new System.Windows.Forms.Button();
            this.modernRowLabel = new System.Windows.Forms.Label();
            this.modernPrimaryButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.modernSecondaryButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.modernDangerButton = new com.example.WinForms.Controls.Input.ModernButton();
            this.textRowLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new com.example.WinForms.Controls.Input.ModernTextBox();
            this.statusCaptionLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // titleLabel
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.titleLabel.Location = new System.Drawing.Point(24, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(360, 25);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "기존 WinForms 폼 — 버튼·입력창만 WPF로 교체";

            // descLabel
            this.descLabel.AutoSize = true;
            this.descLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.descLabel.Location = new System.Drawing.Point(24, 52);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(560, 15);
            this.descLabel.TabIndex = 1;
            this.descLabel.Text = "라벨은 네이티브 그대로, 액션 버튼과 입력창만 ModernButton·ModernTextBox로 교체했습니다. 디자이너 드래그 후에도 정상 동작합니다.";

            // nativeRowLabel
            this.nativeRowLabel.AutoSize = true;
            this.nativeRowLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.nativeRowLabel.Location = new System.Drawing.Point(24, 108);
            this.nativeRowLabel.Name = "nativeRowLabel";
            this.nativeRowLabel.Size = new System.Drawing.Size(150, 15);
            this.nativeRowLabel.TabIndex = 2;
            this.nativeRowLabel.Text = "기존 네이티브 Button:";

            // nativeButton (standard WinForms button, for contrast)
            this.nativeButton.Location = new System.Drawing.Point(200, 102);
            this.nativeButton.Name = "nativeButton";
            this.nativeButton.Size = new System.Drawing.Size(120, 32);
            this.nativeButton.TabIndex = 3;
            this.nativeButton.Text = "저장 (native)";
            this.nativeButton.UseVisualStyleBackColor = true;
            this.nativeButton.Click += new System.EventHandler(this.NativeButton_Click);

            // modernRowLabel
            this.modernRowLabel.AutoSize = true;
            this.modernRowLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.modernRowLabel.Location = new System.Drawing.Point(24, 178);
            this.modernRowLabel.Name = "modernRowLabel";
            this.modernRowLabel.Size = new System.Drawing.Size(150, 15);
            this.modernRowLabel.TabIndex = 4;
            this.modernRowLabel.Text = "WPF ModernButton (교체):";

            // modernPrimaryButton — replaces a primary action button
            this.modernPrimaryButton.BackColor = System.Drawing.Color.White;
            this.modernPrimaryButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.modernPrimaryButton.Location = new System.Drawing.Point(200, 168);
            this.modernPrimaryButton.Name = "modernPrimaryButton";
            this.modernPrimaryButton.Size = new System.Drawing.Size(100, 40);
            this.modernPrimaryButton.TabIndex = 5;
            this.modernPrimaryButton.Text = "저장";
            this.modernPrimaryButton.Click += new System.EventHandler(this.ModernPrimaryButton_Click);

            // modernSecondaryButton
            this.modernSecondaryButton.BackColor = System.Drawing.Color.White;
            this.modernSecondaryButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Secondary;
            this.modernSecondaryButton.Location = new System.Drawing.Point(312, 168);
            this.modernSecondaryButton.Name = "modernSecondaryButton";
            this.modernSecondaryButton.Size = new System.Drawing.Size(100, 40);
            this.modernSecondaryButton.TabIndex = 6;
            this.modernSecondaryButton.Text = "취소";
            this.modernSecondaryButton.Click += new System.EventHandler(this.ModernSecondaryButton_Click);

            // modernDangerButton
            this.modernDangerButton.BackColor = System.Drawing.Color.White;
            this.modernDangerButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Danger;
            this.modernDangerButton.Location = new System.Drawing.Point(424, 168);
            this.modernDangerButton.Name = "modernDangerButton";
            this.modernDangerButton.Size = new System.Drawing.Size(100, 40);
            this.modernDangerButton.TabIndex = 7;
            this.modernDangerButton.Text = "삭제";
            this.modernDangerButton.Click += new System.EventHandler(this.ModernDangerButton_Click);

            // textRowLabel
            this.textRowLabel.AutoSize = true;
            this.textRowLabel.ForeColor = System.Drawing.Color.FromArgb(55, 65, 81);
            this.textRowLabel.Location = new System.Drawing.Point(24, 244);
            this.textRowLabel.Name = "textRowLabel";
            this.textRowLabel.Size = new System.Drawing.Size(150, 15);
            this.textRowLabel.TabIndex = 8;
            this.textRowLabel.Text = "WPF ModernTextBox (교체):";

            // nameTextBox — replaces a native TextBox
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.Location = new System.Drawing.Point(200, 236);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(320, 72);
            this.nameTextBox.TabIndex = 9;
            this.nameTextBox.Title = "이름";
            this.nameTextBox.PlaceholderText = "이름을 입력하세요";

            // statusCaptionLabel
            this.statusCaptionLabel.AutoSize = true;
            this.statusCaptionLabel.ForeColor = System.Drawing.Color.FromArgb(107, 114, 128);
            this.statusCaptionLabel.Location = new System.Drawing.Point(24, 332);
            this.statusCaptionLabel.Name = "statusCaptionLabel";
            this.statusCaptionLabel.Size = new System.Drawing.Size(90, 15);
            this.statusCaptionLabel.TabIndex = 10;
            this.statusCaptionLabel.Text = "마지막 클릭:";

            // statusLabel
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.statusLabel.Location = new System.Drawing.Point(116, 332);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(80, 17);
            this.statusLabel.TabIndex = 11;
            this.statusLabel.Text = "(없음)";

            // ButtonSwapDemoForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 388);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.statusCaptionLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.textRowLabel);
            this.Controls.Add(this.modernDangerButton);
            this.Controls.Add(this.modernSecondaryButton);
            this.Controls.Add(this.modernPrimaryButton);
            this.Controls.Add(this.modernRowLabel);
            this.Controls.Add(this.nativeButton);
            this.Controls.Add(this.nativeRowLabel);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.titleLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(560, 360);
            this.Name = "ButtonSwapDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModernButton 교체 예시";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
