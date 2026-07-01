using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using com.example.Models.Ui;

namespace com.example.Demo
{
    /// <summary>
    /// Master/detail inspection console for tray judgement (트레이 검사 콘솔). An alternative
    /// composition to <see cref="TrayInspectionForm"/>: the operator picks a tray in the
    /// left master grid and judges it in the right detail card with big SUCC/FAIL buttons,
    /// then jumps to the next pending row. A progress bar and a "pending only" filter keep
    /// the throughput visible. Built entirely from common wrapper controls.
    ///
    /// Data flow matches the grid form: home seeds 6 sample rows; at work call
    /// <see cref="LoadFromJson"/> before <c>ShowDialog</c>, read <see cref="Rows"/> back on
    /// <see cref="Result"/> == Saved.
    /// </summary>
    public partial class TrayInspectionConsoleForm : Form
    {
        private List<TrayInspectionRow> allRows;
        private List<TrayInspectionRow> shown;
        private TrayInspectionRow currentRow;
        private bool showPendingOnly;
        private bool suppress;

        /// <summary>What the user did; the caller acts on this after ShowDialog returns.</summary>
        public TrayInspectionResult Result { get; private set; }

        public TrayInspectionConsoleForm()
        {
            this.InitializeComponent();
            this.Result = TrayInspectionResult.None;
            this.allRows = new List<TrayInspectionRow>();
            this.shown = new List<TrayInspectionRow>();

            this.ConfigureMasterColumns();
            this.slotCombo.ItemsSource = new List<string> { "1", "2", "3", "4", "5", "6" };
            this.fingerCombo.ItemsSource = new List<string> { "A", "B", "C", "D", "E" };
            this.idxCombo.ItemsSource = new List<string> { "Top", "Left", "Right" };
            this.masterGrid.SelectionChanged += this.MasterGrid_SelectionChanged;

            this.LoadSampleData();
        }

        /// <summary>The rows including the operator's judgements (read after Save).</summary>
        public IList<TrayInspectionRow> Rows
        {
            get { return this.allRows; }
        }

        private void ConfigureMasterColumns()
        {
            this.masterGrid.AddTextColumn("DurableId", "DurableId", null, true);
            this.masterGrid.AddTextColumn("TrayType", "TrayType", null, true);
            this.masterGrid.AddTextColumn("Slot", "SlotNo", null, true);
            this.masterGrid.AddTextColumn("Finger", "FingerId", null, true);
            this.masterGrid.AddTextColumn("Idx", "FingerIdx", null, true);
            this.masterGrid.AddBadgeColumn("Result", "JudgeDisplay", "JudgeTone", "*");
        }

        /// <summary>Six hard-coded rows standing in for the server query (judge fields blank).</summary>
        public void LoadSampleData()
        {
            List<TrayInspectionRow> sample = new List<TrayInspectionRow>();
            sample.Add(this.CreateQueriedRow("DUR-0001", "LCC", "20260701090000", "LOT24001", "01", "CLOT9001", "03", "LLOT7001", "05"));
            sample.Add(this.CreateQueriedRow("DUR-0002", "LCC", "20260701091500", "LOT24001", "02", "CLOT9001", "04", "LLOT7001", "06"));
            sample.Add(this.CreateQueriedRow("DUR-0003", "STUB", "20260701093000", "LOT24002", "05", "CLOT9002", "07", "LLOT7002", "08"));
            sample.Add(this.CreateQueriedRow("DUR-0004", "LCC", "20260701094500", "LOT24002", "06", "CLOT9002", "09", "LLOT7002", "10"));
            sample.Add(this.CreateQueriedRow("DUR-0005", "STUB", "20260701100000", "LOT24003", "11", "CLOT9003", "12", "LLOT7003", "13"));
            sample.Add(this.CreateQueriedRow("DUR-0006", "LCC", "20260701101500", "LOT24003", "14", "CLOT9003", "15", "LLOT7003", "16"));

            this.SetRows(sample);
        }

        /// <summary>Fills from a JSON array (see <see cref="TrayInspectionRow"/> members).</summary>
        public void LoadFromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                this.SetRows(new List<TrayInspectionRow>());
                return;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<TrayInspectionRow> parsed = serializer.Deserialize<List<TrayInspectionRow>>(json);
            this.SetRows(parsed);
        }

        /// <summary>Replaces the rows and rebinds the console.</summary>
        public void SetRows(IEnumerable<TrayInspectionRow> source)
        {
            this.allRows = new List<TrayInspectionRow>();
            if (source != null)
            {
                foreach (TrayInspectionRow row in source)
                {
                    if (row != null)
                    {
                        this.allRows.Add(row);
                    }
                }
            }

            this.currentRow = null;
            this.showPendingOnly = false;
            this.filterButton.Text = "미판정만 보기";
            this.RebuildShown();
            this.UpdateProgress();
        }

        // ----- Master / detail wiring -----

