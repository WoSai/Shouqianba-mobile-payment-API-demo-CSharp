using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string vendor_sn = "";      //需要联系收钱吧技术支持申请获得
            string vendor_key = "";     //需要联系收钱吧技术支持申请获得
            string appid="";            //需要登录收钱吧服务商平台选择相应业务场景生成
            string code = "";           //需联系收钱吧提供激活码 
            JObject result= WoSaiPay.activate(vendor_sn, vendor_key,appid, code);
            if (result != null)
            {
                string terminal_sn =(string) result.GetValue("terminal_sn");
                string terminal_key = (string)result.GetValue("terminal_key");

                JObject terminal = WoSaiPay.checkin(terminal_sn, terminal_key);

                if (terminal != null)
                {
                    string newTerminal_sn = (string)result.GetValue("terminal_sn");
                    string newTterminal_key = (string)result.GetValue("terminal_key");

                    WoSaiPay.pay(newTerminal_sn, newTterminal_key);
                    WoSaiPay.refund(newTerminal_sn, newTterminal_key);
                    WoSaiPay.query(newTerminal_sn, newTterminal_key);
                    WoSaiPay.cancel(newTerminal_sn, newTterminal_key);
                    WoSaiPay.revoke(newTerminal_sn, newTterminal_key);
                    WoSaiPay.precreate(newTerminal_sn, newTterminal_key);
                }
            }
        }
    }
}
