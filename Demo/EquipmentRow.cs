namespace com.example.Demo
{
    /// <summary>
    /// Row shown in the Equipment grid of <see cref="EquipmentLotForm"/>.
    /// <see cref="EqpStateTone"/> drives the state badge color
    /// (Run=success, Idle=neutral, Setup=info, PM=warning, Down=danger).
    /// </summary>
    public class EquipmentRow
    {
        public string EqpId { get; set; }

        public string EqpType { get; set; }

        public string EqpState { get; set; }

        /// <summary>Badge tone for EqpState (success / neutral / info / warning / danger).</summary>
        public string EqpStateTone { get; set; }

        public string Recipe { get; set; }

        /// <summary>Load-port usage, e.g. "2 / 4".</summary>
        public string Port { get; set; }

        public int LotCount { get; set; }

        public string Operator { get; set; }

        public string LastEvent { get; set; }

        public string LastEventTime { get; set; }
    }
}
