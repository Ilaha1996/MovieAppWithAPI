using Microsoft.AspNetCore.Mvc;
using MovieApp.MVC.APIResponseMessages;
using MovieApp.MVC.ViewModels.GenreVM;
using MovieApp.MVC.ViewModels.MovieVM;
using RestSharp;

namespace MovieApp.MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly RestClient _restClient;
        public MovieController()
        {
            _restClient = new RestClient("https://localhost:7006/api");
        }
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("movies", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<MovieGetVM>>>(request);

            if(!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            return View(response.Data.Data);
        }

        public async Task<IActionResult> Create()
        {
            var request = new RestRequest("genres",Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);
            if(!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            ViewBag.Genres = response.Data.Data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateVM vm)
        {
            var request = new RestRequest("genres", Method.Get);
            var response = await _restClient.ExecuteAsync<ApiResponseMessage<List<GenreGetVM>>>(request);
            if (!response.IsSuccessful)
            {
                ViewBag.Err = response.Data.ErrorMessage;
                return View();
            }

            ViewBag.Genres = response.Data.Data;

            if (!ModelState.IsValid) return View();

            return RedirectToAction("Index");


        }

    }
}
