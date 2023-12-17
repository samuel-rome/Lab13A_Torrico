namespace Lab13A.Models.Request
{
    public class DetailRequest
    {
        public int DetailID { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float SubTotal { get; set; }
        public int InvoiceID { get; set; }
        public bool Active { get; set; }

    }
}
