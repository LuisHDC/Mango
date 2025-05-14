using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private ResponseDTO _response;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public object? Get()
        {
            try
            {
                var coupons = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(coupons);
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
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public object? Get(string code)
        {
            try
            {
                var coupon = _dbContext.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDTO Post([FromBody] CouponDTO couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Add(coupon);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        public ResponseDTO Put([FromBody] CouponDTO couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _dbContext.Coupons.Update(coupon);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                var coupon = _dbContext.Coupons.First(u => u.CouponId == id);
                _dbContext.Coupons.Remove(coupon);
                _dbContext.SaveChanges();
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
