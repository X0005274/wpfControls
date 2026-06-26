namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// A column definition for <see cref="ModernDataGridControl"/>. When
    /// <see cref="TonePath"/> is set the column renders a <c>ModernBadgeControl</c>
    /// (badge) using <see cref="Binding"/> as the label path and
    /// <see cref="TonePath"/> as the tone path; otherwise it is a plain text column.
    /// </summary>
    public class ModernDataGridColumn
    {
        /// <summary>Column header text.</summary>
        public string Header { get; set; }

        /// <summary>Property path for the cell value (text), or the badge label.</summary>
        public string Binding { get; set; }

        /// <summary>Property path for the badge tone (info/success/warning/danger/neutral).
        /// Null or empty means this is a plain text column.</summary>
        public string TonePath { get; set; }

        /// <summary>Column width: "*", "2*", "Auto", or a pixel number. Null means Auto.</summary>
        public string Width { get; set; }

        /// <summary>True when this column should render a badge (i.e. has a tone path).</summary>
        public bool IsBadge
        {
            get { return !string.IsNullOrEmpty(this.TonePath); }
        }
    }
}
