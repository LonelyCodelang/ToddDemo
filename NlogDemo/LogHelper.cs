using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NlogDemo
{
    public class LogHelper
    {
        // Fields
        private static readonly bool Isinit = false;
        private static bool _logComplementEnable = false;
        private static bool _logDubugEnable = false;
        private static bool _logErrorEnable = false;
        private static bool _logExceptionEnable = false;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static bool _logInfoEnable = false;

        // Methods
        static LogHelper()
        {
            if (!Isinit)
            {
                Isinit = true;
                SetConfig();
            }
        }

        private static string BuildMessage(string info)
        {
            return BuildMessage(info, null);
        }

        private static string BuildMessage(string info, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            HttpRequest request = null;
            if ((HttpContext.Current != null) && (HttpContext.Current.Request != null))
            {
                request = HttpContext.Current.Request;
            }
            sb.AppendFormat("Time:{0}-{1}\r\n", DateTime.Now, info);
            if (request != null)
            {
                sb.AppendFormat("Url:{0}\r\n", request.Url);
                if (null != request.UrlReferrer)
                {
                    sb.AppendFormat("UrlReferrer:{0}\r\n", request.UrlReferrer);
                }
                string realip = (request.ServerVariables == null) ? string.Empty : request.ServerVariables["HTTP_X_REAL_IP"];
                string proxy = (request.Headers == null) ? string.Empty : request.Headers.Get("HTTP_NDUSER_FORWARDED_FOR_HAPROXY");
                sb.AppendFormat("UserHostAddress:{0};{1};{2}\r\n", request.UserHostAddress, realip, proxy);
                sb.AppendFormat("WebServer:{0}\r\n", request.ServerVariables["LOCAL_ADDR"]);
            }
            if (ex != null)
            {
                sb.AppendFormat("Exception:{0}\r\n", ex);
            }
            sb.AppendLine();
            return sb.ToString();
        }

        public static void SetConfig()
        {
            Logger enable = LogManager.GetLogger("con");//获取配置，设置是否允许输出
            _logInfoEnable = enable.IsInfoEnabled;
            _logErrorEnable = enable.IsErrorEnabled;
            _logExceptionEnable = enable.IsErrorEnabled;
            _logComplementEnable = enable.IsTraceEnabled;
            _logDubugEnable = enable.IsDebugEnabled;
        }

        public static void WriteComplement(string info)
        {
            if (_logComplementEnable)
            {
                Logger.Trace(BuildMessage(info));
            }
        }

        public static void WriteComplement(string info, Exception ex)
        {
            if (_logComplementEnable)
            {
                Logger.Trace(BuildMessage(info, ex));
            }
        }

        public static void WriteDebug(string info)
        {
            if (_logDubugEnable)
            {
                Logger.Debug(BuildMessage(info));
            }
        }

        public static void WriteError(string info)
        {
            if (_logErrorEnable)
            {
                Logger.Error(BuildMessage(info));
            }
        }

        public static void WriteException(string info, Exception ex)
        {
            if (_logExceptionEnable)
            {
                Logger.Error(BuildMessage(info, ex));
            }
        }

        public static void WriteFatal(string info)
        {
            if (_logErrorEnable)
            {
                Logger.Fatal(BuildMessage(info));
            }
        }

        public static void WriteInfo(string info)
        {
            if (_logInfoEnable)
            {
                Logger.Info(BuildMessage(info));
            }
        }
    }
}
