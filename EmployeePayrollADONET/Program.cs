using System;

namespace EmployeePayrollADONET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to Payroll Service program using MSSQL & ADO.NET \n");
            EmployeeOperations eops = new EmployeeOperations();
            eops.GetEmployeeDetails();
        }
    }
}
