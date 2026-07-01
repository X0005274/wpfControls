namespace com.example.Demo
{
    /// <summary>A single line in the process-flow event log (grid row).</summary>
    public class FlowLogRow
    {
        public string Time { get; set; }
        public string Stage { get; set; }
        public string Event { get; set; }
        public string Detail { get; set; }
    }
}
