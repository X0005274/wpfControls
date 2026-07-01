using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using com.example.Controls.Wpf.Data;

namespace com.example.WinForms.Controls.Data
{
    /// <summary>
    /// 모던 스타일을 입힌 FarPoint Spread 8 기반 데이터 그리드입니다. 회사에서 쓰는
    /// Spread 라이선스가 있을 때만 컴파일됩니다(환경변수 <c>FARPOINT_SPREAD8_HOME</c>).
    /// <see cref="ModernDataGrid"/>(WPF 기반)와 같은 API 표면
    /// (AddTextColumn / AddComboColumn / ItemsSource / SelectedItem / SelectionChanged /
    /// IsReadOnly)을 제공해, 화면 코드를 거의 그대로 두고 그리드만 교체할 수 있게 했습니다.
    ///
    /// ⚠️ 이 파일은 Spread SDK 없이 표준 Spread 8 API 기준으로 작성되었습니다. 회사에서
    /// 실제 버전에 맞춰 셀타입/스타일 프로퍼티명(예: SelectionBackColor, GridLineColor,
    /// AlternatingRows)을 한 번 확인·조정하세요. 나머지 구성(조건부 컴파일, 컬럼/바인딩
    /// 흐름)은 그대로 사용 가능합니다.
    /// </summary>
    [ToolboxItem(true)]
    public class ModernSpreadGrid : UserControl
    {
        // Design tokens as GDI colors (mirrors Themes/Tokens.xaml).
        private static readonly Color HeaderBackColor = Color.FromArgb(0xF3, 0xF4, 0xF6);
        private static readonly Color HeaderForeColor = Color.FromArgb(0x6B, 0x72, 0x80);
        private static readonly Color GridLineColor = Color.FromArgb(0xE5, 0xE7, 0xEB);
        private static readonly Color AltRowColor = Color.FromArgb(0xF9, 0xFA, 0xFB);
        private static readonly Color SelectionBackColor = Color.FromArgb(0xE6, 0xF2, 0xFB);
        private static readonly Color TextColor = Color.FromArgb(0x11, 0x18, 0x27);

        private readonly FpSpread spread;
        private readonly List<ModernDataGridColumn> columnDefinitions;
        private IEnumerable itemsSource;
        private bool isReadOnly;

        /// <summary>선택된 행이 바뀔 때 발생합니다. (ModernDataGrid 와 동일한 이벤트 표면)</summary>
        public event EventHandler SelectionChanged;

        public ModernSpreadGrid()
        {
            this.columnDefinitions = new List<ModernDataGridColumn>();
            this.isReadOnly = true;
            this.Size = new Size(560, 320);

            this.spread = new FpSpread();
            this.spread.Dock = DockStyle.Fill;
            this.Controls.Add(this.spread);

            this.ApplyModernStyle();

            // Row click → surface a standard SelectionChanged. (Spread also raises its own
            // SelectionChanged; CellClick is used here as the most version-stable hook.)
            this.spread.CellClick += this.Spread_CellClick;
        }

        private SheetView Sheet
        {
            get { return this.spread.Sheets.Count > 0 ? this.spread.Sheets[0] : null; }
        }

        /// <summary>목록에 표시할 데이터(코드에서 설정).</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable ItemsSource
        {
            get { return this.itemsSource; }
            set
            {
                this.itemsSource = value;
                this.Rebind();
            }
        }

        /// <summary>현재 선택된 항목(바인딩된 데이터 객체).</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get
            {
                SheetView sheet = this.Sheet;
                IList list = this.itemsSource as IList;
                if (sheet == null || list == null)
                {
                    return null;
                }

                int row = sheet.ActiveRowIndex;
                if (row < 0 || row >= list.Count)
                {
                    return null;
                }

                return list[row];
            }

            set
            {
                SheetView sheet = this.Sheet;
                IList list = this.itemsSource as IList;
                if (sheet == null || list == null || value == null)
                {
                    return;
                }

                int index = list.IndexOf(value);
                if (index >= 0)
                {
                    sheet.SetActiveCell(index, sheet.ActiveColumnIndex < 0 ? 0 : sheet.ActiveColumnIndex);
                    this.OnSelectionChanged();
                }
            }
        }

