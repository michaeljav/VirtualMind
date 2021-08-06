using Currency.CustomModels;
using Currency.Helper;
using Currency.Models;
using Currency.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private CurrencyService _currencyService;

        public CurrencyController(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("{currency}")]
        public async Task<IActionResult> Currency(string currency) {
          
                var response = await _currencyService.Currency(currency);
                if (response == null)
                    return Ok(new Response(false, "No existe conversión para esta moneda, pruebe dolar o real", null));

                return Ok(new Response(true, HttpStatusCode.OK.ToString(), response));
           
            
        }

       
        [Authorize]
        [HttpPost("change")]
        public async Task<IActionResult> CurrencyChange(CurrencyChangeRequest currencyChange)
        {
            if (String.IsNullOrWhiteSpace(currencyChange.CbuyCurrencyOrigenAmount) || String.IsNullOrWhiteSpace(currencyChange.CbuyCurrencyToBuyType))            
                return Ok(new Response(false, "Por favor Introducir un número", null));
          

            if (currencyChange.CbuyCurrencyOrigenAmount != "." && !IsNumeri.IsNumeric(currencyChange.CbuyCurrencyOrigenAmount))
                return Ok(new Response(false, "Por favor Introducir un número", null));

            if (Convert.ToDecimal(currencyChange.CbuyCurrencyOrigenAmount) <= 0)
                return Ok(new Response(false, "El valor debe ser mayor de 0", null));


            var response = await _currencyService.ChangeCurrency(currencyChange);
            if (response == null)
                return Ok(new Response(false, "No existe conversión para esta moneda, pruebe dolar o real", null));

            if (response.IslimitExceeded)
                return Ok(new Response(false, "El limite de compra  es  "
                                                + response.ValueLimit
                                                +" para la moneda "
                                                +currencyChange.CbuyCurrencyToBuyType
                                                +", por lo cual, solo puede comprar por este mes "
                                                +response.MaxValueToExchange.ToString("#.000")
                                                +"  Pesos Argentinos---> ARS", null));

            return Ok(new Response(true, HttpStatusCode.OK.ToString(), response));


           
            
        }

    }
}
