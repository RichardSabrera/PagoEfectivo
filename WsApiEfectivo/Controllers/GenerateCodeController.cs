using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WsApiEfectivo.BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WsApiEfectivo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateCodeController : ControllerBase
    { 
        public IConfiguration _configuration { get; }
        public readonly IGenerateCode _generateCode;

        public GenerateCodeController(IConfiguration configuration, IGenerateCode generateCode)
        {
            _configuration = configuration;
            _generateCode = generateCode;
            ConstVariable.connStr = _configuration.GetConnectionString("DevConnection");
        }

        // POST api/<GenerateCodeController>
        [HttpPost]
        public ActionResult Post([FromBody] Client client)
        {
            return Ok(_generateCode.CreateCodeForNameEmail(client.Name, client.Email).Result);
        }
    }
}
