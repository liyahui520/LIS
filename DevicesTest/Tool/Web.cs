using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace DevicesTest
{

    public class WebLogic
    {
        public static HttpResult<R> GetHttpResult<R>(HttpItem item)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            // 准备请求...
            //try
            //{
            // 获取Request
            request = CreateRequest(item);

            using (response = request.GetResponse() as HttpWebResponse)
            {
                return new HttpResult<R>(response);
            }
        }

        private static HttpWebRequest CreateRequest(HttpItem item)
        {
            HttpWebRequest request = WebRequest.Create(item.Url) as HttpWebRequest;
            request.Method = item.Method.ToString();
            request.ContentType = item.ContentType;
            request.Date = item.Date;
            //request.ra
            if (item.Headers != null)
            {
                foreach (var head in item.Headers)
                {
                    if (head.StartsWith("DATE:"))
                        request.Date = DateTime.Parse(head.Substring(head.IndexOf(":") + 2));
                    else if (head.StartsWith("Content-Type:"))
                        request.ContentType = head.Substring(head.IndexOf(":") + 2);
                    else if (head.StartsWith("Range:"))
                    {
                        string[] spli = head.Split('-');
                        request.AddRange(Convert.ToInt32(spli[0].Remove(0, 13)), Convert.ToInt32(spli[1]));
                    }
                    else
                        request.Headers.Add(head);
                }
            }

            if (item.Content != null && item.Content.Length > 0)
            {
                request.ContentLength = item.Content.Length;
                Stream outstream = request.GetRequestStream();
                outstream.Write(item.Content, 0, item.Content.Length);
                outstream.Close();
            }
            return request;
        }

        static WebLogic()
        {
            ServicePointManager.SecurityProtocol |= (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        }


    }

    /// <summary>
    /// Http请求参数
    /// </summary>
    public class HttpItem
    {
        public HttpItem()
        {
            Headers = new List<string>();
            Headers.Add(string.Format("DATE: {0}", DateTime.UtcNow.ToString("R")));
        }


        /// <summary>
        /// 是否取消当前请求
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                //foreach (var item in Headers)
                //    if (item.StartsWith("DATE:"))
                //        return date;
                //date = DateTime.UtcNow;
                //Headers.Add(string.Format("DATE: {0}", date.ToString("R")));
                return date;
            }
            set
            {
                date = value;
                //Headers.RemoveAll(o => o.StartsWith("DATE:"));
                //Headers.Add(string.Format("DATE: {0}", date.ToString("R")));
            }
        }
        public string Url { get; set; }
        /// <summary>
        /// 请求方法  post,get,put delete等
        /// </summary>
        public HttpMethod Method { get; set; }
        public List<string> Headers { get; set; }

        public Dictionary<string, string> HeadersToDictionary()
        {
            if (Headers.Count == 0)
                return null;
            Dictionary<string, string> dirc = new Dictionary<string, string>();
            foreach (var item in Headers)
            {
                string key = item.Substring(0, item.IndexOf(":"));
                string value = item.Substring(key.Length + 1);
                if (dirc.ContainsKey(key))
                    dirc.Remove(key);
                dirc.Add(key, value);
            }
            return dirc;
        }

        /// <summary>
        /// 如果ContentType赋值,它会覆盖Headers中的conentType
        /// </summary>
        public string ContentType
        {
            get
            {
                foreach (var item in Headers)
                    if (item.StartsWith("Content-Type:"))
                        return item.Substring(item.IndexOf(":") + 2);
                return null;
            }
            set
            {
                Headers.RemoveAll(o => o.StartsWith("DATE:"));
                Headers.Add(string.Format("Content-Type: {0}", value));
            }

        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public object ContentObject { get; set; }


        int _Timeout = 100000;
        /// <summary>
        /// 默认请求超时时间
        /// </summary>
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }
        int _ReadWriteTimeout = 30000;


        /// <summary>
        /// 默认写入Post数据超时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _ReadWriteTimeout; }
            set { _ReadWriteTimeout = value; }
        }


        string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        /// <summary>
        /// 客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }

        CookieCollection cookiecollection = null;
        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection
        {
            get { return cookiecollection; }
            set { cookiecollection = value; }
        }


        string _CerPath = string.Empty;
        /// <summary>
        /// 证书绝对路径
        /// </summary>
        public string CerPath
        {
            get { return _CerPath; }
            set { _CerPath = value; }
        }
        private Boolean isToLower = true;
        /// <summary>
        /// 是否设置为全文小写
        /// </summary>
        public Boolean IsToLower
        {
            get { return isToLower; }
            set { isToLower = value; }
        }
        private Boolean allowautoredirect = true;
        /// <summary>
        /// 支持跳转页面，查询结果将是跳转后的页面
        /// </summary>
        public Boolean Allowautoredirect
        {
            get { return allowautoredirect; }
            set { allowautoredirect = value; }
        }
        private int connectionlimit = 1024;
        /// <summary>
        /// 最大连接数
        /// </summary>
        public int Connectionlimit
        {
            get { return connectionlimit; }
            set { connectionlimit = value; }
        }
        private string proxyusername = string.Empty;
        /// <summary>
        /// 代理Proxy 服务器用户名
        /// </summary>
        public string ProxyUserName
        {
            get { return proxyusername; }
            set { proxyusername = value; }
        }
        private string proxypwd = string.Empty;
        /// <summary>
        /// 代理 服务器密码
        /// </summary>
        public string ProxyPwd
        {
            get { return proxypwd; }
            set { proxypwd = value; }
        }
        private string proxyip = string.Empty;
        /// <summary>
        /// 代理 服务IP
        /// </summary>
        public string ProxyIp
        {
            get { return proxyip; }
            set { proxyip = value; }
        }


        /// <summary>
        /// http body
        /// </summary>
        public byte[] Content { get; set; }

    }

    public enum HttpMethod
    {
        Get = 1,
        Post = 2,
        Put = 3,
        Head = 4,
        Delete = 5
    }

    public class HttpResult<R>
    {
        public int StatusCode { get; set; }
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success
        {
            get;
            private set;
        }

        /// <summary>
        /// 返回值
        /// </summary>
        public R Result
        {
            get;
            private set;
        }

        /// <summary>
        /// 响应返回的原始值 
        /// </summary>
        public object Content
        {
            get;
            private set;
        }

        /// <summary>
        /// 服务器返回消息
        /// </summary>
        public string Message
        {
            get;
            private set;
        }

        public Dictionary<string, string> Headers
        {
            get;
            private set;
        }


        public HttpResult(bool success, string result, string msg)
        {
            Success = success;
            Message = msg;
            Content = result;
        }

        public HttpResult(HttpWebResponse response)
        {
            if (response == null)
                return;
            Message = response.StatusDescription;
            StatusCode = (int)response.StatusCode;

            if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300)
                Success = true;

            Headers = new Dictionary<string, string>();
            foreach (var item in response.Headers.AllKeys)
                Headers.Add(item, response.Headers[item]);
            string content = null;
            Stream myResponseStream = response.GetResponseStream();
            byte[] buffer = null;
            if (response.ContentLength > 0)
            {
                buffer = new byte[response.ContentLength];
                int offset = 0;
                int count = 0;
                int length = buffer.Length;
                do
                {
                    count = myResponseStream.Read(buffer, offset, length);
                    offset += count;
                    length -= count;
                } while (count > 0);
                Content = buffer;
                content = System.Text.UTF8Encoding.UTF8.GetString(buffer);
            }
            else
            {
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                content = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                Content = content;
            }

            try
            {
                string typename = typeof(R).Name;
                if (typename == "String")
                {
                    Result = (R)(object)content;
                    return;
                }
                if (typename == "Byte[]" && buffer != null)
                {
                    Result = (R)(object)buffer;
                    return;
                }
                if (!string.IsNullOrEmpty(content))
                {
                    if (typeof(R).IsValueType)
                        Result = (R)Convert.ChangeType(content, typeof(R));
                    else if (response.ContentType.Contains("application/json"))
                        Result = Newtonsoft.Json.JsonConvert.DeserializeObject<R>(content); ;// (R)Json.Deserialize(content);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

