namespace InventoryManagementSystem.Models
{
    public class UsersBL
    {
        public List<User> GetUsers()
        {
            // In Realtime you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data
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
