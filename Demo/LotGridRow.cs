namespace com.example.Demo
{
    /// <summary>
    /// Row shown in <see cref="LotReceiveForm"/>'s grid: a lot id and its state.
    /// <see cref="LotStateTone"/> drives the state badge color
    /// (Created=success/green, Released=neutral/default, Scrapped=danger/red).
    /// </summary>
    public class LotGridRow
    {
        public string LotId { get; set; }

        public string LotState { get; set; }

        /// <summary>Badge tone for LotState (success / neutral / danger).</summary>
        public string LotStateTone { get; set; }
    }
}
