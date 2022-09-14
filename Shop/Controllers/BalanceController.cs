using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Entity;
using Shop.Service.Interfaces;

namespace Shop.Controllers
{
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userName = User.Identity.Name;
            var response = await _balanceService.Get(userName);
            return View(response.Data as User);
        }
    }
}