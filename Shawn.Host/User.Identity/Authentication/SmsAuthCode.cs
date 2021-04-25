using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using User.Identity.Services;

namespace User.Identity.Authentication
{
    public class SmsAuthCode : IExtensionGrantValidator
    {
        public IAuthCodeService _authcodeservice;
        public IUserService _userService;


        public SmsAuthCode(IAuthCodeService authcodeservice, IUserService userService)
        {
            _authcodeservice = authcodeservice;
            _userService = userService;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var phone = context.Request.Raw.Get("phone");
            var authcode = context.Request.Raw.Get("auth_code");
            var errorGrantValidationResult = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
            if (string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(authcode))
            {
                context.Result = errorGrantValidationResult;
                return;
            }

            if (!await _authcodeservice.ValidateAuthCode())
            {
                context.Result = errorGrantValidationResult;
                return;
            }

            var userid = await _userService.CheckOrCreate(phone);
            if (!userid)
            {
                context.Result = errorGrantValidationResult;
                return;
            }
            context.Result = new GrantValidationResult(userid.ToString(), GrantType);
        }

        public string GrantType => "sms_code";
    }
}
