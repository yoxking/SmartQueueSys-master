using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntFrm.Framework.Utility;
using EntFrm.System.Model;
using EntFrm.System.Model.Collections;
using EntFrm.System.IDAL;

namespace EntFrm.Business.SQLServerDAL
{
  public class TeachResourceTypeDAL: ITeachResourceType
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From TeachResourceType Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From TeachResourceType Where   AppCode like @AppCode And   ValidityState=1 And TchResTyNo=@TchResTyNo";
        private const string SQL_GET_NAME_BY_NO = @"Select Name From TeachResourceType Where   AppCode like @AppCode And   ValidityState=1 And TchResTyNo=@TchResTyNo";
        private const string SQL_ADD_RECORD = @"Insert into TeachResourceType
                                              (TchResTyNo,TchResTyName,TchResTyOrder,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@TchResTyNo,@TchResTyName,@TchResTyOrder,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update TeachResourceType set
                                                 TchResTyNo=@TchResTyNo,TchResTyName=@TchResTyName,TchResTyOrder=@TchResTyOrder,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And TchResTyNo=@TchResTyNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From TeachResourceType Where   AppCode like @AppCode And   TchResTyNo=@TchResTyNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update TeachResourceType set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And TchResTyNo=@TchResTyNo";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From TeachResourceType Where    AppCode like @AppCode And   ValidityState=1 And ClassNo=@ClassNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From TeachResourceType Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_TCHRESTYNO = "@TchResTyNo";
        private const string PARAM_TCHRESTYNAME = "@TchResTyName";
        private const string PARAM_TCHRESTYORDER = "@TchResTyOrder";
        private const string PARAM_ADDOPTOR = "@AddOptor";
        private const string PARAM_ADDDATE = "@AddDate";
        private const string PARAM_MODOPTOR = "@ModOptor";
        private const string PARAM_MODDATE = "@ModDate";
        private const string PARAM_VALIDITYSTATE = "@ValidityState";
        private const string PARAM_COMMENTS = "@Comments";
        private const string PARAM_APPCODE = "@AppCode";
        private const string PARAM_VERSION = "@Version";
        #endregion

        private string connStr;
        private string appCode;

        public TeachResourceTypeDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public TeachResourceTypeCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            TeachResourceTypeCollections infos = null;
            TeachResourceType info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                 };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS,paras);

                if (reader.HasRows)
                {
                    infos = new TeachResourceTypeCollections();
                    while (reader.Read())
                    {
                        info = new TeachResourceType();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 查询所有记录(DAL层|GetAllRecords)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public TeachResourceTypeCollections GetRecordsByClassNo(string sClassNo)
        {
            /*SqlConnection connection = null;
            SqlDataReader reader = null;
            TeachResourceTypeCollections infos = null;
            TeachResourceType info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_CLASSNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new TeachResourceTypeCollections();
                    while (reader.Read())
                    {
                        info = new TeachResourceType();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sClassNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }*/
            return null;
        }

        public TeachResourceTypeCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            TeachResourceTypeCollections infos = null;
            TeachResourceType info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_TCHRESTYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new TeachResourceTypeCollections();
                    while (reader.Read())
                    {
                        info = new TeachResourceType();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public string GetRecordNameByNo(string sNo)
        {
            SqlConnection connection = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_TCHRESTYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录名称(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int AddRecord(TeachResourceType info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_TCHRESTYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TCHRESTYNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TCHRESTYORDER,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sTchResTyNo;
                paras[1].Value = info.sTchResTyName;
                paras[2].Value = info.iTchResTyOrder;
                paras[3].Value = info.sAddOptor;
                paras[4].Value = info.dAddDate;
                paras[5].Value = info.sModOptor;
                paras[6].Value = info.dModDate;
                paras[7].Value = info.iValidityState;
                paras[8].Value = info.sComments;
                paras[9].Value = info.sAppCode;

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_ADD_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 添加(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int UpdateRecord(TeachResourceType info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_TCHRESTYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TCHRESTYNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_TCHRESTYORDER,SqlDbType.Int),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sTchResTyNo;
                paras[1].Value = info.sTchResTyName;
                paras[2].Value = info.iTchResTyOrder;
                paras[3].Value = info.sAddOptor;
                paras[4].Value = info.dAddDate;
                paras[5].Value = info.sModOptor;
                paras[6].Value = info.dModDate;
                paras[7].Value = info.iValidityState;
                paras[8].Value = info.sComments;
                paras[9].Value = info.sAppCode;
                paras[10].Value = StringUtil.ConvertToBytes(info.sVersion);

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_UPDATE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 更新记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int HardDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_TCHRESTYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 硬删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int SoftDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_TCHRESTYNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_SOFT_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 软删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public TeachResourceTypeCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            TeachResourceTypeCollections infos = null;
            TeachResourceType info = null;

            try
            {
                 if (s_model.sCondition.Length==0)
                {
                    s_model.sCondition = " Where  AppCode like '%" + appCode + ";%' And ValidityState=1";
                }
                else
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And ValidityState=1 And  " + s_model.sCondition;
                }

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new TeachResourceTypeCollections();
                    while (reader.Read())
                    {
                        info = new TeachResourceType();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 分页查询(DAL层)记录时出错;;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int GetCountByCondition(string sCondition)
        {
            SqlConnection connection = null;

            try
            {
                string strSql = SQL_GET_COUNT_BY_CONDITION;
                if(sCondition.Length>0)
                {
                    strSql +="  And " + sCondition;
                }

                SqlParameter[] paras = new SqlParameter[]
                {
                   new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql, paras));
            }
            catch (Exception ex)
            {
                throw new Exception(" 计算记录总数(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        #region PutObjectProperty 设置对象属性
        /// <summary>
        /// 从 SqlDataReader 类对象中读取并设置对象属性
        /// </summary>
       /// <param name=" obj_info">主题对象</param>
        /// <param name="dr">读入数据</param>
        internal static void PutObjectProperty(TeachResourceType obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sTchResTyNo= reader["TchResTyNo"].ToString();
            obj_info.sTchResTyName= reader["TchResTyName"].ToString();
            obj_info.iTchResTyOrder= int.Parse(reader["TchResTyOrder"].ToString());
            obj_info.sAddOptor= reader["AddOptor"].ToString();
            obj_info.dAddDate= DateTime.Parse(reader["AddDate"].ToString());
            obj_info.sModOptor= reader["ModOptor"].ToString();
            obj_info.dModDate= DateTime.Parse(reader["ModDate"].ToString());
            obj_info.iValidityState= int.Parse(reader["ValidityState"].ToString());
            obj_info.sComments= reader["Comments"].ToString();
            obj_info.sAppCode= reader["AppCode"].ToString();
            obj_info.sVersion= StringUtil.ConvertToString((byte[])reader["Version"].ToString());
        }
        #endregion
    }
}
