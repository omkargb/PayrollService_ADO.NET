Create procedure spAddEmployeeDetails
(
@EmpName varchar(20),
@Gender varchar(6),
@Phone varchar(16),
@EmpAddress varchar(24),
@StartDate varchar(20),
@Department varchar(16),
@BasicPay float,
@Deductions float,
@IncomeTax float,
@NetPay float
)
as

begin try
Insert into EmployeePayrollTable values(@EmpName,@Gender,@Phone,@EmpAddress,@StartDate,@Department,@BasicPay,@Deductions,@IncomeTax,@NetPay)
End Try

BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH