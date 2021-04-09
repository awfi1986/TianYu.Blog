using SqlSugar;

namespace TianYu.Core.Common.BaseViewModel
{
    public class BaseRequest
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWords { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; } = 1;
        /// <summary>
        /// 页码大小
        /// </summary>
        public int Limit { get; set; } = 10;
        /// <summary>
        /// 返回总记录数
        /// </summary>
        public RefAsync<int> Total { get; set; } = 0;
    }
}
