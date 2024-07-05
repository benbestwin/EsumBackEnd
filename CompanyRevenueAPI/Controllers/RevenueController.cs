using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyRevenueAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.IO;
using System.Text;
using CompanyRevenueAPI.Service;

namespace CompanyRevenueAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly CompanyRevenueContext _context;
        private readonly ICsvService _csvService;

        public RevenueController(CompanyRevenueContext context, ICsvService csvService)
        {
            _context = context;
            _csvService = csvService;
        }
        /*
        [HttpGet("{companyId}")]
        public async Task<ActionResult<IEnumerable<MonthlyRevenue>>> GetDataByCompanyId(string companyId)
        {
            var records = await _context.MonthlyRevenues
                .Where(r => r.CompanyId == companyId)
                .Distinct().ToListAsync();

            if (records == null || records.Count == 0)
            {
                return NotFound();
            }

            return records;
        }
        */
        [HttpGet("storedproc/{companyId}")]
        public ActionResult<IEnumerable<MonthlyRevenue>> GetDataByCompanyIdUsingStoredProc(string companyId)
        {
            var records = _csvService.GetDataByCompanyIdUsingStoredProc(companyId);

            if (records == null || records.Count == 0)
            {
                return NotFound();
            }

            return records;
        }

        [HttpPost("import")]
        public IActionResult ImportCsv(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File is not selected or the file is empty.");
            }

            try
            {
                _csvService.ImportCsvToDatabase(file);
                return Ok("CSV data imported successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /*
        [HttpPost("import")]
        public IActionResult ImportCsv()
        {
            try
            {
                _csvService.ImportCsvToDatabase("data/t187ap05_L.csv");
                return Ok("CSV data imported successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        */

    }
}