        /// <summary>읽기 전용 여부. 콤보 컬럼은 이 값과 무관하게 편집 가능합니다.</summary>
        [Category("모던 컨트롤")]
        [Description("읽기 전용 여부")]
        [DefaultValue(true)]
        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
            set
            {
                this.isReadOnly = value;
                this.Rebind();
            }
        }

        /// <summary>텍스트 컬럼을 추가합니다.</summary>
        public void AddTextColumn(string header, string dataField)
        {
            this.AddTextColumn(header, dataField, null, false);
        }

        /// <summary>너비/가운데 정렬을 지정해 텍스트 컬럼을 추가합니다. (너비: 픽셀 숫자 또는 null=자동)</summary>
        public void AddTextColumn(string header, string dataField, string width, bool centerText)
        {
            ModernDataGridColumn column = new ModernDataGridColumn();
            column.Header = header;
            column.Binding = dataField;
            column.Width = width;
            column.CenterText = centerText;
            this.columnDefinitions.Add(column);
            this.Rebind();
        }

        /// <summary>편집 가능한 콤보 컬럼을 추가합니다.</summary>
        public void AddComboColumn(string header, string dataField, IEnumerable<string> options)
        {
            this.AddComboColumn(header, dataField, options, null);
        }

        /// <summary>너비를 지정해 콤보 컬럼을 추가합니다.</summary>
        public void AddComboColumn(string header, string dataField, IEnumerable<string> options, string width)
        {
            ModernDataGridColumn column = new ModernDataGridColumn();
            column.Header = header;
            column.Binding = dataField;
            column.Width = width;
            column.ComboOptions = new List<string>(options);
            this.columnDefinitions.Add(column);
            this.Rebind();
        }

        /// <summary>지정한 컬럼을 모두 지웁니다.</summary>
        public void ClearColumns()
        {
            this.columnDefinitions.Clear();
            this.Rebind();
        }

        // ----- Styling -----

        private void ApplyModernStyle()
        {
            this.spread.BackColor = Color.White;
            this.spread.BorderStyle = BorderStyle.None;
            this.spread.GrayAreaBackColor = Color.White;

            SheetView sheet = this.Sheet;
            if (sheet == null)
            {
                return;
            }

            sheet.RowHeader.Visible = false;
            sheet.GridLineColor = GridLineColor;
            sheet.SelectionBackColor = SelectionBackColor;
            sheet.SelectionForeColor = TextColor;

            // Data rows
            sheet.DefaultStyle.BackColor = Color.White;
            sheet.DefaultStyle.ForeColor = TextColor;
            sheet.DefaultStyle.Font = new Font("Segoe UI", 9F);
            sheet.DefaultStyle.VerticalAlignment = CellVerticalAlignment.Center;
            sheet.Rows.Default.Height = 32F;

            // Column header
            sheet.ColumnHeader.DefaultStyle.BackColor = HeaderBackColor;
            sheet.ColumnHeader.DefaultStyle.ForeColor = HeaderForeColor;
            sheet.ColumnHeader.DefaultStyle.Font = new Font("Segoe UI Semibold", 9F);
            sheet.ColumnHeader.Rows[0].Height = 34F;

            // Alternating rows (banded)
            AlternatingRow band = sheet.AlternatingRows.Add();
            band.BackColor = AltRowColor;
        }

        // ----- Data / columns -----

        private void Rebind()
        {
            SheetView sheet = this.Sheet;
            if (sheet == null)
            {
                return;
            }

            this.spread.SuspendLayout();
            try
            {
                sheet.AutoGenerateColumns = false;
                sheet.DataSource = null;

                sheet.ColumnCount = this.columnDefinitions.Count;
                for (int i = 0; i < this.columnDefinitions.Count; i++)
                {
                    ModernDataGridColumn definition = this.columnDefinitions[i];
                    Column column = sheet.Columns[i];
                    column.Label = definition.Header;
                    column.DataField = definition.Binding;
                    column.HorizontalAlignment = definition.CenterText
                        ? CellHorizontalAlignment.Center
                        : CellHorizontalAlignment.Left;

                    if (definition.IsCombo)
                    {
                        ComboBoxCellType combo = new ComboBoxCellType();
                        combo.Items = ToArray(definition.ComboOptions);
                        combo.Editable = false;
                        column.CellType = combo;
                        column.Locked = false; // combos stay editable even when read-only
                    }
                    else
                    {
                        column.CellType = new TextCellType();
                        column.Locked = true;
                    }

                    double pixels;
                    if (!string.IsNullOrEmpty(definition.Width) && double.TryParse(definition.Width, out pixels))
                    {
                        column.Width = (float)pixels;
                    }
                    else
                    {
                        column.Width = Math.Max(80F, column.Width);
                    }
                }

                // Protect enforces per-column Locked flags (text locked, combos editable).
                sheet.Protect = true;

                if (this.itemsSource != null)
                {
                    sheet.DataSource = this.itemsSource;
                }
            }
            finally
            {
                this.spread.ResumeLayout();
            }
        }

        private void Spread_CellClick(object sender, CellClickEventArgs e)
        {
            // Column-header clicks report negative row; ignore those.
            if (e.Row >= 0)
            {
                this.OnSelectionChanged();
            }
        }

        private void OnSelectionChanged()
        {
            EventHandler handler = this.SelectionChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private static string[] ToArray(IList<string> options)
        {
            if (options == null)
            {
                return new string[0];
            }

            string[] result = new string[options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                result[i] = options[i];
            }

            return result;
        }
    }
}
