using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollADONET
{
    public class EmployeeOperations
    {
        public static string connectionString = "Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=EmployeePayrollDB";
        Employee emp = new Employee();

        public void GetEmployeeDetails()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    SqlCommand com = new SqlCommand("spGetAllEmployeeDetails", sqlConnect); //query, sqlconnection
                    com.CommandType = CommandType.StoredProcedure;

                    Console.WriteLine(" Database connected.");
                    Console.WriteLine(" Database name : "+sqlConnect.Database);

                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        Console.WriteLine("\n EmployeePayroll Table Contents : \n");
                        Console.WriteLine(" Id\tName\tGender\tPhone\t\tAddress Department\tStartDate \tBasicPay  Deductions  IncomeTax  NetPay");
                        Console.WriteLine();
                        while (dr.Read())
                        {
                            emp.EmpId = dr.GetInt32(0);
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

                            Console.Write(" {0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t\t{7}\t{8} \t {9}\t{10}\n", emp.EmpId, emp.EmpName, emp.Gender, emp.Phone, emp.EmpAddress, emp.Department, emp.StartDate, emp.BasicPay, emp.Deductions, emp.IncomeTax, emp.NetPay);
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception e) 
            { 
                Console.WriteLine(" Database not connected. Error details : \n"+e.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }

        public void AddEmpDetails()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    Console.WriteLine(" SQL Database connection open..");
                    Console.WriteLine(" Database name : " + sqlConnect.Database);

                    SqlCommand cmd = new SqlCommand("spAddEmpDetails", sqlConnect);
                        cmd.CommandType = CommandType.StoredProcedure;

                    Console.Write(" Add EmpName : ");     emp.EmpName = Console.ReadLine();
                    Console.Write(" Add Gender : ");      emp.Gender = Console.ReadLine();
                    Console.Write(" Add Phone : ");       emp.Phone = Console.ReadLine();
                    Console.Write(" Add EmpAddress : ");  emp.EmpAddress = Console.ReadLine();
                    Console.Write(" Add Department : ");  emp.Department = Console.ReadLine();
                    Console.Write(" Add StartDate (DD/MM/YYYY) : ");   emp.StartDate = Console.ReadLine();
                    Console.Write(" Add BasicPay : ");    emp.BasicPay = int.Parse(Console.ReadLine());
                    Console.Write(" Add Deductions : ");  emp.Deductions = int.Parse(Console.ReadLine());
                    Console.Write(" Add IncomeTax : ");   emp.IncomeTax = int.Parse(Console.ReadLine());
                    emp.NetPay = emp.BasicPay - (emp.Deductions + emp.IncomeTax);

                    cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@Phone", emp.Phone);
                    cmd.Parameters.AddWithValue("@EmpAddress", emp.EmpAddress);
                    cmd.Parameters.AddWithValue("@StartDate", emp.StartDate);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    cmd.Parameters.AddWithValue("@BasicPay", emp.BasicPay);
                    cmd.Parameters.AddWithValue("@Deductions", emp.Deductions);
                    cmd.Parameters.AddWithValue("@IncomeTax", emp.IncomeTax);
                    cmd.Parameters.AddWithValue("@NetPay", emp.NetPay);

                    int affRows = cmd.ExecuteNonQuery();    //returns num of affected rows after query execution
                    sqlConnect.Close();
                    Console.WriteLine(" SQL Database connection closed..");
                    if (affRows >= 1)
                    {
                        Console.WriteLine("Employee added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not added..");
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void RemoveEmpDetails()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    Console.WriteLine(" SQL Database connection open..");
                    SqlCommand cmd = new SqlCommand("spDeleteEmployeeDetails", sqlConnect);
                    cmd.CommandType = CommandType.StoredProcedure;

                    Console.Write(" Select id to delete : "); 
                    emp.EmpId = int.Parse(Console.ReadLine());

                    cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                    int affRows = cmd.ExecuteNonQuery();    //returns num of affected rows after query execution
                    sqlConnect.Close();
                    Console.WriteLine(" SQL Database connection closed..");

                    if (affRows >= 1)
                    {   Console.WriteLine("Employee details Removed successfully.");    }
                    else
                    {   Console.WriteLine("Employee not Removed..");    }
                }
            }
            catch (Exception ex)
            {   Console.WriteLine(ex.Message);  }
        }

        public void UpdateBasicPay(string empName)
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    Console.WriteLine(" SQL Database connection open..");
                    SqlCommand cmd = new SqlCommand("spUpdateBasicPay", sqlConnect);

                    cmd.CommandType = CommandType.StoredProcedure;
                    emp.EmpName = empName;
                    cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);

                    //update net pay when updating basic pay
                    SqlCommand com = new SqlCommand("spGetAllEmployeeDetails", sqlConnect); //query, sqlconnection
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            emp.EmpName = dr.GetString(1);
                            emp.Deductions = dr.GetDouble(8);
                            emp.IncomeTax = dr.GetDouble(9);
                            if (emp.EmpName.ToUpper() == empName.ToUpper())
                            {
                                emp.NetPay = 30000 - (emp.Deductions + emp.IncomeTax);
                            }
                        }
                    }
                    dr.Close();

                    int affRows = cmd.ExecuteNonQuery();    //returns num of affected rows after query execution
                    sqlConnect.Close();
                    Console.WriteLine(" SQL Database connection closed..");

                    if (affRows >= 1)
                    {   Console.WriteLine(" Employee basic pay Updated..");     }
                    else
                    {   Console.WriteLine(" Employee basic pay not Updated...");    }
                }
            }
            catch (Exception ex)
            {   Console.WriteLine(ex.Message);  }
        }

        //get all employee data in a date range
        public void GetRowsByDateRange()
        {
            SqlConnection sqlConnect = new SqlConnection(connectionString);
            try
            {
                using (sqlConnect)
                {
                    sqlConnect.Open();
                    Console.WriteLine(" SQL Database connection open..");
                    SqlCommand cmd = new SqlCommand("spGetRowsByDateRange", sqlConnect);

                    cmd.CommandType = CommandType.StoredProcedure;

                    Console.WriteLine("\n Enter date range (dd/mm/yyyy) :");
                    Console.Write(" Minimum date : ");      string date1 = Console.ReadLine();
                    Console.Write(" Maximum date : ");      string date2 = Console.ReadLine();

                    cmd.Parameters.AddWithValue("@MinDate", date1);
                    cmd.Parameters.AddWithValue("@MaxDate", date2);
                    
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        Console.WriteLine(" Name \t Address \t StartDate \t NetPay");
                        while (dr.Read())
                        {
                            emp.EmpName = dr.GetString(1);
                            emp.EmpAddress = dr.GetString(4);
                            emp.StartDate = dr.GetString(6);
                            emp.NetPay = dr.GetDouble(10);

                            Console.Write(" {0} \t {1} \t {2} \t {3}\n", emp.EmpName, emp.EmpAddress, emp.StartDate, emp.NetPay);
                        }
                    }
                    dr.Close();

                    int affRows = cmd.ExecuteNonQuery();    //returns num of affected rows after query execution
                    sqlConnect.Close();
                    Console.WriteLine(" SQL Database connection closed..");

                    if (affRows >= 1)
                    { Console.WriteLine(" Query Executed successfully."); }

                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }

    }
}
