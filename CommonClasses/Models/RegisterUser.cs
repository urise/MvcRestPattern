using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Interfaces.DbInterfaces;

namespace CommonClasses.Models
{
    public class RegisterUser : IUser
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = Messages.LoginRequired)]
        public string Login { get; set; }

        [Required(ErrorMessage = Messages.PasswordRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = Messages.ConfirmPasswordRequired)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = Messages.ConfirmPasswordDonNotMatch)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = Messages.EmailRequired)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(RegexExpressions.EmailRegex, ErrorMessage = Messages.WrondEmailFormat)]
        public string Email { get; set; }
        public string UserFio { get; set; }
        public string RegistrationCode { get; set; }
        public bool IsActive { get; set; }

        public RegisterUser(IUser userDb)
        {
            UserId = userDb.UserId;
            Login = userDb.Login;
            Password = userDb.Password;
            Email = userDb.Email;
            UserFio = userDb.UserFio;
            IsActive = userDb.IsActive;
            RegistrationCode = userDb.RegistrationCode;
        }

        public RegisterUser() { }
    }
}