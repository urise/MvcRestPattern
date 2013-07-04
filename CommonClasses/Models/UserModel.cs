using System.ComponentModel.DataAnnotations;
using Interfaces.DbInterfaces;

namespace CommonClasses.Models
{
    public class UserModel : IUser
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = Messages.LoginRequired)]
        public string Login { get; set; }

        [Required(ErrorMessage = Messages.PasswordRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }
        public string UserFio { get; set; }
        public string RegistrationCode { get; set; }
        public bool IsActive { get; set; }

        public bool KeepLoggedIn { get; set; }

        public UserModel(IUser userDb)
        {
            UserId = userDb.UserId;
            Login = userDb.Login;
            Password = userDb.Password;
            Email = userDb.Email;
            UserFio = userDb.UserFio;
            IsActive = userDb.IsActive;
            RegistrationCode = userDb.RegistrationCode;
        }

        public UserModel() { }
    }
}