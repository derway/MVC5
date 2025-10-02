using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5.Service
{
    public class UserService
    {
        // 模擬的使用者資料庫
        private List<Models.User> _users = new List<Models.User>
    {
        new User { Id = 1, Username = "admin", Password = "123456", Role = "Admin" },
        new User { Id = 2, Username = "user", Password = "password", Role = "User" },
        new User { Id = 2, Username = "1", Password = "1", Role = "User" }
    };

        public User Authenticate(string username, string password)
        {
            // 驗證使用者名稱與密碼是否正確
            return _users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);
        }
    }
}