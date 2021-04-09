
using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Blog.Domain.ViewModel.Request
{
    public class QuerySysDictionaryRequestModel: BaseRequest
    {
        public int ParentId { get; set; }
    }
    public class SaveSysDictionaryRequestModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary> 
        public string DictionaryName { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary> 
        public string DictionaryCode { get; set; }
        /// <summary>
        /// 父级Id（-1=顶级）
        /// </summary> 
        public int ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }
    }
}
