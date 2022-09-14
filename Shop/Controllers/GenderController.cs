using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Service.Interfaces;

namespace Shop.Controllers
{
    public class GenderController : Controller
    {
        private readonly IClothService _clothService;

        public GenderController(IClothService clothService)
        {
            _clothService = clothService;
        }

        [HttpPost]
        public async Task<JsonResult> GetGenders(string term)
        {
            var types = await _clothService.GetGenders(term);
            return Json(types.Data);
        }
    }
}