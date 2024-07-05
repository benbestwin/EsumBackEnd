using CompanyRevenueAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CompanyRevenueAPI.Service;

public interface ICsvService
{
    void ImportCsvToDatabase(IFormFile file);
    public List<MonthlyRevenue> GetDataByCompanyIdUsingStoredProc(string companyId);
 
    //public ActionResult<IEnumerable<MonthlyRevenue>> GetDataByCompanyIdUsingStoredProc(string companyId);

}

