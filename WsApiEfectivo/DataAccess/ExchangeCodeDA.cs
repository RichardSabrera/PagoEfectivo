using Domain.Constants;
using Domain.Entities;
using Infrastructure.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WsApiEfectivo.DataAccess
{
    public class ExchangeCodeDA
    {
        private static BaseDataAccess conn = new BaseDataAccess(ConstVariable.connStr);
        private string _response = string.Empty;

        public async Task<string> ExchangeCodeValidate(string code)
        {
            try
            {
                var result = await Task.Run(() => conn.ConnectionWithParameters("SP_EXCHANGE", new List<Parameters>() {
                                    new Parameters() { parameter = "@code", type = SqlDbType.VarChar, value = code} }));

                foreach (DataRow dr in result.Rows)
                {
                    _response = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
            return _response;
        }
    }
}
