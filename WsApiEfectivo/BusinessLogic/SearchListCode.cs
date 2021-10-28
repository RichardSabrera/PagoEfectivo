using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsApiEfectivo.DataAccess;

namespace WsApiEfectivo.BusinessLogic
{
    public class SearchListCode : ISearchListCode
    {
        private static ResponseCustom _responseCustom = new ResponseCustom();
        private static SearchListDA _searchListDA = new SearchListDA();

        public async Task<ResponseCustom> SearchListAll()
        {
            _responseCustom.result = await _searchListDA.SearchListAll();
            _responseCustom.status = true;
            return _responseCustom;
        }
    }
}
