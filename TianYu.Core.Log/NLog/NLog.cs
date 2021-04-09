using NLog; 

namespace TianYu.Core.Log
{
    public class NLog : BaseLog
    {
        private static readonly Logger log = LogManager.GetLogger("");

        public override void LogDebug(string source, string message, params string[] args)
        {
            log.Debug($"{source},异常信息：{message}", args);
        }

        public override void LogError(string source, string message, params string[] args)
        {
            log.Error($"{source},异常信息：{message}", args);
        }

        public override void LogInfo(string source, string message, params string[] args)
        {
            log.Info($"{source},异常信息：{message}", args);
        }

        public override void LogWarn(string source, string message, params string[] args)
        {
            log.Warn($"{source},异常信息：{message}", args);
        }
    }
}
