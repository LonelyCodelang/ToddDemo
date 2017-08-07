using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Log4netDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.Info("隐隐约约隐隐约约隐隐约约隐隐约约隐隐约约隐隐约约亿元");
            LogHelper.Error("dsd", new Exception("asdfasdfasdf"));

            LogHelper.Info("阿萨德发送方");
            LogHelper.Error("撒旦防撒旦法", new Exception("11111111111"));

            try
            {
                throw new Exception("哈哈哈哈哈哈");
            }
            catch (Exception ex)
            {
                LogHelper.Error("test", ex);
            }

            Console.ReadLine();
        }
    }


    /// <summary>
    /// Log4Net日志封装类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 信息标志
        /// </summary>
        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        /// <summary>
        /// 错误标志
        /// </summary>
        private static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        /// <summary>
        /// 调试标志
        /// </summary>
        private static readonly log4net.ILog logdebug = log4net.LogManager.GetLogger("logdebug");


        /// <summary>
        /// Log4Net信息记录封装
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void Info(string message)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(message);
            }
        }

        /// <summary>
        /// Log4Net错误记录封装
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void Error(string message)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(message);
            }
        }

        /// <summary>
        /// Log4Net错误记录封装
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static void Error(string message, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(message, ex);
            }
        }


        /// <summary>
        /// Log4Net调试记录封装
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void Debug(string message)
        {
            if (logdebug.IsErrorEnabled)
            {
                logdebug.Debug(message);
            }
        }
    }
}
