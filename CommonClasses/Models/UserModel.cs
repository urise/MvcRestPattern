using System.ComponentModel.DataAnnotations;
using Interfaces.DbInterfaces;

namespace CommonClasses.Models
{
    public class UserModel : IUserDb
    {
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "Введите логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string FinanceKey { get; set; }

        public string Email { get; set; }
        public string UserFIO { get; set; }
        public string RegistrationCode { get; set; }
        public bool IsActive { get; set; }

        public bool KeepLoggedIn { get; set; }

        public UserModel(IUserDb userDb)
        {
            UserId = userDb.UserId;
            Login = userDb.Login;
            Password = userDb.Password;
            Email = userDb.Email;
            UserFIO = userDb.UserFIO;
            IsActive = userDb.IsActive;
            RegistrationCode = userDb.RegistrationCode;
        }

        public UserModel(){}
    }
}