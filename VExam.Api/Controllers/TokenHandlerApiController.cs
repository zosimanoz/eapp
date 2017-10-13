
using System;
using System.Text;
using Jose;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VExam.DTO;
using VPortal.WebExtensions.API;

namespace VExam.Api
{
   [AllowAnonymous]
    public class TokenHandlerApiController : BaseApiController
    {

        [HttpPost]
        [Route("api/v1/token/decode")]
        public ApiResponse Decode(string accessToken)
        {
           
            var secret = Encoding.UTF8.GetBytes("18b4bebb-8dd7-4b97-ac2c-16ceca5647f2");
            string decoded = JWT.Decode(accessToken, secret);
            var result = JsonConvert.DeserializeObject<Token>(decoded);
            return HttpResponse(200, "", result);
        }
    }
}