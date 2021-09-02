Create or alter procedure spDeleteEmployeeDetails
(  
   @EmpId int  
)  
as
begin try
Delete from EmployeePayrollTable where Id=@EmpId
 End Try
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH
