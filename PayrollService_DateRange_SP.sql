Create or alter procedure spGetRowsByDateRange
(  
   @MinDate varchar(16),
   @MaxDate varchar(16)
)  
as
begin try
SELECT * from EmployeePayrollTable where StartDate between @MinDate and @MaxDate;
End Try
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH