
//--------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2021-01-18 15:24:15 
//     对此文件的更改可能会导致不正确的行为，并且如果重新生成代码，这些更改将会丢失。
//--------------------------------------------------------------------
using SqlSugar;
using System;

namespace TianYu.Blog.Domain.DomainModel
{
	 ///<summary>
	 ///
	 ///</summary>
	 [SugarTable("sys_login_log")]	
	 public class SysLoginLog  
	 {
	  
		/// <summary>
        /// Id
        /// </summary>
		[SugarColumn(ColumnName = "Id"  , IsPrimaryKey = true)]
		public int Id { get; set; }
	  
		/// <summary>
        /// 执行类型（1＝登录；2＝退出）
        /// </summary>
		[SugarColumn(ColumnName = "action_type"  )]
		public int ActionType { get; set; }
	  
		/// <summary>
        /// 执行人
        /// </summary>
		[SugarColumn(ColumnName = "operator"  )]
		public string Operator { get; set; }
	  
		/// <summary>
        /// 执行IP
        /// </summary>
		[SugarColumn(ColumnName = "exec_ip" , IsNullable = false )]
		public string ExecIp { get; set; }
	  
		/// <summary>
        /// 执行时间
        /// </summary>
		[SugarColumn(ColumnName = "exec_time"  )]
		public DateTime ExecTime { get; set; }
	  
		/// <summary>
        /// 执行内容
        /// </summary>
		[SugarColumn(ColumnName = "exec_content"  )]
		public string ExecContent { get; set; }
	  
		/// <summary>
        /// 执行结果
        /// </summary>
		[SugarColumn(ColumnName = "exec_result"  )]
		public string ExecResult { get; set; }
	  
		/// <summary>
        /// 备注
        /// </summary>
		[SugarColumn(ColumnName = "remark" , IsNullable = false )]
		public string Remark { get; set; }
	  
	 }
}	 
