using System;

namespace EmployeePayrollADONET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Welcome to Payroll Service program using MSSQL & ADO.NET \n");
            EmployeeOperations eops = new EmployeeOperations();
            OptionsDisplay();

            void OptionsDisplay()
            {
                Console.Write("\n Select option :\n 1.[UC2]Get Payroll Table Details \t 2.Add data to table  \n 3. Delete By Id \t 4.[UC3,4]Update basic pay \n 5.[UC5]Get data in date_range \t 6.Exit\n Provide option number : --> ");
                //Retrieve all employee payroll table details
                int option = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (option)
                {
                    case 1:
                        eops.GetEmployeeDetails();
                        OptionsDisplay();
                        break;
                    case 2:
                        eops.AddEmpDetails();
                        OptionsDisplay();
                        break;
                    case 3:
                        eops.RemoveEmpDetails();
                        OptionsDisplay();
                        break;
                    case 4:
                        Console.Write(" Enter Employee Name to update Basic pay as Rs.30000 : ");
                        string empName = Console.ReadLine();
                        eops.UpdateBasicPay(empName);
                        OptionsDisplay();
                        break;
                    case 5:
                        eops.GetRowsByDateRange();
                        OptionsDisplay();
                        break;
                    default:
                        break;
                }
            }       //end options

        }
    }
}
