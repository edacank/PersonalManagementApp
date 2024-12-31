using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalManagementApp.Models; // LeaveRequest modelini kullanmak için ekliyoruz
using PersonalManagementApp.Repositories; // Repositories'i ekle
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PersonalManagementApp.Controllers
{
    public class LeaveMvcController : Controller
    {
        private readonly HttpClient _httpClient;

        public LeaveMvcController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Index action: Tüm izin taleplerini getirir
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:44320/api/leaves");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var leaves = JsonConvert.DeserializeObject<List<LeaveRequest>>(jsonResponse);
                return View(leaves);
            }
            // API'dan veri alınamazsa boş bir liste döndür
            return View(new List<LeaveRequest>());
        }

        // Create action: Yeni izin talep formunu gösterir
        public IActionResult Create()
        {
            return View();
        }

        // Create POST action: Yeni izin talebini kaydeder
        [HttpPost]
        public async Task<IActionResult> Create(LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(leaveRequest);
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:5001/api/leaves", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index)); // Başarılı ise Index sayfasına yönlendir
                }
            }
            // Model geçerli değilse veya API isteği başarısız olursa tekrar formu göster
            return View(leaveRequest);
        }
    }
}
