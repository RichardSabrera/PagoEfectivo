using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WsApiEfectivo.BusinessLogic
{
    public interface ISearchListCode
    {
        public Task<ResponseCustom> SearchListAll();
    }
}
