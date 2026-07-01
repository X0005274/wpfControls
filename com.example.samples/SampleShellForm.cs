using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using com.example.Controls.Wpf.Screens;

namespace com.example.Demo
{
    /// <summary>
    /// Menu-based shell that hosts the sample screens. The left sidebar lists every
    /// registered sample; clicking one embeds that screen (a <see cref="Form"/>) into
    /// the content area. Adding a new sample is a single line in <see cref="RegisterSamples"/>.
    /// </summary>
    public partial class SampleShellForm : Form
    {
        private readonly List<SampleEntry> samples = new List<SampleEntry>();
        private Form currentSample;
        private Button activeNavButton;

        private static readonly Color AccentColor = Color.FromArgb(0, 120, 212);
        private static readonly Color AccentHoverColor = Color.FromArgb(16, 110, 190);
        private static readonly Color NavTextColor = Color.FromArgb(55, 65, 81);
        private static readonly Color NavHoverColor = Color.FromArgb(243, 244, 246);

        public SampleShellForm()
        {
            this.InitializeComponent();
            this.RegisterSamples();
            this.BuildNav();
            this.ShowFirstSample();
        }

        /// <summary>
        /// The single place to register sample screens. Add one line per screen:
        /// <c>this.AddSample("Title", () =&gt; new YourForm());</c>
        /// </summary>
        private void RegisterSamples()
        {
            this.AddSample("Lot Receive", delegate { return new LotReceiveForm(); });
            this.AddSample("Equipment / Lot", delegate { return new EquipmentLotForm(); });
            this.AddSample("Payroll", delegate { return new WpfScreenHostForm(new PayrollRunScreen()); });
            this.AddSample("Employee Mgmt", delegate { return new WpfScreenHostForm(new EmployeeManagementScreen()); });
            this.AddSample("Tray Inspection", delegate { return new TrayInspectionForm(); });
            this.AddSample("Tray Console", delegate { return new TrayInspectionConsoleForm(); });
            this.AddSample("Process Flow", delegate { return new ProcessFlowForm(); });
            this.AddSample("User Form", delegate { return new InputSampleForm(); });
        }

        private void AddSample(string title, Func<Form> factory)
        {
            this.samples.Add(new SampleEntry(title, factory));
        }

        private void BuildNav()
        {
            foreach (SampleEntry entry in this.samples)
            {
                Button button = this.CreateNavButton(entry.Title);
                SampleEntry capturedEntry = entry;
                Button capturedButton = button;
                button.Click += delegate { this.ShowSample(capturedEntry, capturedButton); };
                this.navPanel.Controls.Add(button);
            }
        }

        private Button CreateNavButton(string text)
        {
            Button button = new Button();
            button.Text = text;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = NavHoverColor;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(20, 0, 0, 0);
            button.Margin = new Padding(0);
            button.Size = new Size(this.navPanel.ClientSize.Width, 46);
            button.Font = new Font("Segoe UI", 10.5F);
            button.ForeColor = NavTextColor;
            button.BackColor = Color.White;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            return button;
        }

        private void ShowFirstSample()
        {
            if (this.navPanel.Controls.Count == 0)
            {
                return;
            }

            Button firstButton = this.navPanel.Controls[0] as Button;
            if (firstButton != null)
            {
                this.ShowSample(this.samples[0], firstButton);
            }
        }

        private void ShowSample(SampleEntry entry, Button navButton)
        {
            this.contentPanel.SuspendLayout();

            if (this.currentSample != null)
            {
                this.contentPanel.Controls.Remove(this.currentSample);
                this.currentSample.Dispose();
                this.currentSample = null;
            }

            Form form = entry.Factory();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.MinimumSize = Size.Empty;
            form.Dock = DockStyle.Fill;
            this.contentPanel.Controls.Add(form);
            this.currentSample = form;
            form.Show();

            this.contentPanel.ResumeLayout();
            this.HighlightNav(navButton);
        }

        private void HighlightNav(Button activeButton)
        {
            foreach (Control control in this.navPanel.Controls)
            {
                Button button = control as Button;
                if (button == null)
                {
                    continue;
                }

                if (button == activeButton)
                {
                    button.BackColor = AccentColor;
                    button.ForeColor = Color.White;
                    // Hover on the active button must stay dark-accent, otherwise the
                    // light hover colour shows under the white text and hides it.
                    button.FlatAppearance.MouseOverBackColor = AccentHoverColor;
                    button.Font = new Font("Segoe UI Semibold", 10.5F);
                }
                else
                {
                    button.BackColor = Color.White;
                    button.ForeColor = NavTextColor;
                    button.FlatAppearance.MouseOverBackColor = NavHoverColor;
                    button.Font = new Font("Segoe UI", 10.5F);
                }
            }

            this.activeNavButton = activeButton;
        }
    }

    /// <summary>A registered sample: a display title and a factory for its screen.</summary>
    public class SampleEntry
    {
        private readonly string title;
        private readonly Func<Form> factory;

        public SampleEntry(string title, Func<Form> factory)
        {
            this.title = title;
            this.factory = factory;
        }

        public string Title
        {
            get { return this.title; }
        }

        public Func<Form> Factory
        {
            get { return this.factory; }
        }
    }
}
