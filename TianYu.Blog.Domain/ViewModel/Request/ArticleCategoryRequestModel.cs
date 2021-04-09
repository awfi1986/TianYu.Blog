using System;
using System.Collections.Generic;
using System.Text;
using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Blog.Domain.ViewModel.Request
{
    public class ArticleCategoryListRequestModel : BaseRequest
    {
    }

    public class SaveArticleCategoryRequestModel
    {
        /// <summary>
        /// id
        /// </summary> 
        public int Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary> 
        public string Name { get; set; }
        /// <summary>
        /// 父类Id
        /// </summary> 
        public int ParentId { get; set; }
        /// <summary>
        /// 层级
        /// </summary> 
        public int Level { get; set; } = 1;
        /// <summary>
        /// 排序
        /// </summary> 
        public int Sort { get; set; }
    }
}
