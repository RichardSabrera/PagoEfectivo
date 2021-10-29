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
    public class GenerateCodeTests
    {
        public string sqlConn = "Data Source=.;Initial Catalog=PagoEfectivo;Integrated Security=True";
        public static GenerateCode _generateCode = new GenerateCode();

        [TestMethod()]
        public void CreateCodeForNameEmailTestTrue()
        {
            ConstVariable.connStr = sqlConn;
            var _result = _generateCode.CreateCodeForNameEmail("Richard", "pruebatest@hotmail.com").Result;
            Assert.AreEqual(true, _result.status);
        }

    }
}