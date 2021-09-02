Create or alter procedure spAddEmpDetails
(
@EmpName varchar(20),
@Gender varchar(6),
@Phone varchar(16),
@EmpAddress varchar(24),
@Department varchar(16),
@StartDate varchar(20),
@BasicPay float,
@Deductions float,
@IncomeTax float,
@NetPay float
)
as

begin try
/* sequence important */
Insert into EmployeePayrollTable values(@EmpName,@Gender,@Phone,@EmpAddress,@Department,@StartDate,@BasicPay,@Deductions,@IncomeTax,@NetPay);
End Try

BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH