using Domain.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WsApiEfectivo.BusinessLogic;

namespace WsApiEfectivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        public IConfiguration _configuration { get; }
        public IExchangeCode _ExchangeCode;
        public ExchangeController(IConfiguration configuration, IExchangeCode exchangeCode)
        {
            _ExchangeCode = exchangeCode;
            _configuration = configuration;
            ConstVariable.connStr = _configuration.GetConnectionString("DevConnection");
        }

        [HttpGet("{code}")]
        public ActionResult Get(string code)
        {
            return Ok(_ExchangeCode.ExchangeCodeValidate(code).Result);
        }
    }
}
