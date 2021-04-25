using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace User.Identity.Services
{
    public class UserService:IUserService
    {
        private IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> CheckOrCreate(string phone)
        {
            var client=_httpClientFactory.CreateClient("UserApi");
            HttpContent httpContent = new StringContent("{\"phone\":\"17602180068\"}", Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"/api/User/check-or-create", httpContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                int intResult=Convert.ToInt32(result);
                return intResult > 0;
            }
            //调用user 的 checkorcreate
            else
            {
                return false;
            }
        }
    }
}
