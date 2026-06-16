namespace com.example.Controls.Wpf.Selection
{
    /// <summary>
    /// Determines how <see cref="ModernComboBoxControl"/> matches the typed text
    /// against each item's display text while auto-suggesting.
    /// </summary>
    public enum ComboBoxFilterMode
    {
        /// <summary>
        /// The item's display text must start with the typed text (case-insensitive).
        /// </summary>
        StartsWith = 0,

        /// <summary>
        /// The item's display text must contain the typed text anywhere (case-insensitive).
        /// </summary>
        Contains = 1
    }
}
