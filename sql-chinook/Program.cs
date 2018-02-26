using sql_chinook.DataAccess;
using System;
using System.Linq;

namespace sql_chinook
{
    class Program
    {
        static void Main(string[] args)
        {
            var invoiceQuery = new InvoiceQuery();


            Console.WriteLine("Please make a selection");
            Console.WriteLine("1: Invoice Listing By Agent\n" +
                              "2: Invoice Detail Listing\n" +
                              "3: Invoice Lineitem Count by Invoice Id");
            var input = int.Parse(Console.ReadLine());

            if (input == 1)
            {

                // -- Invoices By Agent -- //
                var employeeInvoices = invoiceQuery.GetInvoicesBySalesAgent();
                var agentGroups = from employee in employeeInvoices
                                  group employee.InvoiceId by employee.EmployeeFullName into a
                                  select new { agent = a.Key, invoices = a.ToList() };

                foreach (var agent in agentGroups)
                {

                    Console.WriteLine($"Invoices for: {agent.agent}");
                    foreach (int invoice in agent.invoices)
                    {
                        Console.WriteLine($"{invoice} ");
                    }
                }
                // -------------------- //
            }
            else if (input == 2)
            {
                // -- Invoice Detail -- //
                var invoiceDetail = invoiceQuery.GetInvoiceDetail();
                foreach (var invoice in invoiceDetail)
                {
                    Console.WriteLine($"{invoice.InvoiceId} - {invoice.Total}: {invoice.CustomerName}, Agent: {invoice.EmployeeFullName}");
                }
                // ------------------- //
            }
            else if (input == 3)
            {
                // -- Invoice Line Item Count -- //
                Console.WriteLine("Enter an invoice id for lookup");
                var number = int.Parse(Console.ReadLine());
                var invoiceLineItemCount = invoiceQuery.InvoiceLineItemCount(number);
                Console.WriteLine($"Line items in invoice {number}: {invoiceLineItemCount}");
                // ---------------------------- //
            }
            else
            {
                Console.WriteLine($"{input} is not a valid selection");
            }
            Console.ReadKey();
        }
    }
}
