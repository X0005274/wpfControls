using System.Collections.Generic;

namespace com.example.Controls.Wpf.Data
{
    /// <summary>
    /// A column definition for <see cref="ModernDataGridControl"/>.
    /// <list type="bullet">
    /// <item>Set <see cref="TonePath"/> → a <c>ModernBadgeControl</c> (badge) column using
    /// <see cref="Binding"/> as the label and <see cref="TonePath"/> as the tone.</item>
    /// <item>Set <see cref="ComboOptions"/> → an editable combo column (single click opens
    /// it) whose selection two-way binds to <see cref="Binding"/>. Optionally
    /// <see cref="ConditionPath"/>/<see cref="ConditionValue"/> replace the combo with a
    /// read-only dash when a sibling value matches, and <see cref="ValueTones"/> tints the
    /// selected text by value (success/danger/warning/neutral).</item>
    /// <item>Otherwise it is a plain read-only text column.</item>
    /// </list>
    /// </summary>
    public class ModernDataGridColumn
    {
        /// <summary>Column header text.</summary>
        public string Header { get; set; }

        /// <summary>Property path for the cell value (text / badge label / combo selection).</summary>
        public string Binding { get; set; }

        /// <summary>Property path for the badge tone (info/success/warning/danger/neutral).
        /// Null or empty means this is not a badge column.</summary>
        public string TonePath { get; set; }

        /// <summary>Options for an editable combo column. Null means this is not a combo column.</summary>
        public IList<string> ComboOptions { get; set; }

        /// <summary>Optional sibling path checked to disable the combo (show a dash instead).</summary>
        public string ConditionPath { get; set; }

        /// <summary>When the <see cref="ConditionPath"/> value equals this, the combo is
        /// replaced by a read-only dash (the option does not apply to that row).</summary>
        public string ConditionValue { get; set; }

        /// <summary>Optional map of combo value → tone (success/danger/warning/neutral) that
        /// tints the selected text with the matching badge color.</summary>
        public IDictionary<string, string> ValueTones { get; set; }

        /// <summary>Center the text of a plain text column (default false = left).</summary>
        public bool CenterText { get; set; }

        /// <summary>Column width: "*", "2*", "Auto", or a pixel number. Null means Auto.</summary>
        public string Width { get; set; }

        /// <summary>True when this column should render a badge (i.e. has a tone path).</summary>
        public bool IsBadge
        {
            get { return !string.IsNullOrEmpty(this.TonePath); }
        }

        /// <summary>True when this column should render an editable combo (i.e. has options).</summary>
        public bool IsCombo
        {
            get { return this.ComboOptions != null; }
        }
    }
}
