
using SqlSugar;  

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [SugarTable("sys_dictionary")]
    public class SysDictionary : AggregateRoot
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        [SugarColumn(ColumnName = "dictionary_name")]
        public string DictionaryName { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        [SugarColumn(ColumnName = "dictionary_code")]
        public string DictionaryCode { get; set; }
        /// <summary>
        /// 父级Id（-1=顶级）
        /// </summary>
        [SugarColumn(ColumnName = "parent_id")]
        public int ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort")]
        public int Sort { get; set; } 
    }
}
