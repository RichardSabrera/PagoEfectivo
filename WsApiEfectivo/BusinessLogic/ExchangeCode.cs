using Domain.Entities;
using System.Threading.Tasks;
using WsApiEfectivo.DataAccess;

namespace WsApiEfectivo.BusinessLogic
{
    public class ExchangeCode: IExchangeCode
    {
        private static ResponseCustom _responseCustom = new ResponseCustom();
        private static ExchangeCodeDA _exchangeCodeDA = new ExchangeCodeDA();
        public async Task<ResponseCustom> ExchangeCodeValidate(string code)
        {
            _responseCustom.message = await _exchangeCodeDA.ExchangeCodeValidate(code);
            _responseCustom.status = true;

            return _responseCustom;
        }
    }
}
