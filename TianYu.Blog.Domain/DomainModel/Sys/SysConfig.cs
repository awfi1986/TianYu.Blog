using SqlSugar; 

namespace TianYu.Blog.Domain.DomainModel
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [SugarTable("sys_config")]
    public class SysConfig
    {
        /// <summary>
        /// 配置项编码
        /// </summary>
        [SugarColumn(ColumnName = "config_code")]
        public string ConfigCode { get; set; }
        /// <summary>
        /// 配置项内容
        /// </summary>
        [SugarColumn(ColumnName = "config_content")]
        public string ConfigContent { get; set; } 
    }
}
