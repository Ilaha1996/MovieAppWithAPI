using Microsoft.AspNetCore.Mvc;
using MovieApp.MVC.APIResponseMessages;
using MovieApp.MVC.ViewModels.GenreVM;
using RestSharp;
using System.Text.Json;

namespace MovieApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestClient _restClient;  //HttpClient-in alternativi
        public HomeController()
        {
            _restClient = new RestClient("https://localhost:7006/api");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Genres", Method.Get);
            //var response = await _restClient.ExecuteAsync(request);
            var response = await _restClient.ExecuteAsync<List<GenreGetVM>>(request);

            if (!response.IsSuccessful) 
            {
                ViewBag.Err = response.ErrorMessage;
                return View();
            }

            //List<GenreGetVM> vm = JsonSerializer.Deserialize<List<GenreGetVM>>(response.Content, new JsonSerializerOptions {PropertyNameCaseInsensitive = true }); - response-dan vm e kecidin diger uzun yolu,asagidaki qisa yoludur.
            List<GenreGetVM> vm = response.Data;

            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var request = new RestRequest($"Genres/{id}", Method.Get);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreGetVM>>(request);

            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            GenreGetVM vm = response.Data.Data;

            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreCreateVM vm)
        {
            var request = new RestRequest("Genres", Method.Post);
            request.AddJsonBody(vm);

            var response = await _restClient.ExecuteAsync<ApiResponseMessage<GenreCreateVM>>(request);

            if (!response.IsSuccessful)
            {
                ModelState.AddModelError("Name", response.Data.ErrorMessage);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
