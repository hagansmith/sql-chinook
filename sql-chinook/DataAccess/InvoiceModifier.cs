using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sql_chinook.DataAccess
{
    class InvoiceModifier
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString;
       
        //INSERT a new invoice with parameters for customerid and billing address
        public bool NewInvoice (int custId, string billingAddr, string billingCity, string billingState, string billingCountry, string billingPost, double invoiceTotal )
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO [dbo].[Invoice]
                                               ([CustomerId]
                                               ,[InvoiceDate]
                                               ,[BillingAddress]
                                               ,[BillingCity]
                                               ,[BillingState]
                                               ,[BillingCountry]
                                               ,[BillingPostalCode]
                                               ,[Total])
                                    VALUES
                                                (@customerId
                                                ,@date
                                                ,@billingAddr
                                                ,@billingCity
                                                ,@billingState
                                                ,@billingCountry
                                                ,@billingPostal
                                                ,@invoiceTotal)";

                var customerIdParam = new SqlParameter("@customerId", System.Data.SqlDbType.Int);
                customerIdParam.Value = custId;
                cmd.Parameters.Add(customerIdParam);

                var dateParam = new SqlParameter("@date", System.Data.SqlDbType.Date);
                dateParam.Value = "2009 - 01 - 02 00:00:00.000";
                cmd.Parameters.Add(dateParam);

                var billingAddressParam = new SqlParameter("@billingAddr", System.Data.SqlDbType.NVarChar);
                billingAddressParam.Value = billingAddr;
                cmd.Parameters.Add(billingAddressParam);

                var billingCityParam = new SqlParameter("@billingCity", System.Data.SqlDbType.NVarChar);
                billingCityParam.Value = billingCity;
                cmd.Parameters.Add(billingCityParam);

                var billingStateParam = new SqlParameter("@billingState", System.Data.SqlDbType.NVarChar);
                billingStateParam.Value = billingState;
                cmd.Parameters.Add(billingStateParam);

                var billingCountryParam = new SqlParameter("@billingCountry", System.Data.SqlDbType.NVarChar);
                billingCountryParam.Value = billingCountry;
                cmd.Parameters.Add(billingCountryParam);

                var billingPostalParam = new SqlParameter("@billingPostal", System.Data.SqlDbType.NVarChar);
                billingPostalParam.Value = billingPost;
                cmd.Parameters.Add(billingPostalParam);

                var invoiceTotalParam = new SqlParameter("@invoiceTotal", System.Data.SqlDbType.Decimal);
                invoiceTotalParam.Value = invoiceTotal;
                cmd.Parameters.Add(invoiceTotalParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }

        //UPDATE an Employee's name with a parameter for Employee Id and new name
        public bool updateEmployee(int empId, string fName, string lName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE [dbo].[Employee]
                                       SET [LastName] = @lastName
                                          ,[FirstName] = @firstName
                                     WHERE Employee.EmployeeId = @employeeId";

                var employeeIdParam = new SqlParameter("@employeeId", System.Data.SqlDbType.Int);
                employeeIdParam.Value = empId;
                cmd.Parameters.Add(employeeIdParam);

                var fisrtNameParam = new SqlParameter("@firstName", System.Data.SqlDbType.NVarChar);
                fisrtNameParam.Value = fName;
                cmd.Parameters.Add(fisrtNameParam);

                var lastNameParam = new SqlParameter("@lastName", System.Data.SqlDbType.NVarChar);
                lastNameParam.Value = lName;
                cmd.Parameters.Add(lastNameParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}

