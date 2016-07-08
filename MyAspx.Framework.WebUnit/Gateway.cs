using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Net;

namespace MyAspx.Framework.WebUnit.Common
{
    public class EasyPay
    {
        //MD5�����㷨
        public static string GetMD5(string s, string charset)
        {
            /// <summary>
            /// ��ASP���ݵ�MD5�����㷨
            /// </summary>

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        //��������
        public static string[] BubbleSort(string[] r)
        {
            /// <summary>
            /// ð������
            /// </summary>
            int i, j; //������־ 
            string temp;
            bool exchange;

            for (i = 0; i < r.Length; i++) //�����R.Length-1������ 
            {
                exchange = false; //��������ʼǰ��������־ӦΪ��

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)��//��������
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;
                        exchange = true; //�����˽������ʽ�������־��Ϊ�� 
                    }
                }

                if (!exchange) //��������δ������������ǰ��ֹ�㷨 
                {
                    break;
                }

            }
            return r;
        }

        //��ȡԶ�̷�����ATN���
        public static String Get_Http(String a_strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {

                strResult = "����" + exp.Message;
            }
            return strResult;
        }

        private static string linkUrl(string[] Oristr, string gateway, string charset, string key, string sign_type)
        {
            int i;
            //��������
            string[] Sortedstr = BubbleSort(Oristr);

            //�����md5ժҪ�ַ��� ��
            StringBuilder prestr = new StringBuilder();

            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr.Append(Sortedstr[i]);

                }
                else
                {
                    if (!Sortedstr[i].EndsWith("="))
                    {
                        prestr.Append(Sortedstr[i] + "&");
                    }
                }

            }

            prestr.Append(key);

            //����Md5ժҪ��
            string sign = GetMD5(prestr.ToString(), charset);

            //����֧��Url��
            char[] delimiterChars = { '=' };
            StringBuilder parameter = new StringBuilder();
            parameter.Append(gateway);
            for (i = 0; i < Sortedstr.Length; i++)
            {

                parameter.Append(Sortedstr[i].Split(delimiterChars)[0] + "=" + HttpUtility.UrlEncode(Sortedstr[i].Split(delimiterChars)[1]) + "&");
            }

            parameter.Append("sign=" + sign + "&sign_type=" + sign_type);

            //����֧��Url��
            return parameter.ToString();
        }

        //����ύ�ַ���
        public static string CreatPayUrl(
            string gateway,
            string service,
            string merchant_ID,
            string payment_type,
            string seller_email,
            string return_url,
            string notify_url,
            string charset,
            string order_no,
            string title,
            string body,
            string buyer_email,
            string total_fee,
            string paymethod,
            string defaultbank,
            string key,
            string sign_type
            )
        {
            //�������飻
            string[] Oristr ={ 
                "service="+service,
		        "payment_type=1",
		        "merchant_ID="+merchant_ID,
		        "seller_email="+seller_email,
		        "return_url="+return_url,
		        "notify_url="+notify_url,
		        "charset="+charset,
		        "order_no="+order_no,
		        "title="+title,
		        "body="+body,
		        "buyer_email="+buyer_email,
				"total_fee="+total_fee,
				"paymethod="+paymethod,
				"defaultbank="+defaultbank
            };
            return linkUrl(Oristr, gateway, charset, key, sign_type);
        }

        //����ύ�ַ���
        public static string CreatRefundUrl(
            string gateway,
            string merchant_ID,
            string charset,
            string orig_order_no,
            string amount,
            string subject,
            string key,
            string sign_type
            )
        {
            //�������飻
            string[] Oristr ={ 
		        "merchant_ID="+merchant_ID,
		        "_charset="+charset,
		        "orig_order_no="+orig_order_no,
		        "subject="+subject,
				"amount="+amount,
            };
            return linkUrl(Oristr, gateway, charset, key, sign_type);
        }

        //����ύ�ַ���
        public static string CreatQueryUrl(
            string gateway,
            string merchant_ID,
            string charset,
            string order_no,
            string key,
            string sign_type
            )
        {
            //�������飻
            string[] Oristr ={ 
		        "merchant_ID="+merchant_ID,
		        "_charset="+charset,
		        "order_no="+order_no,
            };
            return linkUrl(Oristr, gateway, charset, key, sign_type);
        }
    }
    public class Gateway
    {
        //MD5�����㷨
        public static string GetMD5(string s, string charset)
        {
            /// <summary>
            /// ��ASP���ݵ�MD5�����㷨
            /// </summary>

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        //��������
        public static string[] BubbleSort(string[] r)
        {
            /// <summary>
            /// ð������
            /// </summary>
            int i, j; //������־ 
            string temp;
            bool exchange;

            for (i = 0; i < r.Length; i++) //�����R.Length-1������ 
            {
                exchange = false; //��������ʼǰ��������־ӦΪ��

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)��//��������
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;
                        exchange = true; //�����˽������ʽ�������־��Ϊ�� 
                    }
                }

                if (!exchange) //��������δ������������ǰ��ֹ�㷨 
                {
                    break;
                }

            }
            return r;
        }

        //��ȡԶ�̷�����ATN���
        public static String Get_Http(String a_strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {

                strResult = "����" + exp.Message;
            }
            return strResult;
        }

        private static string linkUrl(string[] Oristr, string gateway, string charset, string key)
        {
            int i;
            //��������
            string[] Sortedstr = BubbleSort(Oristr);

            //�����md5ժҪ�ַ��� ��
            StringBuilder prestr = new StringBuilder();

            for (i = 0; i < Sortedstr.Length; i++)
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr.Append(Sortedstr[i]);

                }
                else
                {
                    if (!Sortedstr[i].EndsWith("="))
                    {
                        prestr.Append(Sortedstr[i] + "&");
                    }
                }

            }

            prestr.Append(key);

            //����Md5ժҪ��
            string sign = GetMD5(prestr.ToString(), charset);

            //����֧��Url��
            char[] delimiterChars = { '=' };
            StringBuilder parameter = new StringBuilder();
            parameter.Append(gateway);
            for (i = 0; i < Sortedstr.Length; i++)
            {

                parameter.Append(Sortedstr[i].Split(delimiterChars)[0] + "=" + HttpUtility.UrlEncode(Sortedstr[i].Split(delimiterChars)[1]) + "&");
            }

            parameter.Append("sign=" + sign + "");

            //����֧��Url��
            return parameter.ToString();
        }

        public static string CreatPayUrl(
         string gateway,
         string service,
         string merchant_ID,
         string payment_type,
         string seller_email,
         string return_url,
         string notify_url,
         string charset,
         string order_no,
         string title,
         string body,
         string buyer_email,
         string total_fee,
         string paymethod,
         string defaultbank,
         string key,
         string sign_type
         )
        {
            //�������飻
            string[] Oristr ={ 
                "service="+service,
		        "payment_type=1",
		        "merchant_ID="+merchant_ID,
		        "seller_email="+seller_email,
		        "return_url="+return_url,
		        "notify_url="+notify_url,
		        "charset="+charset,
		        "order_no="+order_no,
		        "title="+title,
		        "body="+body,
		        "buyer_email="+buyer_email,
				"total_fee="+total_fee,
				"paymethod="+paymethod,
				"defaultbank="+defaultbank
            };
            return linkUrl(Oristr, gateway, charset, key);
        }

    }
}