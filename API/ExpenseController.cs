using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PersonalManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await _unitOfWork.Expenses.GetAllAsync();
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<IActionResult> AddExpense(Expense expense)
        {
            await _unitOfWork.Expenses.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, Expense expense)
        {
            var existingExpense = await _unitOfWork.Expenses.GetByIdAsync(id);
            if (existingExpense == null)
                return NotFound();

            existingExpense.Amount = expense.Amount;
            existingExpense.Description = expense.Description;
            _unitOfWork.Expenses.Update(existingExpense);
            await _unitOfWork.SaveChangesAsync();
            return Ok(existingExpense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _unitOfWork.Expenses.GetByIdAsync(id);
            if (expense == null)
                return NotFound();

            _unitOfWork.Expenses.Delete(expense);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }

}
