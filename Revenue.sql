CREATE TABLE MonthlyRevenue (
    ReportDate VARCHAR(7) NOT NULL,
    YearMonth VARCHAR(5) NOT NULL,
    CompanyID VARCHAR(10) NOT NULL,
    CompanyName NVARCHAR(100) NOT NULL,
    IndustryType NVARCHAR(50) NOT NULL,
    CurrentMonthRevenue DECIMAL(18, 2) NOT NULL,
    PreviousMonthRevenue DECIMAL(18, 2) NOT NULL,
    LastYearCurrentMonthRevenue DECIMAL(18, 2) NOT NULL,
    PreviousMonthComparison DECIMAL(18, 2) NOT NULL,
    LastYearSameMonthComparison DECIMAL(18, 2) NOT NULL,
    AccumulatedCurrentMonthRevenue DECIMAL(18, 2) NOT NULL,
    AccumulatedLastYearRevenue DECIMAL(18, 2) NOT NULL,
    PreviousPeriodComparison DECIMAL(18, 2) NOT NULL,
    Notes NVARCHAR(500)
);


CREATE PROCEDURE InsertMonthlyRevenue
    @ReportDate VARCHAR(7),
    @YearMonth VARCHAR(5),
    @CompanyID VARCHAR(10),
    @CompanyName NVARCHAR(100),
    @IndustryType NVARCHAR(100),
    @CurrentMonthRevenue DECIMAL(18, 2),
    @PreviousMonthRevenue DECIMAL(18, 2),
    @LastYearCurrentMonthRevenue DECIMAL(18, 2),
    @PreviousMonthComparison DECIMAL(18, 2),
    @LastYearSameMonthComparison DECIMAL(18, 2),
    @AccumulatedCurrentMonthRevenue DECIMAL(18, 2),
    @AccumulatedLastYearRevenue DECIMAL(18, 2),
    @PreviousPeriodComparison DECIMAL(18, 2),
    @Notes NVARCHAR(500)
AS
BEGIN
    INSERT INTO MonthlyRevenue (ReportDate, YearMonth, CompanyID, CompanyName, IndustryType, CurrentMonthRevenue, PreviousMonthRevenue, LastYearCurrentMonthRevenue, PreviousMonthComparison, LastYearSameMonthComparison, AccumulatedCurrentMonthRevenue, AccumulatedLastYearRevenue, PreviousPeriodComparison, Notes)
    VALUES (@ReportDate, @YearMonth, @CompanyID, @CompanyName, @IndustryType, @CurrentMonthRevenue, @PreviousMonthRevenue, @LastYearCurrentMonthRevenue, @PreviousMonthComparison, @LastYearSameMonthComparison, @AccumulatedCurrentMonthRevenue, @AccumulatedLastYearRevenue, @PreviousPeriodComparison, @Notes);
END;


CREATE PROCEDURE GetMonthlyRevenueByCompanyID
    @CompanyID VARCHAR(10)
AS
BEGIN
    SELECT * FROM MonthlyRevenue
    WHERE CompanyID = @CompanyID;
END;


ALTER TABLE MonthlyRevenue
ADD CONSTRAINT PK_MonthlyRevenue PRIMARY KEY (CompanyID, YearMonth);



 

--dotnet ef dbcontext scaffold "Server=localhost;Database=CompanyRevenue;User Id=user1;Password=12345678;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models



--dotnet ef dbcontext scaffold Name=DefaultConnection Microsoft.EntityFrameworkCore.SqlServer -o Models --force

