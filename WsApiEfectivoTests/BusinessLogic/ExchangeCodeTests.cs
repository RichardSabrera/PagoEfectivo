using Microsoft.VisualStudio.TestTools.UnitTesting;
using WsApiEfectivo.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Connection;
using Domain.Constants;

namespace WsApiEfectivo.BusinessLogic.Tests
{
    [TestClass()]
    public class ExchangeCodeTests
    {
        public string sqlConn="Data Source=.;Initial Catalog=PagoEfectivo;Integrated Security=True";
        public static ExchangeCode _exchangeCode = new ExchangeCode();
        private const string _response = "El código no existe o fue canjeado anteriormente.";
        [TestMethod()]
        public void ExchangeCodeValidateTestCodeNotExist()
        {
            ConstVariable.connStr = sqlConn;
            var _result = _exchangeCode.ExchangeCodeValidate("XmaJas123").Result;
            Assert.AreEqual(_response, _result.message);
        }


    }
}