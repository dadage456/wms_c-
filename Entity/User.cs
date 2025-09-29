using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class User
    {
        private User() { }
        private static User instance;
        public static User Instance
        {
            get
            {
                if (instance == null) instance = new User();
                return instance;
            }
        }

        UserInfo userData = new UserInfo();
        public UserInfo UserData
        {
            get { return userData; }
            set { userData = value; }
        }

        public class UserInfo
        {
            private string userId;
            public string UserId
            {
                get { return userId; }
                set { userId = value; }
            }

            private string userCode;
            public string UserCode
            {
                get { return userCode; }
                set { userCode = value; }
            }

            private string userName;
            public string UserName
            {
                get { return userName; }
                set { userName = value; }
            }
        }

        /// <summary>
        /// 添加人员信息
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        public void AddUserInfo(string userCode, string userName,string userId)
        {
            UserInfo user = new UserInfo();
            user.UserCode = userCode;
            user.UserName = userName;
            user.UserId = userId;
            this.userData = user;
        }


    } 
}
