using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDTO _response;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public object? Get()
        {
            try
            {
                var coupons = _dbContext.Coupons.ToList();
                _response.Result = coupons;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object? Get(int id)
        {
            try
            {
                var coupon = _dbContext.Coupons.First(u => u.CouponId == id);
                _response.Result = coupon;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
