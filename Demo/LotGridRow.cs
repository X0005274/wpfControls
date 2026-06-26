namespace com.example.Demo
{
    /// <summary>
    /// Row shown in <see cref="LotReceiveForm"/>'s grid. The ModernDataGrid wrapper
    /// auto-generates columns from these public properties, so the declaration order
    /// and names become the column order and headers (same columns as Lot Search).
    /// LastEventTime is kept as a pre-formatted string (yyyy-MM-dd HH:mm:ss).
    /// </summary>
    public class LotGridRow
    {
        public string LotId { get; set; }

        public string ProdId { get; set; }

        public string ProduceType { get; set; }

        public string ProdType { get; set; }

        public string SubProdType { get; set; }

        public string LastEventTime { get; set; }

        public string LastEventCd { get; set; }

        public string OperId { get; set; }
    }
}
