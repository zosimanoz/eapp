using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace VPortal.TokenManager
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(24);
        public SigningCredentials SigningCredentials { get; set; }
        public Func<string, string, Task<ClaimsIdentity>> IdentityResolver { get; set; }
        public Func<Task<string>> NonceGenerator { get; set; }
             = () => Task.FromResult(Guid.NewGuid().ToString());  
    }
}