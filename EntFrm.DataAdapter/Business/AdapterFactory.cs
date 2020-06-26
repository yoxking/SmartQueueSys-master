using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Business
{
    public class AdapterFactory
    {
        public static IAdapterBusiness Create()
        {
            try
            { 
                string adapter = IUserContext.GetConfigValue("Adapter").ToString(); 

                IAdapterBusiness adapterBoss = null;

                switch (adapter)
                {
                    
                    default:
                        adapterBoss = new DefaultMyAdapter();
                        break;
                }

                return adapterBoss;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过工厂模式创建Adapter时出错;" + ex.Message);
            }
        }
    }
}