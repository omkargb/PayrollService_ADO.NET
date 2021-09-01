using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollADONET
{
    class EmployeeOperations
    {
        public static string connectionString = "Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=EmployeePayrollDB";
        SqlConnection sqlConnect = new SqlConnection(connectionString);

        public void GetEmployeeDetails()
        {
            try
            {
                DataSet dset = new DataSet();
                using (this.sqlConnect)
                {
                    this.sqlConnect.Open();
                    SqlCommand com = new SqlCommand("spGetAllEmployeeDetails", sqlConnect);
                    com.CommandType = CommandType.StoredProcedure;

                    Console.WriteLine(" Database connected.");
                    Console.WriteLine(" Database name : "+this.sqlConnect.Database);
                }
            }
            catch (Exception e) 
            { 
                Console.WriteLine(" Database not connected. Error details : \n"+e.Message);
            }
            finally
            {
                this.sqlConnect.Close();
            }
        }

    }
}
