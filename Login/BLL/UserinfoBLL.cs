using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Login.DAL;
using Login.Model;

namespace Login.BLL
{
    public class UserinfoBLL
    {
        UserInfoDAL userInfoDal = new UserInfoDAL();
        public int Login(UserInfo user)
        {
            int count = userInfoDal.UserLogin(user);
            return count;
        }
    }
}
