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
        public bool NewInvoice (int custId, string billingAddr)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO [dbo].[Invoice]
                                               ([InvoiceId]
                                               ,[CustomerId]
                                               ,[InvoiceDate]
                                               ,[BillingAddress]
                                               ,[BillingCity]
                                               ,[BillingState]
                                               ,[BillingCountry]
                                               ,[BillingPostalCode]
                                               ,[Total])
                                    VALUES
                                                (<InvoiceId, int,>
                                                ,<CustomerId, int,>
                                                ,<InvoiceDate, datetime,>
                                                ,<BillingAddress, nvarchar(70),>
                                                ,<BillingCity, nvarchar(40),>
                                                ,<BillingState, nvarchar(40),>
                                                ,<BillingCountry, nvarchar(40),>
                                                ,<BillingPostalCode, nvarchar(10),>
                                                ,<Total, numeric(10,2),>)";

                var customerIdParam = new SqlParameter("@customerId", System.Data.SqlDbType.Int);
                customerIdParam.Value = custId;
                cmd.Parameters.Add(customerIdParam);

                var billingAddress = new SqlParameter("@billingAddr", System.Data.SqlDbType.NVarChar);
                // will need to parse address into values
                billingAddress.Value = billingAddr;
                cmd.Parameters.Add(customerIdParam);

                connection.Open();

                var result = cmd.ExecuteNonQuery();

                return result == 1;
            }
        }
    }
}