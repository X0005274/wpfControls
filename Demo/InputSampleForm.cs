using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using com.example.WinForms.Controls.Input;
using com.example.WinForms.Controls.Selection;

namespace com.example.Demo
{
    /// <summary>
    /// A second sample screen for the gallery: a simple user-registration form built
    /// from the WPF wrapper controls inside a single card. Demonstrates how a new
    /// sample plugs into <see cref="SampleShellForm"/>.
    /// </summary>
    public class InputSampleForm : Form
    {
        private CardPanel card;
        private Label titleLabel;
        private ModernTextBox nameBox;
        private ModernTextBox emailBox;
        private ModernComboBox deptCombo;
        private ModernButton saveButton;
        private Label statusLabel;

        public InputSampleForm()
        {
            this.BuildLayout();
            this.ConfigureDepartments();
        }

        private void BuildLayout()
        {
            this.SuspendLayout();

            this.BackColor = Color.FromArgb(225, 229, 235);
            this.ClientSize = new Size(1280, 760);
            this.Font = new Font("Segoe UI", 9F);
            this.Text = "User Form";

            this.card = new CardPanel();
            this.card.BackColor = Color.White;
            this.card.Location = new Point(24, 24);
            this.card.Size = new Size(620, 400);

            this.titleLabel = new Label();
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new Font("Segoe UI Semibold", 14F);
            this.titleLabel.ForeColor = Color.FromArgb(17, 24, 39);
            this.titleLabel.Location = new Point(24, 20);
            this.titleLabel.Text = "User Registration";

            this.nameBox = new ModernTextBox();
            this.nameBox.BackColor = Color.White;
            this.nameBox.Location = new Point(24, 60);
            this.nameBox.Size = new Size(560, 72);
            this.nameBox.Title = "User Name";
            this.nameBox.PlaceholderText = "Enter full name";

            this.emailBox = new ModernTextBox();
            this.emailBox.BackColor = Color.White;
            this.emailBox.Location = new Point(24, 142);
            this.emailBox.Size = new Size(560, 72);
            this.emailBox.Title = "Email";
            this.emailBox.PlaceholderText = "name@example.com";

            this.deptCombo = new ModernComboBox();
            this.deptCombo.BackColor = Color.White;
            this.deptCombo.Location = new Point(24, 224);
            this.deptCombo.Size = new Size(280, 72);
            this.deptCombo.Title = "Department";
            this.deptCombo.IsRequired = true;

            this.saveButton = new ModernButton();
            this.saveButton.BackColor = Color.White;
            this.saveButton.Location = new Point(24, 320);
            this.saveButton.Size = new Size(150, 44);
            this.saveButton.Kind = com.example.Controls.Wpf.Input.ButtonKind.Primary;
            this.saveButton.IconGlyph = "";
            this.saveButton.Text = "Save";
            this.saveButton.Click += this.SaveButton_Click;

            this.statusLabel = new Label();
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            this.statusLabel.ForeColor = Color.FromArgb(107, 114, 128);
            this.statusLabel.Location = new Point(190, 333);
            this.statusLabel.Text = "Fill the form and press Save.";

            this.card.Controls.Add(this.statusLabel);
            this.card.Controls.Add(this.saveButton);
            this.card.Controls.Add(this.deptCombo);
            this.card.Controls.Add(this.emailBox);
            this.card.Controls.Add(this.nameBox);
            this.card.Controls.Add(this.titleLabel);
            this.Controls.Add(this.card);

            this.ResumeLayout(false);
        }

        private void ConfigureDepartments()
        {
            List<string> departments = new List<string> { "Diffusion", "Photo", "Etch", "Metrology", "CMP" };
            this.deptCombo.ItemsSource = departments;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string name = (this.nameBox.Text ?? string.Empty).Trim();
            if (name.Length == 0)
            {
                this.statusLabel.ForeColor = Color.FromArgb(185, 28, 28);
                this.statusLabel.Text = "User Name is required.";
                return;
            }

            this.statusLabel.ForeColor = Color.FromArgb(21, 128, 61);
            this.statusLabel.Text = "Saved: " + name;
        }
    }
}
