using Microsoft.AspNetCore.Mvc;
using PersonalManagementApp.Models; // LeaveRequest ve diğer modeller için
using PersonalManagementApp.Repositories; // Veritabanı erişimi için
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PersonalManagementApp.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LeaveController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaves()
        {
            // LeaveRequest repository'sinden tüm izin taleplerini alıyoruz
            var leaveRequests = await _unitOfWork.LeaveRequests.GetAllAsync();

            // Eğer veriler boşsa NotFound dönebiliriz
            if (leaveRequests == null || !leaveRequests.Any())
            {
                return NotFound("Herhangi bir izin talebi bulunamadı.");
            }

            // Eğer veriler varsa, bu verileri API çağrısına geri döndürüyoruz
            return Ok(leaveRequests);
        }

        [HttpPost]
        public async Task<IActionResult> RequestLeave([FromBody] LeaveRequest leaveRequest)
        {
            if (leaveRequest == null)
            {
                return BadRequest("Leave request cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.LeaveRequests.AddAsync(leaveRequest);
            await _unitOfWork.SaveChangesAsync();

            // Başarılı ekleme işlemi sonrası oluşturulan nesneyi döndür
            return CreatedAtAction(nameof(RequestLeave), new { id = leaveRequest.LeaveRequestId }, leaveRequest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeave(int id, [FromBody] LeaveRequest leave)
        {
            var existingLeave = await _unitOfWork.LeaveRequests.GetByIdAsync(id);
            if (existingLeave == null)
            {
                return NotFound("İzin talebi bulunamadı.");
            }

            existingLeave.Reason = leave.Reason;
            existingLeave.StartDate = leave.StartDate;
            existingLeave.EndDate = leave.EndDate;

            _unitOfWork.LeaveRequests.Update(existingLeave);
            await _unitOfWork.SaveChangesAsync();

            return Ok(existingLeave);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelLeave(int id)
        {
            var leave = await _unitOfWork.LeaveRequests.GetByIdAsync(id);
            if (leave == null)
            {
                return NotFound("İzin talebi bulunamadı.");
            }

            _unitOfWork.LeaveRequests.Delete(leave);
            await _unitOfWork.SaveChangesAsync();

            return Ok("İzin talebi iptal edildi.");
        }
    }
}
