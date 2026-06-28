using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using com.example.Controls.Wpf.Data;
using com.example.WinForms.Controls.Hosting;

namespace com.example.WinForms.Controls.Data
{
    /// <summary>
    /// 데이터 그리드 컨트롤입니다. WinForms 폼의 도구 상자에서 드래그해 사용합니다.
    /// (WPF ModernDataGridControl 을 ElementHost로 감싼 래퍼입니다.)
    ///
    /// 컬럼을 직접 지정하지 않으면 데이터 속성으로 자동 생성됩니다.
    /// AddTextColumn / AddBadgeColumn 으로 컬럼을 지정하면 그 구성이 적용되며,
    /// 배지 컬럼은 톤(info/success/warning/danger/neutral)에 따라 색이 칠해집니다.
    /// </summary>
    [ToolboxItem(true)]
    public class ModernDataGrid : WpfElementHostBase<com.example.Controls.Wpf.Data.ModernDataGridControl>
    {
        private readonly List<ModernDataGridColumn> columnDefinitions;

        /// <summary>선택된 행이 바뀔 때 발생합니다.</summary>
        public event EventHandler SelectionChanged;

        /// <summary>행을 우클릭할 때 발생합니다. 발생 시점에는 해당 행이 이미 선택되어
        /// 있으므로, 핸들러에서 SelectedItem 으로 대상 행을 읽어 컨텍스트 메뉴를 띄울 수
        /// 있습니다.</summary>
        public event EventHandler RowRightClicked;

        /// <summary>기본 크기를 지정하며 컨트롤을 만듭니다.</summary>
        public ModernDataGrid()
        {
            this.columnDefinitions = new List<ModernDataGridColumn>();
            this.Size = new Size(480, 300);

            // 안쪽 WPF 컨트롤의 SelectedItem 변경을 표준 이벤트로 노출합니다.
            DependencyPropertyDescriptor descriptor = DependencyPropertyDescriptor.FromProperty(
                com.example.Controls.Wpf.Data.ModernDataGridControl.SelectedItemProperty,
                typeof(com.example.Controls.Wpf.Data.ModernDataGridControl));
            descriptor.AddValueChanged(this.Wpf, this.OnWpfSelectionChanged);

            // 우클릭 이벤트를 표준 이벤트로 노출합니다.
            this.Wpf.RowRightClicked += this.OnWpfRowRightClicked;
        }

        private void OnWpfSelectionChanged(object sender, EventArgs e)
        {
            EventHandler handler = this.SelectionChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnWpfRowRightClicked(object sender, EventArgs e)
        {
            EventHandler handler = this.RowRightClicked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>텍스트 컬럼을 추가합니다.</summary>
        public void AddTextColumn(string header, string binding)
        {
            this.AddTextColumn(header, binding, null);
        }

        /// <summary>너비를 지정해 텍스트 컬럼을 추가합니다. (너비: "*", "2*", "Auto", 픽셀 숫자)</summary>
        public void AddTextColumn(string header, string binding, string width)
        {
            ModernDataGridColumn column = new ModernDataGridColumn();
            column.Header = header;
            column.Binding = binding;
            column.Width = width;
            this.columnDefinitions.Add(column);
            this.Wpf.SetColumns(this.columnDefinitions);
        }

        /// <summary>배지 컬럼을 추가합니다. labelBinding=표시 텍스트 경로, tonePath=톤 경로.</summary>
        public void AddBadgeColumn(string header, string labelBinding, string tonePath)
        {
            this.AddBadgeColumn(header, labelBinding, tonePath, null);
        }

        /// <summary>너비를 지정해 배지 컬럼을 추가합니다.</summary>
        public void AddBadgeColumn(string header, string labelBinding, string tonePath, string width)
        {
            ModernDataGridColumn column = new ModernDataGridColumn();
            column.Header = header;
            column.Binding = labelBinding;
            column.TonePath = tonePath;
            column.Width = width;
            this.columnDefinitions.Add(column);
            this.Wpf.SetColumns(this.columnDefinitions);
        }

        /// <summary>지정한 컬럼을 모두 지우고 자동 생성으로 되돌립니다.</summary>
        public void ClearColumns()
        {
            this.columnDefinitions.Clear();
            this.Wpf.SetColumns(this.columnDefinitions);
        }

        /// <summary>목록에 표시할 데이터(코드에서 설정)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.IEnumerable ItemsSource
        {
            get { return this.Wpf.ItemsSource; }
            set { this.Wpf.ItemsSource = value; }
        }

        /// <summary>선택된 항목(코드에서 사용)</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return this.Wpf.SelectedItem; }
            set { this.Wpf.SelectedItem = value; }
        }

        /// <summary>읽기 전용 여부</summary>
        [Category("모던 컨트롤")]
        [Description("읽기 전용 여부")]
        [DefaultValue(false)]
        public bool IsReadOnly
        {
            get { return this.Wpf.IsReadOnly; }
            set { this.Wpf.IsReadOnly = value; }
        }
    }
}
