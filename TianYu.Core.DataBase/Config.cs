using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TianYu.Core.Common;

namespace TianYu.Core.DataBase
{
    public class Config
    {   /*  
         * 多库操作，默认加载的是appsettings.json设置为true的第一个db连接。
         */
        public static List<MutiDBOperate> MutiConnectionString => MutiInitConn();
        private static string DifDBConnOfSecurity(params string[] conn)
        {
            foreach (var item in conn)
            {
                try
                {
                    if (File.Exists(item))
                    {
                        return File.ReadAllText(item).Trim();
                    }
                }
                catch (System.Exception) { }
            }

            return conn[conn.Length - 1];
        }


        public static List<MutiDBOperate> MutiInitConn()
        {
            List<MutiDBOperate> listdatabase = new List<MutiDBOperate>();
            List<MutiDBOperate> listdatabaseSimpleDB = new List<MutiDBOperate>();
            string Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";
            using (var file = new StreamReader(Path))
            using (var reader = new JsonTextReader(file))
            {
                var jObj = (JObject)JToken.ReadFrom(reader);
                if (jObj != null)
                {
                    var secJt = jObj["DBS"];
                    if (secJt != null)
                    {
                        for (int i = 0; i < secJt.Count(); i++)
                        {
                            if (bool.Parse(secJt[i]["Enabled"] + ""))
                            {
                                listdatabase.Add(SpecialDbString(new MutiDBOperate()
                                {
                                    ConnId = secJt[i]["ConnId"] + "",
                                    Conn = secJt[i]["Connection"] + "",
                                    DbType = (DataBaseType)(Convert.ToInt32(secJt[i]["DBType"])),
                                }));
                            }
                        }
                    }
                }


                // 单库，只保留一个
                if (!Convert.ToBoolean(AppsettingsHelper.app(new string[] { "MutiDBEnabled" })))
                {
                    if (listdatabase.Count == 1)
                    {
                        return listdatabase;
                    }
                    else
                    {
                        var dbFirst = listdatabase.FirstOrDefault(d => d.ConnId == AppsettingsHelper.app(new string[] { "MainDB" }) + "");
                        if (dbFirst == null)
                        {
                            dbFirst = listdatabase.FirstOrDefault();
                        }
                        listdatabaseSimpleDB.Add(dbFirst);
                        return listdatabaseSimpleDB;
                    }
                }

                return listdatabase;
            }
        }

        private static MutiDBOperate SpecialDbString(MutiDBOperate mutiDBOperate)
        {
            if (mutiDBOperate.DbType == DataBaseType.Sqlite)
            {
                mutiDBOperate.Conn = $"DataSource=" + Path.Combine(Environment.CurrentDirectory, mutiDBOperate.Conn);
            }
            else if (mutiDBOperate.DbType == DataBaseType.SqlServer)
            {
                mutiDBOperate.Conn = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1.txt", @"c:\my-file\dbCountPsw1.txt", mutiDBOperate.Conn);
            }
            else if (mutiDBOperate.DbType == DataBaseType.MySql)
            {
                mutiDBOperate.Conn = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1_MySqlConn.txt", @"c:\my-file\dbCountPsw1_MySqlConn.txt", mutiDBOperate.Conn);
            }
            else if (mutiDBOperate.DbType == DataBaseType.Oracle)
            {
                mutiDBOperate.Conn = DifDBConnOfSecurity(@"D:\my-file\dbCountPsw1_OracleConn.txt", @"c:\my-file\dbCountPsw1_OracleConn.txt", mutiDBOperate.Conn);
            }

            return mutiDBOperate;
        }
    }


    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }
    public class MutiDBOperate
    {
        public string ConnId { get; set; }
        public string Conn { get; set; }
        public DataBaseType DbType { get; set; }
    }
}