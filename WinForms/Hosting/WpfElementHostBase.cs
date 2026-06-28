using System.ComponentModel;
using System.Windows.Forms.Integration;

namespace com.example.WinForms.Controls.Hosting
{
    /// <summary>
    /// 모든 래퍼 컨트롤의 공통 부모 클래스입니다.
    ///
    /// WinForms 폼은 WPF 컨트롤을 직접 올릴 수 없고, 반드시 ElementHost라는
    /// "다리" 컨트롤 안에 넣어야 합니다. 이 클래스는 그 ElementHost를 상속해서,
    /// 생성될 때 안에 들어갈 WPF 컨트롤을 자동으로 만들어 끼워 넣습니다.
    ///
    /// 실제 래퍼들은 이 클래스를 상속하고, 안쪽 WPF 컨트롤의 속성을
    /// 디자이너 속성창에 보이도록 다시 노출합니다.
    /// </summary>
    /// <typeparam name="TWpf">감쌀 WPF 컨트롤의 형식(예: ModernComboBoxControl)</typeparam>
    [ToolboxItem(false)]                 // 베이스 자체는 도구 상자에 표시하지 않음
    [DesignerCategory("Code")]           // 빈 컴포넌트 디자이너가 열리지 않도록
    // ElementHost 기본 디자이너(ElementHostDesigner)는 폼 디자이너에서 Child(호스팅된
    // WPF 컨트롤)를 새로 만들어 직렬화해 버려, 생성자가 끼운 Wpf 와 어긋나 디자인이
    // 깨집니다. 평범한 ControlDesigner 로 바꿔 래퍼를 "불투명 컨트롤"로 취급하게 하면
    // Child 를 건드리지 않으므로 드래그/재직렬화 후에도 정상 동작합니다.
    [Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public abstract class WpfElementHostBase<TWpf> : ElementHost
        where TWpf : System.Windows.FrameworkElement, new()
    {
        /// <summary>
        /// 안쪽에 들어 있는 실제 WPF 컨트롤입니다.
        /// 자식 래퍼들이 이 객체의 속성을 읽고 씁니다.
        /// </summary>
        protected TWpf Wpf { get; private set; }

        /// <summary>
        /// 생성자: WPF 컨트롤을 만들어 ElementHost의 자식으로 끼웁니다.
        /// </summary>
        protected WpfElementHostBase()
        {
            this.Wpf = new TWpf();

            // 디자인 타임(VS 폼 디자이너)에서는 WPF 컨트롤을 ElementHost 자식으로
            // 끼우지 않습니다. 디자이너가 WPF를 디자인 표면에 렌더/재호스팅하면서
            // 폼이 깨지는 것을 막기 위함입니다. 속성(Text/Kind 등)은 this.Wpf 에
            // 그대로 적용되어 직렬화는 정상 동작하고, 실제 화면 호스팅은 런타임에만
            // 일어납니다. (런타임 렌더링은 OnHandleCreated 에서 연결)
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                this.Child = this.Wpf;
            }
        }

        /// <summary>
        /// 런타임에 핸들이 만들어질 때 WPF 컨트롤을 호스팅합니다. 디자인 타임에
        /// 생성자에서 호스팅을 건너뛴 경우를 보완합니다.
        /// </summary>
        protected override void OnHandleCreated(System.EventArgs e)
        {
            if (this.Child == null && this.Wpf != null)
            {
                this.Child = this.Wpf;
            }

            base.OnHandleCreated(e);
        }

        /// <summary>
        /// ElementHost가 원래 가지고 있는 Child(자식) 속성은 디자이너에서
        /// 직접 만질 필요가 없으므로 속성창과 코드 저장에서 숨깁니다.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new System.Windows.UIElement Child
        {
            get { return base.Child; }
            set { base.Child = value; }
        }
    }
}
