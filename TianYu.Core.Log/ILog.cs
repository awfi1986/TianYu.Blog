using System;

namespace TianYu.Core.Log
{
    public interface ILog
    {
        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogError(string source, string message, params string[] args);
        /// <summary>
        /// 调试
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogDebug(string source, string message, params string[] args);
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogInfo(string source, string message, params string[] args);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogWarn(string source, string message, params string[] args);
        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogTrace(string source, string message, params string[] args);
    }
}
