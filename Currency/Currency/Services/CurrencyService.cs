using Currency.CustomModels;
using Currency.Data;
using Currency.Helper;
using Currency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Currency.Services
{
    public class CurrencyService
    {
        private readonly AppSettings _appSettings;
        private readonly CurrencyContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CurrencyService(IOptions<AppSettings> appSettings, CurrencyContext context, IHttpContextAccessor httpContext)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _httpContext = httpContext;
        }
        /// <summary>
        ///search  currency at bancoprovincia https://www.bancoprovincia.com.ar/Principal/Dolar   
        /// </summary>
        /// <param name="currency">dolar or real</param>
        /// <returns>CurrencyResponse object</returns>
        public async Task<CurrencyResponse> Currency(string currency)
        {
            
            if (String.IsNullOrWhiteSpace(currency))
            {
                return null;
            }

            try
            {
                //Currency change
                CurrencyResponse currencyResponse = await CurrencyRate(currency);

                return currencyResponse;
            }
            catch (Exception ex)
            {
                //Here we can implement logs
                return null;
            }
        }
        /// <summary>
        /// I give back a quarter of the dollar
        /// </summary>
        /// <param name="currencyResponse"></param>
        /// <returns></returns>
        public CurrencyResponse CurrencyReal(CurrencyResponse currencyResponse)
        {
            string numberDecimal = "#.000";

            currencyResponse.Buy = (Double.Parse(currencyResponse.Buy) / 4).ToString(numberDecimal);
            currencyResponse.Sell = (Double.Parse(currencyResponse.Sell) / 4).ToString(numberDecimal);

            return currencyResponse;
        }
        /// <summary>
        /// Get Currency Rate
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<CurrencyResponse> CurrencyRate(string currency)
        {


            try
            {
                //if the currency is "real" the currency to search is dollar otherwise what was send in currency
                string currencySearch = currency.Trim().ToUpper() == "REAL" ? "dolar" : currency;

                //String Api Url
                string api = _appSettings.Api_Usd_Real + currencySearch;
                var client = new RestClient(api);
                var request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                //Convert from  string to list 
                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<String>>(response.Content);

                //Insert the response into an object 
                var currencyObj = new CurrencyResponse(model[0], model[1], model[2]);

                //if it is real currency
                if (currency.Trim().ToUpper() == "REAL")
                {
                    return CurrencyReal(currencyObj);
                }

                return currencyObj;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public async Task<CurrencyChangeResonse> ChangeCurrency(CurrencyChangeRequest currencyChange)
        {
            // change
            CurrencyChangeResonse currencyChangeResonse = new CurrencyChangeResonse();
                       
            var user = (User)_httpContext.HttpContext.Items["User"];

            //Currency to change
            string currency = currencyChange.CbuyCurrencyToBuyType;
           

            try
            {

                //Current exchange rate
                CurrencyResponse currencyResponse = await CurrencyRate(currency);

                //limits by currency type
                Dictionary<string, string> limitsByCurrencyType = new Dictionary<string, string>();
                limitsByCurrencyType.Add("Dolar", "200");
                limitsByCurrencyType.Add("Real", "300");

                //currency changed               
                Dictionary<string, string> _currentChanged = await ConvertToSellCurrencyWithLimit(user.UseId, Convert.ToDecimal(currencyChange.CbuyCurrencyOrigenAmount),
                                                                                                  Convert.ToDecimal(currencyResponse.Sell),currency, limitsByCurrencyType);

               

                //object to return
                
                currencyChangeResonse.UseId = user.UseId;
                currencyChangeResonse.UseName = user.UseName;
                currencyChangeResonse.CbuyCurrencyOrigenType = "ARS";
                currencyChangeResonse.CbuyCurrencyOrigenAmount = Convert.ToDecimal(currencyChange.CbuyCurrencyOrigenAmount);
                currencyChangeResonse.CbuyCurrencyToBuyType = currencyChange.CbuyCurrencyToBuyType;                
                currencyChangeResonse.CbuyCurrencyToBuyRate = Convert.ToDecimal(currencyResponse.Sell);
                currencyChangeResonse.CbuyCurrencyToBuyAmountCurrencyChanged = _currentChanged["exchanged"] != "" ? Math.Round(Convert.ToDecimal(_currentChanged["exchanged"]),3): 0;
                currencyChangeResonse.IslimitExceeded = Convert.ToBoolean(_currentChanged["overlimit"]);
                currencyChangeResonse.CbuyCreateDate = DateTime.Now;

                if (_currentChanged["overlimit"] == "true")
                {
                    if (currency.Trim().ToUpper() == "DOLAR")
                    {
                        currencyChangeResonse.ValueLimit = Convert.ToDecimal(limitsByCurrencyType["Dolar"]);
                    }else
                    if (currency.Trim().ToUpper() == "REAL")
                    {
                        currencyChangeResonse.ValueLimit = Convert.ToDecimal(limitsByCurrencyType["Real"]);
                    }

                    currencyChangeResonse.MaxValueToExchange = Convert.ToDecimal(_currentChanged["MaxValueToExchange"]);
                    currencyChangeResonse.IslimitExceeded = true;
                    return currencyChangeResonse;
                }

                //Save purchase
                if (currencyChange.toSave)
                {
                    await SavePurchaseAsync(currencyChangeResonse);
                }
              

                return currencyChangeResonse;
            }
            catch (Exception ex)
            {
                //Here we can implement logs
                return null;
            }
        }

        public async Task SavePurchaseAsync(CurrencyChangeResonse currencyChangeResonse)
        {
            try
            {
                CurrencyBuy currencyBuy = new CurrencyBuy();
                currencyBuy.UseId = currencyChangeResonse.UseId;
                currencyBuy.UseName = currencyChangeResonse.UseName;
                currencyBuy.CbuyCurrencyOrigenType = currencyChangeResonse.CbuyCurrencyOrigenType;
                currencyBuy.CbuyCurrencyOrigenAmount = currencyChangeResonse.CbuyCurrencyOrigenAmount;
                currencyBuy.CbuyCurrencyToBuyType = currencyChangeResonse.CbuyCurrencyToBuyType;
                currencyBuy.CbuyCurrencyToBuyRate = currencyChangeResonse.CbuyCurrencyToBuyRate;
                currencyBuy.CbuyCurrencyToBuyAmountCurrencyChanged = currencyChangeResonse.CbuyCurrencyToBuyAmountCurrencyChanged;
                currencyBuy.CbuyCreateDate = DateTime.Now;
                _context.CurrencyBuys.Add(currencyBuy);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Convert a amount limited of money
        /// </summary>
        /// <param name="currencyOrigen">Amount in Currency Origen</param>
        /// <param name="sellRate">Currency Exchange Rate</param>
        /// <param name="currentToBuyType">Currency Type to buy</param>
        /// <param name="limit">Dictioanry of limits by currency for Dollar 200 and for Real  300</param>
        /// <returns>Dictionary</returns>

        public async Task<Dictionary<string,string>> ConvertToSellCurrencyWithLimit(int userId,decimal currencyOrigen,decimal sellRate,string currentToBuyType, Dictionary<string, string> limit) {

            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("exchanged", "");
                dict.Add("overlimit", "false");
                dict.Add("MaxValueToExchange", "");

                //currency changed current
                //   decimal _currentChanged = Convert.ToDecimal(currencyOrigen) / Convert.ToDecimal(sellRate);
                decimal _currentChanged = currencyOrigen / sellRate;

                //Search total of currency exchanges done to this user, currency type to buy and this month
                //SP_GetTotalMonth totalMonthExchanges = await GetChangesOfMonth(3, "Dolar");
                SP_GetTotalMonth totalMonthExchanges = await GetChangesOfMonth(userId, currentToBuyType);

                //sum current currency exchange and total amount currency exchanges done by the user
                var totalCurrencyChangedNowAndMonth = _currentChanged + totalMonthExchanges.TotalMonth;

                //I can't change more than milit
                if (currentToBuyType.Trim().ToUpper() == "DOLAR" && totalCurrencyChangedNowAndMonth > Convert.ToDecimal(limit["Dolar"]))
                {
                    dict["overlimit"] = "true";

                    //max value to exchange

                    //difference in foreign currency
                    var diffInforeignCurrency = totalCurrencyChangedNowAndMonth - Convert.ToDecimal(limit["Dolar"]);
                    //get difference  in currency origen
                    var diffInOrigenCurrency = diffInforeignCurrency * sellRate;
                    //get max value to exchange
                    dict["MaxValueToExchange"] = (currencyOrigen - diffInOrigenCurrency).ToString();


                    return dict;
                }

                if (currentToBuyType.Trim().ToUpper() == "REAL" && totalCurrencyChangedNowAndMonth > Convert.ToDecimal(limit["Real"]))
                {
                    dict["overlimit"] = "true";

                    //difference in foreign currency
                    var diffInforeignCurrency = totalCurrencyChangedNowAndMonth - Convert.ToDecimal(limit["Real"]);
                    //get difference  in currency origen
                    var diffInOrigenCurrency = diffInforeignCurrency * sellRate;
                    //get max value to exchange
                    dict["MaxValueToExchange"] = (currencyOrigen - diffInOrigenCurrency).ToString();

                    return dict;
                }

                dict["exchanged"]= _currentChanged.ToString();
                return dict;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
        /// <summary>
        /// Get changes of the month by user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="currentToBuyType"></param>
        /// <returns></returns>
        public async Task<SP_GetTotalMonth> GetChangesOfMonth(int userId,string currentToBuyType)
        {
            try
            {
                SP_GetTotalMonth sP_GetTotalMonth = new SP_GetTotalMonth();

              string sql = "EXEC dbo.GetTotalMonth  @userId, @currentToBuyType";
              List<SqlParameter> parms = new List<SqlParameter>
                { 
                  // Create parameters    
                    new SqlParameter { ParameterName = "@userId", Value = userId },
                    new SqlParameter { ParameterName = "@currentToBuyType", Value = currentToBuyType }
                    
                };

               
                List<SP_GetTotalMonth>  sP_GetTotalMonthList = await _context.sp_GetTotalMonth.FromSqlRaw(sql, parms.ToArray()).ToListAsync();

                if (sP_GetTotalMonthList.Count > 0)
                {
                    sP_GetTotalMonth = sP_GetTotalMonthList[0];
                }
               

                return sP_GetTotalMonth;
            }
            catch (Exception EX)
            {

                throw;
            }
           
           
        }


    }
}
