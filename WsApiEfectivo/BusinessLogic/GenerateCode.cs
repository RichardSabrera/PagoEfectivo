using Domain.Entities;
using Domain.Useful;
using System;
using System.Threading.Tasks;
using WsApiEfectivo.DataAccess;

namespace WsApiEfectivo.BusinessLogic
{
    public class GenerateCode: IGenerateCode
    {
        public static GenerateCodeDA _gcode = new GenerateCodeDA();
        public static RandomGenerator _generate = new RandomGenerator();
        public static ResponseCustom _responseCustom = new ResponseCustom();
        public async Task<ResponseCustom> CreateCodeForNameEmail(string name, string email)
        {
            var _inv = _generate.RandomPassword();
            int _result;
            if (await _gcode.ValidateEmail(email) == 0)
            {
                _result = await _gcode.InsDataUser(name, email, _inv, DateTime.Now.AddYears(1));
            }
            else
            {
                _responseCustom.message = "El correo ingresado ya existe.";
                _responseCustom.status = false;
                return _responseCustom;
            }
            _responseCustom.message = _result != 0 ? $"Se generó el codigo {_inv} correctamente" : "No se registro el código";
            _responseCustom.status = true;

            return _responseCustom;
        }
    }
}
