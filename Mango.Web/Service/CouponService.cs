using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateCouponsAsync(CouponDTO couponDTO) =>
            await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDTO,
                Url = $"{SD.CouponAPIBase}/api/coupon/",
            });

        public async Task<ResponseDTO?> DeleteCouponsAsync(int id) =>
            await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.CouponAPIBase}/api/coupon/{id}",
            });

        public async Task<ResponseDTO?> GetAllCouponsAsync() =>
            await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}/api/coupon",
            });


        public async Task<ResponseDTO?> GetCouponAsync(string couponCode) =>
            await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}/api/coupon/GetByCode/{couponCode}",
            });

        public async Task<ResponseDTO?> GetCouponByIDAsync(int id) =>
            await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}/api/coupon/{id}",
            });

        public async Task<ResponseDTO?> UpdateCouponsAsync(CouponDTO couponDTO) =>
            await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDTO,
                Url = $"{SD.CouponAPIBase}/api/coupon/",
            });
    }
}
