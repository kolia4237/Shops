using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Domain.Entity;
using Shop.Domain.ViewModels;
using Shop.Service.Interfaces;

namespace Shop.Controllers
{
    public class ClothController : Controller
    {
        private readonly IClothService _clothService;

        public ClothController(IClothService clothService)
        {
            _clothService = clothService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> List(int parameter)
        {
            var response = await _clothService.GetClothes(parameter);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data as List<Cloth>);
            }
            return View("Error");
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _clothService.Get(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data as Cloth);   
            }
            return View("Error");
        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _clothService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("List");
            }
            return View("Error");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View();
            var response = await _clothService.Get(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data as ProductViewModel);   
            }
            return View("Error");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData = null;
                    if (model.Avatar != null)
                    {
                        using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                        }
                    }
                    
                    var response = await _clothService.Create(model, imageData);
                    return RedirectToAction("List");
                }
                else
                {
                    var response = await _clothService.Edit(model.Id, model);
                }
                return RedirectToAction("List");   
            }

            return View();
        }
    }
}