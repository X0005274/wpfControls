using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// A lot receive screen built with WPF wrapper controls on a native WinForms
    /// form, split into three clearly separated regions: 조회 (Lot ID + Search) on
    /// top, the data grid in the center, and 실행 (Receive Lot) at the bottom.
    /// </summary>
    public partial class LotReceiveForm : Form
    {
        private readonly List<LotGridRow> allRows;
        private List<LotGridRow> shownRows;

        // Random generator for sample Product Ids (H5G32ABCDEFGHI_AA001-XX001 ~ XX999).
        private readonly System.Random productRng = new System.Random();

        // Selected-lot label fonts: bold + italic when a row is selected (emphasized),
        // italic only for the empty "No selection" state.
        private readonly System.Drawing.Font selectedFontStrong =
            new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        private readonly System.Drawing.Font selectedFontEmpty =
            new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);

        public LotReceiveForm()
        {
            this.InitializeComponent();
            this.ConfigureColumns();
            this.ConfigureWaferColumns();
            this.ConfigureFabFilter();
            this.ConfigureStateFilter();
            this.ConfigureStateBadges();
            this.lotGrid.SelectionChanged += this.LotGrid_SelectionChanged;
            this.allRows = this.BuildSampleData();
            this.ShowRows(this.allRows);
            this.PopulateWafers(null);
        }

        private void ConfigureFabFilter()
        {
            List<string> fabs = new List<string> { "M14", "M15", "M16", "M17" };
            this.fabCombo.ItemsSource = fabs;
            this.fabCombo.SelectedItem = "M14";
        }

        private void ConfigureStateFilter()
        {
            // Multi-select check combo; nothing checked = no state filter (all states).
            List<string> states = new List<string> { "Created", "Released", "Scrapped" };
            this.stateCombo.ItemsSource = states;
        }

        private void ConfigureStateBadges()
        {
            // Use the same tones as the grid Lot State badges so the summary card
            // reads with one consistent color language.
            this.createdBadge.SetTone("success");
            this.releasedBadge.SetTone("neutral");
            this.scrappedBadge.SetTone("danger");
        }

        private void ConfigureColumns()
        {
            // Auto-width columns: each sizes to fit its data and header label.
            this.lotGrid.AddTextColumn("Lot Id", "LotId");
            this.lotGrid.AddBadgeColumn("Lot State", "LotState", "LotStateTone");
            this.lotGrid.AddTextColumn("Event", "Event");
            this.lotGrid.AddTextColumn("Event Time", "EventTime");
            this.lotGrid.AddTextColumn("Product Id", "ProductId");
            this.lotGrid.AddTextColumn("Oper Id", "OperId");
            this.lotGrid.AddTextColumn("Carrier Id", "CarrierId");
        }

        private void ConfigureWaferColumns()
        {
            // Wafer grid: filled from the lot selected on the left.
            this.waferGrid.AddTextColumn("Wafer Id", "WaferId");
            this.waferGrid.AddBadgeColumn("Wafer State", "WaferState", "WaferStateTone");
            this.waferGrid.AddTextColumn("Event", "Event");
            this.waferGrid.AddTextColumn("Event Time", "EventTime");
        }

        private List<LotGridRow> BuildSampleData()
        {
            // TH10001 ~ TH10010, with a mix of the three states.
            string[] states = new string[]
            {
                "Created", "Released", "Released", "Scrapped", "Created",
                "Released", "Created", "Scrapped", "Released", "Created"
            };

            List<LotGridRow> rows = new List<LotGridRow>();
            for (int index = 0; index < states.Length; index++)
            {
                string lotId = "TH" + (10001 + index).ToString();
                rows.Add(this.NewRow(lotId, states[index], index));
            }

            return rows;
        }

        private LotGridRow NewRow(string lotId, string lotState, int index)
        {
            LotGridRow row = new LotGridRow();
            row.LotId = lotId;
            row.LotState = lotState;
            row.LotStateTone = this.ToneForState(lotState);
            row.Event = this.EventForState(lotState);
            row.EventTime = new DateTime(2026, 6, 27, 8, 0, 0).AddMinutes(index * 37).ToString("yyyy-MM-dd HH:mm:ss");
            row.ProductId = "H5G32ABCDEFGHI_AA001-XX" + this.productRng.Next(1, 1000).ToString("D3");
            row.SubProductId = "SUB-" + (index + 1).ToString("D2");
            row.FlowId = "FLOW-" + ((index % 3) + 1).ToString();
            row.OperId = "OP" + (1010 + index * 10).ToString();
            row.CarrierId = "CAR" + (200 + index).ToString();
            row.EqpId = "EQP" + (10 + index).ToString();
            row.StkId = "STK" + (5 + index).ToString();
            return row;
        }

        private string EventForState(string lotState)
        {
            if (lotState == "Created")
            {
                return "Create";
            }
            if (lotState == "Scrapped")
            {
                return "Scrap";
            }

            return "Release";
        }

        private string ToneForState(string lotState)
        {
            if (lotState == "Created")
            {
                return "success";
            }
            if (lotState == "Scrapped")
            {
                return "danger";
            }

            // Released → default (neutral).
            return "neutral";
        }

        private void ShowRows(List<LotGridRow> rows)
        {
            // Reassign a fresh list so the grid regenerates its rows.
            this.shownRows = rows;
            this.lotGrid.ItemsSource = null;
            this.lotGrid.ItemsSource = rows;
            this.UpdateExecutionState();
        }

        private void LotGrid_SelectionChanged(object sender, EventArgs e)
        {
            LotGridRow selected = this.lotGrid.SelectedItem as LotGridRow;
            this.UpdateExecutionState();
            this.PopulateWafers(selected);
        }

        /// <summary>
        /// Fills the Wafer grid with the wafers of the given lot (.01 ~ .25), or
        /// clears it when no lot is selected.
        /// </summary>
        private void PopulateWafers(LotGridRow lot)
        {
            List<WaferGridRow> wafers = lot == null ? new List<WaferGridRow>() : this.BuildWafers(lot);
            this.waferGrid.ItemsSource = null;
            this.waferGrid.ItemsSource = wafers;
        }

        private List<WaferGridRow> BuildWafers(LotGridRow lot)
        {
            // 25 wafers per lot: WaferId = "<LotId>.01" ~ ".25", with freely-varied
            // states (deterministic per lot so the same lot always shows the same set).
            int seed = 0;
            foreach (char ch in lot.LotId)
            {
                seed += ch;
            }

            System.Random rng = new System.Random(seed);
            DateTime baseTime = new DateTime(2026, 6, 27, 9, 0, 0);

            List<WaferGridRow> wafers = new List<WaferGridRow>();
            for (int index = 0; index < 25; index++)
            {
                string state;
                string tone;
                this.RandomWaferState(rng, out state, out tone);

                WaferGridRow wafer = new WaferGridRow();
                wafer.WaferId = lot.LotId + "." + (index + 1).ToString("D2");
                wafer.WaferState = state;
                wafer.WaferStateTone = tone;
                wafer.Event = this.EventForWaferState(state);
                wafer.EventTime = baseTime.AddMinutes(index * 3).ToString("yyyy-MM-dd HH:mm:ss");
                wafers.Add(wafer);
            }

            return wafers;
        }

        private void RandomWaferState(System.Random rng, out string state, out string tone)
        {
            int roll = rng.Next(100);
            if (roll < 70)
            {
                state = "Good";
                tone = "success";
                return;
            }
            if (roll < 90)
            {
                state = "In Process";
                tone = "neutral";
                return;
            }
            if (roll < 97)
            {
                state = "Hold";
                tone = "warning";
                return;
            }

            state = "Scrap";
            tone = "danger";
        }

        private string EventForWaferState(string state)
        {
            if (state == "Good")
            {
                return "Measure";
            }
            if (state == "In Process")
            {
                return "Process";
            }
            if (state == "Hold")
            {
                return "Hold";
            }

            return "Scrap";
        }

        /// <summary>
        /// Drives the three bottom cards: the count card (queried row count, screen-
        /// agnostic wording), the state card (per-state badges in the grid's tones),
        /// and the execution card (selection + Receive Lot).
        /// </summary>
        private void UpdateExecutionState()
        {
            LotGridRow selected = this.lotGrid.SelectedItem as LotGridRow;

            int total = this.allRows.Count;
            int shown = this.shownRows == null ? 0 : this.shownRows.Count;

            this.countValueLabel.Text = shown.ToString();
            if (shown == total)
            {
                this.countCaptionLabel.Text = "rows";
            }
            else
            {
                this.countCaptionLabel.Text = "of " + total + " rows";
            }

            int created = 0;
            int released = 0;
            int scrapped = 0;
            if (this.shownRows != null)
            {
                foreach (LotGridRow row in this.shownRows)
                {
                    if (row.LotState == "Created")
                    {
                        created++;
                    }
                    else if (row.LotState == "Released")
                    {
                        released++;
                    }
                    else if (row.LotState == "Scrapped")
                    {
                        scrapped++;
                    }
                }
            }

            this.createdBadge.Text = "Created " + created;
            this.releasedBadge.Text = "Released " + released;
            this.scrappedBadge.Text = "Scrapped " + scrapped;

            if (selected != null)
            {
                this.selectedLabel.Text = selected.LotId + "  (" + selected.LotState + ")";
                this.selectedLabel.ForeColor = this.ColorForTone(this.ToneForState(selected.LotState));
                this.selectedLabel.Font = this.selectedFontStrong;
                this.receiveButton.IsButtonEnabled = true;
            }
            else
            {
                this.selectedLabel.Text = "No selection";
                this.selectedLabel.ForeColor = System.Drawing.Color.FromArgb(156, 163, 175);
                this.selectedLabel.Font = this.selectedFontEmpty;
                this.receiveButton.IsButtonEnabled = false;
            }
        }

        /// <summary>
        /// Maps a badge tone ("success" / "danger" / "neutral") to the same text
        /// color used by the grid Lot State badges, so the selected-lot label reads
        /// with the matching badge color.
        /// </summary>
        private System.Drawing.Color ColorForTone(string tone)
        {
            if (tone == "success")
            {
                return System.Drawing.Color.FromArgb(21, 128, 61);
            }
            if (tone == "danger" || tone == "error")
            {
                return System.Drawing.Color.FromArgb(185, 28, 28);
            }

            return System.Drawing.Color.FromArgb(55, 65, 81);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string keyword = (this.lotIdBox.Text ?? string.Empty).Trim();

            HashSet<string> selectedStates = new HashSet<string>();
            System.Collections.IList checkedItems = this.stateCombo.SelectedItems;
            if (checkedItems != null)
            {
                foreach (object item in checkedItems)
                {
                    if (item != null)
                    {
                        selectedStates.Add(item.ToString());
                    }
                }
            }

            List<LotGridRow> matched = new List<LotGridRow>();
            foreach (LotGridRow row in this.allRows)
            {
                bool lotIdOk = keyword.Length == 0
                    || (row.LotId != null && row.LotId.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                bool stateOk = selectedStates.Count == 0 || selectedStates.Contains(row.LotState);
                if (lotIdOk && stateOk)
                {
                    matched.Add(row);
                }
            }

            this.ShowRows(matched);
        }

        private void ReceiveButton_Click(object sender, EventArgs e)
        {
            LotGridRow selected = this.lotGrid.SelectedItem as LotGridRow;
            if (selected == null)
            {
                MessageBox.Show(this, "Please select a lot from the grid to receive.", "Receive Lot",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(this, "Lot received: " + selected.LotId, "Receive Lot",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
