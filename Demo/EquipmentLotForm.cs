using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// Equipment / Lot screen: a vertically split center with the Equipment list on top
    /// and, below it, the Lots that can be connected to the equipment selected above.
    /// Equipment and lot states are shown as colored badges so status reads at a glance,
    /// and right-clicking an equipment opens a Job Prepare / Start / End context menu.
    /// </summary>
    public partial class EquipmentLotForm : Form
    {
        private readonly string[] equipmentTypes = new string[]
        {
            "Etcher", "CVD", "Photo", "CMP", "Implant", "Diffusion"
        };

        private List<EquipmentRow> allEquipment;
        private List<EquipmentLotRow> allLots;
        private List<EquipmentLotRow> shownLots = new List<EquipmentLotRow>();

        private ContextMenuStrip jobMenu;
        private ToolStripMenuItem jobPrepareItem;
        private ToolStripMenuItem jobStartItem;
        private ToolStripMenuItem jobEndItem;

        // Selected-lot label fonts: bold when a lot is selected, italic for the empty state.
        private readonly Font selLabelStrong = new Font("Segoe UI", 11F, FontStyle.Bold);
        private readonly Font selLabelEmpty = new Font("Segoe UI", 11F, FontStyle.Italic);

        // Fixed seed so the sample data is stable across runs.
        private readonly Random rng = new Random(20260628);

        public EquipmentLotForm()
        {
            this.InitializeComponent();
            this.ConfigureEquipmentColumns();
            this.ConfigureLotColumns();
            this.BuildJobMenu();

            this.equipmentGrid.SelectionChanged += this.EquipmentGrid_SelectionChanged;
            this.equipmentGrid.RowRightClicked += this.EquipmentGrid_RowRightClicked;
            this.lotGrid.SelectionChanged += this.LotGrid_SelectionChanged;

            this.allEquipment = this.BuildEquipment();
            this.allLots = this.BuildLots();

            this.equipmentGrid.ItemsSource = this.allEquipment;
            if (this.allEquipment.Count > 0)
            {
                // Selecting the first equipment populates its connectable lots.
                this.equipmentGrid.SelectedItem = this.allEquipment[0];
            }

            this.UpdateExecutionState();
        }

        private void ConfigureEquipmentColumns()
        {
            this.equipmentGrid.AddTextColumn("Equipment Id", "EqpId");
            this.equipmentGrid.AddBadgeColumn("State", "EqpState", "EqpStateTone");
            this.equipmentGrid.AddTextColumn("Type", "EqpType");
            this.equipmentGrid.AddTextColumn("Recipe", "Recipe");
            this.equipmentGrid.AddTextColumn("Port", "Port");
            this.equipmentGrid.AddTextColumn("Lots", "LotCount");
            this.equipmentGrid.AddTextColumn("Operator", "Operator");
            this.equipmentGrid.AddTextColumn("Last Event", "LastEvent");
            this.equipmentGrid.AddTextColumn("Event Time", "LastEventTime");
        }

        private void ConfigureLotColumns()
        {
            this.lotGrid.AddTextColumn("Lot Id", "LotId");
            this.lotGrid.AddBadgeColumn("State", "LotState", "LotStateTone");
            this.lotGrid.AddBadgeColumn("Priority", "Priority", "PriorityTone");
            this.lotGrid.AddTextColumn("Product Id", "ProductId");
            this.lotGrid.AddTextColumn("Qty", "Qty");
            this.lotGrid.AddTextColumn("Step", "Step");
            this.lotGrid.AddTextColumn("Recipe", "Recipe");
            this.lotGrid.AddTextColumn("Due", "DueTime");
        }

        private void BuildJobMenu()
        {
            this.jobMenu = new ContextMenuStrip();
            this.jobMenu.Font = new Font("Segoe UI", 9.5F);

            this.jobPrepareItem = new ToolStripMenuItem("Job Prepare");
            this.jobPrepareItem.Click += delegate { this.ApplyJob("Prepare"); };

            this.jobStartItem = new ToolStripMenuItem("Job Start");
            this.jobStartItem.Click += delegate { this.ApplyJob("Start"); };

            this.jobEndItem = new ToolStripMenuItem("Job End");
            this.jobEndItem.Click += delegate { this.ApplyJob("End"); };

            this.jobMenu.Items.Add(this.jobPrepareItem);
            this.jobMenu.Items.Add(this.jobStartItem);
            this.jobMenu.Items.Add(this.jobEndItem);
        }

        private void EquipmentGrid_SelectionChanged(object sender, EventArgs e)
        {
            EquipmentRow eqp = this.equipmentGrid.SelectedItem as EquipmentRow;
            this.PopulateLots(eqp);
            this.UpdateExecutionState();
        }

        private void LotGrid_SelectionChanged(object sender, EventArgs e)
        {
            this.UpdateExecutionState();
        }

        private void EquipmentGrid_RowRightClicked(object sender, EventArgs e)
        {
            EquipmentRow eqp = this.equipmentGrid.SelectedItem as EquipmentRow;
            if (eqp == null)
            {
                return;
            }

            // Enable only the transitions that make sense for the current state.
            string state = eqp.EqpState;
            this.jobPrepareItem.Enabled = state == "Idle" || state == "Down";
            this.jobStartItem.Enabled = state == "Setup" || state == "Idle";
            this.jobEndItem.Enabled = state == "Run";

            // Defer showing the menu until the WPF (ElementHost) right-click finishes;
            // showing it synchronously inside the WPF input event makes it lose focus and
            // close immediately.
            Point at = Cursor.Position;
            this.BeginInvoke((MethodInvoker)delegate { this.jobMenu.Show(at); });
        }

        /// <summary>
        /// Applies a job action to the selected equipment, moving its state
        /// (Prepare → Setup, Start → Run, End → Idle) and refreshing the grid badge.
        /// </summary>
        private void ApplyJob(string action)
        {
            EquipmentRow eqp = this.equipmentGrid.SelectedItem as EquipmentRow;
            if (eqp == null)
            {
                return;
            }

            if (action == "Prepare")
            {
                eqp.EqpState = "Setup";
            }
            else if (action == "Start")
            {
                eqp.EqpState = "Run";
            }
            else if (action == "End")
            {
                eqp.EqpState = "Idle";
            }

            eqp.EqpStateTone = this.ToneForEqpState(eqp.EqpState);
            eqp.LastEvent = "Job " + action;
            eqp.LastEventTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            this.RefreshEquipmentGrid(eqp);
            this.UpdateExecutionState();
        }

        /// <summary>
        /// Rebinds the equipment grid so changed rows repaint (the row models are plain
        /// objects), then restores the previously selected row.
        /// </summary>
        private void RefreshEquipmentGrid(EquipmentRow keep)
        {
            this.equipmentGrid.ItemsSource = null;
            this.equipmentGrid.ItemsSource = this.allEquipment;
            if (keep != null)
            {
                this.equipmentGrid.SelectedItem = keep;
            }
        }

        /// <summary>
        /// Fills the Lot grid with the lots whose required type matches the selected
        /// equipment, or clears it when no equipment is selected.
        /// </summary>
        private void PopulateLots(EquipmentRow eqp)
        {
            List<EquipmentLotRow> lots = new List<EquipmentLotRow>();
            if (eqp != null)
            {
                foreach (EquipmentLotRow lot in this.allLots)
                {
                    if (lot.RequiredType == eqp.EqpType)
                    {
                        lots.Add(lot);
                    }
                }
            }

            this.shownLots = lots;
            this.lotGrid.ItemsSource = null;
            this.lotGrid.ItemsSource = lots;
        }

        private void UpdateExecutionState()
        {
            EquipmentRow eqp = this.equipmentGrid.SelectedItem as EquipmentRow;
            EquipmentLotRow lot = this.lotGrid.SelectedItem as EquipmentLotRow;

            if (eqp != null)
            {
                this.eqpSelLabel.Text = "Equipment: " + eqp.EqpId + "  (" + eqp.EqpState + ")";
                this.eqpSelLabel.ForeColor = this.ColorForTone(eqp.EqpStateTone);
                this.lotTitleLabel.Text = "Lot  —  " + this.shownLots.Count + " connectable";
            }
            else
            {
                this.eqpSelLabel.Text = "Equipment: —";
                this.eqpSelLabel.ForeColor = Color.FromArgb(156, 163, 175);
                this.lotTitleLabel.Text = "Lot";
            }

            if (lot != null)
            {
                this.lotSelLabel.Text = "Lot: " + lot.LotId + "  (" + lot.LotState + ", " + lot.Priority + ")";
                this.lotSelLabel.ForeColor = this.ColorForTone(lot.LotStateTone);
                this.lotSelLabel.Font = this.selLabelStrong;
            }
            else
            {
                this.lotSelLabel.Text = "Lot: —";
                this.lotSelLabel.ForeColor = Color.FromArgb(156, 163, 175);
                this.lotSelLabel.Font = this.selLabelEmpty;
            }

            this.connectButton.IsButtonEnabled = eqp != null && lot != null;
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            EquipmentRow eqp = this.equipmentGrid.SelectedItem as EquipmentRow;
            EquipmentLotRow lot = this.lotGrid.SelectedItem as EquipmentLotRow;
            if (eqp == null || lot == null)
            {
                MessageBox.Show(this, "Select an equipment and a lot to connect.", "Connect Lot",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(this, "Connected lot " + lot.LotId + " to " + eqp.EqpId + ".", "Connect Lot",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<EquipmentRow> BuildEquipment()
        {
            string[][] defs = new string[][]
            {
                new string[] { "ETCH-01", "Etcher", "Run" },
                new string[] { "ETCH-02", "Etcher", "Idle" },
                new string[] { "CVD-01", "CVD", "Run" },
                new string[] { "CVD-02", "CVD", "PM" },
                new string[] { "PHOTO-01", "Photo", "Setup" },
                new string[] { "PHOTO-02", "Photo", "Down" },
                new string[] { "CMP-01", "CMP", "Run" },
                new string[] { "IMP-01", "Implant", "Idle" },
                new string[] { "DIFF-01", "Diffusion", "Run" },
                new string[] { "DIFF-02", "Diffusion", "Idle" }
            };

            string[] operators = new string[] { "Kim J.", "Lee S.", "Park M.", "Choi H.", "Jung Y." };

            List<EquipmentRow> rows = new List<EquipmentRow>();
            DateTime baseTime = new DateTime(2026, 6, 28, 8, 0, 0);
            for (int i = 0; i < defs.Length; i++)
            {
                string id = defs[i][0];
                string type = defs[i][1];
                string state = defs[i][2];
                bool active = state == "Run" || state == "Setup";

                EquipmentRow row = new EquipmentRow();
                row.EqpId = id;
                row.EqpType = type;
                row.EqpState = state;
                row.EqpStateTone = this.ToneForEqpState(state);
                row.Recipe = active ? type.ToUpperInvariant() + "-RCP-" + this.rng.Next(1, 9).ToString("D2") : "-";
                row.Port = (state == "Run" ? this.rng.Next(1, 5) : 0).ToString() + " / 4";
                row.LotCount = state == "Run" ? this.rng.Next(1, 3) : 0;
                row.Operator = operators[i % operators.Length];
                row.LastEvent = this.EventForEqpState(state);
                row.LastEventTime = baseTime.AddMinutes(i * 17).ToString("yyyy-MM-dd HH:mm:ss");
                rows.Add(row);
            }

            return rows;
        }

        private List<EquipmentLotRow> BuildLots()
        {
            string[] states = new string[] { "Ready", "Waiting", "Hold" };

            List<EquipmentLotRow> rows = new List<EquipmentLotRow>();
            DateTime due = new DateTime(2026, 6, 28, 12, 0, 0);
            for (int i = 0; i < 32; i++)
            {
                string type = this.equipmentTypes[i % this.equipmentTypes.Length];
                string state = states[this.rng.Next(states.Length)];
                bool hot = this.rng.Next(100) < 22;

                EquipmentLotRow row = new EquipmentLotRow();
                row.LotId = "LOT" + (24001 + i).ToString();
                row.LotState = state;
                row.LotStateTone = this.ToneForLotState(state);
                row.Priority = hot ? "Hot" : "Normal";
                row.PriorityTone = hot ? "danger" : "neutral";
                row.ProductId = "H5G32" + type.Substring(0, 2).ToUpperInvariant() + "-AA" + this.rng.Next(1, 999).ToString("D3");
                row.Qty = this.rng.Next(1, 26);
                row.Step = "OP" + (1000 + this.rng.Next(1, 40) * 10).ToString();
                row.Recipe = type.ToUpperInvariant() + "-RCP-" + this.rng.Next(1, 9).ToString("D2");
                row.DueTime = due.AddMinutes(i * 23).ToString("MM-dd HH:mm");
                row.RequiredType = type;
                rows.Add(row);
            }

            return rows;
        }

        private string EventForEqpState(string state)
        {
            if (state == "Run")
            {
                return "Process Start";
            }
            if (state == "Setup")
            {
                return "Setup Start";
            }
            if (state == "PM")
            {
                return "PM Start";
            }
            if (state == "Down")
            {
                return "Alarm";
            }

            return "Job End";
        }

        private string ToneForEqpState(string state)
        {
            if (state == "Run")
            {
                return "success";
            }
            if (state == "Setup")
            {
                return "info";
            }
            if (state == "PM")
            {
                return "warning";
            }
            if (state == "Down")
            {
                return "danger";
            }

            // Idle
            return "neutral";
        }

        private string ToneForLotState(string state)
        {
            if (state == "Ready")
            {
                return "info";
            }
            if (state == "Hold")
            {
                return "warning";
            }
            if (state == "Running")
            {
                return "success";
            }

            // Waiting
            return "neutral";
        }

        /// <summary>
        /// Maps a badge tone to the matching text color used by the grid badges, so the
        /// selection labels read with the same color language.
        /// </summary>
        private Color ColorForTone(string tone)
        {
            if (tone == "success")
            {
                return Color.FromArgb(21, 128, 61);
            }
            if (tone == "danger" || tone == "error")
            {
                return Color.FromArgb(185, 28, 28);
            }
            if (tone == "warning")
            {
                return Color.FromArgb(180, 83, 9);
            }
            if (tone == "info")
            {
                return Color.FromArgb(29, 78, 216);
            }

            return Color.FromArgb(55, 65, 81);
        }
    }
}
