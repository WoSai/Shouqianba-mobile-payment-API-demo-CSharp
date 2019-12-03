using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace demo
{
    class HttpUtil
    {
        public static string httpPost(string url,string body,string sign,string vendor_sn)
        {
            //发送数据
            HttpWebRequest request = null;
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(body);
            
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                          
             }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add(HttpRequestHeader.Authorization, vendor_sn+" "+sign);
            request.ContentLength = byte1.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(byte1, 0, byte1.Length);
            stream.Close();


            //发送成功后接收返回的json信息
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream newStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(newStream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }


        
    }
}