        private void MasterGrid_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.suppress)
            {
                return;
            }

            this.FlushDetail();
            this.currentRow = this.masterGrid.SelectedItem as TrayInspectionRow;
            this.LoadDetail(this.currentRow);
        }

        private void RebuildShown()
        {
            this.FlushDetail();

            TrayInspectionRow keep = this.currentRow;
            this.shown = new List<TrayInspectionRow>();
            foreach (TrayInspectionRow row in this.allRows)
            {
                if (!this.showPendingOnly || IsPending(row))
                {
                    this.shown.Add(row);
                }
            }

            this.suppress = true;
            this.masterGrid.ItemsSource = null;
            this.masterGrid.ItemsSource = this.shown;
            this.suppress = false;

            TrayInspectionRow select = null;
            if (keep != null && this.shown.Contains(keep))
            {
                select = keep;
            }
            else if (this.shown.Count > 0)
            {
                select = this.shown[0];
            }

            this.currentRow = null;
            if (select != null)
            {
                this.masterGrid.SelectedItem = select;
            }
            else
            {
                this.LoadDetail(null);
            }
        }

        private void LoadDetail(TrayInspectionRow row)
        {
            if (row == null)
            {
                this.detailHeaderLabel.Text = "—";
                this.detailInfoLabel.Text = string.Empty;
                this.detailStatusBadge.Text = "미판정";
                this.detailStatusBadge.SetTone("neutral");
                this.slotCombo.SelectedItem = null;
                this.fingerCombo.SelectedItem = null;
                this.idxCombo.SelectedItem = null;
                this.slotCombo.IsEditorEnabled = false;
                this.fingerCombo.IsEditorEnabled = false;
                this.idxCombo.IsEditorEnabled = false;
                return;
            }

            this.detailHeaderLabel.Text = row.DurableId + "  ·  " + row.TrayType;
            this.detailInfoLabel.Text =
                "Timekey : " + row.Timekey + "\r\n\r\n" +
                "Lot / Wf : " + row.LotId + " / " + row.WfId + "\r\n" +
                "Chip Lot / Wf : " + row.ChipLotId + " / " + row.ChipWfId + "\r\n" +
                "Lamella Lot / Wf : " + row.LamellaLotId + " / " + row.LamellaWfId;

            this.detailStatusBadge.Text = StatusText(row.JudgeResult);
            this.detailStatusBadge.SetTone(ToneFor(row.JudgeResult));

            bool isLcc = row.TrayType == "LCC";
            this.slotCombo.IsEditorEnabled = true;
            this.fingerCombo.IsEditorEnabled = isLcc;
            this.idxCombo.IsEditorEnabled = isLcc;

            this.slotCombo.SelectedItem = string.IsNullOrEmpty(row.SlotNo) ? null : row.SlotNo;
            this.fingerCombo.SelectedItem = (isLcc && !string.IsNullOrEmpty(row.FingerId)) ? row.FingerId : null;
            this.idxCombo.SelectedItem = (isLcc && !string.IsNullOrEmpty(row.FingerIdx)) ? row.FingerIdx : null;

            this.fingerCombo.Title = isLcc ? "FingerId" : "FingerId (N/A)";
            this.idxCombo.Title = isLcc ? "FingerIdx" : "FingerIdx (N/A)";
        }

        /// <summary>Writes the detail combo values back to the current row.</summary>
        private void FlushDetail()
        {
            if (this.currentRow == null)
            {
                return;
            }

            string slot = this.slotCombo.SelectedItem as string;
            this.currentRow.SlotNo = slot ?? string.Empty;

            if (this.currentRow.TrayType == "LCC")
            {
                string finger = this.fingerCombo.SelectedItem as string;
                string idx = this.idxCombo.SelectedItem as string;
                this.currentRow.FingerId = finger ?? string.Empty;
                this.currentRow.FingerIdx = idx ?? string.Empty;
            }
        }

        // ----- Judge / navigate -----

        private void SuccButton_Click(object sender, System.EventArgs e)
        {
            this.Judge("SUCC");
        }

        private void FailButton_Click(object sender, System.EventArgs e)
        {
            this.Judge("FAIL");
        }

        private void Judge(string result)
        {
            if (this.currentRow == null)
            {
                return;
            }

            this.FlushDetail();

            List<string> missing = this.MissingInputs(this.currentRow);
            if (missing.Count > 0)
            {
                MessageBox.Show(
                    this,
                    "판정 전에 입력이 필요합니다: " + string.Join(", ", missing),
                    "입력 필요",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            this.currentRow.JudgeResult = result;
            this.detailStatusBadge.Text = StatusText(result);
            this.detailStatusBadge.SetTone(ToneFor(result));
            this.UpdateProgress();
            this.SelectNextPending();
        }

        private void PrevButton_Click(object sender, System.EventArgs e)
        {
            int index = this.shown.IndexOf(this.currentRow);
            if (index > 0)
            {
                this.masterGrid.SelectedItem = this.shown[index - 1];
            }
        }

        private void NextButton_Click(object sender, System.EventArgs e)
        {
            this.SelectNextPending();
        }

        private void SelectNextPending()
        {
            int start = this.shown.IndexOf(this.currentRow) + 1;
            TrayInspectionRow next = this.FindPending(start);
            if (next == null)
            {
                next = this.FindPending(0);
            }

            if (next != null && next != this.currentRow)
            {
                this.masterGrid.SelectedItem = next;
            }
        }

        private TrayInspectionRow FindPending(int from)
        {
            for (int i = System.Math.Max(0, from); i < this.shown.Count; i++)
            {
                if (IsPending(this.shown[i]))
                {
                    return this.shown[i];
                }
            }

            return null;
        }

        // ----- Filter / progress -----

        private void FilterButton_Click(object sender, System.EventArgs e)
        {
            this.showPendingOnly = !this.showPendingOnly;
            this.filterButton.Text = this.showPendingOnly ? "전체 보기" : "미판정만 보기";
            this.RebuildShown();
        }

        private void UpdateProgress()
        {
            int total = this.allRows.Count;
            int judged = 0;
            foreach (TrayInspectionRow row in this.allRows)
            {
                if (!IsPending(row))
                {
                    judged++;
                }
            }

            this.progressBar.Maximum = total < 1 ? 1 : total;
            this.progressBar.Value = judged;
            this.progressBar.Title = "진행 " + judged.ToString() + " / " + total.ToString();
            this.countLabel.Text = "총 " + total.ToString() + "건 · 미판정 " + (total - judged).ToString();
        }

        // ----- Save -----

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            this.FlushDetail();

            TrayInspectionRow firstInvalid = null;
            List<string> errors = new List<string>();
            int incomplete = 0;
            foreach (TrayInspectionRow row in this.allRows)
            {
                List<string> missing = this.MissingInputs(row);
                if (string.IsNullOrEmpty(row.JudgeResult))
                {
                    missing.Add("Result");
                }

                if (missing.Count == 0)
                {
                    continue;
                }

                incomplete++;
                if (firstInvalid == null)
                {
                    firstInvalid = row;
                }

                if (errors.Count < 12)
                {
                    errors.Add(row.DurableId + " : " + string.Join(", ", missing));
                }
            }

            if (errors.Count > 0)
            {
                if (incomplete > errors.Count)
                {
                    errors.Add("… 외 " + (incomplete - errors.Count).ToString() + "건");
                }

                if (firstInvalid != null)
                {
                    if (this.showPendingOnly && !this.shown.Contains(firstInvalid))
                    {
                        this.showPendingOnly = false;
                        this.filterButton.Text = "미판정만 보기";
                        this.RebuildShown();
                    }

                    this.masterGrid.SelectedItem = firstInvalid;
                }

                MessageBox.Show(
                    this,
                    "입력 항목을 확인하세요.\r\n\r\n" + string.Join("\r\n", errors),
                    "입력 확인",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            this.Result = TrayInspectionResult.Saved;
            this.Close();
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                this.Judge("SUCC");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F)
            {
                this.Judge("FAIL");
                e.Handled = true;
            }
        }

        // ----- Helpers -----

        /// <summary>Required inputs missing on a row: SlotNo (all), FingerId/FingerIdx (LCC only).</summary>
        private List<string> MissingInputs(TrayInspectionRow row)
        {
            List<string> missing = new List<string>();
            if (string.IsNullOrEmpty(row.SlotNo))
            {
                missing.Add("SlotNo");
            }

            if (row.TrayType == "LCC")
            {
                if (string.IsNullOrEmpty(row.FingerId))
                {
                    missing.Add("FingerId");
                }

                if (string.IsNullOrEmpty(row.FingerIdx))
                {
                    missing.Add("FingerIdx");
                }
            }

            return missing;
        }

        private static bool IsPending(TrayInspectionRow row)
        {
            return string.IsNullOrEmpty(row.JudgeResult);
        }

        private static string StatusText(string judgeResult)
        {
            if (judgeResult == "SUCC" || judgeResult == "FAIL")
            {
                return judgeResult;
            }

            return "미판정";
        }

        private static string ToneFor(string judgeResult)
        {
            if (judgeResult == "SUCC")
            {
                return "success";
            }

            if (judgeResult == "FAIL")
            {
                return "danger";
            }

            return "neutral";
        }

        private TrayInspectionRow CreateQueriedRow(
            string durableId,
            string trayType,
            string timekey,
            string lotId,
            string wfId,
            string chipLotId,
            string chipWfId,
            string lamellaLotId,
            string lamellaWfId)
        {
            TrayInspectionRow row = new TrayInspectionRow();
            row.DurableId = durableId;
            row.TrayType = trayType;
            row.Timekey = timekey;
            row.LotId = lotId;
            row.WfId = wfId;
            row.ChipLotId = chipLotId;
            row.ChipWfId = chipWfId;
            row.LamellaLotId = lamellaLotId;
            row.LamellaWfId = lamellaWfId;
            return row;
        }
    }
}
