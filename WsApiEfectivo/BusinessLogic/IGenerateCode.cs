using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApiEfectivo.BusinessLogic
{
    public interface IGenerateCode
    {
        public Task<ResponseCustom> CreateCodeForNameEmail(string name, string email);
    }
}
