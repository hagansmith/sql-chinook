using System.Collections.Generic;
using System.Configuration;
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


                //var firstLetterParam = new SqlParameter("@FirstLetter", SqlDbType.NVarChar);
                //firstLetterParam.Value = firstCharacter;
                //cmd.Parameters.Add(firstLetterParam);

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


                //var firstLetterParam = new SqlParameter("@FirstLetter", SqlDbType.NVarChar);
                //firstLetterParam.Value = firstCharacter;
                //cmd.Parameters.Add(firstLetterParam);

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




        //Looking at the InvoiceLine table, provide a query that COUNTs the number of line items 
        //for an Invoice with a parameterized Id from user input
        //hint, this will use ExecuteScalar

        //INSERT a new invoice with parameters for customerid and billing address

        //UPDATE an Employee's name with a parameter for Employee Id and new name


    }
}