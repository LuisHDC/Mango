using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public CouponAPIController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public object? Get()
        {
            try
            {
                var coupons = _dbContext.Coupons.ToList();
                return coupons;
            }
            catch (Exception)
            {

            }

            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object? Get(int id)
        {
            try
            {
                var coupon = _dbContext.Coupons.FirstOrDefault(u => u.CouponId == id);
                return coupon;
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}
