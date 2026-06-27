namespace com.example.Demo
{
    /// <summary>
    /// Row shown in the Wafer grid of <see cref="LotReceiveForm"/>. Wafers belong to
    /// the lot currently selected on the left; <see cref="WaferStateTone"/> drives the
    /// state badge color (Good=success, In Process=neutral, Hold=warning, Scrap=danger).
    /// </summary>
    public class WaferGridRow
    {
        public string WaferId { get; set; }

        public string WaferState { get; set; }

        /// <summary>Badge tone for WaferState (success / neutral / warning / danger).</summary>
        public string WaferStateTone { get; set; }

        public string Event { get; set; }

        public string EventTime { get; set; }
    }
}
