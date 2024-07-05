using CompanyRevenueAPI.Models;
using CompanyRevenueAPI.Service;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;


public class CsvService:ICsvService
{
    private readonly CompanyRevenueContext _context;

    public CsvService(CompanyRevenueContext context)
    {
        _context = context;
    }

    public void ImportCsvToDatabase(IFormFile filePath)
    {
        using (var reader = new StreamReader(filePath.OpenReadStream()))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<MonthlyRevenueMap>();

          

            var records = csv.GetRecords<MonthlyRevenue>().ToList();
            foreach (var record in records)
            {
                var currentMonthRevenue = record.CurrentMonthRevenue ?? 0m;
                var previousMonthRevenue = record.PreviousMonthRevenue ?? 0m;
                var lastYearCurrentMonthRevenue = record.LastYearCurrentMonthRevenue ?? 0m;
                var previousMonthComparison = record.PreviousMonthComparison ?? 0m;
                var lastYearSameMonthComparison = record.LastYearSameMonthComparison ?? 0m;
                var accumulatedCurrentMonthRevenue = record.AccumulatedCurrentMonthRevenue ?? 0m;
                var accumulatedLastYearRevenue = record.AccumulatedLastYearRevenue ?? 0m;
                var previousPeriodComparison = record.PreviousPeriodComparison ?? 0m;
                _context.Database.ExecuteSqlRaw(
                    "EXEC InsertMonthlyRevenue @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13",
                    record.ReportDate, record.YearMonth, record.CompanyId, record.CompanyName, record.IndustryType, currentMonthRevenue,
                    previousMonthRevenue, lastYearCurrentMonthRevenue, previousMonthComparison, lastYearSameMonthComparison,
                    accumulatedCurrentMonthRevenue, accumulatedLastYearRevenue, previousPeriodComparison, record.Notes);
            }
        }
    }



    public List<MonthlyRevenue> GetDataByCompanyIdUsingStoredProc(string companyId)
    {
        var companyIdParam = new SqlParameter("@p0", companyId);
        var monthlyRevenues = _context.MonthlyRevenues
                                      .FromSqlRaw("EXEC GetMonthlyRevenueByCompanyID @p0", companyIdParam)
                                      .AsNoTracking()
                                      .AsEnumerable()
                                      .Distinct()
                                      .ToList();

        return monthlyRevenues;
    }
}






public class MonthlyRevenueMap : ClassMap<MonthlyRevenue>
{
    public MonthlyRevenueMap()
    {
        Map(m => m.ReportDate).Name("出表日期");
        Map(m => m.YearMonth).Name("資料年月");
        Map(m => m.CompanyId).Name("公司代號");
        Map(m => m.CompanyName).Name("公司名稱");
        Map(m => m.IndustryType).Name("產業別");
        Map(m => m.CurrentMonthRevenue).Name("營業收入-當月營收");
        Map(m => m.PreviousMonthRevenue).Name("營業收入-上月營收");
        Map(m => m.LastYearCurrentMonthRevenue).Name("營業收入-去年當月營收");
        Map(m => m.PreviousMonthComparison).Name("營業收入-上月比較增減(%)");
        Map(m => m.LastYearSameMonthComparison).Name("營業收入-去年同月增減(%)");
        Map(m => m.AccumulatedCurrentMonthRevenue).Name("累計營業收入-當月累計營收");
        Map(m => m.AccumulatedLastYearRevenue).Name("累計營業收入-去年累計營收");
        Map(m => m.PreviousPeriodComparison).Name("累計營業收入-前期比較增減(%)");
        Map(m => m.Notes).Name("備註");
    }
}
