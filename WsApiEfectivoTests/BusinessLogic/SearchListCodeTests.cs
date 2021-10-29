using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsApiEfectivo.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;

namespace WsApiEfectivo.BusinessLogic.Tests
{
    [TestClass()]
    public class SearchListCodeTests
    {
        public string sqlConn = "Data Source=.;Initial Catalog=PagoEfectivo;Integrated Security=True";
        private static SearchListCode _searchListCode = new SearchListCode();
        [TestMethod()]
        public void SearchListAllTest()
        {
            ConstVariable.connStr = sqlConn;

            var _result = _searchListCode.SearchListAll().Result;

            Assert.AreEqual(true, _result.status);
        }
    }
}