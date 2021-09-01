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

                    SqlDataReader dr = com.ExecuteReader();
                    Employee emp = new Employee();

                    if (dr.HasRows)
                        Console.WriteLine("\n 1. EmployeePayroll Table Contents : \n");                        Console.WriteLine(" Name\tGender\tPhone\t\tAddress Department\tStartDate \tBasicPay  Deductions  IncomeTax  NetPay");
                    Console.WriteLine();
                    {
                        while (dr.Read())
                        {
                            emp.EmpName = dr.GetString(1);
                            emp.Gender = dr.GetString(2);
                            emp.Phone = dr.GetString(3);
                            emp.EmpAddress = dr.GetString(4);
                            emp.Department = dr.GetString(5);
                            emp.StartDate = dr.GetString(6);
                            emp.BasicPay = dr.GetDouble(7);
                            emp.Deductions = dr.GetDouble(8);
                            emp.IncomeTax = dr.GetDouble(9);
                            emp.NetPay = dr.GetDouble(10);

                            Console.Write(" {0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t\t{7}\t{8} \t {9}\n", emp.EmpName, emp.Gender, emp.Phone, emp.EmpAddress, emp.Department, emp.StartDate, emp.BasicPay, emp.Deductions, emp.IncomeTax, emp.NetPay);
                        }
                        Console.WriteLine();
                    }
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
