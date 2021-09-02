Create or alter procedure spUpdateBasicPay
(  
   @EmpName varchar(20),
   @NetPay float
)  
as
begin try
UPDATE EmployeePayrollTable set BasicPay=30000,NetPay=@NetPay where EmpName=@EmpName;
End Try
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH