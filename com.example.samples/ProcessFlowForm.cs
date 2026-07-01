using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace com.example.Demo
{
    /// <summary>
    /// Work-flow console — the single starting point that walks a request through the line.
    /// It opens on the 의뢰서 목록 (request list, carrying each request's LOT info); the operator
    /// picks a request and presses LOT 생성 to create the LOT from it, then works the stages
    /// FOUP EXCHANGE → LASER SAW → 4"FIB → TEM, each with 작업준비 / 작업시작 / 작업종료.
    /// Stage rules: FOUP output port takes an empty FOUP; LASER SAW output is fixed to port 3
    /// with a Tray and creates Chips on end; 4"FIB / TEM use a single In/Out port with a Tray,
    /// and FIB creates Lamella on end. UI shell only (mock data).
    /// </summary>
    public partial class ProcessFlowForm : Form
    {
        private const int StageRequest = 0;
        private const int StageLot = 1;
        private const int StageFoup = 2;
        private const int StageSaw = 3;
        private const int StageFib = 4;
        private const int StageTem = 5;

        private enum StageState
        {
            Pending,
            Prepared,
            Running,
            Done
        }

        private readonly List<FlowLogRow> log = new List<FlowLogRow>();
        private readonly List<RequestRow> requests = new List<RequestRow>();
        private readonly List<string> ports = new List<string> { "P1", "P2", "P3", "P4" };
        private readonly List<string> emptyFoups = new List<string> { "FOUP-E01", "FOUP-E02", "FOUP-E03" };
        private readonly List<string> trays = new List<string> { "TRAY-01", "TRAY-02", "TRAY-03", "TRAY-04", "TRAY-05" };

        private int currentStage;
        private StageState stageState;
        private int chipCount;
        private int lamellaCount;
        private int lotSeq;
        private int waferQty;
        private string lotId = string.Empty;
        private RequestRow selectedRequest;

        private bool suppressDialogs;
        private string captureDir;

        public ProcessFlowForm()
        {
            this.InitializeComponent();

            this.stepIndicator.ItemsSource = new List<string>
            {
                "의뢰서", "LOT 생성", "FOUP EXCHANGE", "LASER SAW", "4\"FIB", "TEM"
            };

            this.requestGrid.AddTextColumn("의뢰번호", "RequestId", "140");
            this.requestGrid.AddTextColumn("제품", "Product", "140");
            this.requestGrid.AddTextColumn("Wafer", "WaferQty", "90", true);
            this.requestGrid.AddTextColumn("공정", "Process", "220");
            this.requestGrid.AddTextColumn("의뢰일", "RequestDate", "140");
            this.requestGrid.AddTextColumn("상태", "Status", "*", true);
            this.LoadRequests();
            this.requestGrid.SelectionChanged += this.RequestGrid_SelectionChanged;

            this.logGrid.AddTextColumn("시간", "Time", "90");
            this.logGrid.AddTextColumn("단계", "Stage", "150");
            this.logGrid.AddTextColumn("이벤트", "Event", "120");
            this.logGrid.AddTextColumn("상세", "Detail", "*");
            this.logGrid.ItemsSource = this.log;

            this.GoToRequestPhase();
        }

        private void LoadRequests()
        {
            this.requests.Add(this.NewRequest("REQ-2026-001", "H5G32-A", "25", "SAW→FIB→TEM", "2026-07-01"));
            this.requests.Add(this.NewRequest("REQ-2026-002", "H5G32-B", "25", "SAW→FIB→TEM", "2026-07-01"));
            this.requests.Add(this.NewRequest("REQ-2026-003", "K7A11-C", "13", "SAW→FIB→TEM", "2026-07-02"));
            this.requests.Add(this.NewRequest("REQ-2026-004", "K7A11-D", "6", "SAW→FIB", "2026-07-02"));
            this.requestGrid.ItemsSource = this.requests;
        }

        private RequestRow NewRequest(string id, string product, string qty, string process, string date)
        {
            RequestRow row = new RequestRow();
            row.RequestId = id;
            row.Product = product;
            row.WaferQty = qty;
            row.Process = process;
            row.RequestDate = date;
            row.Status = "대기";
            return row;
        }

        private void GoToRequestPhase()
        {
            this.currentStage = StageRequest;
            this.stepIndicator.CurrentStep = StageRequest;
            this.createLotButton.IsButtonEnabled = true;

            this.requestGrid.Visible = true;
            this.stageCard.Visible = false;
            this.chipCard.Visible = false;
            this.lamellaCard.Visible = false;
            this.lotLabel.Text = "LOT: -   (의뢰서를 선택하세요)";
        }

        private void RequestGrid_SelectionChanged(object sender, EventArgs e)
        {
            this.selectedRequest = this.requestGrid.SelectedItem as RequestRow;
            if (this.selectedRequest != null && this.currentStage == StageRequest)
            {
                this.stepIndicator.CurrentStep = StageLot;
                this.lotLabel.Text = "LOT: -   (선택: " + this.selectedRequest.RequestId + " · " + this.selectedRequest.Product + " · " + this.selectedRequest.WaferQty + "W)";
            }
        }

        private void CreateLotButton_Click(object sender, EventArgs e)
        {
            if (this.selectedRequest == null)
            {
                this.Warn("의뢰서 목록에서 하나를 선택하세요.", "LOT 생성");
                return;
            }

            this.lotSeq++;
            this.lotId = "LOT-20260702-" + this.lotSeq.ToString("D3");
            int qty;
            this.waferQty = int.TryParse(this.selectedRequest.WaferQty, out qty) ? qty : 0;
            this.selectedRequest.Status = "진행중";

            this.lotLabel.Text = "LOT: " + this.lotId + "   (" + this.selectedRequest.RequestId + " · " + this.selectedRequest.Product + " · " + this.waferQty.ToString() + "W)";
            this.createLotButton.IsButtonEnabled = false;
            this.requestGrid.Visible = false;
            this.stageCard.Visible = true;
            this.chipCard.Visible = true;
            this.lamellaCard.Visible = true;

            this.AddLog("LOT 생성", "생성", this.lotId + " ← " + this.selectedRequest.RequestId + " (" + this.selectedRequest.Product + ", " + this.waferQty.ToString() + "W)");
            this.EnterStage(StageFoup);
        }

        private void EnterStage(int stage)
        {
            this.currentStage = stage;
            this.stageState = StageState.Pending;
            this.stepIndicator.CurrentStep = stage;
            this.ConfigureStagePanel(stage);
            this.UpdateButtons();
        }

        private void ConfigureStagePanel(int stage)
        {
            this.comboA.Visible = true;
            this.comboC.Visible = true;
            this.comboA.IsEditorEnabled = true;
            this.comboB.IsEditorEnabled = true;
            this.comboC.IsEditorEnabled = true;
            this.comboA.SelectedItem = null;
            this.comboB.SelectedItem = null;
            this.comboC.SelectedItem = null;

            if (stage == StageFoup)
            {
                this.stageTitleLabel.Text = "FOUP EXCHANGE";
                this.comboA.Title = "Input Port";
                this.comboA.ItemsSource = new List<string>(this.ports);
                this.comboB.Visible = true;
                this.comboB.Title = "Output Port";
                this.comboB.ItemsSource = new List<string>(this.ports);
                this.comboC.Title = "빈 FOUP (출력)";
                this.comboC.ItemsSource = new List<string>(this.emptyFoups);
                this.hintLabel.Text = "입력/출력 포트를 지정하고, 출력 포트에는 빈 FOUP을 선택합니다.";
            }
            else if (stage == StageSaw)
            {
                this.stageTitleLabel.Text = "LASER SAW";
                this.comboA.Title = "Input Port";
                this.comboA.ItemsSource = new List<string>(this.ports);
                this.comboB.Visible = true;
                this.comboB.Title = "Output Port (3 고정)";
                this.comboB.ItemsSource = new List<string> { "3" };
                this.comboB.SelectedItem = "3";
                this.comboB.IsEditorEnabled = false;
                this.comboC.Title = "Tray (출력)";
                this.comboC.ItemsSource = new List<string>(this.trays);
                this.hintLabel.Text = "입력 포트를 선택합니다. 출력은 3번 고정이며 출력 포트에 Tray를 지정합니다. 작업종료 시 Chip이 생성됩니다.";
            }
            else if (stage == StageFib)
            {
                this.stageTitleLabel.Text = "4\"FIB";
                this.comboA.Title = "In/Out Port";
                this.comboA.ItemsSource = new List<string>(this.ports);
                this.comboB.Visible = false;
                this.comboC.Title = "Tray (In=Out)";
                this.comboC.ItemsSource = new List<string>(this.trays);
                this.hintLabel.Text = "단일 In/Out 포트로 Tray가 들어가고 Tray로 나옵니다. 작업종료 시 Lamella가 생성됩니다.";
            }
            else if (stage == StageTem)
            {
                this.stageTitleLabel.Text = "TEM";
                this.comboA.Title = "In/Out Port";
                this.comboA.ItemsSource = new List<string>(this.ports);
                this.comboB.Visible = false;
                this.comboC.Title = "Tray (In=Out)";
                this.comboC.ItemsSource = new List<string>(this.trays);
                this.hintLabel.Text = "단일 In/Out 포트로 Tray가 들어가고 Tray로 나옵니다.";
            }
        }

        private void UpdateButtons()
        {
            this.prepButton.IsButtonEnabled = this.stageState == StageState.Pending;
            this.startButton.IsButtonEnabled = this.stageState == StageState.Prepared;
            this.endButton.IsButtonEnabled = this.stageState == StageState.Running;

            if (this.stageState == StageState.Pending)
            {
                this.stageStatusBadge.Text = "대기";
                this.stageStatusBadge.SetTone("neutral");
            }
            else if (this.stageState == StageState.Prepared)
            {
                this.stageStatusBadge.Text = "준비됨";
                this.stageStatusBadge.SetTone("neutral");
            }
            else if (this.stageState == StageState.Running)
            {
                this.stageStatusBadge.Text = "진행중";
                this.stageStatusBadge.SetTone("neutral");
            }
            else
            {
                this.stageStatusBadge.Text = "완료";
                this.stageStatusBadge.SetTone("success");
            }
        }

        private void PrepButton_Click(object sender, EventArgs e)
        {
            List<string> missing = new List<string>();
            if (this.GetSel(this.comboA).Length == 0)
            {
                missing.Add(this.comboA.Title);
            }

            if (this.comboB.Visible && this.GetSel(this.comboB).Length == 0)
            {
                missing.Add(this.comboB.Title);
            }

            if (this.comboC.Visible && this.GetSel(this.comboC).Length == 0)
            {
                missing.Add(this.comboC.Title);
            }

            if (missing.Count > 0)
            {
                this.Warn("입력이 필요합니다: " + string.Join(", ", missing), "작업준비");
                return;
            }

            this.stageState = StageState.Prepared;
            this.AddLog(this.stageTitleLabel.Text, "작업준비", this.PrepDetail());
            this.UpdateButtons();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (this.stageState != StageState.Prepared)
            {
                return;
            }

            this.stageState = StageState.Running;
            this.AddLog(this.stageTitleLabel.Text, "작업시작", this.PrepDetail());
            this.UpdateButtons();
        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            if (this.stageState != StageState.Running)
            {
                return;
            }

            this.stageState = StageState.Done;
            string stageName = this.stageTitleLabel.Text;

            if (this.currentStage == StageSaw)
            {
                int made = this.waferQty > 0 ? this.waferQty : 24;
                this.chipCount += made;
                this.chipValue.Text = this.chipCount.ToString();
                this.AddLog(stageName, "작업종료", "Chip " + made.ToString() + "EA 생성 (" + this.GetSel(this.comboC) + ")");
            }
            else if (this.currentStage == StageFib)
            {
                int made = this.waferQty > 0 ? System.Math.Max(1, this.waferQty / 3) : 8;
                this.lamellaCount += made;
                this.lamellaValue.Text = this.lamellaCount.ToString();
                this.AddLog(stageName, "작업종료", "Lamella " + made.ToString() + "EA 생성 (" + this.GetSel(this.comboC) + ")");
            }
            else
            {
                this.AddLog(stageName, "작업종료", this.PrepDetail());
            }

            if (this.currentStage < StageTem)
            {
                this.EnterStage(this.currentStage + 1);
            }
            else
            {
                this.UpdateButtons();
                this.prepButton.IsButtonEnabled = false;
                this.startButton.IsButtonEnabled = false;
                this.endButton.IsButtonEnabled = false;
                if (this.selectedRequest != null)
                {
                    this.selectedRequest.Status = "완료";
                }

                this.hintLabel.Text = "모든 단계가 완료되었습니다. (LOT " + this.lotId + ")";
                this.Info("전체 작업이 완료되었습니다.\r\nChip " + this.chipCount.ToString() + "EA · Lamella " + this.lamellaCount.ToString() + "EA", "완료");
            }
        }

        // ----- Helpers -----

        private string PrepDetail()
        {
            string detail = this.comboA.Title + "=" + this.GetSel(this.comboA);
            if (this.comboB.Visible)
            {
                detail += ", " + this.comboB.Title + "=" + this.GetSel(this.comboB);
            }

            if (this.comboC.Visible)
            {
                detail += ", " + this.comboC.Title + "=" + this.GetSel(this.comboC);
            }

            return detail;
        }

        private void AddLog(string stage, string ev, string detail)
        {
            FlowLogRow row = new FlowLogRow();
            row.Time = DateTime.Now.ToString("HH:mm:ss");
            row.Stage = stage;
            row.Event = ev;
            row.Detail = detail;
            this.log.Insert(0, row);

            this.logGrid.ItemsSource = null;
            this.logGrid.ItemsSource = this.log;
        }

        private string GetSel(com.example.WinForms.Controls.Selection.ModernComboBox combo)
        {
            string value = combo.SelectedItem as string;
            return value ?? string.Empty;
        }

        private void Warn(string text, string caption)
        {
            if (this.suppressDialogs)
            {
                return;
            }

            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Info(string text, string caption)
        {
            if (this.suppressDialogs)
            {
                return;
            }

            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ----- Screenshot harness (`--shots <dir>`): drive the flow and capture each stage -----

        /// <summary>Arms capture mode: on Shown, runs the scripted flow and saves a PNG per stage.</summary>
        public void EnableCaptureMode(string outDir)
        {
            this.captureDir = outDir;
            this.suppressDialogs = true;
            this.Shown += delegate { this.BeginInvoke(new Action(this.RunCaptureScript)); };
        }

        private void RunCaptureScript()
        {
            Directory.CreateDirectory(this.captureDir);
            this.TopMost = true;
            this.Activate();

            this.Pump();
            this.Capture("01-request-list");

            this.requestGrid.SelectedItem = this.requests[0];
            this.Pump();
            this.CreateLotButton_Click(null, EventArgs.Empty);
            this.Pump();
            this.Capture("02-foup-exchange");

            this.comboA.SelectedItem = "P1";
            this.comboB.SelectedItem = "P2";
            this.comboC.SelectedItem = "FOUP-E01";
            this.Pump();
            this.PrepButton_Click(null, EventArgs.Empty);
            this.StartButton_Click(null, EventArgs.Empty);
            this.Pump();
            this.Capture("03-foup-running");
            this.EndButton_Click(null, EventArgs.Empty);
            this.Pump();

            // LASER SAW
            this.comboA.SelectedItem = "P1";
            this.comboC.SelectedItem = "TRAY-01";
            this.Pump();
            this.PrepButton_Click(null, EventArgs.Empty);
            this.StartButton_Click(null, EventArgs.Empty);
            this.Pump();
            this.Capture("04-laser-saw");
            this.EndButton_Click(null, EventArgs.Empty);
            this.Pump();
            this.Capture("05-fib-after-chip");

            // 4"FIB
            this.comboA.SelectedItem = "P1";
            this.comboC.SelectedItem = "TRAY-02";
            this.Pump();
            this.PrepButton_Click(null, EventArgs.Empty);
            this.StartButton_Click(null, EventArgs.Empty);
            this.EndButton_Click(null, EventArgs.Empty);
            this.Pump();
            this.Capture("06-tem-after-lamella");

            // TEM
            this.comboA.SelectedItem = "P2";
            this.comboC.SelectedItem = "TRAY-03";
            this.Pump();
            this.PrepButton_Click(null, EventArgs.Empty);
            this.StartButton_Click(null, EventArgs.Empty);
            this.EndButton_Click(null, EventArgs.Empty);
            this.Pump();
            this.Capture("07-complete");

            this.Close();
        }

        private void Pump()
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(450);
            Application.DoEvents();
        }

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        private const uint PwRenderFullContent = 0x00000002;

        private void Capture(string name)
        {
            Application.DoEvents();
            System.Threading.Thread.Sleep(150);

            using (Bitmap bmp = new Bitmap(this.Width, this.Height))
            {
                // 1) WinForms chrome (title, labels, card surfaces) via PrintWindow. This
                //    leaves the ElementHost (WPF-airspace) regions blank.
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    IntPtr hdc = g.GetHdc();
                    try
                    {
                        PrintWindow(this.Handle, hdc, PwRenderFullContent);
                    }
                    finally
                    {
                        g.ReleaseHdc(hdc);
                    }
                }

                // 2) Overlay each hosted WPF control rendered via RenderTargetBitmap (pure
                //    WPF render — headless-safe), positioned at its offset in the window.
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    List<System.Windows.Forms.Integration.ElementHost> hosts = new List<System.Windows.Forms.Integration.ElementHost>();
                    this.CollectHosts(this, hosts);
                    foreach (System.Windows.Forms.Integration.ElementHost host in hosts)
                    {
                        if (!host.Visible || host.Width <= 0 || host.Height <= 0)
                        {
                            continue;
                        }

                        System.Windows.UIElement child = host.Child;
                        if (child == null)
                        {
                            continue;
                        }

                        System.Windows.Media.Imaging.RenderTargetBitmap rtb =
                            new System.Windows.Media.Imaging.RenderTargetBitmap(
                                host.Width, host.Height, 96, 96, System.Windows.Media.PixelFormats.Pbgra32);
                        rtb.Render(child);

                        System.Windows.Media.Imaging.PngBitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                        encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            encoder.Save(ms);
                            ms.Position = 0;
                            using (Bitmap childBmp = new Bitmap(ms))
                            {
                                Point screen = host.PointToScreen(Point.Empty);
                                g.DrawImage(childBmp, screen.X - this.Bounds.Left, screen.Y - this.Bounds.Top);
                            }
                        }
                    }
                }

                bmp.Save(Path.Combine(this.captureDir, name + ".png"), ImageFormat.Png);
            }
        }

        private void CollectHosts(Control parent, List<System.Windows.Forms.Integration.ElementHost> acc)
        {
            foreach (Control child in parent.Controls)
            {
                System.Windows.Forms.Integration.ElementHost host = child as System.Windows.Forms.Integration.ElementHost;
                if (host != null)
                {
                    acc.Add(host);
                }
                else
                {
                    this.CollectHosts(child, acc);
                }
            }
        }
    }
}
