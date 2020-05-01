using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDemo.BLL
{
    public class UserInfo
    {
        public static Pepole GetUserDetails()
        {
            return new Pepole()
            {
                name = "king",
                age=28,
                gender="男",
                remark="everything is possible!"
            };
        }
    }
}
