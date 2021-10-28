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
    public class SearchListDA
    {
        private static BaseDataAccess conn = new BaseDataAccess(ConstVariable.connStr);
        private int _value = 0;
        private static List<SearchAll> searchAlls = new List<SearchAll>();
        public async Task<List<SearchAll>> SearchListAll()
        {
            var result = await Task.Run(() => conn.ConnectionWithParameters("SP_SEARCH_ALL", new List<Parameters>() { }));

            foreach (DataRow dr in result.Rows)
            {
                SearchAll searchAll = new SearchAll();
                searchAll.Name = dr["Name"].ToString();
                searchAll.Email = dr["Email"].ToString();
                searchAll.Code = dr["Code"].ToString();
                searchAll.Description = dr["Description"].ToString();
                searchAll.TimeExpire = Convert.ToDateTime(dr["TimeExpire"]);
                searchAlls.Add(searchAll);
            }
            return searchAlls;
        }
    }
}
