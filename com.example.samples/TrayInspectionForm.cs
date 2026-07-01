using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using com.example.Models.Ui;

namespace com.example.Demo
{
    /// <summary>Outcome of the form, read by the caller after ShowDialog.</summary>
    public enum TrayInspectionResult
    {
        None,
        Saved
    }

    /// <summary>
    /// Tray inspection / judge screen (트레이 검사·판정), built as a native WinForms form
    /// with the common <c>ModernDataGrid</c> so it opens and edits in the VS designer.
    /// The judge columns are editable combo cells (single click opens them): SlotNo (1~6),
    /// FingerId (A~E), FingerIdx (Top/Left/Right) and Result (SUCC/FAIL). Every other
    /// column shows the queried result read-only. A card-style footer summarises the row
    /// count and the SUCC / FAIL / Pending judgement tally.
    ///
    /// This is the deliverable carried into production and shown as a popup:
    /// <code>
    /// TrayInspectionForm form = new TrayInspectionForm();
    /// form.LoadFromJson(jsonFromServer);   // fill queried columns; operator judges
    /// if (form.ShowDialog(owner) == DialogResult.OK &amp;&amp; form.Result == TrayInspectionResult.Saved)
    /// {
    ///     // read form.Rows back and send to the server
    /// }
    /// </code>
    /// At home the constructor seeds 6 sample rows via <see cref="LoadSampleData"/>.
    /// </summary>
    public partial class TrayInspectionForm : Form
    {
        private List<TrayInspectionRow> rows;

        /// <summary>What the user did; the caller acts on this after ShowDialog returns.</summary>
        public TrayInspectionResult Result { get; private set; }

        public TrayInspectionForm()
        {
            this.InitializeComponent();
            this.Result = TrayInspectionResult.None;
            this.rows = new List<TrayInspectionRow>();
            this.ConfigureColumns();
            this.ConfigureBadges();
            this.LoadSampleData();
        }

        private void ConfigureColumns()
        {
            // Read-only queried columns + editable judge combos (single click to open).
            // FingerId / FingerIdx apply only to LCC trays — STUB rows show '-' (수정불가).
            // Result is tinted with the badge colors (SUCC green / FAIL red).
            Dictionary<string, string> judgeTones = new Dictionary<string, string>();
            judgeTones["SUCC"] = "success";
            judgeTones["FAIL"] = "danger";

            // Text columns: auto width, centered data. Combo columns: fixed width so the
            // cell does not resize when a value is picked.
            this.inspectionGrid.AddTextColumn("DurableId", "DurableId", null, true);
            this.inspectionGrid.AddTextColumn("TrayType", "TrayType", null, true);
            this.inspectionGrid.AddComboColumn("SlotNo", "SlotNo", new List<string> { "1", "2", "3", "4", "5", "6" }, "110");
            this.inspectionGrid.AddComboColumn("FingerId", "FingerId", new List<string> { "A", "B", "C", "D", "E" }, "TrayType", "STUB", null, "110");
            this.inspectionGrid.AddComboColumn("FingerIdx", "FingerIdx", new List<string> { "Top", "Left", "Right" }, "TrayType", "STUB", null, "110");
            this.inspectionGrid.AddComboColumn("Result", "JudgeResult", new List<string> { "SUCC", "FAIL" }, null, null, judgeTones, "110");
            this.inspectionGrid.AddTextColumn("Timekey", "Timekey", null, true);
            this.inspectionGrid.AddTextColumn("LotId", "LotId", null, true);
            this.inspectionGrid.AddTextColumn("WfId", "WfId", null, true);
            this.inspectionGrid.AddTextColumn("ChipLotId", "ChipLotId", null, true);
            this.inspectionGrid.AddTextColumn("ChipWfId", "ChipWfId", null, true);
            this.inspectionGrid.AddTextColumn("LamellaLotId", "LamellaLotId", null, true);
            this.inspectionGrid.AddTextColumn("LamellaWfId", "LamellaWfId", null, true);
        }

