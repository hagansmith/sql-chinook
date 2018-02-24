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
            var employeeInvoices = invoiceQuery.GetInvoicesBySalesAgent();



            var agentGroups = from employee in employeeInvoices
                              group employee by employee.EmployeeFullName;

            foreach (var agent in agentGroups)
            { 

                    Console.WriteLine($"{agent.ToString()}");


            }

            Console.ReadKey();
        }
    }
}
