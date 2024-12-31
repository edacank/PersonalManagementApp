using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PersonalManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var companies = await _unitOfWork.Companies.GetAllAsync();
            return Ok(companies);
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company company)
        {
            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.SaveChangesAsync();
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, Company company)
        {
            var existingCompany = await _unitOfWork.Companies.GetByIdAsync(id);
            if (existingCompany == null)
                return NotFound();

            existingCompany.CompanyName = company.CompanyName;
            existingCompany.Address = company.Address;
            _unitOfWork.Companies.Update(existingCompany);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(id);
            if (company == null)
                return NotFound();

            _unitOfWork.Companies.Delete(company);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }

}
