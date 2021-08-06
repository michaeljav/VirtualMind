using Currency.CustomModels;
using Currency.Data;
using Currency.Interface;
using Currency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Currency.Controllers
{



    //[Route("api/[controller]")]
    //[ApiController]

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly CurrencyContext _context;

        public UserController(IUserService userService, CurrencyContext context)
        {
            _userService = userService;
            _context = context;
        }

        //public UserController()
        //{
           
        //}

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response =  await _userService.Authenticate(model);     

            if (response == null)
                return Ok(new Response( false,"Usuario o Contraña no existe", null));
           

            return Ok(new Response( true, HttpStatusCode.OK.ToString(),  response));
        }
    }
}
