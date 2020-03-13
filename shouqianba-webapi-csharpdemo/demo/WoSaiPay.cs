
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace demo
{
    class WoSaiPay
    {
        public static string api_domain= "https://vsi-api.shouqianba.com";
        public static JObject activate(string vendor_sn,string vendor_key,string appid,string code)
        {
            string url = api_domain + "/terminal/activate";
            JObject Jparams = new JObject();
            
            Jparams.Add(new JProperty("appid", appid));                 //appid，必填
            Jparams.Add(new JProperty("code", code));                   //激活码，必填
            Jparams.Add(new JProperty("device_id", ""));   //客户方收银终端序列号，需保证同一appid下唯一，必填。为方便识别，建议格式为“品牌名+门店编号+‘POS’+POS编号“
            Jparams.Add(new JProperty("client_sn", "POS01"));           //客户方终端编号，一般客户方或系统给收银终端的编号，必填
            Jparams.Add(new JProperty("name", "1号款台"));               //客户方终端名称，必填
            Jparams.Add(new JProperty("os_info",""));
            Jparams.Add(new JProperty("sdk_version",""));

            string sign = getSign(Jparams.ToString() + vendor_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, vendor_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码："+resCode);
            if (resCode.Equals("200"))
            {
                
                string responseStr = retObj["biz_response"].ToString();
                JObject terminal = JObject.Parse(responseStr);
                Console.WriteLine("返回信息:" + terminal);
                return terminal;
            }
            else
            {
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("返回信息："+errorMsg);
            }
            return null;
        }


        public static JObject checkin(string terminal_sn,string terminal_key)
        {
            string url = api_domain + "/terminal/checkin";
            JObject Jparams = new JObject();

            Jparams.Add(new JProperty("terminal_sn", terminal_sn));
            Jparams.Add(new JProperty("device_id", ""));
            Jparams.Add(new JProperty("os_info", ""));
            Jparams.Add(new JProperty("sdk_version", ""));

            string sign = getSign(Jparams.ToString() + terminal_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, terminal_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码：" + resCode);
            if (resCode.Equals("200"))
            {

                string responseStr = retObj["biz_response"].ToString();
                JObject newTerminal = JObject.Parse(responseStr);
                Console.WriteLine("返回信息:" + newTerminal);
                return newTerminal;
            }
            else
            {
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("返回信息：" + errorMsg);
            }
            return null;
        }


        public static JObject pay(string terminal_sn,string terminal_key)
        {
            string url = api_domain + "upay/v2/pay";
            JObject Jparams = new JObject();
            Jparams.Add(new JProperty("terminal_sn", terminal_sn));
            Jparams.Add(new JProperty("client_sn", ""));
            Jparams.Add(new JProperty("total_amount", ""));
            Jparams.Add(new JProperty("payway", "3"));
            Jparams.Add(new JProperty("dynamic_id", ""));
            Jparams.Add(new JProperty("subject", ""));
            Jparams.Add(new JProperty("operator",""));
            Jparams.Add(new JProperty("description", ""));
            Jparams.Add(new JProperty("longitude", ""));
            Jparams.Add(new JProperty("latitude", ""));
            Jparams.Add(new JProperty("device_id", ""));
            Jparams.Add(new JProperty("extended", ""));
            Jparams.Add(new JProperty("reflect", ""));
            Jparams.Add(new JProperty("notify_url", ""));


            string sign = getSign(Jparams.ToString() + terminal_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, terminal_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码：" + resCode);
            if (resCode.Equals("200"))
            {

                string responseStr = retObj["biz_response"].ToString();
                JObject newTerminal = JObject.Parse(responseStr);
               // JObject response = (JObject)newTerminal.GetValue("data");
                Console.WriteLine("返回信息:" + newTerminal);
                return newTerminal;
            }
            else
            {
                string errorCode = retObj["error_code"].ToString();
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("错误码："+errorCode+"\n"+"返回信息：" + errorMsg);
            }
            return null;
        }

        public static JObject refund(string terminal_sn,string terminal_key)
        {
            string url = api_domain + "/upay/v2/refund";
            JObject Jparams = new JObject();
            Jparams.Add(new JProperty("terminal_sn", terminal_sn));
            Jparams.Add(new JProperty("sn", ""));
            Jparams.Add(new JProperty("client_sn", ""));
            Jparams.Add(new JProperty("client_tsn", ""));
            Jparams.Add(new JProperty("refund_request_no", ""));
            Jparams.Add(new JProperty("operator", ""));
            Jparams.Add(new JProperty("refund_amount", ""));

            string sign = getSign(Jparams.ToString() + terminal_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, terminal_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码：" + resCode);
            if (resCode.Equals("200"))
            {

                string responseStr = retObj["biz_response"].ToString();
                JObject newTerminal = JObject.Parse(responseStr);
                // JObject response = (JObject)newTerminal.GetValue("data");
                Console.WriteLine("返回信息:" + newTerminal);
                return newTerminal;
            }
            else
            {
                string errorCode = retObj["error_code"].ToString();
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("错误码：" + errorCode + "\n" + "返回信息：" + errorMsg);
            }
            return null;
        }

        //查询
        public static JObject query(string terminal_sn,string terminal_key)
        {
            string url = api_domain + "/upay/v2/query";
            JObject Jparams = new JObject();
            Jparams.Add(new JProperty("terminal_sn", terminal_sn));
            Jparams.Add(new JProperty("sn", ""));
            Jparams.Add(new JProperty("client_sn", ""));

            string sign = getSign(Jparams.ToString() + terminal_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, terminal_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码：" + resCode);
            if (resCode.Equals("200"))
            {

                string responseStr = retObj["biz_response"].ToString();
                JObject newTerminal = JObject.Parse(responseStr);
                // JObject response = (JObject)newTerminal.GetValue("data");
                Console.WriteLine("返回信息:" + newTerminal);
                return newTerminal;
            }
            else
            {
                string errorCode = retObj["error_code"].ToString();
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("错误码：" + errorCode + "\n" + "返回信息：" + errorMsg);
            }
            return null;
        }

        public static JObject cancel(string terminal_sn,string terminal_key)
        {
            string url = api_domain + "/upay/v2/cancel";
            JObject Jparams = new JObject();

            Jparams.Add(new JProperty("terminal_sn", terminal_sn));
            Jparams.Add(new JProperty("sn", ""));
            Jparams.Add(new JProperty("client_sn", ""));

            string sign = getSign(Jparams.ToString() + terminal_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, terminal_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码：" + resCode);
            if (resCode.Equals("200"))
            {

                string responseStr = retObj["biz_response"].ToString();
                JObject newTerminal = JObject.Parse(responseStr);
                // JObject response = (JObject)newTerminal.GetValue("data");
                Console.WriteLine("返回信息:" + newTerminal);
                return newTerminal;
            }
            else
            {
                string errorCode = retObj["error_code"].ToString();
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("错误码：" + errorCode + "\n" + "返回信息：" + errorMsg);
            }
            return null;
        }


        public static JObject precreate(string terminal_sn,string terminal_key)
        {
            string url = api_domain + "upay/v2/precreate";
            JObject Jparams = new JObject();

            Jparams.Add(new JProperty("terminal_sn", terminal_sn));
            Jparams.Add(new JProperty("client_sn", ""));
            Jparams.Add(new JProperty("total_amount", ""));
            Jparams.Add(new JProperty("payway", "3"));
            Jparams.Add(new JProperty("sub_payway", ""));
            Jparams.Add(new JProperty("payer_uid", ""));
            Jparams.Add(new JProperty("subject", ""));
            Jparams.Add(new JProperty("operator", ""));
            Jparams.Add(new JProperty("description", ""));
            Jparams.Add(new JProperty("longitude", ""));
            Jparams.Add(new JProperty("latitude", ""));
            Jparams.Add(new JProperty("extended", ""));
            Jparams.Add(new JProperty("reflect", ""));
            Jparams.Add(new JProperty("notify_url", ""));

            string sign = getSign(Jparams.ToString() + terminal_key);
            string result = HttpUtil.httpPost(url, Jparams.ToString(), sign, terminal_sn);
            JObject retObj = JObject.Parse(result);

            string resCode = retObj["result_code"].ToString();
            Console.WriteLine("返回码：" + resCode);
            if (resCode.Equals("200"))
            {

                string responseStr = retObj["biz_response"].ToString();
                JObject newTerminal = JObject.Parse(responseStr);
                // JObject response = (JObject)newTerminal.GetValue("data");
                Console.WriteLine("返回信息:" + newTerminal);
                return newTerminal;
            }
            else
            {
                string errorCode = retObj["error_code"].ToString();
                string errorMsg = retObj["error_message"].ToString();
                Console.WriteLine("错误码：" + errorCode + "\n" + "返回信息：" + errorMsg);
            }
            return null;
        }


        private static string getSign(string signStr)
        {
            string md5 = MD5(signStr);
            return md5;
        }
        public static string MD5(string value)
        {
            StringBuilder result = new StringBuilder("");
            string cl1 = value;
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] s = md5.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(cl1));
            for (int i = 0; i < s.Length; i++)
            {
                result.AppendFormat("{0:x2}", s[i]);
            }
            return result.ToString();
        }
    }
}
