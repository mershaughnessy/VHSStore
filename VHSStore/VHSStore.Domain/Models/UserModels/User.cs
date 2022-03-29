using VHSStore.Domain.Models.UserModels;

namespace VHSStore.Domain.Models
{
    public class User
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public bool Subscribed { get; set; }
        public bool EmailVerified { get; set; }

        public User()
        {
        }

        public User(AddUser addUser, string password, string salt)
        {
            this.UserName = addUser.UserName;
            this.Password = password;
            this.Salt = salt;
            this.Email = addUser.Email;
            this.Subscribed = addUser.Subscribed;
        }
    }
}
