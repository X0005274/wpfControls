namespace com.example.Demo
{
    /// <summary>
    /// A work request (의뢰서) row shown in the start-up list. Carries the LOT info the
    /// LOT is created from: product, wafer quantity, and the target process route.
    /// </summary>
    public class RequestRow
    {
        public string RequestId { get; set; }
        public string Product { get; set; }
        public string WaferQty { get; set; }
        public string Process { get; set; }
        public string RequestDate { get; set; }
        public string Status { get; set; }
    }
}
