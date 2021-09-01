create database EmployeePayrollDB

use EmployeePayrollDB

create table EmployeePayrollTable
(
Id int identity(1,1) Primary key,
EmpName varchar(20),
Gender varchar(6),
Phone varchar(16),
EmpAddress varchar(24),
Department varchar(16),
StartDate varchar(20),
BasicPay float not null default 0,
Deductions float not null default 0,
IncomeTax float not null default 0,
NetPay float not null default 0
)

Insert into EmployeePayrollTable 
(EmpName, Gender, Phone, EmpAddress, Department, StartDate, BasicPay, Deductions, IncomeTax, NetPay) values ('Riya','F',8887779999,'Mumbai','Accounts','28/08/2021',20000,2000,0,18000),('Omkar','Male',9999888877,'Pune','Developer','26/08/2021',30000,2000,1000,27000);

select * from EmployeePayrollTable;
