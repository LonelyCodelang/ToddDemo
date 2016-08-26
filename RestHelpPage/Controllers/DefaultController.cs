using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestHelpPage.Controllers
{
    /// <summary>
    /// 测试api
    /// </summary>
    public class DefaultController : ApiController
    {
        /// <summary>
        /// 这是一个测试的方法
        /// </summary>
        /// <param name="id">id值</param>
        /// <returns></returns>
        public string GetTest(string id)
        {
            return "aaa";
        }
    }
}
