using System;
using System.Web;
using System.Web.UI;
namespace MyAspx.Framework.WebUnit.Common
{
    public class Jscript
    {

        public static void Alert(string strMessage)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
            {
                currentPage.ClientScript.RegisterStartupScript(
                                  typeof(System.String),
                                  "messagebox",
                                  string.Format("alert(\"{0}\");", strMessage.Replace("\r\n", " ")),
                                  true
                                  );
            }
        }
        public static void RegScripts(string strMessage)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
            {
                currentPage.ClientScript.RegisterStartupScript(
                                  typeof(System.String),
                                  "messageboxs",
                                  string.Format("{0}", strMessage.Replace("\r\n", " ")),
                                  true
                                  );
            }
        }
        public static void RegScripts(string strMessage, string name)
        {
            Page currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
            {
                currentPage.ClientScript.RegisterStartupScript(
                                  typeof(System.String),
                                  name,
                                  string.Format("{0}", strMessage.Replace("\r\n", " ")),
                                  true
                                  );
            }
        }

        /// <summary>
        /// ������Ϣ����ת���µ�URL
        /// </summary>
        /// <param name="message">��Ϣ����</param>
        /// <param name="toURL">���ӵ�ַ</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            #region
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            #endregion
        }

        /// <summary>
        /// �ص���ʷҳ��
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            #endregion
        }

        /// <summary>
        /// �رյ�ǰ����
        /// </summary>
        public static void CloseWindow()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
            #endregion
        }

        /// <summary>
        /// ˢ�¸�����
        /// </summary>
        public static void RefreshParent(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ˢ�´򿪴���
        /// </summary>
        public static void RefreshOpener()
        {
            #region
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ��ָ����С���´���
        /// </summary>
        /// <param name="url">��ַ</param>
        /// <param name="width">��</param>
        /// <param name="heigth">��</param>
        /// <param name="top">ͷλ��</param>
        /// <param name="left">��λ��</param>
        public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
        {
            #region
            string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

            HttpContext.Current.Response.Write(js);
            #endregion
        }


        /// <summary>
        /// ת��Url�ƶ���ҳ��
        /// </summary>
        /// <param name="url">���ӵ�ַ</param>
        public static void JavaScriptLocationHref(string url)
        {
            #region
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
            #endregion
        }

        /// <summary>
        /// ��ָ����Сλ�õ�ģʽ�Ի���
        /// </summary>
        /// <param name="webFormUrl">���ӵ�ַ</param>
        /// <param name="width">��</param>
        /// <param name="height">��</param>
        /// <param name="top">������λ��</param>
        /// <param name="left">������λ��</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features);
            #endregion
        }

        public static void ShowModalDialogWindow(string webFormUrl, string features)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            HttpContext.Current.Response.Write(js);
        }

        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }
        /// <summary> 
        /// ��ȡ�ͻ��˲鿴�ؼ��Ľű� ˢ�±���ԭλ
        /// </summary> 
        /// <param name="controlName"></param> 
        /// <returns>�ű�����</returns> 
        public static string GetViewControlScript(string controlName)
        {

            //�����ͻ��˺���ViewObj 
            string script = "\n";
            script += "<script language=\"javascript\">\n";
            script += "function ViewObj(objName)\n";
            script += "{\n";
            script += "var obj = document.all.item(objName);\n";
            script += "if (obj != null)\n";
            script += "{\n";
            script += "\tobj.scrollIntoView();\n";
            script += "\tobj.focus();\n";
            script += "}\n";
            script += "}\n";

            //�����ͻ��˺���ToDo 
            script += "function ToDo()";
            script += "{\n";
            script += string.Format("setTimeout(\"ViewObj('{0}')\",1000);\n", controlName);
            script += "}\n";

            script += "window.onload = ToDo;\n";
            script += "</script>\n";

            return script;
        }

    }
}