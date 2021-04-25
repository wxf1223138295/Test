using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.Api.Model;
using User.Api.Model.Input;
using User.Api.UserDb;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserDbContext _dbContext;

        public UserController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("check-or-create")]
        [HttpPost]
        public async Task<int> CheckOrCreate(CreateUserInput input)
        {
            var isExsit = await _dbContext.UserInfo.AnyAsync(p => p.Phone == input.Phone);
            if (isExsit)
            {
                var idresult = await _dbContext.UserInfo.FirstOrDefaultAsync(p => p.Phone == input.Phone);
                return idresult.Id;
            }
            else
            {
                UserInfo userinfo =new UserInfo();
                userinfo.Phone = input.Phone;
                await _dbContext.AddAsync(userinfo);
                var result = await _dbContext.SaveChangesAsync();
                return result;
            }
        }
        [HttpGet]
        [Route("Info")]
        public async Task<IActionResult> Info(string name)
        {
            var retsult=await _dbContext.UserInfo.Where(p => p.Name == name).ToListAsync();

            return new JsonResult(retsult);
        }
    }
}