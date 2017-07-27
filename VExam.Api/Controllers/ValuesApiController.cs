using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace VExam.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesApiController : Controller
    {
        // GET api/values
        [HttpPost]
       // [ValidateAntiForgeryToken]
         [Route("logout")]
        public async Task<bool> Logout()
        {
            Console.WriteLine("Logging out...");
          	 await HttpContext.Authentication.SignOutAsync("Cookie");
        	return true;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
         [Authorize]
        public async Task<string> GetAsync(int id)
        {
            Console.WriteLine("Logging out...");
        //  JWTAuth::invalidate($token);
        await HttpContext.Authentication.SignOutAsync("Cookie");
        return "value";
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
