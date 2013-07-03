namespace CommonClasses
{
    public class Messages
    {
        public const string SessionTimeOut = "Время рабочей сессии истекло.";
        public const string NotEnoughPermissions = "У Вас недостаточно прав для выполнения операции.";

        public const string LoginAlreadyUsed = "Такой логин уже зарегистрирован в системе";
        public const string EmailAlreadyUsed = "Такой электронный адрес уже зарегистрирован в системе";
        public const string ConfirmPasswordDonNotMatch = "Ошибка при подтверждении пароля";
        public const string EmailRequired = "Введите электронный адрес";
        public const string LoginRequired = "Введите логин";
        public const string PasswordRequired = "Введите пароль";
        public const string ConfirmPasswordRequired = "Введите пароль повторно";
        public const string WrondEmailFormat = "Электронный адрес имеет неверный формат";

        public const string WrongLoginOrPassword = "Пользователь с указанным логином или паролем не был найден";
        public const string UserEmailUnapproved = "Необходимо подтвердить электронный адрес прежде, чем начать использовать этот логин.";
        public const string UserInstanceDoesntMatch = "Этот пользователь не имеет прав на выбранную компанию.";
        public const string UserNotFoundByPassword = "Введен неверный пароль";
        public const string NewPasswordIsNotDifferentFromTheOld = "Новый пароль должен отличаться от старого";
        public const string WrongLogin = "Пользователь с указанным логином не был найден";
        public const string CantForgotPassword = "Ошибка восстановления пароля";
        public const string TemporaryCodeExpired = "Срок годности ссылки востановления пароля истек";
        public const string EmailSentPasswordReset = "Ссылка для восстановления пароля была отправлена на Ваш электронный адрес";
        public const string UserNotFoundByLogin = "Пользователь с таким логином не зарегистрирован в системе";
        public const string UserNotFoundByEmail = "Пользователь с таким электронным адресом не существует";
        //public const string UserNotFoundByPassword = "Введен неверный пароль";
        //        public const string UserCompanyDoesntExist = "Для указанного пользователь не создана ни одна компания";
        //        public const string CompanyDataIsNotFull = "Данные компании неполные";
        //        public const string UserCompanyNotFound = "Указанный пользователь не связан с данной компанией";
        //        public const string UserCompanyAlreadyExist = "Указанный пользователь уже связан с данной компанией";

        public const string InstanceNotFound = "Компания не найдена.";
    }
}
