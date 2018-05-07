using Login.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Login.DBHelper;

namespace Login.DAL
{
    public class UserInfoDAL
    {
        SQLHelper sqlHelp = new SQLHelper();
        public int UserLogin(UserInfo user)
        {
            int i = Convert.ToInt32(sqlHelp.ExecuteScalar("select count(*) from T_login where username=@UserName and password=@PassWord",
                 new SqlParameter("@UserName", user.userName),
                  new SqlParameter("@PassWord", user.password)));
            return i;
        }
    }
}