        /// <summary>
        /// The rows shown in the grid, including the operator's judge edits. Read this
        /// after <c>ShowDialog</c> when <see cref="Result"/> is Saved.
        /// </summary>
        public IList<TrayInspectionRow> Rows
        {
            get { return this.rows; }
        }

        private void ConfigureBadges()
        {
            // Same tones as the badge color language: SUCC green / FAIL red / Pending gray.
            this.succBadge.SetTone("success");
            this.failBadge.SetTone("danger");
            this.pendingBadge.SetTone("neutral");
        }

        /// <summary>
        /// Six hard-coded rows standing in for the server query. The judge columns
        /// (SlotNo / FingerId / FingerIdx / Result) are left blank on purpose — the
        /// operator fills them.
        /// </summary>
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

        /// <summary>
        /// Fills the grid from a JSON array (top-level array of objects whose members match
        /// <see cref="TrayInspectionRow"/> property names). The judge members may be empty.
        /// This is the production entry point — call it in place of <see cref="LoadSampleData"/>.
        /// </summary>
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

        /// <summary>Replaces the grid rows with <paramref name="source"/>.</summary>
        public void SetRows(IEnumerable<TrayInspectionRow> source)
        {
            // Detach the summary handler from the old rows before swapping.
            foreach (TrayInspectionRow existing in this.rows)
            {
                existing.PropertyChanged -= this.Row_PropertyChanged;
            }

            List<TrayInspectionRow> next = new List<TrayInspectionRow>();
            if (source != null)
            {
                foreach (TrayInspectionRow row in source)
                {
                    if (row != null)
                    {
                        row.PropertyChanged += this.Row_PropertyChanged;
                        next.Add(row);
                    }
                }
            }

            this.rows = next;
            this.inspectionGrid.ItemsSource = null;
            this.inspectionGrid.ItemsSource = this.rows;
            this.UpdateSummary();
        }

        private void Row_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateSummary();
        }

        /// <summary>Recomputes the footer count + judgement badges from the current rows.</summary>
        private void UpdateSummary()
        {
            int total = this.rows.Count;
            int succ = 0;
            int fail = 0;

            foreach (TrayInspectionRow row in this.rows)
            {
                if (row.JudgeResult == "SUCC")
                {
                    succ++;
                }
                else if (row.JudgeResult == "FAIL")
                {
                    fail++;
                }
            }

            this.countValueLabel.Text = total.ToString();
            this.succBadge.Text = "SUCC " + succ.ToString();
            this.failBadge.Text = "FAIL " + fail.ToString();
            this.pendingBadge.Text = "Pending " + (total - succ - fail).ToString();
        }

        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            TrayInspectionRow firstInvalid;
            List<string> errors = this.CollectValidationErrors(out firstInvalid);
            if (errors.Count > 0)
            {
                // Show the offending row in the grid, then report the missing fields.
                if (firstInvalid != null)
                {
                    this.inspectionGrid.SelectedItem = firstInvalid;
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

        /// <summary>
        /// Validates the operator input on every row: SlotNo and Result are required on
        /// all rows; FingerId and FingerIdx are required only on LCC trays (STUB rows show
        /// '-' and are skipped). Returns one line per incomplete row (capped), and the
        /// first incomplete row via <paramref name="firstInvalid"/>.
        /// </summary>
        private List<string> CollectValidationErrors(out TrayInspectionRow firstInvalid)
        {
            const int MaxLines = 12;
            List<string> errors = new List<string>();
            firstInvalid = null;
            int incomplete = 0;

            foreach (TrayInspectionRow row in this.rows)
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

                if (errors.Count < MaxLines)
                {
                    string label = string.IsNullOrEmpty(row.DurableId) ? "(DurableId 없음)" : row.DurableId;
                    errors.Add(label + " : " + string.Join(", ", missing));
                }
            }

            if (incomplete > errors.Count)
            {
                errors.Add("… 외 " + (incomplete - errors.Count).ToString() + "건");
            }

            return errors;
        }

        private void CloseButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Builds a row with the queried columns filled and the judge columns
        /// (SlotNo / FingerId / FingerIdx / JudgeResult) left blank for the operator.
        /// </summary>
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
