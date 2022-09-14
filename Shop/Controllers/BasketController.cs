using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Entity;
using Shop.Service.Interfaces;

namespace Shop.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        
        public async Task<IActionResult> Add(int id)
        {
            await _basketService.Add(User.Identity.Name, id);
            return RedirectToAction("List");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _basketService.Delete(User.Identity.Name, id);
            return RedirectToAction("List");
        }
        
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = User.Identity.Name;
            var response = await _basketService.Select(user);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data as List<Cloth>);
            }
            return View("Error");
        }
    }
}