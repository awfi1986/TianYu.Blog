using System;
using System.DrawingCore;
using System.DrawingCore.Imaging; 
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public sealed class Utils
    {
        static Random random = new Random(DateTime.Now.Second);

        /// <summary>
        /// 获取一个带时序的GUID
        /// </summary>
        /// <returns></returns>
        public static string NewStrGuid()
        {
            return NewGuid().ToString();
        }
        /// <summary>
        /// 获取一个带时序的GUID
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            DateTime dt = DateTime.Now;
            Guid g = Guid.NewGuid();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}-{1}", dt.ToString("yyyyMMdd"), dt.ToString("HHmm"));
            var guids = g.ToString().Split('-');
            for (int i = 2; i < guids.Length; i++)
            {
                if (i == 2)
                {
                    sb.AppendFormat("-{0}{1}", dt.ToString("ss"), guids[i].Substring(2));
                }
                else { sb.AppendFormat("-{0}", guids[i]); }
            }

            return Guid.Parse(sb.ToString());
        }
        /// <summary>
        /// 获取两个经纬度之间的距离（单位：米）
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>两经纬度之间距离（单位：米）</returns>
        public static double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            double dd = HaversineFormula.Distance(longitude1, latitude1, longitude2, latitude2);
            dd = Math.Round(dd, 2);
            return dd;
        }
        /// <summary>
        /// 获取两个经纬度之间的距离（单位：米）
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>两经纬度之间距离（单位：米）</returns>
        public static double GetDistance(string longitude1, string latitude1, string longitude2, string latitude2)
        {
            return GetDistance(longitude1.ToDouble(), latitude1.ToDouble(), longitude2.ToDouble(), latitude2.ToDouble());

        }

        ///// <summary>
        ///// URL字符编码
        ///// </summary>
        //public static string UrlEncode(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return "";
        //    }
        //    str = str.Replace("'", "");
        //    return HttpContext.Current.Server.UrlEncode(str);
        //}

        ///// <summary>
        ///// URL字符解码
        ///// </summary>
        //public static string UrlDecode(string str)
        //{
        //    if (string.IsNullOrEmpty(str))
        //    {
        //        return "";
        //    }
        //    return HttpContext.Current.Server.UrlDecode(str);
        //}
          
        /// <summary>
        /// 生成随机单据号
        /// </summary>
        /// <returns></returns>
        public static string GetFlowOrderCode(string prefix = "", string timeFormat = "yyyyMMddHHmmss", int randomLength = 6)
        {
            return string.Format("{0}{1}{2}", prefix, DateTime.Now.ToString(timeFormat), GetRamdonCode(randomLength));
        }
        /// <summary>
        /// 生成短信验证码(随机生成6位数字)
        /// </summary>
        /// <returns></returns>
        public static string GetRamdonCode(int length = 6)
        {
            if (length > 21)
            {
                length = 21;
            }
            StringBuilder maxNumber = new StringBuilder();
            StringBuilder minNumber = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                maxNumber.Append("9");
                if (i == 0)
                {
                    minNumber.Append("1");
                }
                else
                {
                    minNumber.Append("0");
                }
            }
            return random.Next(minNumber.ToString().ToInt(), maxNumber.ToString().ToInt()).ToString();
        }
        /// <summary>
        /// 日期字符串转换格式
        /// </summary>
        /// <param name="date">日期字符串</param>
        /// <param name="format">当前格式</param>
        /// <param name="newformat">待转格式</param>
        /// <returns></returns>
        public static string DateStringToFormat(string date, string format, string newformat)
        {
            if (string.IsNullOrWhiteSpace(date))
                return string.Empty;
            if (string.IsNullOrWhiteSpace(format) || string.IsNullOrWhiteSpace(newformat))
                return date;
            return DateTime.ParseExact(date, format, System.Globalization.CultureInfo.CurrentCulture).ToString(newformat);
        }

        /// <summary>
        /// 小程序数据解密
        /// </summary>
        /// <param name="encryptedData">加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移值</param>
        /// <returns></returns>
        public static string MiniAppsAESDecrypt(string encryptedData, string key, string iv)
        {
            if (string.IsNullOrEmpty(encryptedData)) return "";
            byte[] encryptedData2 = Convert.FromBase64String(encryptedData);
            var rm = new RijndaelManaged
            {
                Key = Convert.FromBase64String(key),
                IV = Convert.FromBase64String(iv),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            var ctf = rm.CreateDecryptor();
            Byte[] resultArray = ctf.TransformFinalBlock(encryptedData2, 0, encryptedData2.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="CharArray">字符串集</param>
        /// <param name="n">位数</param>
        /// <param name="IsNumber">是否纯数字</param>
        /// <returns>验证码字符串</returns>
        private static string CreateRandomCode(string[] CharArray, int n, bool IsNumber)
        {
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(CharArray.Length - 1);
                //if (temp == t)
                //{
                //    return CreateRandomCode(n, IsNumber);
                //} 
                temp = t;
                randomCode += CharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="n">位数</param>
        /// <param name="IsNumber">是否仅数字</param>
        /// <returns>验证码字符串</returns>
        public static string CreateRandomCode(int n, bool IsNumber)
        {
            string charSet = "";
            if (IsNumber)
            {
                charSet = "0,1,2,3,4,5,6,8,9";
            }
            else
            {
                charSet = "2,3,4,5,6,8,9,A,B,C,D,E,F,G,H,J,K,M,N,P,R,S,U,W,X,Y,a,b,c,d,e,f,g,h,j,k,m,n,p,r,s,u,w,x,y";
            }
            string[] CharArray = charSet.Split(',');
            return CreateRandomCode(CharArray, n, IsNumber);
        }

        /// <summary>
        /// 绘画验证码图片
        /// </summary>
        /// <param name="chkCode">验证码</param>
        /// <returns></returns>
        public static MemoryStream CreateVerifyImage(out string code, int numbers = 4)
        {
            code = CreateRandomCode(numbers, false);
            MemoryStream ms = null;
            Random random = new Random();
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };


            using (var img = new Bitmap((int)code.Length * 18, 32))
            {
                using (var g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);//背景设为白色
                    Random rnd = new Random();
                    //画图片的干扰线
                    for (int i = 0; i < 25; i++)
                    {
                        int x1 = rnd.Next(img.Width);
                        int x2 = rnd.Next(img.Width);
                        int y1 = rnd.Next(img.Height);
                        int y2 = rnd.Next(img.Height);
                        Color clr = color[rnd.Next(color.Length)];
                        g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
                    }

                    StringFormat objStringFormat = new StringFormat(StringFormatFlags.NoClip);
                    objStringFormat.Alignment = StringAlignment.Center;
                    objStringFormat.LineAlignment = StringAlignment.Center;
                    //画验证码字符串 
                    for (int i = 0; i < code.Length; i++)
                    {
                        string fnt = font[rnd.Next(font.Length)];
                        Font ft = new Font(fnt, 18);
                        Color clr = color[rnd.Next(color.Length)];

                        //验证码旋转，防止机器识别 
                        float angle = rnd.Next(-45, 45);
                        g.TranslateTransform(12, 12);
                        g.RotateTransform(angle);
                        g.DrawString(code[i].ToString(), ft, new SolidBrush(clr), -2, 2, objStringFormat);
                        g.RotateTransform(-angle);
                        g.TranslateTransform(2, -12);
                    }
                    //画图片的前景干扰线
                    for (int i = 0; i < 100; i++)
                    {
                        int x = rnd.Next(img.Width);
                        int y = rnd.Next(img.Height);
                        Color clr = color[rnd.Next(color.Length)];
                        img.SetPixel(x, y, clr);
                    }
                    ms = new MemoryStream();//生成内存流对象  
                    img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中  
                }
            }

            return ms;
        }

        /// <summary>
        /// 图片转base64字符串
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static string ToBase64(Bitmap bmp)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.DrawingCore.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                return strbaser64;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// base64字符串转图片
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static Bitmap Base64StringToImage(string inputStr)
        {
            try
            {
                byte[] arr = Convert.FromBase64String(inputStr);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        ///// <summary>
        ///// 获取客户端IP地址
        ///// </summary>
        ///// <returns></returns>
        //public static string GetClientIp()
        //{
        //    string ip = "";
        //    if (HttpContext.Current != null && HttpContext.Current.Request != null)
        //    {
        //        ip = HttpContext.Current.Request.Headers["X-Forwarded-For"];
        //        if (ip.IsNullOrWhiteSpace())
        //        {
        //            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // using proxy 
        //            {
        //                ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 
        //            }
        //            else// not using proxy or can't get the Client IP 
        //            {
        //                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 
        //            }

        //            if (ip.IsNullOrWhiteSpace())
        //            {
        //                ip = HttpContext.Current.Request.UserHostAddress;
        //            }
        //        }
        //    }
        //    return ip;
        //}
        ///// <summary>
        ///// 获取app请求接口版本号
        ///// </summary>
        ///// <returns></returns>
        //public static string GetRequestAppVersion()
        //{
        //    string version = string.Empty;
        //    if (HttpContext.Current != null && HttpContext.Current.Request != null)
        //    {
        //        version = HttpContext.Current.Request.Headers[TianYuConsts.ApiVersionKey];
        //    }
        //    return version;
        //}
        ///// <summary>
        ///// 获取服务器IP地址
        ///// </summary>
        ///// <returns></returns>
        //public static string GetServerIp()
        //{
        //    string ip = "";
        //    if (HttpContext.Current != null && HttpContext.Current.Request != null)
        //    {
        //        ip = HttpContext.Current.Request.ServerVariables.Get("Local_Addr").ToString();
        //    }
        //    return ip;
        //}

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string Sha1Encrypt(string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            byte[] byteArray = encoding.GetBytes(source);
            using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            {
                byteArray = hashAlgorithm.ComputeHash(byteArray);
                StringBuilder stringBuilder = new StringBuilder(256);
                foreach (byte item in byteArray)
                {
                    stringBuilder.AppendFormat("{0:x2}", item);
                }
                hashAlgorithm.Clear();
                return stringBuilder.ToString();
            }
        }
    }
}

