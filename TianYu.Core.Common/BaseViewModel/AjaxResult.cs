 
namespace TianYu.Core.Common
{
    public enum ResultCode
    {

        /// <summary>
        /// 0
        /// </summary>
        Succeed = 0,
        /// <summary>
        /// 300
        /// </summary>
        Failure = 300,
        /// <summary>
        /// 400
        /// </summary>
        PasswordError = 400,
        /// <summary>
        /// 登陆超时
        /// </summary>
        NoLogin = 401, 
        /// <summary>
        /// 服务器错误
        /// </summary>
        ServerError = 500
    }
    public class AjaxResult
    {
        public AjaxResult()
        {
            Code = ResultCode.Succeed;
            Message = "操作成功";
        }
        public object Data { get; set; }
        public int Curr { get; set; }
        public int PageTotal { get; set; }
        public long Count { get; set; }
        public ResultCode Code { get; set; }
        public string Message { get; set; }
    } 
}

