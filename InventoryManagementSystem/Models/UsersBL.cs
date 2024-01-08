namespace InventoryManagementSystem.Models
{
    public class UsersBL
    {
        public List<User> GetUsers()
        {
           
            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "CustomerUser",
                Password = "123456",
                Roles = "Customer",
                Email = "user@gmail.com"
            });
            userList.Add(new User()
            {
                ID = 102,
                UserName = "AdminUser",
                Password = "abcdef",
                Roles = "Admin",
                Email = "admin@gmail.com"
            });
            
            return userList;
        }
    }
}
