using System.Text.RegularExpressions;
using Application.DTOs.Request;
using Application.Errors;

namespace Application.Validators
{
    public class UserValidator
    {
        public static void ValidateCreateUserDTO(CreateUserDTO request)
        {
            if (string.IsNullOrEmpty(request.FirstName))
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, "Nome"));

            if (string.IsNullOrEmpty(request.LastName))
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, "Sobrenome"));

            if (string.IsNullOrEmpty(request.PhoneNumber))
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, "Telefone"));

            if (!Regex.IsMatch(request.PhoneNumber, @"^\d{10,11}$"))
                throw new ArgumentException(ErrorMsg.InvalidPhoneNumberFormat);

            if (string.IsNullOrEmpty(request.Email))
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, "Email"));

            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, "Senna"));
        }
    }
}