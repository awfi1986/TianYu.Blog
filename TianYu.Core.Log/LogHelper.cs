using TianYu.Core.Common;
using System.Threading.Tasks; 
using System;

namespace TianYu.Core.Log
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        private static ILog _ilog = null;
        private static string BudlierTempSource(string source)
        {
            return $"请求IP:{0},Source:{source}，服务器Ip:{2}";
        }
        static LogHelper()
        {
            var logType = "TianYu.Core.Log."+ AppsettingsHelper.app("LogType");

            Type t = Type.GetType(logType);

            _ilog = (BaseLog)t.Assembly.CreateInstance(logType);
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogError(string source, string message, params string[] args)
        { 
            Task.Run(() => { _ilog.LogError(BudlierTempSource(source), message, args); });
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDebug(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogDebug(BudlierTempSource(source), message, args); });
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogInfo(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogInfo(BudlierTempSource(source), message, args); });
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogWarn(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogWarn(BudlierTempSource(source), message, args); });
        }
    }
}
