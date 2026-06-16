namespace com.example.Demo
{
    /// <summary>
    /// Sample row model used by the DataGrid and ListView demos. Public
    /// properties become auto-generated DataGrid columns.
    /// </summary>
    public class DemoRow
    {
        public DemoRow(string code, string name, string status)
        {
            this.Code = code;
            this.Name = name;
            this.Status = status;
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
    }
}
