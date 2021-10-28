using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApiEfectivo.BusinessLogic
{
    public interface IExchangeCode
    {
        public Task<ResponseCustom> ExchangeCodeValidate(string code);
    }
}
