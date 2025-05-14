using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public async Task<IActionResult> CouponIndex()
        {
            var coupons = new List<CouponDTO>();

            var response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Coupon created successfully!";
                coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result) ?? string.Empty);
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(coupons);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _couponService.CreateCouponsAsync(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            var response = await _couponService.GetCouponByIDAsync(couponId);

            if (response != null && response.IsSuccess)
            {
                var model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result) ?? string.Empty);

                return View(model);
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDTO model)
        {
            var response = await _couponService.DeleteCouponsAsync(model.CouponId);

            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Coupon deleted successfully!";
                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["Error"] = response?.Message;
            }

            return View(model);
        }
    }
}
