using SqlSugar;
using System;
using TianYu.Blog.Infrastructure.Enums;

namespace TianYu.Blog.Domain
{
    /// <summary>
    /// 聚合根的抽象实现类，定义聚合根的公共属性和行为
    /// </summary>
    public abstract class AggregateRoot : IAggregateRoot 
    {
        /// <summary>
        /// 状态（0＝有效;1＝无效）
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public StatusEnum Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "creator")]
        public string Creator { get; set; }
        /// <summary>
        /// 创建人Guid
        /// </summary>
        [SugarColumn(ColumnName = "create_guid")]
        public string CreateGuid { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName = "modify_time")]
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [SugarColumn(ColumnName = "modifier")]
        public string Modifier { get; set; }
        /// <summary>
        /// 修改人Guid
        /// </summary>
        [SugarColumn(ColumnName = "modify_guid")]
        public string ModifyGuid { get; set; }
    }
}
