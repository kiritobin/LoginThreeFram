using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Login.DBHelper
{
    class SQLHelper
    {
        private string connStr = null;
        private SqlConnection conn = null;
        private SqlTransaction trans = null;
        public SQLHelper()
        {
            connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void OpenDB()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void CloseDB()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行带参非查询SQL命令
        /// </summary>
        /// <param name="cmdtxt">带参非查询SQL命令</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>影响的行数</returns>
        private SqlCommand CreateSqlCommand(string sql, SqlParameter[] parmas = null, CommandType cmdType = CommandType.Text)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = cmdType;
            cmd.CommandText = sql;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            if (parmas != null)
            {
                cmd.Parameters.AddRange(parmas);
            }
            return cmd;
        }
        /// <summary>
        /// 执行一般非查询SQL命令
        /// </summary>
        /// <param name="cmdtxt">非查询SQL命令</param>
        /// <returns>影响的行数</returns>
        public int ExecuteSqlNonQuery(string sql)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                {
                    CloseDB();
                }
            }
        }
        /// <summary>
        /// 根据SQL指令返回第一行第一列结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameters">cmd.Parameters.AddWithValue</param>
        /// <returns>返回第一行第一列结果</returns>
        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql, parameters);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                {
                    CloseDB();
                }
            }
        }
        /// <summary>
        /// 执行不带参非查询存储过程
        /// </summary>
        /// <param name="proctxt">带参非查询存储过程名</param>        
        /// <returns>影响的行数</returns>
        public int ExecuteProcNonQuery(string proc)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(proc, null, CommandType.StoredProcedure);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                {
                    CloseDB();
                }
            }
        }
        /// <summary>
        /// 执行带参非查询存储过程
        /// </summary>
        /// <param name="proctxt">带参非查询存储过程名</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>影响的行数</returns>
        public int ExecuteProcNonQuery(string proc, params SqlParameter[] parameters)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(proc, parameters, CommandType.StoredProcedure);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (trans == null)
                {
                    CloseDB();
                }
            }
        }
        /// <summary>
        /// 执行一般查询SQL命令，返回DataReader对象
        /// </summary>
        /// <param name="cmdtxt">一般查询SQL命令</param>
        /// <returns>返回DataReader对象</returns>
        public SqlDataReader ExecuteSqlReader(string sql)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql);
            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行带参查询SQL命令，返回DataReader对象
        /// </summary>
        /// <param name="cmdtxt">带参非查询SQL命令</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>返回DataReader对象</returns>
        public SqlDataReader ExecuteSqlReader(string sql, params SqlParameter[] parameters)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql, parameters);
            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行不带参查询存储过程
        /// </summary>
        /// <param name="proctxt">不参查询存储过程名</param>        
        /// <returns>返回DataReader对象</returns>
        public SqlDataReader ExecuteProcReader(string proc)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(proc, null, CommandType.StoredProcedure);
            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行带参查询存储过程，返回DataReader对象
        /// </summary>
        /// <param name="cmdtxt">带参查询存储过程</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>返回DataReader对象</returns>
        public SqlDataReader ExecuteProcReader(string proc, params SqlParameter[] parameters)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(proc, parameters, CommandType.StoredProcedure);
            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行普通汇总查询SQL命令，返回object对象
        /// </summary>
        /// <param name="cmdtxt">普通汇总查询SQL命令</param>
        /// <returns>汇总结果，object对象</returns>
        public object ExecuteScalar(string sql)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql);
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
        }
        /// <summary>
        /// 执行带参汇总查询SQL命令，返回object对象
        /// </summary>
        /// <param name="cmdtxt">带参汇总查询SQL命令</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>汇总结果，object对象</returns>  
        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql, parameters);
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
        }
        /// <summary>
        /// 执行不带参汇总查询存储过程，返回object对象
        /// </summary>
        /// <param name="cmdtxt">存储过程名</param>       
        /// <returns>汇总结果，object对象</returns>        
        public object ExecuteProcScalar(string proc)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(proc, null, CommandType.StoredProcedure);
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
        }
        /// <summary>
        /// 执行带参汇总查询存储过程，返回object对象
        /// </summary>
        /// <param name="cmdtxt">存储过程名</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>汇总结果，object对象</returns>   
        public object ExecuteProcScalar(string proc, params SqlParameter[] parameters)
        {
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(proc, parameters, CommandType.StoredProcedure);
            try
            {
                return cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                CloseDB();
            }
        }
        /// <summary>
        /// 执行普通查询SQL命令，返回结果集DataTable
        /// </summary>
        /// <param name="cmdtxt">普通查询SQL命令</param>
        /// <returns>结果集DataTable</returns>
        public DataTable ExecuteTable(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = CreateSqlCommand(sql);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行带参查询SQL命令，返回结果集DataTable
        /// </summary>
        /// <param name="cmdtxt">带参查询SQL命令</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>结果集DataTable</returns>
        public DataTable ExecuteSqlTable(string sql, params SqlParameter[] parameters)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = CreateSqlCommand(sql, parameters);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行不带参查询存储过程，返回结果集DataTable
        /// </summary>
        /// <param name="cmdtxt">存储过程名</param>        
        /// <returns>结果集DataTable</returns>
        public DataTable ExecuteProcTable(string proc)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = CreateSqlCommand(proc, null, CommandType.StoredProcedure);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行带参查询存储过程，返回结果集DataTable
        /// </summary>
        /// <param name="cmdtxt">存储过程名</param>
        /// <param name="parmas">参数对象数组</param>
        /// <returns>结果集DataTable</returns>
        public DataTable ExecuteProcTable(string proc, params SqlParameter[] parameters)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = CreateSqlCommand(proc, parameters, CommandType.StoredProcedure);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                return dt;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            //conn = new SqlConnection(connStr);
            OpenDB();
            SqlCommand cmd = CreateSqlCommand(sql, parameters);
            DataSet dataset = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            try
            {
                adapter.Fill(dataset);
                return dataset.Tables[0];
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 开始一个事务
        /// </summary>
        public void BeginSqlTranscation()
        {
            OpenDB();
            trans = conn.BeginTransaction();
        }

        /// <summary>
        /// 提交一个事务
        /// </summary>
        public void CommitSqlTranscation()
        {
            trans.Commit();
            CloseDB();
            trans = null;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void RollBackSqlTranscation()
        {
            trans.Rollback();
            CloseDB();
            trans = null;
        }
    }
}
