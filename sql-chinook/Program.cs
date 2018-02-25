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
            //var employeeInvoices = invoiceQuery.GetInvoicesBySalesAgent();
            var invoiceDetail = invoiceQuery.GetInvoiceDetail();

            //var agentGroups = from employee in employeeInvoices
            //                  group employee.InvoiceId by employee.EmployeeFullName into a
            //                  select new { agent = a.Key, invoices = a.ToList() };

            //foreach (var agent in agentGroups)
            //{ 

            //    Console.WriteLine($"Invoices for: {agent.agent}");
            //    foreach (int invoice in agent.invoices)
            //    {
            //        Console.WriteLine($"{invoice} ");
            //    }


            //}

            foreach (var invoice in invoiceDetail)
            {
                Console.WriteLine($"{invoice.InvoiceId} - {invoice.Total}: {invoice.CustomerName} Agent: {invoice.EmployeeFullName}");
            }

            Console.ReadKey();
        }
    }
}
