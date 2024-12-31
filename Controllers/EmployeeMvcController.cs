using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using PersonalManagementApp.Models;





namespace PersonalManagementApp.Controllers
{
    
        public class EmployeeMvcController : Controller
        {
            private readonly HttpClient _httpClient;

            public EmployeeMvcController(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<IActionResult> Index()
            {
                // Web API'den verileri çek
                var response = await _httpClient.GetAsync("https://localhost:5001/api/employees");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var employees = JsonConvert.DeserializeObject<List<Employee>>(jsonResponse);
                    return View(employees); // Veriyi View'a gönder
                }
                return View(new List<Employee>()); // API çalışmazsa boş liste döner
            }

            public IActionResult Create()
            {
                return View(); // Yeni çalışan formu
            }

            [HttpPost]
            public async Task<IActionResult> Create(Employee employee)
            {
                if (ModelState.IsValid)
                {
                    var jsonContent = JsonConvert.SerializeObject(employee);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync("https://localhost:5001/api/employees", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index)); // Başarılı olursa listeye geri dön
                    }
                }
                return View(employee); // Model hatalıysa tekrar formu göster
            }
        }
    
}
