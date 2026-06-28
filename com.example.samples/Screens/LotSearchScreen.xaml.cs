using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using com.example.Models.Ui;

namespace com.example.Controls.Wpf.Screens
{
    /// <summary>
    /// WPF version of the Lot 조회 (lot search) screen, styled to match the other
    /// screens: header → 조회영역(Lot ID + Search) → 그리드영역(Lot 목록) →
    /// 실행영역(Receive Lot). This is a UI shell only; it carries no business logic.
    /// </summary>
    public partial class LotSearchScreen : UserControl
    {
        private readonly List<LotInfo> allLots;
        private readonly ObservableCollection<LotInfo> displayed;

        public LotSearchScreen()
        {
            this.InitializeComponent();
            this.ApplyBackground();
            this.allLots = new List<LotInfo>();
            this.displayed = new ObservableCollection<LotInfo>();
            this.LotGrid.ItemsSource = this.displayed;
            this.LoadSampleData();
            this.ApplyFilter(string.Empty);
        }

        /// <summary>Exposes the rows currently shown in the grid.</summary>
        public ObservableCollection<LotInfo> Displayed
        {
            get { return this.displayed; }
        }

        private void ApplyBackground()
        {
            Brush background = this.TryFindResource("Brush.Background") as Brush;
            if (background != null)
            {
                this.Background = background;
            }
        }

        private void LoadSampleData()
        {
            this.allLots.Add(this.NewLot("LOT240601001", "PRD-A100", "WAFER", "MEMORY", "DRAM",
                new DateTime(2026, 6, 25, 9, 12, 33), "TRACK_IN", "OP1010"));
            this.allLots.Add(this.NewLot("LOT240601002", "PRD-A101", "WAFER", "MEMORY", "NAND",
                new DateTime(2026, 6, 25, 14, 5, 7), "TRACK_OUT", "OP1020"));
            this.allLots.Add(this.NewLot("LOT240602010", "PRD-B200", "PACKAGE", "LOGIC", "AP",
                new DateTime(2026, 6, 26, 8, 41, 19), "HOLD", "OP2030"));
            this.allLots.Add(this.NewLot("LOT240602011", "PRD-B201", "PACKAGE", "LOGIC", "MODEM",
                new DateTime(2026, 6, 26, 10, 27, 58), "RELEASE", "OP2031"));
            this.allLots.Add(this.NewLot("LOT240603022", "PRD-C300", "TEST", "SENSOR", "CIS",
                new DateTime(2026, 6, 26, 16, 3, 44), "TRACK_IN", "OP3040"));
        }

        private LotInfo NewLot(
            string lotId,
            string prodId,
            string produceType,
            string prodType,
            string subProdType,
            DateTime lastEventTime,
            string lastEventCd,
            string operId)
        {
            LotInfo lot = new LotInfo();
            lot.LotId = lotId;
            lot.ProdId = prodId;
            lot.ProduceType = produceType;
            lot.ProdType = prodType;
            lot.SubProdType = subProdType;
            lot.LastEventTime = lastEventTime;
            lot.LastEventCd = lastEventCd;
            lot.OperId = operId;
            return lot;
        }

        private void ApplyFilter(string lotIdFilter)
        {
            string keyword = (lotIdFilter ?? string.Empty).Trim();

            this.displayed.Clear();
            foreach (LotInfo lot in this.allLots)
            {
                if (keyword.Length == 0 ||
                    (lot.LotId != null && lot.LotId.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    this.displayed.Add(lot);
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.ApplyFilter(this.LotIdTextBox.Text);
        }

        private void LotIdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ApplyFilter(this.LotIdTextBox.Text);
            }
        }

        private void ReceiveLotButton_Click(object sender, RoutedEventArgs e)
        {
            LotInfo selected = this.LotGrid.SelectedItem as LotInfo;
            if (selected == null)
            {
                MessageBox.Show(
                    "Receive할 Lot을 그리드에서 선택하세요.",
                    "Receive Lot", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MessageBox.Show(
                "Lot을 Receive 했습니다: " + selected.LotId,
                "Receive Lot", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
