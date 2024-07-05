namespace CompanyRevenueAPI.Dtos;

public class MonthlyRevenuedDto
{
    public string CompanyId { get; set; }
    public string YearMonth { get; set; }
    public decimal Revenue { get; set; }
}