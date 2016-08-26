using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestSwaggerUI.Controllers
{
    /// <summary>
    /// 测试类
    /// </summary>
    public class DefaultController : ApiController
    {
        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="id">id值</param>
        /// <returns>返回指定的值</returns>
        public string Test1(string id)
        {
            return "这是一个测试";
        }
    }
}
