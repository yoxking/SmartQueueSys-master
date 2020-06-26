using EntFrm.Framework.Utility;
using System;
using System.Drawing;
using System.IO;

namespace EntFrm.MainService.Business
{
    public class CommonBusiness
    {
        private volatile static CommonBusiness _instance = null;
        private static readonly object lockHelper = new object(); 
        public static CommonBusiness CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new CommonBusiness();
                }
            }
            return _instance;
        }


        public string HelloQueue()
        {
            return "Hello," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string doPrintMessage(string sMessage)
        {
            try
            {
                string strFormat = DateTime.Now.ToString("[HH:mm:ss] ") + sMessage;

                MainFrame.PrintMessage(strFormat);

                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }
        public string getImageFrom(string sImageFile)
        {
            try
            {
                string sResult = "";

                sImageFile = System.Windows.Forms.Application.StartupPath + sImageFile;

                Image myImage = Image.FromFile(sImageFile);
                sResult = ImageConvert.ToBaseString(myImage);
                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string getHtmlSource(string sUrl)
        {
            StreamReader sr = null; 
            string sResult = "";
            try
            {
                if (!string.IsNullOrEmpty(sUrl))
                {
                    sUrl = System.Windows.Forms.Application.StartupPath + sUrl;
                    sr = new StreamReader(sUrl, System.Text.Encoding.UTF8);
                    string temp = sr.ReadToEnd();
                     
                    sResult = temp;
                }

                return sResult;
            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                sr.Close();
            }
        }
    }
}
