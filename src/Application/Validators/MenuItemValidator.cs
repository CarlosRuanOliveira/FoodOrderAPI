using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Errors;

namespace Application.Validators
{
    public class MenuItemValidator
    {

        private const decimal MinimumMenuItemPrice = 0.01M;

        public static void ValidateCreateMenuItemDTO(CreateMenuItemDTO request)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new ValidationException(string.Format(ErrorMsg.ParamRequired, "Nome"));

            if (request.PriceCents < MinimumMenuItemPrice)
                throw new ValidationException(string.Format(ErrorMsg.MinimumPrice, MinimumMenuItemPrice));
        }
    }
}