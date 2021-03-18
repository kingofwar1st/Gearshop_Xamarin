using GearShopAPI.Data;
using GearShopAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GearShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        static Regex ValidEmailRegex = CreateValidEmailRegex();
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }
        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }

        GearContext _userContext;
        public UserController(GearContext userContext)
        {
            _userContext = userContext;
        }
        [HttpGet]
        public ActionResult GetUser()
        {
            return Ok(_userContext.Users);
        }
        [HttpPost("adduser")]
        public ActionResult Post([FromBody] UserInformation user)
        {
            bool kq = EmailIsValid(user.Email);
            var checkSdt = _userContext.Users.SingleOrDefault(x => x.Sdt == user.Sdt);
            var checkEmail = _userContext.Users.SingleOrDefault(x => x.Email == user.Email);

            if (checkSdt == null && checkEmail == null && kq == true)
            {

                _userContext.Users.Add(user);
                _userContext.SaveChanges();
                return Ok(new ApiResponse()
                {
                    Success = true
                });
            }
            else if (kq == false)
            {
                return Ok(new ApiResponse()
                {
                    Message = "EmailFalse",
                    Success = false
                });
            }
            else if (checkSdt != null)
            {
                return Ok(new ApiResponse()
                {
                    Message = "Sdt",
                    Success = false
                });
            }
            else if (checkEmail != null)
            {
                return Ok(new ApiResponse()
                {
                    Message = "Email",
                    Success = false
                });
            }
            else
            {
                return Ok(new ApiResponse()
                {
                    Message = "Sdt&Email",
                    Success = false
                });
            }
        }
        [HttpPost("CheckUser")]
        public ActionResult Check([FromBody] UserInformation user)
        {
            var productcheck = _userContext.Users.SingleOrDefault(x => x.Pass == user.Pass && (x.Sdt == user.Sdt || x.Email == user.Email));
            if (productcheck == null)
            {
                return Ok(new ApiResponse()
                {
                    Success = false
                });
            }
            else
            {
                return Ok(new ApiResponse()
                {

                    Success = true,
                    Content = productcheck
                });
            }
        }
    }
}
