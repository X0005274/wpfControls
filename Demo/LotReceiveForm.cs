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

        public LotReceiveForm()
        {
            this.InitializeComponent();
            this.allRows = this.BuildSampleData();
            this.ShowRows(this.allRows);
        }

        private List<LotGridRow> BuildSampleData()
        {
            List<LotGridRow> rows = new List<LotGridRow>();
            rows.Add(this.NewRow("LOT240601001", "PRD-A100", "WAFER", "MEMORY", "DRAM", "2026-06-25 09:12:33", "TRACK_IN", "OP1010"));
            rows.Add(this.NewRow("LOT240601002", "PRD-A101", "WAFER", "MEMORY", "NAND", "2026-06-25 14:05:07", "TRACK_OUT", "OP1020"));
            rows.Add(this.NewRow("LOT240602010", "PRD-B200", "PACKAGE", "LOGIC", "AP", "2026-06-26 08:41:19", "HOLD", "OP2030"));
            rows.Add(this.NewRow("LOT240602011", "PRD-B201", "PACKAGE", "LOGIC", "MODEM", "2026-06-26 10:27:58", "RELEASE", "OP2031"));
            rows.Add(this.NewRow("LOT240603022", "PRD-C300", "TEST", "SENSOR", "CIS", "2026-06-26 16:03:44", "TRACK_IN", "OP3040"));
            return rows;
        }

        private LotGridRow NewRow(
            string lotId,
            string prodId,
            string produceType,
            string prodType,
            string subProdType,
            string lastEventTime,
            string lastEventCd,
            string operId)
        {
            LotGridRow row = new LotGridRow();
            row.LotId = lotId;
            row.ProdId = prodId;
            row.ProduceType = produceType;
            row.ProdType = prodType;
            row.SubProdType = subProdType;
            row.LastEventTime = lastEventTime;
            row.LastEventCd = lastEventCd;
            row.OperId = operId;
            return row;
        }

        private void ShowRows(List<LotGridRow> rows)
        {
            // Reassign a fresh list so the grid regenerates its rows.
            this.lotGrid.ItemsSource = null;
            this.lotGrid.ItemsSource = rows;

            int total = this.allRows.Count;
            int shown = rows.Count;
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
            if (keyword.Length == 0)
            {
                this.ShowRows(this.allRows);
                return;
            }

            List<LotGridRow> matched = new List<LotGridRow>();
            foreach (LotGridRow row in this.allRows)
            {
                if (row.LotId != null && row.LotId.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
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
