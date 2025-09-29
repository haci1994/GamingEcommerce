using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.GeneralServices;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;

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

        public async Task<string> AddToCart(int id, int colorId)
        {
            var existingProduct = await _productService.GetByIdAsync(id);
            var color = "";
            var html = "";


            if (existingProduct == null || existingProduct.ProductColors.Count < 1)
                return "Error";
            else
            {
                color = existingProduct.ProductColors.FirstOrDefault(x => x.Id == colorId)!.Name;
            }
            
            var list = GetCookie();

            int index = list.FindIndex(x => x.ProductId == id);

            if(index == -1)
            {
                list.Add(new BasketItemViewModel
                {
                    Color = color,
                    ImageName = existingProduct.ProductColors.FirstOrDefault(x => x.Id == colorId)!.ProductColorImages[0].ImageName,
                    Name = existingProduct.Name,
                    ProductId = existingProduct.Id,
                    Price = existingProduct.Price,
                });

                html = $"<div class=\"tf-mini-cart-item\" id=\"{existingProduct.Id}-{existingProduct.Id}\">\r\n        <div class=\"tf-mini-cart-image\">\r\n            <a href=\"product-detail.html\">\r\n                <img src=\"/images/products/{existingProduct.ProductColors[0].ProductColorImages[0].ImageName}\" alt=\"\">\r\n            </a>\r\n        </div>\r\n        <div class=\"tf-mini-cart-info\">\r\n            <a class=\"title link\" href=\"product-detail.html\">{existingProduct.Name}</a>\r\n            <div class=\"meta-variant\">{existingProduct.ProductColors[0].Name}</div>\r\n            <div class=\"price fw-6\">{existingProduct.Price}</div>\r\n            <div class=\"tf-mini-cart-btns\">\r\n               <div class=\"wg-quantity small\" style=\"background-color:white; border: 1px solid gray\">\r\n                                                         <input disabled type=\"text\"  name=\"number\" value=1>\r\n   <span style=\"padding-right:20px\">pcs</span>\r\n           </div>                <a class=\"tf-mini-cart-remove\" onclick=\"RemoveFromBasket({existingProduct.Id}, this)\">Remove</a>\r\n            </div>\r\n        </div>\r\n    </div>`";
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

             
            return html;
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

        public void RemoveFromBasket(int id)
        {
            var list = GetCookie();

            var item = list.Find(x => x.ProductId == id);

            if (item == null)
            {
                return;
            }
            else
            {
                list.Remove(item);
            }

            var listJson = JsonConvert.SerializeObject(list);

            Response.Cookies.Append("GAMING_ECOMMERCE_BASKET", listJson, new CookieOptions
            {
                Expires= DateTime.UtcNow.AddDays(1), HttpOnly = true
            });
        }

        public async Task<decimal> GetPrice(int id)
        {
            var product =  await _productService.GetByIdAsync(id);

            if(product == null) return 0;

            return product.Price;
        }
    }
}
