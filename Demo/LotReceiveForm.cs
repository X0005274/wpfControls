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

        public LotReceiveForm()
        {
            this.InitializeComponent();
            this.ConfigureColumns();
            this.ConfigureStateFilter();
            this.lotGrid.SelectionChanged += this.LotGrid_SelectionChanged;
            this.allRows = this.BuildSampleData();
            this.ShowRows(this.allRows);
        }

        private void ConfigureStateFilter()
        {
            // Multi-select check combo; nothing checked = no state filter (all states).
            List<string> states = new List<string> { "Created", "Released", "Scrapped" };
            this.stateCombo.ItemsSource = states;
        }

        private void ConfigureColumns()
        {
            this.lotGrid.AddTextColumn("Lot Id", "LotId", "*");
            this.lotGrid.AddBadgeColumn("Lot State", "LotState", "LotStateTone", "220");
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
                rows.Add(this.NewRow(lotId, states[index]));
            }

            return rows;
        }

        private LotGridRow NewRow(string lotId, string lotState)
        {
            LotGridRow row = new LotGridRow();
            row.LotId = lotId;
            row.LotState = lotState;
            row.LotStateTone = this.ToneForState(lotState);
            return row;
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
            this.UpdateExecutionState();
        }

        /// <summary>
        /// Drives the execution area: when a row is selected, show the selected lot
        /// and enable Receive Lot; otherwise show the row count and disable it.
        /// </summary>
        private void UpdateExecutionState()
        {
            LotGridRow selected = this.lotGrid.SelectedItem as LotGridRow;
            if (selected != null)
            {
                this.receiveButton.IsButtonEnabled = true;
                this.statusLabel.Text = "Selected: " + selected.LotId + " (" + selected.LotState + ")";
                return;
            }

            this.receiveButton.IsButtonEnabled = false;
            int total = this.allRows.Count;
            int shown = this.shownRows == null ? 0 : this.shownRows.Count;
            if (shown == total)
            {
                this.statusLabel.Text = total + " lots";
            }
            else
            {
                this.statusLabel.Text = "Showing " + shown + " of " + total + " lots";
            }
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
