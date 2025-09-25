using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.GeneralServices;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;

namespace GamingEcommerce.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        private const string CART_KEY = "GAMING_ECOMMERCE_BASKET";

        public async Task<IActionResult> AddToCart(int id, int colorId)
        {
            var existingProduct = await _productService.GetByIdAsync(id);
            var color = "";
            
            if (existingProduct == null || existingProduct.ProductColors.Count < 1)
                return BadRequest();
            else
            {
                color = existingProduct.ProductColors.FirstOrDefault(x => x.Id == colorId).Name;
            }
            
            var list = GetCookie();

            int index = list.FindIndex(x => x.ProductId == id);

            if(index == -1)
            {
                list.Add(new BasketItemViewModel
                {
                    Color = color,
                    ImageName = existingProduct.ProductColors.FirstOrDefault(x => x.Id == colorId).ProductColorImages[0].ImageName,
                    Name = existingProduct.Name,
                    ProductId = existingProduct.Id
                });
            }
            else
            {
                list[index].Count++;
            }

                var json = JsonConvert.SerializeObject(list);
            Response.Cookies.Append(CART_KEY, json, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1),
                HttpOnly = true,
                IsEssential = true
            });

            return NoContent();
        }
            

        public List<BasketItemViewModel> GetCookie()
        {
            var json = Request.Cookies[CART_KEY];

            var list = new List<BasketItemViewModel>();

            if (!string.IsNullOrEmpty(json))
            {
                list = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json) ?? [];
            }

            return list;
        }
    }
}
