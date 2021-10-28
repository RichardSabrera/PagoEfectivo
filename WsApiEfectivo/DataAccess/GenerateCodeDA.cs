using Domain.Constants;
using Domain.Entities;
using Infrastructure.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WsApiEfectivo.DataAccess
{
    public class GenerateCodeDA
    {
        private static BaseDataAccess conn = new BaseDataAccess(ConstVariable.connStr);
        private int _value = 0;
        public async Task<int> ValidateEmail(string email)
        {
            var result = await Task.Run(() => conn.ConnectionWithParameters("SP_EXIST_EMAIL", new List<Parameters>() {
                                    new Parameters() { parameter = "@email", type = SqlDbType.VarChar, value = email} }));

            foreach (DataRow dr in result.Rows)
            {
                _value = Convert.ToInt32(dr[0]);
            }
            return _value;
        }


        public async Task<int> InsDataUser(string name, string email, string code, DateTime time)
        {
            var result = await Task.Run(() => conn.ConnectionExecuteWithParameters("SP_SAVE_DATA", new List<Parameters>() {
                                    new Parameters() { parameter = "@email", type = SqlDbType.VarChar, value = email},
                                    new Parameters() { parameter = "@name", type = SqlDbType.VarChar, value = name},
                                    new Parameters() { parameter = "@code", type = SqlDbType.VarChar, value = code},
                                    new Parameters() { parameter = "@time", type = SqlDbType.DateTime, value = time }}));
            return result;
        }
    }
}
