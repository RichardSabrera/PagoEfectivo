using Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApiEfectivo.BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WsApiEfectivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchListController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        public readonly ISearchListCode _searchListCode;
        public SearchListController(IConfiguration configuration, ISearchListCode searchListCode)
        {
            _configuration = configuration;
            _searchListCode = searchListCode;
            ConstVariable.connStr = _configuration.GetConnectionString("DevConnection");
        }

        // GET api/<SearchListController>/5
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_searchListCode.SearchListAll().Result);
        }
    }
}
