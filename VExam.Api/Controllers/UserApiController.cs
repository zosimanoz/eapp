using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VExam.DTO;
using VExam.Services.Users;
using VPortal.Core.Log;
using VPortal.WebExtensions.API;

namespace VExam.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/user")]
    public class UserApiController : BaseApiController
    {

        private IUserService _userService;
        private ILogger _logger;

        public UserApiController(IUserService userService, ILogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
         [AllowAnonymous]
        [Route("register")]
        public async Task<ApiResponse> RegisterAsync([FromBody] User model)
        {
            model.Password = PasswordManager.GetHashedPassword(model.EmailAddress, model.Password);
            try
            {
                var result = await _userService.CrudService.InsertAsync(model);
                return HttpResponse(200, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
        [HttpPost]
        //  [Authorize]
        [Route("changepassword")]
        public async Task<ApiResponse> ChangeAsync([FromBody] Password model)
        {
            if (model.NewPassword != model.ConfirmPassword)
            {
                return HttpResponse(200, "", "Password and Confirm Password doesn't match.");
            }
            string hashedUserPassword = await _userService.GetUserPasswordAsync(model.EmailAddress);

            bool oldPasswordIsValid = PasswordManager.ValidateBcrypt(model.EmailAddress, model.OldPassword, hashedUserPassword);
            if (oldPasswordIsValid)
            {
                try
                {
                    model.NewPassword = PasswordManager.GetHashedPassword(model.EmailAddress, model.NewPassword);
                    var result = await _userService.UpdatePasswordAsync(model);
                    return HttpResponse(500, "", result);
                }
                catch (Exception e)
                {
                    _logger.Log(LogType.Error, () => e.Message, e);
                    return HttpResponse(500, e.Message);
                }
            }
            else
            {
                return HttpResponse(500, "", "Invalid Old Password.");
            }
        }
        [HttpPost]
        //  By HR only
        [Route("changepassword")]
        public async Task<ApiResponse> ResetPasswordAsync(string emailAddress, string Password)
        {
            try
            {
                string hashedPassword = PasswordManager.GetHashedPassword(emailAddress, Password);
                var result = await _userService.ResetPasswordAsync(emailAddress, hashedPassword);
                return HttpResponse(500, "", result);
            }
            catch (Exception e)
            {
                _logger.Log(LogType.Error, () => e.Message, e);
                return HttpResponse(500, e.Message);
            }
        }
    }
}