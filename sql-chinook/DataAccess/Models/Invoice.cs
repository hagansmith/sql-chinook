namespace sql_chinook.DataAccess.Models
{
    internal class Invoice
    {
        public int InvoiceId { get; set; }
        public string EmployeeFullName { get; set; }
        public string CustomerName { get; set; }
        public double Total { get; set; }
        public string BillingCountry { get; set; }
    }
}