
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Models
{/// <summary>
/// Response class 
/// </summary>
    public class AuthenticateResponse
    {
       
        public string Token { get; set; }


        public AuthenticateResponse( string token)
        {            
            Token = token;
        }
        public AuthenticateResponse()
        {
         
        }
    }
}
