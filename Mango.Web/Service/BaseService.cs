using System.Net;
using System.Text;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MangoAPI");
                var message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                //token

                if (string.IsNullOrWhiteSpace(requestDTO.Url))
                {
                    return new ResponseDTO()
                    {
                        Message = "Request URL is null or empty",
                        IsSuccess = false
                    };
                }

                message.RequestUri = new Uri(requestDTO.Url);

                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                var apiResponse = new HttpResponseMessage();

                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDTO()
                        {
                            Message = "Not found",
                            IsSuccess = false
                        };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDTO()
                        {
                            Message = "Access Denied",
                            IsSuccess = false
                        };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDTO()
                        {
                            Message = "Unauthorized",
                            IsSuccess = false
                        };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDTO()
                        {
                            Message = "Internal Server Error",
                            IsSuccess = false
                        };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var responseDto = new ResponseDTO()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
                return responseDto;
            }
        }
    }
}
