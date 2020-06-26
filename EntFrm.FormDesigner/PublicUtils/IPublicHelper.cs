using EntFrm.Framework.Utility;
using System.Configuration;
using System.Windows.Forms;

namespace MyFormDesinger
{
    public class IPublicHelper
    {
        public static string Get_AppCode()
        {
            return ConfigurationManager.AppSettings["AppCode"].ToString(); 
        }

        public static string Get_ConnStr()
        {
            string connStr = ConfigurationManager.AppSettings["SqlServer"].ToString();

            return EnconfigHelper.Decrypt(connStr);
        } 

        public static void Set_ConfigValue(string Name, string Value)
        {
            ConfigurationManager.AppSettings.Set(Name, Value);

            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings[Name].Value = Value;
            config.Save(ConfigurationSaveMode.Modified);
            config = null;
        } 
    }
}
