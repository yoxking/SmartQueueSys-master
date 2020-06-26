using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.Framework.Utility;

namespace EntFrm.Business.IDAL
{
  public interface ILaboratoryTable
  {
        LaboratoryTableCollections GetAllRecords();
        LaboratoryTableCollections GetRecordsByClassNo(string sClassNo);
        LaboratoryTableCollections GetRecordsByNo(string sNo);
        string GetRecordNameByNo(string sNo);
        int AddRecord(LaboratoryTable info);
        int UpdateRecord(LaboratoryTable info); 
        int HardDeleteRecord(string sNo);
        int SoftDeleteRecord(string sNo);
        LaboratoryTableCollections GetRecords_Paging(SqlModel s_model);
        int GetCountByCondition(string sCondition);
    }
  }

