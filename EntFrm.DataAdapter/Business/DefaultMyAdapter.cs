using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntFrm.DataAdapter.Business
{
    public class DefaultMyAdapter : IAdapterBusiness
    {
        /// <summary>
        /// 清空心跳数据
        /// </summary>
        /// <returns></returns>
        public bool wipeHrtbeatFlows()
        {
            return true;
        }

    }
}
