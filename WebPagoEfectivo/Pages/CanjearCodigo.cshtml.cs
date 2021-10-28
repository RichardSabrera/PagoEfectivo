using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebPagoEfectivo.Connection;

namespace WebPagoEfectivo.Pages
{
    public class CanjearCodigoModel : PageModel
    {
        private static CalledToApi calledToApi = new CalledToApi();
        public void OnGet()
        {

        }

        public async Task OnGetSearchAsync(string search)
        {
            var _resukt = calledToApi.CallToApiGET($"https://localhost:44396/api/Exchange/", "search").Result;

            _resukt.EnsureSuccessStatusCode();

            JsonConvert.DeserializeObject<ResponseCustom>(_resukt.Content.ReadAsStringAsync().Result); 
        }

        public async Task<JsonResult> OnGetProductAsJsonAsync(int id)
        {
            var _resukt = calledToApi.CallToApiGET($"https://localhost:44396/api/Exchange/", "search").Result;

            _resukt.EnsureSuccessStatusCode();

            return new JsonResult(JsonConvert.DeserializeObject<ResponseCustom>(_resukt.Content.ReadAsStringAsync().Result));
        }
    }
}
