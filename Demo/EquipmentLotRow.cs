namespace com.example.Demo
{
    /// <summary>
    /// Row shown in the Lot grid of <see cref="EquipmentLotForm"/> — a lot that can be
    /// connected to the selected equipment. <see cref="LotStateTone"/> drives the state
    /// badge (Waiting=neutral, Ready=info, Hold=warning, Running=success);
    /// <see cref="PriorityTone"/> the priority badge (Hot=danger, Normal=neutral).
    /// <see cref="RequiredType"/> is used to match a lot to an equipment type and is not
    /// shown as a column.
    /// </summary>
    public class EquipmentLotRow
    {
        public string LotId { get; set; }

        public string LotState { get; set; }

        /// <summary>Badge tone for LotState (neutral / info / warning / success).</summary>
        public string LotStateTone { get; set; }

        public string Priority { get; set; }

        /// <summary>Badge tone for Priority (danger for Hot, neutral for Normal).</summary>
        public string PriorityTone { get; set; }

        public string ProductId { get; set; }

        public int Qty { get; set; }

        public string Step { get; set; }

        public string Recipe { get; set; }

        public string DueTime { get; set; }

        /// <summary>Equipment type this lot needs; matched against the selected equipment.</summary>
        public string RequiredType { get; set; }
    }
}
