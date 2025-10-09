using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GamingEcommerce.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly IDiscountCodeService _couponService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CouponController(IDiscountCodeService couponService, IOrderService orderService, IMapper mapper)
        {
            _couponService = couponService;
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _couponService.GetAllAsync();
            
            foreach(var item in list)
            {
                var orderCount = await _orderService.GetAllAsync(predicate: x => x.DiscountCodeId == item.Id);
                item.Used = orderCount.Count();
            }

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create (CreateDiscountCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }

            var existCoupon = await _couponService.GetAsync(predicate: x=> x.Code == model.Code);

            if (existCoupon != null)
            {
                ModelState.AddModelError("Code", "The code you entered is exist");
                return View(model);
            }

            await _couponService.AddAsync(model);

            return RedirectToAction("Index", "Coupon");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var existCoupon =  await _couponService.GetByIdAsync(id);

            if (existCoupon == null) return NotFound();

            existCoupon.IsDeleted = true;
            existCoupon.IsActive = false;

            await _couponService.UpdateAsync(_mapper.Map<UpdateDiscountCodeViewModel>(existCoupon));

            return RedirectToAction("Index",  "Coupon");
        }

        public async Task<IActionResult> Restore(int id)
        {
            var existCoupon = await _couponService.GetByIdAsync(id);

            if (existCoupon == null) return NotFound();

            existCoupon.IsDeleted = false;
            

            await _couponService.UpdateAsync(_mapper.Map<UpdateDiscountCodeViewModel>(existCoupon));

            return RedirectToAction("Index", "Coupon");
        }

        public async Task<IActionResult> Deactivate(int id)
        {
            var existCoupon = await _couponService.GetByIdAsync(id);

            if (existCoupon == null) return NotFound();

            existCoupon.IsActive = false;

            await _couponService.UpdateAsync(_mapper.Map<UpdateDiscountCodeViewModel>(existCoupon));

            return RedirectToAction("Index", "Coupon");
        }

        public async Task<IActionResult> Activate(int id)
        {
            var existCoupon = await _couponService.GetByIdAsync(id);

            if (existCoupon == null) return NotFound();

            existCoupon.IsActive = true;

            await _couponService.UpdateAsync(_mapper.Map<UpdateDiscountCodeViewModel>(existCoupon));

            return RedirectToAction("Index", "Coupon");
        }

    }
}
