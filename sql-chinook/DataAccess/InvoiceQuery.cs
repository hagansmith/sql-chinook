using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using sql_chinook.DataAccess.Models;


namespace sql_chinook.DataAccess
{
    class InvoiceQuery
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;

        public List<Invoice> GetInvoicesBySalesAgent()
        {
            //Provide a query that shows the invoices associated with each sales agent.
            //The resultant table should include the Sales Agent's full name.

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select Employee.FirstName + ' ' + Employee.LastName as 'Employee Full Name', Invoice.InvoiceId
                                        from Customer
                                   join Invoice on Customer.CustomerId = Invoice.InvoiceId
                                   join Employee on Customer.SupportRepId = Employee.EmployeeId";

                var reader = cmd.ExecuteReader();

                var invoices = new List<Invoice>();

                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString()),
                        EmployeeFullName = reader["Employee Full Name"].ToString(),
                    };
                    invoices.Add(invoice);
                }
                return invoices;
            }
        }


        public List<Invoice> GetInvoiceDetail()
        {
            //Provide a query that shows the Invoice Total, Customer name, Country and Sale Agent name 
            //for all invoices.

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select Employee.FirstName + ' ' + Employee.LastName as 'Employee Full Name', 
                                        Invoice.InvoiceId, 
                                        Customer.FirstName + ' ' + Customer.LastName as 'Customer Name', 
                                        Invoice.Total, 
                                        Invoice.BillingCountry                                  
                                    from Customer
                                    join Invoice on Customer.CustomerId = Invoice.InvoiceId
                                    join Employee on Customer.SupportRepId = Employee.EmployeeId";

                var reader = cmd.ExecuteReader();

                var invoicesDetail = new List<Invoice>();

                while (reader.Read())
                {
                    var invoice = new Invoice
                    {
                        InvoiceId = int.Parse(reader["InvoiceId"].ToString()),
                        EmployeeFullName = reader["Employee Full Name"].ToString(),
                        CustomerName = reader["Customer Name"].ToString(),
                        Total = double.Parse(reader["Total"].ToString()),
                        BillingCountry = reader["BillingCountry"].ToString()
                    };
                    invoicesDetail.Add(invoice);
                }
                return invoicesDetail;
            }



        }


        public int InvoiceLineItemCount( int number)
        {
            //Looking at the InvoiceLine table, provide a query that COUNTs the number of line items 
            //for an Invoice with a parameterized Id from user input

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) as 'Line Items'
                                    FROM [dbo].[InvoiceLine] i
                                    WHERE i.InvoiceId = @InvoiceID
                                    GROUP BY i.InvoiceId";


                var invoiceIdParam = new SqlParameter("@InvoiceID", SqlDbType.NVarChar);
                invoiceIdParam.Value = number;
                cmd.Parameters.Add(invoiceIdParam);

                var reader = cmd.ExecuteScalar();

                return int.Parse(reader.ToString());
            }
        }
    }
}