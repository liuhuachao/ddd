using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DDD.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 以post方式进行Http提交
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="postData">发送的数据</param>
        /// <returns>字符串</returns>
        public static string HttpPost(string url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 10000;
            request.AllowAutoRedirect = false;

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(postData);
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (WebException webex)
            {
                HttpWebResponse res = (HttpWebResponse)webex.Response;
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string html = reader.ReadToEnd();
                throw new Exception(html, webex);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }

            return responseStr;
        }

        /// <summary>
        /// 以get方式进行提交Http请求
        /// </summary>
        /// <param name="url">发送链接地址</param>
        /// <returns>返回字符串</returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 10000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (WebException webex)
            {
                HttpWebResponse res = (HttpWebResponse)webex.Response;
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string html = reader.ReadToEnd();
                throw new Exception(html, webex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                request = null;
                response = null;
            }
            return responseStr;
        }
    }
}
