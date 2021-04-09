using System;
using System.Reflection;

namespace TianYu.Core.Common
{
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举备注
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public static string GetEnumRemark(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }
    }
    /// <summary>
    /// 备注特性
    /// </summary>
    public class RemarkAttribute : Attribute
    {
        private string m_remark;
        public RemarkAttribute(string remark)
        {
            this.m_remark = remark;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return m_remark; }
            set { m_remark = value; }
        }
        /// <summary>
        /// 获取枚举的备注信息
        /// </summary>
        /// <param name="val">枚举值</param>
        /// <returns></returns>
        public static string GetEnumRemark(Enum val)
        {
            Type type = val.GetType();
            FieldInfo fd = type.GetField(val.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(RemarkAttribute), false);
            string name = string.Empty;
            foreach (RemarkAttribute attr in attrs)
            {
                name = attr.Remark;
            }
            return name;
        }
    }
}
